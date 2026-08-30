using Ling.FluentValidation.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;

namespace Ling.FluentValidation.Analyzers.Infrastructure;

/// <summary>
/// The base class for attribute diagnostic analyzers.
/// </summary>
public abstract class AttributeDiagnosticAnalyzer : DiagnosticAnalyzer
{
    /// <summary>
    /// Gets the full qualified metadata names of the attributes that should be used on the property or field.
    /// <para>
    /// For example, <c>"System.ComponentModel.DataAnnotations.RequiredAttribute"</c>.
    /// </para>
    /// </summary>
    public abstract ImmutableArray<string> TargetAttributeFullyQualifiedMetadataNames { get; }

    /// <summary>
    /// Gets the analysis mode of the analyzer.
    /// </summary>
    public virtual GeneratedCodeAnalysisFlags AnalysisMode => GeneratedCodeAnalysisFlags.None;

    /// <summary>
    /// Gets the attribute targets.
    /// </summary>
    public abstract AttributeTargets AttributeTargets { get; }

    /// <inheritdoc/>
    public sealed override void Initialize(AnalysisContext context)
    {
        if (TargetAttributeFullyQualifiedMetadataNames.Length == 0)
        {
            return;
        }

        context.ConfigureGeneratedCodeAnalysis(AnalysisMode);
        context.EnableConcurrentExecution();

        var kinds = GetSyntaxKindsFromAttributeTargets(AttributeTargets);
        if (kinds.Length > 0)
        {
            context.RegisterSyntaxNodeAction(AnalyzeNode, kinds);
        }
    }

    /// <summary>
    /// Analyzes attribute with assembly identifier.
    /// </summary>
    /// <param name="context">The syntax node analysis context.</param>
    /// <param name="syntaxNode">The attribute syntax node.</param>
    /// <param name="attributeInfo">The matched attribute information.</param>
    protected virtual void AnalyzeAssemblyAttribute(SyntaxNodeAnalysisContext context, AttributeSyntax syntaxNode, AttributeInfo attributeInfo) { }

    /// <summary>
    /// Analyzes attribute with module identifier.
    /// </summary>
    /// <param name="context">The syntax node analysis context.</param>
    /// <param name="syntaxNode">The attribute syntax node.</param>
    /// <param name="attributeInfo">The matched attribute information.</param>
    protected virtual void AnalyzeModuleAttribute(SyntaxNodeAnalysisContext context, AttributeSyntax syntaxNode, AttributeInfo attributeInfo) { }

    /// <summary>
    /// Analyzes the class declaration node.
    /// </summary>
    /// <param name="context">The syntax node analysis context.</param>
    /// <param name="syntaxNode">The class declaration syntax node.</param>
    /// <param name="classTypeSymbol">The class type symbol.</param>
    /// <param name="attributes">The matched attribute information.</param>
    protected virtual void AnalyzeClassAttribute(SyntaxNodeAnalysisContext context, ClassDeclarationSyntax syntaxNode, INamedTypeSymbol classTypeSymbol, ImmutableArray<AttributeInfo> attributes) { }

    /// <summary>
    /// Analyzes the struct declaration node.
    /// </summary>
    /// <param name="context">The syntax node analysis context.</param>
    /// <param name="syntaxNode">The struct declaration syntax node.</param>
    /// <param name="structTypeSymbol">The struct type symbol.</param>
    /// <param name="attributes">The matched attribute information.</param>
    protected virtual void AnalyzeStructAttribute(SyntaxNodeAnalysisContext context, StructDeclarationSyntax syntaxNode, INamedTypeSymbol structTypeSymbol, ImmutableArray<AttributeInfo> attributes) { }

    /// <summary>
    /// Analyzes the enum declaration node.
    /// </summary>
    /// <param name="context">The syntax node analysis context.</param>
    /// <param name="syntaxNode">The enum declaration syntax node.</param>
    /// <param name="enumTypeSymbol">The enum type symbol.</param>
    /// <param name="attributes">The matched attribute information.</param>
    protected virtual void AnalyzeEnumAttribute(SyntaxNodeAnalysisContext context, EnumDeclarationSyntax syntaxNode, INamedTypeSymbol enumTypeSymbol, ImmutableArray<AttributeInfo> attributes) { }

    /// <summary>
    /// Analyzes the constructor declaration node.
    /// </summary>
    /// <param name="context">The syntax node analysis context.</param>
    /// <param name="syntaxNode">The constructor declaration syntax node.</param>
    /// <param name="constructorMethodSymbol">The constructor method symbol.</param>
    /// <param name="attributes">The matched attribute information.</param>
    protected virtual void AnalyzeConstructorAttribute(SyntaxNodeAnalysisContext context, ConstructorDeclarationSyntax syntaxNode, IMethodSymbol constructorMethodSymbol, ImmutableArray<AttributeInfo> attributes) { }

    /// <summary>
    /// Analyzes the method declaration node.
    /// </summary>
    /// <param name="context">The syntax node analysis context.</param>
    /// <param name="syntaxNode">The method declaration syntax node.</param>
    /// <param name="methodSymbol">The method symbol.</param>
    /// <param name="attributes">The matched attribute information.</param>
    protected virtual void AnalyzeMethodAttribute(SyntaxNodeAnalysisContext context, MethodDeclarationSyntax syntaxNode, IMethodSymbol methodSymbol, ImmutableArray<AttributeInfo> attributes) { }

    /// <summary>
    /// Analyzes the property declaration node.
    /// </summary>
    /// <param name="context">The syntax node analysis context.</param>
    /// <param name="propertyDeclarationSyntax">The property declaration syntax node.</param>
    /// <param name="propertySymbol">The property symbol.</param>
    /// <param name="attributes">The matched attribute information.</param>
    protected virtual void AnalyzePropertyAttribute(SyntaxNodeAnalysisContext context, PropertyDeclarationSyntax propertyDeclarationSyntax, IPropertySymbol propertySymbol, ImmutableArray<AttributeInfo> attributes) { }

    /// <summary>
    /// Analyzes the field declaration node.
    /// </summary>
    /// <param name="context">The syntax node analysis context.</param>
    /// <param name="fieldDeclarationSyntax">The field declaration syntax node.</param>
    /// <param name="fieldSymbol">The field symbol.</param>
    /// <param name="attributes">The matched attribute information.</param>
    protected virtual void AnalyzeFieldAttribute(SyntaxNodeAnalysisContext context, FieldDeclarationSyntax fieldDeclarationSyntax, IFieldSymbol fieldSymbol, ImmutableArray<AttributeInfo> attributes) { }

    /// <summary>
    /// Analyzes the event declaration node.
    /// </summary>
    /// <param name="context">The syntax node analysis context.</param>
    /// <param name="eventDeclarationSyntax">The event declaration syntax node.</param>
    /// <param name="eventSymbol">The event symbol.</param>
    /// <param name="attributes">The matched attribute information.</param>
    protected virtual void AnalyzeEventAttribute(SyntaxNodeAnalysisContext context, EventDeclarationSyntax eventDeclarationSyntax, IEventSymbol eventSymbol, ImmutableArray<AttributeInfo> attributes) { }

    /// <summary>
    /// Analyzes the interface declaration node.
    /// </summary>
    /// <param name="context">The syntax node analysis context.</param>
    /// <param name="interfaceDeclarationSyntax">The interface declaration syntax node.</param>
    /// <param name="interfaceSymbol">The interface symbol.</param>
    /// <param name="attributes">The matched attribute information.</param>
    protected virtual void AnalyzeInterfaceAttribute(SyntaxNodeAnalysisContext context, InterfaceDeclarationSyntax interfaceDeclarationSyntax, INamedTypeSymbol interfaceSymbol, ImmutableArray<AttributeInfo> attributes) { }

    /// <summary>
    /// Analyze attribute syntax nodes.
    /// </summary>
    /// <param name="context">The syntax node analysis context.</param>
    private void AnalyzeNode(SyntaxNodeAnalysisContext context)
    {
        context.CancellationToken.ThrowIfCancellationRequested();

        switch (context.Node)
        {
            case CompilationUnitSyntax compilationUnitSyntax:
                foreach (var attributeList in compilationUnitSyntax.AttributeLists)
                {
                    if (attributeList.Target?.Identifier.IsKind(SyntaxKind.AssemblyKeyword) == true)
                    {
                        foreach (var attribute in attributeList.Attributes)
                        {
                            var attributeSymbol = context.SemanticModel.GetSymbolInfo(attribute).Symbol as IMethodSymbol;
                            if (attributeSymbol != null)
                            {
                                var attributeData = attributeSymbol.ContainingType;
                            }
                        }
                    }
                }
                break;

            case ClassDeclarationSyntax classDeclarationSyntax:
                var classTypeSymbol = context.SemanticModel.GetDeclaredSymbol(classDeclarationSyntax);
                if (classTypeSymbol?.IsErrorType() == false)
                {
                    var attributes = FindMatchedAttributes(classTypeSymbol.GetAttributes());
                    if (!attributes.IsEmpty)
                    {
                        AnalyzeClassAttribute(context, classDeclarationSyntax, classTypeSymbol, attributes);
                    }
                }
                break;

            case StructDeclarationSyntax structDeclarationSyntax:
                var structTypeSymbol = context.SemanticModel.GetDeclaredSymbol(structDeclarationSyntax);
                if (structTypeSymbol?.IsErrorType() == false)
                {
                    var attributes = FindMatchedAttributes(structTypeSymbol.GetAttributes());
                    if (!attributes.IsEmpty)
                    {
                        AnalyzeStructAttribute(context, structDeclarationSyntax, structTypeSymbol, attributes);
                    }
                }
                break;

            case EnumDeclarationSyntax enumDeclarationSyntax:
                var enumTypeSymbol = context.SemanticModel.GetDeclaredSymbol(enumDeclarationSyntax);
                if (enumTypeSymbol?.IsErrorType() == false)
                {
                    var attributes = FindMatchedAttributes(enumTypeSymbol.GetAttributes());
                    if (!attributes.IsEmpty)
                    {
                        AnalyzeEnumAttribute(context, enumDeclarationSyntax, enumTypeSymbol, attributes);
                    }
                }
                break;

            case ConstructorDeclarationSyntax constructorDeclarationSyntax:
                var constructorMethodSymbol = context.SemanticModel.GetDeclaredSymbol(constructorDeclarationSyntax);
                if (constructorMethodSymbol is not null)
                {
                    var attributes = FindMatchedAttributes(constructorMethodSymbol.GetAttributes());
                    if (!attributes.IsEmpty)
                    {
                        AnalyzeConstructorAttribute(context, constructorDeclarationSyntax, constructorMethodSymbol, attributes);
                    }
                }
                break;

            case MethodDeclarationSyntax methodDeclarationSyntax:
                var methodSymbol = context.SemanticModel.GetDeclaredSymbol(methodDeclarationSyntax);
                if (methodSymbol is not null)
                {
                    var attributes = FindMatchedAttributes(methodSymbol.GetAttributes());
                    if (!attributes.IsEmpty)
                    {
                        AnalyzeMethodAttribute(context, methodDeclarationSyntax, methodSymbol, attributes);
                    }
                }
                break;

            case PropertyDeclarationSyntax propertyDeclarationSyntax:
                var propertySymbol = context.SemanticModel.GetDeclaredSymbol(propertyDeclarationSyntax);
                if (propertySymbol?.IsErrorType() == false)
                {
                    var attributes = FindMatchedAttributes(propertySymbol.GetAttributes());
                    if (!attributes.IsEmpty)
                    {
                        AnalyzePropertyAttribute(context, propertyDeclarationSyntax, propertySymbol, attributes);
                    }
                }
                break;

            case FieldDeclarationSyntax fieldDeclarationSyntax:
                if (fieldDeclarationSyntax is { Declaration.Variables.Count: 1 } &&
                    context.SemanticModel.GetDeclaredSymbol(fieldDeclarationSyntax.Declaration.Variables[0]) is IFieldSymbol fieldSymbol &&
                    !fieldSymbol.IsErrorType())
                {
                    var attributes = FindMatchedAttributes(fieldSymbol.GetAttributes());
                    if (!attributes.IsEmpty)
                    {
                        AnalyzeFieldAttribute(context, fieldDeclarationSyntax, fieldSymbol, attributes);
                    }
                }
                break;

            case EventDeclarationSyntax eventDeclarationSyntax:
                var eventSymbol = context.SemanticModel.GetDeclaredSymbol(eventDeclarationSyntax);
                if (eventSymbol is not null)
                {
                    var attributes = FindMatchedAttributes(eventSymbol.GetAttributes());
                    if (!attributes.IsEmpty)
                    {
                        AnalyzeEventAttribute(context, eventDeclarationSyntax, eventSymbol, attributes);
                    }
                }
                break;

            case InterfaceDeclarationSyntax interfaceDeclarationSyntax:
                var interfaceSymbol = context.SemanticModel.GetDeclaredSymbol(interfaceDeclarationSyntax);
                if (interfaceSymbol is not null)
                {
                    var attributes = FindMatchedAttributes(interfaceSymbol.GetAttributes());
                    if (!attributes.IsEmpty)
                    {
                        AnalyzeInterfaceAttribute(context, interfaceDeclarationSyntax, interfaceSymbol, attributes);
                    }
                }
                break;

            case ParameterSyntax _:
            case DelegateDeclarationSyntax _:
            case TypeParameterSyntax _:
                // Not supported currently.
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// Finds all attributes that match the 'TargetAttributeFullyQualifiedMetadataNames'.
    /// </summary>
    /// <param name="array">The array of attribute data.</param>
    /// <returns>Returns the matched attribute information.</returns>
    private ImmutableArray<AttributeInfo> FindMatchedAttributes(ImmutableArray<AttributeData> array)
    {
        if (array.IsDefaultOrEmpty)
        {
            return [];
        }

        var builder = ImmutableArray.CreateBuilder<AttributeInfo>();
        foreach (var attribute in array)
        {
            var fullQualifiedMetadataName = attribute.AttributeClass?.GetFullyQualifiedMetadataName();
            if (fullQualifiedMetadataName is null)
            {
                continue;
            }

            if (TargetAttributeFullyQualifiedMetadataNames.Contains(fullQualifiedMetadataName))
            {
                builder.Add(new AttributeInfo(fullQualifiedMetadataName, attribute));
            }
        }
        return builder.ToImmutable();
    }

    /// <summary>
    /// Gets the syntax kinds from attribute targets.
    /// </summary>
    /// <param name="targets">The attribute targets.</param>
    /// <returns>Syntax kinds from attribute targets.</returns>
    private static SyntaxKind[] GetSyntaxKindsFromAttributeTargets(AttributeTargets targets)
    {
        var kinds = new HashSet<SyntaxKind>();

        if (targets.HasFlag(AttributeTargets.Assembly) || targets.HasFlag(AttributeTargets.Module))
        {
            kinds.Add(SyntaxKind.CompilationUnit);
        }
        if (targets.HasFlag(AttributeTargets.Class))
        {
            kinds.Add(SyntaxKind.ClassDeclaration);
        }
        if (targets.HasFlag(AttributeTargets.Struct))
        {
            kinds.Add(SyntaxKind.StructDeclaration);
        }
        if (targets.HasFlag(AttributeTargets.Enum))
        {
            kinds.Add(SyntaxKind.EnumDeclaration);
        }
        if (targets.HasFlag(AttributeTargets.Constructor))
        {
            kinds.Add(SyntaxKind.ConstructorDeclaration);
        }
        if (targets.HasFlag(AttributeTargets.Method))
        {
            kinds.Add(SyntaxKind.MethodDeclaration);
        }
        if (targets.HasFlag(AttributeTargets.Property))
        {
            kinds.Add(SyntaxKind.PropertyDeclaration);
        }
        if (targets.HasFlag(AttributeTargets.Field))
        {
            kinds.Add(SyntaxKind.FieldDeclaration);
        }
        if (targets.HasFlag(AttributeTargets.Event))
        {
            kinds.Add(SyntaxKind.EventDeclaration);
        }
        if (targets.HasFlag(AttributeTargets.Interface))
        {
            kinds.Add(SyntaxKind.InterfaceDeclaration);
        }
        if (targets.HasFlag(AttributeTargets.Parameter))
        {
            kinds.Add(SyntaxKind.Parameter);
        }
        if (targets.HasFlag(AttributeTargets.Delegate))
        {
            kinds.Add(SyntaxKind.DelegateDeclaration);
        }
        if (targets.HasFlag(AttributeTargets.ReturnValue))
        {
        }
        if (targets.HasFlag(AttributeTargets.GenericParameter))
        {
            kinds.Add(SyntaxKind.TypeParameter);
        }

        return [.. kinds];
    }

    /// <summary>
    /// The attribute information.
    /// </summary>
    /// <param name="FullyQualifiedMetadataName">The fully qualified metadata name of the attribute.</param>
    /// <param name="AttributeData">The attribute data.</param>
    protected record AttributeInfo(string FullyQualifiedMetadataName, AttributeData AttributeData);
}
