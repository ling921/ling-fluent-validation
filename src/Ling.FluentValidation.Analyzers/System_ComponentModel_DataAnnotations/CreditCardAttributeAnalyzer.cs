using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Ling.FluentValidation.Analyzers.System_ComponentModel_DataAnnotations;

/// <summary>
/// Analyzer for 'System.ComponentModel.DataAnnotations.CreditCardAttribute'.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class CreditCardAttributeAnalyzer : Base64StringAttributeAnalyzer
{
    /// <inheritdoc/>
    public override string TargetAttributeFullyQualifiedMetadataName => Constants.SystemCreditCardAttributeFullyQualifiedMetadataName;

    /// <inheritdoc/>
    protected override void BuildCodeFixParameters(ImmutableDictionary<string, string?>.Builder builder, AttributeData attributeData, ITypeSymbol memberTypeSymbol)
    {
        builder.Add("NewAttribute", Constants.CreditCardAttributeFullyQualifiedMetadataName);
    }
}
