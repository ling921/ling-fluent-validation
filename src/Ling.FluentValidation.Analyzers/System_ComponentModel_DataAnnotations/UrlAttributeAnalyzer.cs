using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Ling.FluentValidation.Analyzers.System_ComponentModel_DataAnnotations;

/// <summary>
/// Analyzer for 'System.ComponentModel.DataAnnotations.UrlAttribute'.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class UrlAttributeAnalyzer : Base64StringAttributeAnalyzer
{
    /// <inheritdoc/>
    public override string TargetAttributeFullyQualifiedMetadataName => Constants.SystemUrlAttributeFullyQualifiedMetadataName;

    /// <inheritdoc/>
    protected override void BuildCodeFixParameters(ImmutableDictionary<string, string?>.Builder builder, AttributeData attributeData, ITypeSymbol memberTypeSymbol)
    {
        builder.Add("NewAttribute", Constants.UrlAttributeFullyQualifiedMetadataName);
    }
}
