using Ling.FluentValidation.Analyzers.Diagnostics;
using Ling.FluentValidation.Analyzers.Infrastructure;
using Ling.FluentValidation.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;

namespace Ling.FluentValidation.Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class RangeAttributeAnalyzer : AttributeDiagnosticAnalyzer
{
    /// <inheritdoc/>
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => [
            DiagnosticDescriptors.AttributeShouldUsedOnPropertyImplimentIComparableError,
            DiagnosticDescriptors.ValidationAttributeParameterShouldAssignableToPropertyError
        ];

    /// <inheritdoc/>
    public override ImmutableArray<string> TargetAttributeFullyQualifiedMetadataNames => [
            Constants.ExclusiveBetweenAttributeFullyQualifiedMetadataName,
            Constants.InclusiveBetweenAttributeFullyQualifiedMetadataName,
            Constants.GreaterThanAttributeFullyQualifiedMetadataName,
            Constants.GreaterThanOrEqualToAttributeFullyQualifiedMetadataName,
            Constants.LessThanAttributeFullyQualifiedMetadataName,
            Constants.LessThanOrEqualToAttributeFullyQualifiedMetadataName,
            Constants.SystemRangeAttributeFullyQualifiedMetadataName
        ];

    /// <inheritdoc/>
    public override AttributeTargets AttributeTargets => AttributeTargets.Property | AttributeTargets.Field;

    /// <inheritdoc/>
    protected override void AnalyzePropertyAttribute(SyntaxNodeAnalysisContext context, PropertyDeclarationSyntax propertyDeclarationSyntax, IPropertySymbol propertySymbol, ImmutableArray<AttributeInfo> attributes)
    {
        AnalyzeAttributeCore(context, propertyDeclarationSyntax, propertySymbol, propertySymbol.Type, attributes);
    }

    /// <inheritdoc/>
    protected override void AnalyzeFieldAttribute(SyntaxNodeAnalysisContext context, FieldDeclarationSyntax fieldDeclarationSyntax, IFieldSymbol fieldSymbol, ImmutableArray<AttributeInfo> attributes)
    {
        AnalyzeAttributeCore(context, fieldDeclarationSyntax, fieldSymbol, fieldSymbol.Type, attributes);
    }

    private void AnalyzeAttributeCore(
        SyntaxNodeAnalysisContext context,
        MemberDeclarationSyntax syntax,
        ISymbol symbol,
        ITypeSymbol propertyOrFieldTypeSymbol,
        ImmutableArray<AttributeInfo> attributes)
    {
        var typeSymbol = propertyOrFieldTypeSymbol.GetNullableUnderlyingType() ?? propertyOrFieldTypeSymbol;
        var namedTypeSymbols = GetNamedTypeSymbolsForAnalysis(context, symbol, typeSymbol);
        var comparableInterface = namedTypeSymbols[0];
        var comparableTInterface = namedTypeSymbols[1];

        foreach (var (_, attributeData) in attributes)
        {
            var attributeSyntax = (AttributeSyntax)attributeData.ApplicationSyntaxReference!.GetSyntax()!;
            if (!(typeSymbol.AllInterfaces.Any(i => SymbolEqualityComparer.Default.Equals(i, comparableInterface)) && typeSymbol.AllInterfaces.Any(i => SymbolEqualityComparer.Default.Equals(i, comparableTInterface))))
            {
                var diagnostic = Diagnostic.Create(
                    DiagnosticDescriptors.AttributeShouldUsedOnPropertyImplimentIComparableError,
                    attributeSyntax.GetLocation(),
                    attributeData.AttributeClass!.Name,
                    typeSymbol.Name);
                context.ReportDiagnostic(diagnostic);
            }

            for (var i = 0; i < attributeData.ConstructorArguments.Length; i++)
            {
                var arg = attributeData.ConstructorArguments[i];
                if (arg.IsNull) continue;

                var argTypeSymbol = arg.Type?.GetNullableUnderlyingType() ?? arg.Type;
                if (!SymbolEqualityComparer.Default.Equals(argTypeSymbol, typeSymbol) &&
                    !ImplicativeTypes.Special.Any(x => argTypeSymbol?.SpecialType == x.Key && x.Value.Contains(typeSymbol.SpecialType)))
                {
                    var info = Diagnostic.Create(
                        DiagnosticDescriptors.ValidationAttributeParameterShouldAssignableToPropertyError,
                        attributeSyntax.ArgumentList!.Arguments[i].GetLocation(),
                        attributeData.AttributeClass!.Name,
                        arg.Value,
                        typeSymbol.Name);
                    context.ReportDiagnostic(info);
                }
            }
        }
    }

    private static ImmutableArray<INamedTypeSymbol> GetNamedTypeSymbolsForAnalysis(
        SyntaxNodeAnalysisContext context,
        ISymbol propertyOrFieldSymbol,
        ITypeSymbol propertyOrFieldTypeSymbol)
    {
        var comparableInterface = context.Compilation.GetTypeByMetadataName("System.IComparable")!;
        var underlyingType = propertyOrFieldTypeSymbol.GetNullableUnderlyingType() ?? propertyOrFieldTypeSymbol;
        var comparableTInterface = context.Compilation.GetTypeByMetadataName("System.IComparable`1")!.Construct(underlyingType);

        return [comparableTInterface, comparableInterface];
    }
}
