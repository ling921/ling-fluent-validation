using Ling.FluentValidation.Analyzers.Diagnostics;
using Ling.FluentValidation.Analyzers.System_ComponentModel_DataAnnotations;
using Ling.FluentValidation.CodeFixes;
using Ling.FluentValidation.Test.Common;
using Microsoft.CodeAnalysis.Testing;
using static Ling.FluentValidation.Test.Common.Defaults.SystemAttribute;

namespace Ling.FluentValidation.Test.System_ComponentModel_DataAnnotations;

#if NET8_0_OR_GREATER
public sealed class AllowedValuesAttributeAnalyzerTests
{
    [Fact]
    public async Task Without_NamedArguments_Test()
    {
        var verifier = new CSharpCodeFixVerifier<AllowedValuesAttributeAnalyzer, MigrateAttributeCodeFix>()
        {
            TestCode = string.Format(CodeTemplate, "AllowedValues(1, 2, 3)", "int"),
            FixedCode = string.Format(CodeTemplate, "Ling.FluentValidation.Annotations.AllowedValues(1, 2, 3)", "int"),
        };

        verifier.ExpectedDiagnostics.Add(new DiagnosticResult(DiagnosticDescriptors.UseLingValidationAttributeFix)
            .WithLocation(CodeLine, CodeColumn)
            .WithArguments("Ling.FluentValidation.Annotations.AllowedValuesAttribute", "AllowedValuesAttribute"));

        await verifier.ExecuteAsync();
    }

    [Fact]
    public async Task With_ErrorMessage_Test()
    {
        var verifier = new CSharpCodeFixVerifier<AllowedValuesAttributeAnalyzer, MigrateAttributeCodeFix>()
        {
            TestCode = string.Format(CodeTemplate, "AllowedValues(1, 2, 3, ErrorMessage = \"test\")", "int"),
            FixedCode = string.Format(CodeTemplate, "Ling.FluentValidation.Annotations.AllowedValues(1, 2, 3, ErrorMessage = \"test\")", "int"),
        };

        verifier.ExpectedDiagnostics.Add(new DiagnosticResult(DiagnosticDescriptors.UseLingValidationAttributeFix)
            .WithLocation(CodeLine, CodeColumn)
            .WithArguments("Ling.FluentValidation.Annotations.AllowedValuesAttribute", "AllowedValuesAttribute"));

        await verifier.ExecuteAsync();
    }

    [Fact]
    public async Task With_ErrorMessageResourceName_Test()
    {
        var verifier = new CSharpCodeFixVerifier<AllowedValuesAttributeAnalyzer, MigrateAttributeCodeFix>()
        {
            TestCode = string.Format(CodeTemplate, "AllowedValues(1, 2, 3, ErrorMessageResourceName = \"test\")", "int"),
            FixedCode = null,
        };

        await verifier.ExecuteAsync();
    }
}
#endif
