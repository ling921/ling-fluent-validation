using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace Ling.FluentValidation.Analyzers.System_ComponentModel_DataAnnotations;

/// <summary>
/// Analyzer for 'System.ComponentModel.DataAnnotations.RegularExpressionAttribute'.
/// </summary>
public sealed class RegularExpressionAttributeAnalyzer : Base64StringAttributeAnalyzer
{
    /// <inheritdoc/>
    public override string TargetAttributeFullyQualifiedMetadataName => Constants.SystemRegularExpressionAttributeFullyQualifiedMetadataName;

    /// <inheritdoc/>
    protected override void BuildCodeFixParameters(ImmutableDictionary<string, string?>.Builder builder, AttributeData attributeData, ITypeSymbol memberTypeSymbol)
    {
        builder.Add("NewAttribute", Constants.MatchesAttributeFullyQualifiedMetadataName);
    }
}
