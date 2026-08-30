using Ling.FluentValidation.Analyzers.Diagnostics;
using Ling.FluentValidation.Analyzers.Infrastructure;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;

namespace Ling.FluentValidation.Analyzers.System_ComponentModel_DataAnnotations;

/// <summary>
/// Analyzer for 'System.ComponentModel.DataAnnotations.DeniedValuesAttribute'.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class DeniedValuesAttributeAnalyzer : SystemAttributeDiagnosticAnalyzer
{
    /// <inheritdoc/>
    public override string TargetAttributeFullyQualifiedMetadataName => Constants.SystemDeniedValuesAttributeFullyQualifiedMetadataName;

    /// <inheritdoc/>
    public override ImmutableArray<DiagnosticDescriptor> AdditionalSupportedDiagnostics => [
            DiagnosticDescriptors.DeniedValuesAttributeShouldHaveAtLeastOneValue,
        ];

    /// <inheritdoc/>
    protected override bool RunAdditionalAnalyze(
        SyntaxNodeAnalysisContext context,
        ISymbol memberSymbol,
        ITypeSymbol memberTypeSymbol,
        AttributeData attributeData,
        ImmutableArray<INamedTypeSymbol> namedTypeSymbols)
    {
        if (attributeData.ConstructorArguments.Length == 0)
        {
            context.ReportDiagnostic(
                Diagnostic.Create(
                    DiagnosticDescriptors.DeniedValuesAttributeShouldHaveAtLeastOneValue,
                    attributeData.ApplicationSyntaxReference!.GetSyntax().GetLocation()));
            return false;
        }

        return true;
    }

    /// <inheritdoc/>
    protected override void BuildCodeFixParameters(ImmutableDictionary<string, string?>.Builder builder, AttributeData attributeData, ITypeSymbol memberTypeSymbol)
    {
        builder.Add("NewAttribute", Constants.DeniedValuesAttributeFullyQualifiedMetadataName);
    }
}
