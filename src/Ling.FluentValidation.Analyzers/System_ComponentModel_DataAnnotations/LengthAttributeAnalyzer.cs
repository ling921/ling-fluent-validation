using Ling.FluentValidation.Analyzers.Diagnostics;
using Ling.FluentValidation.Analyzers.Infrastructure;
using Ling.FluentValidation.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;

namespace Ling.FluentValidation.Analyzers.System_ComponentModel_DataAnnotations;

/// <summary>
/// Analyzer for 'System.ComponentModel.DataAnnotations.LengthAttribute'.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class LengthAttributeAnalyzer : SystemAttributeDiagnosticAnalyzer
{
    /// <inheritdoc/>
    public override string TargetAttributeFullyQualifiedMetadataName => Constants.SystemLengthAttributeFullyQualifiedMetadataName;

    /// <inheritdoc/>
    public override ImmutableArray<DiagnosticDescriptor> AdditionalSupportedDiagnostics => [
            DiagnosticDescriptors.LengthAttributeShouldUsedOnStringOrEnumerableProperty,
        ];

    /// <inheritdoc/>
    protected override bool RunAdditionalAnalyze(
        SyntaxNodeAnalysisContext context,
        ISymbol memberSymbol,
        ITypeSymbol memberTypeSymbol,
        AttributeData attributeData,
        ImmutableArray<INamedTypeSymbol> namedTypeSymbols)
    {
        if (!memberTypeSymbol.IsStringType() && !memberTypeSymbol.IsEnumerableType())
        {
            var attributeSyntax = (AttributeSyntax)attributeData.ApplicationSyntaxReference!.GetSyntax()!;
            var diagnostic = Diagnostic.Create(
                DiagnosticDescriptors.LengthAttributeShouldUsedOnStringOrEnumerableProperty,
                attributeSyntax.GetLocation(),
                attributeData.AttributeClass!.Name);
            context.ReportDiagnostic(diagnostic);

            return false;
        }

        return true;
    }

    /// <inheritdoc/>
    protected override void BuildCodeFixParameters(ImmutableDictionary<string, string?>.Builder builder, AttributeData attributeData, ITypeSymbol memberTypeSymbol)
    {
        builder.Add("NewAttribute", Constants.LengthAttributeFullyQualifiedMetadataName);
    }
}
