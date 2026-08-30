using Ling.FluentValidation.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;

namespace Ling.FluentValidation.Analyzers.Infrastructure;

/// <summary>
/// The base class for all validation property and field diagnostic analyzers.
/// </summary>
public abstract class MemberAttributeDiagnosticAnalyzer : DiagnosticAnalyzer
{
    /// <summary>
    /// The full qualified metadata names of the attributes that should be used on the property or field.
    /// <para>
    /// For example, <c>"System.ComponentModel.DataAnnotations.RequiredAttribute"</c>.
    /// </para>
    /// </summary>
    public abstract ImmutableArray<string> TargetAttributeFullyQualifiedMetadataNames { get; }

    /// <summary>
    /// The analysis mode of the analyzer.
    /// </summary>
    public virtual GeneratedCodeAnalysisFlags AnalysisMode => GeneratedCodeAnalysisFlags.None;

    /// <inheritdoc/>
    public sealed override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(AnalysisMode);
        context.EnableConcurrentExecution();
        context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKind.PropertyDeclaration, SyntaxKind.FieldDeclaration);
    }

    /// <summary>
    /// The method that will be called before analyzing the node.
    /// <para>
    /// This method is used to get named type symbols that are used in the <see cref="AnalyzeNode(SyntaxNodeAnalysisContext, ISymbol, AttributeData)"/> method.
    /// </para>
    /// </summary>
    /// <param name="context">The syntax node analysis context.</param>
    /// <param name="propertyOrFieldSymbol">The property or field symbol of the node.</param>
    /// <param name="propertyOrFieldTypeSymbol">The property or field type symbol of the node.</param>
    /// <returns>The named type symbols.</returns>
    protected virtual ImmutableArray<INamedTypeSymbol> GetNamedTypeSymbolsForAnalysis(
        SyntaxNodeAnalysisContext context,
        ISymbol propertyOrFieldSymbol,
        ITypeSymbol propertyOrFieldTypeSymbol) => [];

    /// <summary>
    /// The method that will be called when attribute is one of the <see cref="MatchAttributeFullQualifiedMetadataNames"/>.
    /// <para>
    /// All the diagnostics should be reported by this method.
    /// </para>
    /// </summary>
    /// <param name="context">The syntax node analysis context.</param>
    /// <param name="propertyOrFieldSymbol">Property or field symbol.</param>
    /// <param name="propertyOrFieldTypeSymbol">Property or field type symbol.</param>
    /// <param name="attributeData">The attribute data that is used to analyze the node.</param>
    /// <param name="namedTypeSymbols">The named type symbols from <see cref="GetNamedTypeSymbolsForAnalysis(SyntaxNodeAnalysisContext)"/>.</param>
    protected abstract void AnalyzeNode(
        SyntaxNodeAnalysisContext context,
        ISymbol propertyOrFieldSymbol,
        ITypeSymbol propertyOrFieldTypeSymbol,
        AttributeData attributeData,
        ImmutableArray<INamedTypeSymbol> namedTypeSymbols);

    /// <summary>
    /// The method is used to analyze the node and attributes.
    /// </summary>
    /// <param name="context">The syntax node analysis context.</param>
    private void AnalyzeNode(SyntaxNodeAnalysisContext context)
    {
        ISymbol propertyOrFieldSymbol;
        ITypeSymbol propertyOrFieldTypeSymbol;
        ImmutableArray<AttributeData> attributes;
        if (context.Node is PropertyDeclarationSyntax propertyDeclaration)
        {
            // Ignore error types
            if (context.SemanticModel.GetDeclaredSymbol(propertyDeclaration) is not IPropertySymbol propertySymbol ||
                propertySymbol is { Kind: SymbolKind.ErrorType } ||
                propertySymbol is { Type.TypeKind: TypeKind.Error })
            {
                return;
            }

            propertyOrFieldSymbol = propertySymbol;
            propertyOrFieldTypeSymbol = propertySymbol.Type;
            attributes = propertySymbol.GetAttributes();
        }
        else if (context.Node is FieldDeclarationSyntax { Declaration.Variables.Count: 1 } fieldDeclaration)
        {
            // Ignore error types
            if (context.SemanticModel.GetDeclaredSymbol(fieldDeclaration.Declaration.Variables[0]) is not IFieldSymbol fieldSymbol ||
                fieldSymbol is { Kind: SymbolKind.ErrorType } ||
                fieldSymbol is { Type.TypeKind: TypeKind.Error })
            {
                return;
            }

            propertyOrFieldSymbol = fieldSymbol;
            propertyOrFieldTypeSymbol = fieldSymbol.Type;
            attributes = fieldSymbol.GetAttributes();
        }
        else
        {
            return;
        }

        var namedTypeSymbols = GetNamedTypeSymbolsForAnalysis(context, propertyOrFieldSymbol, propertyOrFieldTypeSymbol);

        foreach (var attribute in attributes)
        {
            var fullQualifiedMetadataName = attribute.AttributeClass?.GetFullyQualifiedMetadataName();
            if (fullQualifiedMetadataName is not null &&
                TargetAttributeFullyQualifiedMetadataNames.Contains(fullQualifiedMetadataName))
            {
                AnalyzeNode(context, propertyOrFieldSymbol, propertyOrFieldTypeSymbol, attribute, namedTypeSymbols);
            }
        }
    }
}
