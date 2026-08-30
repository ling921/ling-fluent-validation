using Ling.FluentValidation.Analyzers.Diagnostics;
using Ling.FluentValidation.Analyzers.Infrastructure;
using Ling.FluentValidation.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;

namespace Ling.FluentValidation.Analyzers.System_ComponentModel_DataAnnotations;

/// <summary>
/// Analyzer for 'System.ComponentModel.DataAnnotations.Base64StringAttribute'.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class Base64StringAttributeAnalyzer : SystemAttributeDiagnosticAnalyzer
{
    /// <inheritdoc/>
    public override string TargetAttributeFullyQualifiedMetadataName => Constants.SystemBase64StringAttributeFullyQualifiedMetadataName;

    /// <inheritdoc/>
    public override ImmutableArray<DiagnosticDescriptor> AdditionalSupportedDiagnostics => [
            DiagnosticDescriptors.AttributeShouldUsedOnPropertyOfOneType,
        ];

    /// <inheritdoc/>
    protected override bool RunAdditionalAnalyze(
        SyntaxNodeAnalysisContext context,
        ISymbol memberSymbol,
        ITypeSymbol memberTypeSymbol,
        AttributeData attributeData,
        ImmutableArray<INamedTypeSymbol> namedTypeSymbols)
    {
        if (!memberTypeSymbol.IsStringType())
        {
            context.ReportDiagnostic(
                Diagnostic.Create(
                    DiagnosticDescriptors.AttributeShouldUsedOnPropertyOfOneType,
                    attributeData.ApplicationSyntaxReference!.GetSyntax().GetLocation(),
                    attributeData.AttributeClass!.Name,
                    "System.String"));

            return false;
        }

        return true;
    }

    /// <inheritdoc/>
    protected override void BuildCodeFixParameters(ImmutableDictionary<string, string?>.Builder builder, AttributeData attributeData, ITypeSymbol memberTypeSymbol)
    {
        builder.Add("NewAttribute", Constants.Base64StringAttributeFullyQualifiedMetadataName);
    }
}
