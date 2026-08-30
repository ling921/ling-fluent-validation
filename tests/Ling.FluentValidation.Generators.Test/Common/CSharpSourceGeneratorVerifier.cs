using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Immutable;

namespace Ling.FluentValidation.Generators.Test.Common;

internal sealed class CSharpSourceGeneratorVerifier<TSourceGenerator>
    where TSourceGenerator : class, new()
{
    public LanguageVersion LanguageVersion { get; set; } = LanguageVersion.Default;

    public string[] SourceCodes { get; set; } = [];

    public OutputKind OutputKind { get; set; } = OutputKind.DynamicallyLinkedLibrary;
    public (string FileName, string SourceCode)[] GeneratedCodes { get; set; } = [];

    public Task ExecuteAsync()
    {
        var tester = new Test
        {
            LanguageVersion = LanguageVersion,
            TestState =
            {
                Sources = {},
                OutputKind = OutputKind,
                AdditionalReferences = {},
                GeneratedSources = {},
            },
        };

        foreach (var souceCode in SourceCodes)
        {
            tester.TestState.Sources.Add(SourceText.From(souceCode, Encoding.UTF8));
        }

#if NET5_0_OR_GREATER
        tester.TestState.ReferenceAssemblies = new ReferenceAssemblies(ReferenceHelper.TargetFramework);
#endif
        tester.TestState.AdditionalReferences.AddRange(ReferenceHelper.GetRequiredReferences());

        foreach (var (fileName, sourceCode) in GeneratedCodes)
        {
            tester.TestState.GeneratedSources.Add((typeof(TSourceGenerator), fileName, SourceText.From(sourceCode, Encoding.UTF8)));
        }

        return tester.RunAsync();
    }

    private sealed class Test : CSharpSourceGeneratorTest<TSourceGenerator, DefaultVerifier>
    {
        public LanguageVersion LanguageVersion { get; set; } = LanguageVersion.Default;

        protected override CompilationOptions CreateCompilationOptions()
        {
            var compilationOptions = base.CreateCompilationOptions();
            return compilationOptions.WithSpecificDiagnosticOptions(
                 compilationOptions.SpecificDiagnosticOptions.SetItems(GetNullableWarningsFromCompiler()));
        }

        protected override ParseOptions CreateParseOptions()
        {
            return ((CSharpParseOptions)base.CreateParseOptions()).WithLanguageVersion(LanguageVersion);
        }

        private static ImmutableDictionary<string, ReportDiagnostic> GetNullableWarningsFromCompiler()
        {
            var args = new[] { "/warnaserror:nullable" };
            var commandLineArguments = CSharpCommandLineParser.Default.Parse(args, baseDirectory: Environment.CurrentDirectory, sdkDirectory: Environment.CurrentDirectory);
            var nullableWarnings = commandLineArguments.CompilationOptions.SpecificDiagnosticOptions;

            return nullableWarnings;
        }
    }
}
