using Ling.FluentValidation.Analyzers;
using Ling.FluentValidation.Analyzers.Diagnostics;
using Ling.FluentValidation.CodeFixes;
using Ling.FluentValidation.Test.Common;
using Microsoft.CodeAnalysis.Testing;

namespace Ling.FluentValidation.Test;

public sealed class GenerateValidatorMarkerAnalyzerTests
{
    [Fact]
    public async Task RuleOnUnmarkedTypeReportsDiagnostic()
    {
        var verifier = new CSharpAnalyzerVerifier<GenerateValidatorMarkerAnalyzer>
        {
            TestCode = """
                using Ling.FluentValidation.Annotations;
                public sealed class {|#0:Request|}
                {
                    [NotEmpty]
                    public string Name { get; set; } = "";
                }
                """,
        };
        verifier.ExpectedDiagnostics.Add(
            new DiagnosticResult(DiagnosticDescriptors.ValidationTypeMustBeMarked)
                .WithLocation(0)
                .WithArguments("Request"));
        await verifier.ExecuteAsync();
    }

    [Fact]
    public async Task MarkedTypeHasNoDiagnostic()
    {
        var verifier = new CSharpAnalyzerVerifier<GenerateValidatorMarkerAnalyzer>
        {
            TestCode = """
                using Ling.FluentValidation.Annotations;
                [GenerateValidator]
                public sealed class Request
                {
                    [NotEmpty]
                    public string Name { get; set; } = "";
                }
                """,
        };
        await verifier.ExecuteAsync();
    }

    [Fact]
    public async Task SupportedDataAnnotationReportsDiagnostic()
    {
        var verifier = new CSharpAnalyzerVerifier<GenerateValidatorMarkerAnalyzer>
        {
            TestCode = """
                public sealed class {|#0:Request|}
                {
                    [System.ComponentModel.DataAnnotations.Required]
                    public string Name { get; set; } = "";
                }
                """,
        };
        verifier.ExpectedDiagnostics.Add(
            new DiagnosticResult(DiagnosticDescriptors.ValidationTypeMustBeMarked)
                .WithLocation(0)
                .WithArguments("Request"));
        await verifier.ExecuteAsync();
    }

    [Fact]
    public async Task UnrelatedDataAnnotationDoesNotReportDiagnostic()
    {
        var verifier = new CSharpAnalyzerVerifier<GenerateValidatorMarkerAnalyzer>
        {
            TestCode = """
                public sealed class Request
                {
                    [System.ComponentModel.DataAnnotations.Display(Name = "Name")]
                    public string Name { get; set; } = "";
                }
                """,
        };
        await verifier.ExecuteAsync();
    }

    [Fact]
    public async Task CodeFixAddsGenerateValidatorAttribute()
    {
        var verifier = new CSharpCodeFixVerifier<GenerateValidatorMarkerAnalyzer, AddGenerateValidatorAttributeCodeFix>
        {
            TestCode = """
                using Ling.FluentValidation.Annotations;
                public sealed class {|#0:Request|}
                {
                    [NotEmpty]
                    public string Name { get; set; } = "";
                }
                """,
            FixedCode = """
                using Ling.FluentValidation.Annotations;
                [global::Ling.FluentValidation.Annotations.GenerateValidator]
                public sealed class Request
                {
                    [NotEmpty]
                    public string Name { get; set; } = "";
                }
                """,
        };
        verifier.ExpectedDiagnostics.Add(
            new DiagnosticResult(DiagnosticDescriptors.ValidationTypeMustBeMarked)
                .WithLocation(0)
                .WithArguments("Request"));
        await verifier.ExecuteAsync();
    }
}
