using Ling.FluentValidation.Analyzers;
using Ling.FluentValidation.Analyzers.Diagnostics;
using Ling.FluentValidation.Test.Common;
using Microsoft.CodeAnalysis.Testing;

namespace Ling.FluentValidation.Test;

public sealed class AllowedValuesAttributeAnalyzerTests
{
    [Fact]
    public async Task EmptyAllowedValuesReportsDiagnostic()
    {
        var verifier = new CSharpAnalyzerVerifier<AllowedValuesAttributeAnalyzer>
        {
            TestCode = """
                using Ling.FluentValidation.Annotations;

                public class Sample
                {
                    [{|#0:AllowedValues()|}]
                    public int Value { get; set; }
                }
                """,
        };

        verifier.ExpectedDiagnostics.Add(
            new DiagnosticResult(DiagnosticDescriptors.AllowedValuesAttributeShouldHaveAtLeastOneValue)
                .WithLocation(0));

        await verifier.ExecuteAsync();
    }
}
