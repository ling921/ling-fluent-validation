using Ling.FluentValidation.Generators;
using Microsoft.CodeAnalysis.Emit;
using System.Collections.Immutable;

namespace Ling.FluentValidation.Generators.Test;

public sealed class ValidatorGeneratorTest
{
    [Fact]
    public void MarkedLocalType_GeneratesPartialValidatorAndRegistry()
    {
        const string source = """
            using Ling.FluentValidation.Annotations;
            namespace Contracts;
            [GenerateValidator]
            public sealed class CreateRequest
            {
                [NotEmpty]
                public string Name { get; set; } = "";
            }
            """;
        var result = Run(source);
        Assert.Empty(result.Diagnostics.Where(static d => d.Severity == DiagnosticSeverity.Error));
        Assert.Contains(result.Sources, static text => text.Contains("partial class CreateRequestValidator"));
        Assert.Contains(result.Sources, static text => text.Contains("ConfigureAdditionalRules();"));
        Assert.Contains(result.Sources, static text => text.Contains("class GeneratedValidatorRegistry"));
        Assert.Contains(result.Sources, static text => text.Contains("ValidatorTypes => _validatorTypes"));
        Assert.Contains(result.Sources, static text => text.Contains("ValidatorTypesByTargetType"));
        Assert.DoesNotContain(result.Sources, static text => text.Contains("InterfaceTypeMap"));
        var registry = Assert.Single(result.Sources.Where(static text => text.Contains("class GeneratedValidatorRegistry")));
        Assert.DoesNotContain("using ", registry);
        Assert.Contains(result.Sources, static text => text.Contains("using global::FluentValidation;") && text.Contains("global::FluentValidation.AbstractValidator"));
        Assert.Contains(result.Sources, static text => text.Contains("RuleFor(x => x.Name)") && text.Contains(".NotEmpty()"));
    }

    [Fact]
    public void UnmarkedType_DoesNotGenerate()
    {
        const string source = """
            using Ling.FluentValidation.Annotations;
            public sealed class Request { [NotEmpty] public string Name { get; set; } = ""; }
            """;
        Assert.Empty(Run(source).Sources);
    }

    [Fact]
    public void ExplicitReferencedAssembly_GeneratesValidatorInCurrentAssembly()
    {
        const string contracts = """
            using Ling.FluentValidation.Annotations;
            namespace External.Contracts;
            public sealed class ContractMarker { }
            [GenerateValidator]
            public sealed class ExternalRequest
            {
                [System.ComponentModel.DataAnnotations.Required]
                public string Name { get; set; } = "";
            }
            """;
        var contractsReference = CompileReference(contracts, "External.Contracts");
        const string validation = """
            using Ling.FluentValidation.Annotations;
            [assembly: GenerateValidatorsFromAssemblyContaining(typeof(External.Contracts.ContractMarker))]
            """;
        var result = Run(validation, contractsReference);
        Assert.Empty(result.Diagnostics.Where(static d => d.Severity == DiagnosticSeverity.Error));
        Assert.Contains(result.Sources, static text => text.Contains("AbstractValidator<global::External.Contracts.ExternalRequest>"));
        Assert.Contains(result.Sources, static text => text.Contains("namespace External.Contracts.Validators"));
    }

    [Fact]
    public void UnselectedReferencedAssembly_IsNotScanned()
    {
        const string contracts = """
            using Ling.FluentValidation.Annotations;
            [GenerateValidator]
            public sealed class ExternalRequest { [NotNull] public string Name { get; set; } = ""; }
            """;
        Assert.Empty(Run("public sealed class ValidationAnchor { }", CompileReference(contracts, "Unselected.Contracts")).Sources);
    }

    [Fact]
    public void Options_ControlNamespaceAndVisibility()
    {
        const string source = """
            using Ling.FluentValidation.Annotations;
            [assembly: ValidatorGenerationOptions(Namespace = "Application.Validation", Visibility = GeneratedValidatorVisibility.Public)]
            [GenerateValidator]
            public sealed class Request { }
            """;
        var result = Run(source);
        Assert.Empty(result.Diagnostics.Where(static d => d.Severity == DiagnosticSeverity.Error));
        Assert.Contains(result.Sources, static text => text.Contains("namespace Application.Validation") && text.Contains("public sealed partial class RequestValidator") && text.Contains("AbstractValidator<global::Request>"));
    }

    [Fact]
    public void GenericTarget_ReportsDiagnostic()
    {
        const string source = """
            using Ling.FluentValidation.Annotations;
            [GenerateValidator]
            public sealed class Request<T> { }
            """;
        Assert.Contains(Run(source).Diagnostics, static d => d.Id == "LFVG002");
    }

    [Fact]
    public void MultipleExplicitAssemblies_AreGenerated()
    {
        var first = CompileReference("""
            using Ling.FluentValidation.Annotations;
            namespace First.Contracts;
            public sealed class Marker { }
            [GenerateValidator] public sealed class FirstRequest { [NotEmpty] public string Name { get; set; } = ""; }
            """, "First.Contracts");
        var second = CompileReference("""
            using Ling.FluentValidation.Annotations;
            namespace Second.Contracts;
            public sealed class Marker { }
            [GenerateValidator] public sealed class SecondRequest { [NotNull] public object Value { get; set; } = new(); }
            """, "Second.Contracts");
        var result = Run("""
            using Ling.FluentValidation.Annotations;
            [assembly: GenerateValidatorsFromAssemblyContaining(typeof(First.Contracts.Marker))]
            [assembly: GenerateValidatorsFromAssemblyContaining(typeof(Second.Contracts.Marker))]
            """, first, second);
        Assert.Empty(result.Diagnostics.Where(static d => d.Severity == DiagnosticSeverity.Error));
        Assert.Contains(result.Sources, static text => text.Contains("FirstRequestValidator"));
        Assert.Contains(result.Sources, static text => text.Contains("SecondRequestValidator"));
    }

    [Fact]
    public void InheritedRules_AreGeneratedForDerivedTarget()
    {
        var result = Run("""
            using Ling.FluentValidation.Annotations;
            namespace Contracts;
            public abstract class RequestBase { [NotEmpty] public string Id { get; set; } = ""; }
            [GenerateValidator] public sealed class CreateRequest : RequestBase { [NotNull] public object Value { get; set; } = new(); }
            """);
        Assert.Empty(result.Diagnostics.Where(static d => d.Severity == DiagnosticSeverity.Error));
        Assert.Contains(result.Sources, static text => text.Contains("CreateRequestValidator") && text.Contains("RuleFor(x => x.Id)") && text.Contains("RuleFor(x => x.Value)"));
    }

    [Fact]
    public void InaccessibleExternalTarget_ReportsDiagnostic()
    {
        var contracts = CompileReference("""
            using Ling.FluentValidation.Annotations;
            namespace External.Contracts;
            public sealed class Marker { }
            [GenerateValidator] internal sealed class HiddenRequest { [NotEmpty] public string Name { get; set; } = ""; }
            """, "Hidden.Contracts");
        var result = Run("""
            using Ling.FluentValidation.Annotations;
            [assembly: GenerateValidatorsFromAssemblyContaining(typeof(External.Contracts.Marker))]
            """, contracts);
        Assert.Contains(result.Diagnostics, static d => d.Id == "LFVG002" && d.GetMessage().Contains("not accessible"));
    }

    [Fact]
    public void ConflictingGeneratedNames_ReportDiagnostic()
    {
        var result = Run("""
            using Ling.FluentValidation.Annotations;
            [assembly: ValidatorGenerationOptions(Namespace = "Validation")]
            namespace First { [GenerateValidator] public sealed class Request { } }
            namespace Second { [GenerateValidator] public sealed class Request { } }
            """);
        Assert.Contains(result.Diagnostics, static d => d.Id == "LFVG002" && d.GetMessage().Contains("conflicts"));
    }

    [Fact]
    public void MissingFluentValidation_ReportsDiagnostic()
    {
        var compilation = CSharpCompilation.Create(
            "Validation.Target",
            [CSharpSyntaxTree.ParseText("public sealed class Anchor { }")],
            ReferenceHelper.GetRequiredReferences().Where(static reference =>
                !Path.GetFileName(reference.Display ?? string.Empty).StartsWith("FluentValidation", StringComparison.OrdinalIgnoreCase)),
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
        GeneratorDriver driver = CSharpGeneratorDriver.Create(new ValidatorGenerator().AsSourceGenerator());
        driver = driver.RunGenerators(compilation);
        Assert.Contains(driver.GetRunResult().Diagnostics, static d => d.Id == "LFVG001");
    }

    private static GeneratorResult Run(string source, params MetadataReference[] additionalReferences)
    {
        var compilation = CreateCompilation(source, "Validation.Target", additionalReferences);
        GeneratorDriver driver = CSharpGeneratorDriver.Create(new ValidatorGenerator().AsSourceGenerator());
        driver = driver.RunGeneratorsAndUpdateCompilation(compilation, out var output, out var driverDiagnostics);
        var runResult = driver.GetRunResult();
        var sources = runResult.Results.SelectMany(static result => result.GeneratedSources).Select(static item => item.SourceText.ToString()).ToArray();
        var diagnostics = driverDiagnostics.AddRange(runResult.Diagnostics).AddRange(output.GetDiagnostics());
        return new(sources, diagnostics);
    }

    private static PortableExecutableReference CompileReference(string source, string assemblyName)
    {
        var compilation = CreateCompilation(source, assemblyName, []);
        using var stream = new MemoryStream();
        EmitResult result = compilation.Emit(stream);
        Assert.True(result.Success, string.Join(Environment.NewLine, result.Diagnostics));
        return MetadataReference.CreateFromImage(stream.ToArray());
    }

    private static CSharpCompilation CreateCompilation(string source, string assemblyName, IEnumerable<MetadataReference> additionalReferences) =>
        CSharpCompilation.Create(
            assemblyName,
            [CSharpSyntaxTree.ParseText(source, new CSharpParseOptions(LanguageVersion.Latest))],
            ReferenceHelper.GetRequiredReferences().Concat(additionalReferences),
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary, nullableContextOptions: NullableContextOptions.Enable));

    private sealed record GeneratorResult(string[] Sources, ImmutableArray<Diagnostic> Diagnostics);
}
