using Ling.FluentValidation.Analyzers.Infrastructure;
using Ling.FluentValidation.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;

namespace Ling.FluentValidation.Analyzers.System_ComponentModel_DataAnnotations;

/// <summary>
/// Analyzer for 'System.ComponentModel.DataAnnotations.RangeAttribute'.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class RangeAttributeAnalyzer : SystemAttributeDiagnosticAnalyzer
{
    /// <inheritdoc/>
    public override string TargetAttributeFullyQualifiedMetadataName => Constants.SystemRangeAttributeFullyQualifiedMetadataName;

    /// <inheritdoc/>
    public override ImmutableArray<DiagnosticDescriptor> AdditionalSupportedDiagnostics => [
            //DiagnosticDescriptors.RangeAttributeShouldHaveMinimumAndMaximum,
        ];

    /// <inheritdoc/>
    protected override bool RunAdditionalAnalyze(
        SyntaxNodeAnalysisContext context,
        ISymbol memberSymbol,
        ITypeSymbol memberTypeSymbol,
        AttributeData attributeData,
        ImmutableArray<INamedTypeSymbol> namedTypeSymbols)
    {
        // Invalid constructor calls are already reported by the compiler and cannot
        // be migrated safely because the generated rule requires both boundaries.
        return attributeData.ConstructorArguments.Length == 2;
    }

    /// <inheritdoc/>
    protected override void BuildCodeFixParameters(
        ImmutableDictionary<string, string?>.Builder builder,
        AttributeData attributeData,
        ITypeSymbol memberTypeSymbol)
    {
        var minimumIsExclusive = attributeData.GetNamedArgument("MinimumIsExclusive") is { Value: true };
        var maximumIsExclusive = attributeData.GetNamedArgument("MaximumIsExclusive") is { Value: true };
        if (minimumIsExclusive && maximumIsExclusive)
        {
            builder.Add("NewAttribute", Constants.ExclusiveBetweenAttributeFullyQualifiedMetadataName);
        }
        else if (!minimumIsExclusive && !maximumIsExclusive)
        {
            builder.Add("NewAttribute", Constants.InclusiveBetweenAttributeFullyQualifiedMetadataName);
        }
        else
        {
            builder.Add("NewAttribute", minimumIsExclusive ? Constants.GreaterThanAttributeFullyQualifiedMetadataName : Constants.GreaterThanOrEqualToAttributeFullyQualifiedMetadataName);
            builder.Add("NewAttribute_1", maximumIsExclusive ? Constants.LessThanAttributeFullyQualifiedMetadataName : Constants.LessThanOrEqualToAttributeFullyQualifiedMetadataName);
        }
    }
}
