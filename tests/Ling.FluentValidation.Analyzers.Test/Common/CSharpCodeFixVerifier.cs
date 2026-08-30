using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Testing;

namespace Ling.FluentValidation.Test.Common;

internal sealed class CSharpCodeFixVerifier<TAnalyzer, TCodeFix> : CSharpAnalyzerVerifier<TAnalyzer>
    where TAnalyzer : DiagnosticAnalyzer, new()
    where TCodeFix : CodeFixProvider, new()
{
    public string? FixedCode { get; set; }

    public override Task ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var tester = new CSharpCodeFixTest<TAnalyzer, TCodeFix, DefaultVerifier>()
        {
            TestCode = TestCode,
            FixedCode = FixedCode!,
        };

        Configure(tester);

        return tester.RunAsync(cancellationToken);
    }
}
