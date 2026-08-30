using Ling.FluentValidation.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;

namespace Ling.FluentValidation.Analyzers.System_ComponentModel_DataAnnotations;

public sealed class StringLengthAttributeAnalyzer : Base64StringAttributeAnalyzer
{
    /// <inheritdoc/>
    public override string TargetAttributeFullyQualifiedMetadataName => Constants.SystemStringLengthAttributeFullyQualifiedMetadataName;

    /// <inheritdoc/>
    public override ImmutableArray<DiagnosticDescriptor> AdditionalSupportedDiagnostics => [
            ..base.AdditionalSupportedDiagnostics,
        ];

    /// <inheritdoc/>
    protected override bool RunAdditionalAnalyze(
        SyntaxNodeAnalysisContext context,
        ISymbol memberSymbol,
        ITypeSymbol memberTypeSymbol,
        AttributeData attributeData,
        ImmutableArray<INamedTypeSymbol> namedTypeSymbols)
    {
        return base.RunAdditionalAnalyze(
            context,
            memberSymbol,
            memberTypeSymbol,
            attributeData,
            namedTypeSymbols);
    }

    /// <inheritdoc/>
    protected override void BuildCodeFixParameters(
        ImmutableDictionary<string, string?>.Builder builder,
        AttributeData attributeData,
        ITypeSymbol memberTypeSymbol)
    {
        if (attributeData.GetNamedArgument("MinimumLength") is { Value: int })
        {
            builder.Add("NewAttribute", Constants.LengthAttributeFullyQualifiedMetadataName);
        }
        else
        {
            builder.Add("NewAttribute", Constants.MaximumLengthAttributeFullyQualifiedMetadataName);
        }
    }
}
