using Ling.FluentValidation.Generators.Models;
using Ling.FluentValidation.Extensions;
using Microsoft.CodeAnalysis;
using System.Collections.Immutable;

namespace Ling.FluentValidation.Generators;

/// <summary>Generates FluentValidation validators for explicitly marked DTOs.</summary>
[Generator(LanguageNames.CSharp)]
internal sealed partial class ValidatorGenerator : IIncrementalGenerator
{
    private static readonly DiagnosticDescriptor MissingFluentValidation = new(
        "LFVG001", "FluentValidation is required",
        "Ling.FluentValidation.Generators requires a reference to FluentValidation in the target project",
        "Ling.FluentValidation.Generation", DiagnosticSeverity.Error, true);

    private static readonly DiagnosticDescriptor UnsupportedTarget = new(
        "LFVG002", "Validation target cannot be generated",
        "Validator for '{0}' cannot be generated: {1}",
        "Ling.FluentValidation.Generation", DiagnosticSeverity.Error, true);

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var generation = context.CompilationProvider.Select(static (compilation, token) => CreateResult(compilation, token));
        context.RegisterSourceOutput(generation, static (context, result) =>
        {
            foreach (var diagnostic in result.Diagnostics) context.ReportDiagnostic(diagnostic);
            if (result.Specs.Length == 0) return;

            var referencedDI = result.Specs[0].ReferencedAssemblies.Any(a => a.Name == "Microsoft.Extensions.DependencyInjection.Abstractions");
            context.AddSource("GeneratedValidatorRegistry.g.cs", Execute.GetGeneratedValidatorsClassText(result.Specs, referencedDI));
            foreach (var item in result.Specs)
            {
                context.AddSource($"{item.HierarchyInfo.FilenameHint}.Validator.g.cs", Execute.GetValidatorClassText(item));
            }
        });
    }

    private static GenerationResult CreateResult(Compilation compilation, CancellationToken token)
    {
        var diagnostics = ImmutableArray.CreateBuilder<Diagnostic>();
        if (compilation.GetTypeByMetadataName(Constants.AbstractValidatorClassMetadataName) is null)
        {
            diagnostics.Add(Diagnostic.Create(MissingFluentValidation, Location.None));
            return new([], diagnostics.ToImmutable());
        }

        var assemblies = new HashSet<IAssemblySymbol>(SymbolEqualityComparer.Default) { compilation.Assembly };
        foreach (var attribute in compilation.Assembly.GetAttributes())
        {
            token.ThrowIfCancellationRequested();
            var name = attribute.AttributeClass?.GetFullyQualifiedMetadataName();
            INamedTypeSymbol? marker = null;
            if (name == Constants.GenerateValidatorsFromAssemblyContainingAttributeFullyQualifiedMetadataName &&
                attribute.ConstructorArguments.Length == 1 &&
                attribute.ConstructorArguments[0].Value is INamedTypeSymbol markerType)
            {
                marker = markerType;
            }
            else if (name == Constants.GenericGenerateValidatorsFromAssemblyContainingAttributeFullyQualifiedMetadataName &&
                     attribute.AttributeClass?.TypeArguments.Length == 1 &&
                     attribute.AttributeClass.TypeArguments[0] is INamedTypeSymbol genericMarkerType)
            {
                marker = genericMarkerType;
            }
            if (marker is not null) assemblies.Add(marker.ContainingAssembly);
        }

        var supportsSplitCascadeModes = compilation.GetTypeByMetadataName(Constants.AbstractValidatorClassMetadataName)?
            .GetMembers("ClassLevelCascadeMode").Length > 0;
        var specs = ImmutableArray.CreateBuilder<GenerationSpec>();
        var identities = new HashSet<string>(StringComparer.Ordinal);
        foreach (var assembly in assemblies)
        {
            foreach (var type in EnumerateTypes(assembly.GlobalNamespace, token))
            {
                if (!type.GetAttributes().Any(static a => a.AttributeClass?.GetFullyQualifiedMetadataName() == Constants.GenerateValidatorAttributeFullyQualifiedMetadataName)) continue;

                var reason = Execute.GetUnsupportedTypeReason(compilation, type);
                if (reason is not null)
                {
                    diagnostics.Add(Diagnostic.Create(UnsupportedTarget, type.Locations.FirstOrDefault() ?? Location.None, type.ToDisplayString(), reason));
                    continue;
                }

                foreach (var member in type.GetMembers().Where(static member =>
                    member is IPropertySymbol or IFieldSymbol && member.GetAttributes().Any(static attribute =>
                        attribute.AttributeClass is not null && GenerationDefaults.MemberAttributeFullyQualifiedMetadataNames.Contains(attribute.AttributeClass.GetFullyQualifiedMetadataName()))))
                {
                    var memberReason = Execute.GetUnsupportedMemberReason(compilation, member);
                    if (memberReason is not null)
                    {
                        diagnostics.Add(Diagnostic.Create(UnsupportedTarget, member.Locations.FirstOrDefault() ?? Location.None, $"{type.ToDisplayString()}.{member.Name}", memberReason));
                    }
                }

                var spec = Execute.CreateGenerationSpec(compilation, type, type.Locations.FirstOrDefault() ?? Location.None, supportsSplitCascadeModes);
                if (!identities.Add(spec.GenerateClassFullName))
                {
                    diagnostics.Add(Diagnostic.Create(UnsupportedTarget, spec.Location, type.ToDisplayString(), $"generated name '{spec.GenerateClassFullName}' conflicts with another validator"));
                    continue;
                }
                specs.Add(spec);
            }
        }
        return new(specs.ToImmutable(), diagnostics.ToImmutable());
    }

    private static IEnumerable<INamedTypeSymbol> EnumerateTypes(INamespaceSymbol @namespace, CancellationToken token)
    {
        foreach (var member in @namespace.GetMembers())
        {
            token.ThrowIfCancellationRequested();
            if (member is INamespaceSymbol childNamespace)
                foreach (var type in EnumerateTypes(childNamespace, token)) yield return type;
            else if (member is INamedTypeSymbol type)
                foreach (var nested in EnumerateTypes(type, token)) yield return nested;
        }
    }

    private static IEnumerable<INamedTypeSymbol> EnumerateTypes(INamedTypeSymbol type, CancellationToken token)
    {
        yield return type;
        foreach (var nested in type.GetTypeMembers())
        {
            token.ThrowIfCancellationRequested();
            foreach (var item in EnumerateTypes(nested, token)) yield return item;
        }
    }

    private sealed record GenerationResult(ImmutableArray<GenerationSpec> Specs, ImmutableArray<Diagnostic> Diagnostics);
}
