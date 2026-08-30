using Ling.FluentValidation.Analyzers;
using Ling.FluentValidation.Analyzers.Diagnostics;
using Ling.FluentValidation.Test.Common;
using Microsoft.CodeAnalysis.Testing;

namespace Ling.FluentValidation.Test;

public sealed class LengthAttributeAnalyzerTests
{
    [Fact]
    public async Task EqualMinimumAndMaximumReportsSimplification()
    {
        var verifier = new CSharpAnalyzerVerifier<LengthAttributeAnalyzer>
        {
            TestCode = """
                using Ling.FluentValidation.Annotations;

                public class Sample
                {
                    [{|#0:Length(5, 5)|}]
                    public string Value { get; set; }
                }
                """,
        };

        verifier.ExpectedDiagnostics.Add(
            new DiagnosticResult(DiagnosticDescriptors.LengthAttributeMinEqualToMax)
                .WithLocation(0)
                .WithArguments("LengthAttribute"));

        await verifier.ExecuteAsync();
    }
}
