using Ling.FluentValidation.Analyzers;
using Ling.FluentValidation.Analyzers.Diagnostics;
using Ling.FluentValidation.Test.Common;
using Microsoft.CodeAnalysis.Testing;

namespace Ling.FluentValidation.Test;

public sealed class ValidationAttributeTargetAnalyzerTests
{
    [Fact]
    public async Task StaticMemberReportsDiagnostic()
    {
        var verifier = new CSharpAnalyzerVerifier<ValidationAttributeTargetAnalyzer>
        {
            TestCode = """
                using Ling.FluentValidation.Annotations;

                public class Sample
                {
                    [{|#0:NotNull|}]
                    public static string Value { get; set; }
                }
                """,
        };

        verifier.ExpectedDiagnostics.Add(
            new DiagnosticResult(DiagnosticDescriptors.AttributeTargetCannotBeGenerated)
                .WithLocation(0)
                .WithArguments("NotNullAttribute", "Value", "static members are not supported"));

        await verifier.ExecuteAsync();
    }

    [Fact]
    public async Task GenericContainingTypeReportsDiagnostic()
    {
        var verifier = new CSharpAnalyzerVerifier<ValidationAttributeTargetAnalyzer>
        {
            TestCode = """
                using Ling.FluentValidation.Annotations;

                public class Sample<T>
                {
                    [{|#0:NotNull|}]
                    public T Value { get; set; }
                }
                """,
        };

        verifier.ExpectedDiagnostics.Add(
            new DiagnosticResult(DiagnosticDescriptors.AttributeTargetCannotBeGenerated)
                .WithLocation(0)
                .WithArguments("NotNullAttribute", "Value", "generic containing types are not supported"));

        await verifier.ExecuteAsync();
    }
}
