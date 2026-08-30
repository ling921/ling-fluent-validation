using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;

namespace Ling.FluentValidation.Analyzers.System_ComponentModel_DataAnnotations;

/// <summary>
/// Analyzer for 'System.ComponentModel.DataAnnotations.MaxLengthAttribute'.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class MaxLengthAttributeAnalyzer : LengthAttributeAnalyzer
{
    /// <inheritdoc/>
    public override string TargetAttributeFullyQualifiedMetadataName => Constants.SystemMaxLengthAttributeFullyQualifiedMetadataName;

    /// <inheritdoc/>
    protected override void BuildCodeFixParameters(ImmutableDictionary<string, string?>.Builder builder, AttributeData attributeData, ITypeSymbol memberTypeSymbol)
    {
        builder.Add("NewAttribute", Constants.MaximumLengthAttributeFullyQualifiedMetadataName);
    }
}
