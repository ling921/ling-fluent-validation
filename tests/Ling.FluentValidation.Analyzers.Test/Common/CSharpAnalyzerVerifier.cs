using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Testing;

namespace Ling.FluentValidation.Test.Common;

internal class CSharpAnalyzerVerifier<TAnalyzer>
    where TAnalyzer : DiagnosticAnalyzer, new()
{
    public string TestCode { get; set; } = default!;
    public List<DiagnosticResult> ExpectedDiagnostics { get; } = [];
    public LanguageVersion LanguageVersion { get; set; } = LanguageVersion.Default;

    public virtual Task ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var tester = new CSharpAnalyzerTest<TAnalyzer, DefaultVerifier>()
        {
            TestCode = TestCode,
        };

        Configure(tester);

        return tester.RunAsync(cancellationToken);
    }

    protected void Configure(AnalyzerTest<DefaultVerifier> tester)
    {
#if NET5_0_OR_GREATER
        tester.TestState.ReferenceAssemblies = new ReferenceAssemblies(ReferenceHelper.TargetFramework);
#endif
        tester.TestState.AdditionalReferences.AddRange(ReferenceHelper.GetRequiredReferences());

        tester.TestState.ExpectedDiagnostics.AddRange(ExpectedDiagnostics);
    }
}
