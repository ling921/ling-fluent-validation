using Ling.FluentValidation.Analyzers.Infrastructure;
using Ling.FluentValidation.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;

namespace Ling.FluentValidation.Analyzers.System_ComponentModel_DataAnnotations;

/// <summary>
/// Analyzer for 'System.ComponentModel.DataAnnotations.RequiredAttribute'.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class RequiredAttributeAnalyzer : SystemAttributeDiagnosticAnalyzer
{
    /// <inheritdoc/>
    public override string TargetAttributeFullyQualifiedMetadataName => Constants.SystemRequiredAttributeFullyQualifiedMetadataName;

    /// <inheritdoc/>
    protected override void BuildCodeFixParameters(
        ImmutableDictionary<string, string?>.Builder builder,
        AttributeData attributeData,
        ITypeSymbol memberTypeSymbol)
    {
        // By default, 'AllowEmptyStrings' is 'false'.
        if (memberTypeSymbol.IsStringType() &&
            attributeData.GetNamedArgument("AllowEmptyStrings") is not { Value: true })
        {
            builder.Add("NewAttribute", Constants.NotEmptyAttributeFullyQualifiedMetadataName);
        }
        else
        {
            builder.Add("NewAttribute", Constants.NotNullAttributeFullyQualifiedMetadataName);
        }
    }
}
