using Ling.FluentValidation.Analyzers.Diagnostics;
using Ling.FluentValidation.Analyzers.Infrastructure;
using Ling.FluentValidation.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;

namespace Ling.FluentValidation.Analyzers.System_ComponentModel_DataAnnotations;

/// <summary>
/// Analyzer for 'System.ComponentModel.DataAnnotations.CompareAttribute'.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class CompareAttributeAnalyzer : SystemAttributeDiagnosticAnalyzer
{
    /// <inheritdoc/>
    public override string TargetAttributeFullyQualifiedMetadataName => Constants.SystemCompareAttributeFullyQualifiedMetadataName;

    /// <inheritdoc/>
    public override ImmutableArray<DiagnosticDescriptor> AdditionalSupportedDiagnostics => [
            DiagnosticDescriptors.CompareAttributeShouldSpecifyProperty,
            DiagnosticDescriptors.CompareAttributeShouldSpecifySameTypeProperty,
        ];

    /// <inheritdoc/>
    protected override bool RunAdditionalAnalyze(
        SyntaxNodeAnalysisContext context,
        ISymbol memberSymbol,
        ITypeSymbol memberTypeSymbol,
        AttributeData attributeData,
        ImmutableArray<INamedTypeSymbol> namedTypeSymbols)
    {
        var comparedProperty = string.Empty;
        ITypeSymbol? comparedMemberType = null;
        if (attributeData.ConstructorArguments[0].Value is string value)
        {
            comparedProperty = value;

            foreach (var member in memberSymbol.ContainingType.GetMembers())
            {
                if (member is IPropertySymbol or IFieldSymbol && member.Name == comparedProperty)
                {
                    comparedMemberType = member switch
                    {
                        IPropertySymbol property => property.Type,
                        IFieldSymbol field => field.Type,
                        _ => null
                    };
                    break;
                }
            }
        }

        if (comparedMemberType is null)
        {
            var attributeSyntax = (AttributeSyntax)attributeData.ApplicationSyntaxReference!.GetSyntax();
            context.ReportDiagnostic(
                Diagnostic.Create(
                    DiagnosticDescriptors.CompareAttributeShouldSpecifyProperty,
                    attributeSyntax.ArgumentList!.Arguments[0].GetLocation(),
                    comparedProperty,
                    memberSymbol.ContainingType.GetFullyQualifiedMetadataName()));
            return false;
        }
        else if (!SymbolEqualityComparer.Default.Equals(memberTypeSymbol, comparedMemberType))
        {
            var attributeSyntax = (AttributeSyntax)attributeData.ApplicationSyntaxReference!.GetSyntax();
            context.ReportDiagnostic(
                Diagnostic.Create(
                    DiagnosticDescriptors.CompareAttributeShouldSpecifySameTypeProperty,
                    attributeSyntax.ArgumentList!.Arguments[0].GetLocation(),
                    comparedProperty,
                    memberTypeSymbol.GetFullyQualifiedMetadataName()));
            return false;
        }

        return true;
    }

    /// <inheritdoc/>
    protected override void BuildCodeFixParameters(ImmutableDictionary<string, string?>.Builder builder, AttributeData attributeData, ITypeSymbol memberTypeSymbol)
    {
        builder.Add("NewAttribute", Constants.CompareAttributeFullyQualifiedMetadataName);
    }
}
