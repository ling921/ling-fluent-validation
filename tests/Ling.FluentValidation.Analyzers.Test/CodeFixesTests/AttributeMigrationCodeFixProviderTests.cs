using Ling.FluentValidation.Analyzers.Diagnostics;
using Ling.FluentValidation.Analyzers.System_ComponentModel_DataAnnotations;
using Ling.FluentValidation.CodeFixes;
using Ling.FluentValidation.Test.Common;
using Microsoft.CodeAnalysis.Testing;

namespace Ling.FluentValidation.Test.CodeFixesTests;

public sealed class AttributeMigrationCodeFixProviderTests
{
    [Theory]
    [InlineData("Range(0, 10)", "Ling.FluentValidation.Annotations.InclusiveBetween(0, 10)")]
#if NET8_0_OR_GREATER
    [InlineData("Range(0, 10, MinimumIsExclusive = true, MaximumIsExclusive = true)", "Ling.FluentValidation.Annotations.ExclusiveBetween(0, 10)")]
    [InlineData("Range(0, 10, MinimumIsExclusive = true)", "Ling.FluentValidation.Annotations.GreaterThan(0), Ling.FluentValidation.Annotations.LessThanOrEqualTo(10)")]
    [InlineData("Range(0, 10, MaximumIsExclusive = true)", "Ling.FluentValidation.Annotations.GreaterThanOrEqualTo(0), Ling.FluentValidation.Annotations.LessThan(10)")]
#endif
    public async Task RangeAttribute_Test(string oldAttribute, string newAttribute)
    {
        var testCode = string.Format(Defaults.SystemAttribute.CodeTemplate, oldAttribute, "int");
        var expectedCode = string.Format(Defaults.SystemAttribute.CodeTemplate, newAttribute, "int");

        var verifer = new CSharpCodeFixVerifier<RangeAttributeAnalyzer, MigrateAttributeCodeFix>
        {
            TestCode = testCode,
            FixedCode = expectedCode,
        };

        verifer.ExpectedDiagnostics.Add(new DiagnosticResult(DiagnosticDescriptors.UseLingValidationAttributeFix)
            .WithLocation(Defaults.SystemAttribute.CodeLine, Defaults.SystemAttribute.CodeColumn)
            .WithArguments(newAttribute.Split("(")[0].EnsureEndsWith("Attribute"), oldAttribute.Split("(")[0].EnsureEndsWith("Attribute")));

        await verifer.ExecuteAsync();
    }
}
