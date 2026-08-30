using Ling.FluentValidation.Analyzers.Diagnostics;
using Ling.FluentValidation.Analyzers.Infrastructure;
using Ling.FluentValidation.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;

namespace Ling.FluentValidation.Analyzers.System_ComponentModel_DataAnnotations;

/// <summary>
/// Analyzer for 'System.ComponentModel.DataAnnotations.EnumDataTypeAttribute'.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class EnumDataTypeAttributeAnalyzer : SystemAttributeDiagnosticAnalyzer
{
    /// <inheritdoc/>
    public override string TargetAttributeFullyQualifiedMetadataName => Constants.SystemEnumDataTypeAttributeFullyQualifiedMetadataName;

    /// <inheritdoc/>
    public override ImmutableArray<DiagnosticDescriptor> AdditionalSupportedDiagnostics => [
            DiagnosticDescriptors.AttributeShouldUsedOnPropertyOfTwoTypes,
        ];

    /// <inheritdoc/>
    protected override bool RunAdditionalAnalyze(
        SyntaxNodeAnalysisContext context,
        ISymbol memberSymbol,
        ITypeSymbol memberTypeSymbol,
        AttributeData attributeData,
        ImmutableArray<INamedTypeSymbol> namedTypeSymbols)
    {
        if (memberTypeSymbol.SpecialType is not SpecialType.System_String and not SpecialType.System_Enum)
        {
            context.ReportDiagnostic(
                Diagnostic.Create(
                    DiagnosticDescriptors.AttributeShouldUsedOnPropertyOfTwoTypes,
                    attributeData.ApplicationSyntaxReference!.GetSyntax().GetLocation(),
                    attributeData.AttributeClass!.Name,
                    "System.String",
                    "System.Enum"));

            return false;
        }

        return true;
    }

    /// <inheritdoc/>
    protected override void BuildCodeFixParameters(ImmutableDictionary<string, string?>.Builder builder, AttributeData attributeData, ITypeSymbol memberTypeSymbol)
    {
        if (memberTypeSymbol.SpecialType == SpecialType.System_String)
        {
            builder.Add("NewAttribute", Constants.EnumNameAttributeFullyQualifiedMetadataName);
        }
        else
        {
            builder.Add("NewAttribute", Constants.EnumAttributeFullyQualifiedMetadataName);
        }
    }
}
