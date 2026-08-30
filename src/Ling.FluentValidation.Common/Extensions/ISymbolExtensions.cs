using Microsoft.CodeAnalysis;

namespace Ling.FluentValidation.Extensions;

/// <summary>
/// Extension methods for the <see cref="ISymbol"/> type.
/// </summary>
internal static class ISymbolExtensions
{
    private static readonly SymbolDisplayFormat _fullyQualifiedMetadataFormat = new(
        globalNamespaceStyle: SymbolDisplayGlobalNamespaceStyle.Omitted,
        typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces
    );

    /// <summary>
    /// Gets the fully qualified name for a given symbol.
    /// <para>
    /// Format: <c>global::[namespace].[type]</c>
    /// </para>
    /// </summary>
    /// <param name="symbol">The input <see cref="ISymbol"/> instance.</param>
    /// <returns>The fully qualified name for <paramref name="symbol"/>.</returns>
    public static string GetFullyQualifiedName(this ISymbol symbol)
    {
        return symbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
    }

    /// <summary>
    /// Gets the fully qualified metadata name for a given symbol.
    /// <para>
    /// Format: <c>[namespace].[type]</c>
    /// </para>
    /// </summary>
    /// <param name="symbol">The input <see cref="ISymbol"/> instance.</param>
    /// <returns>The fully qualified metadata name for <paramref name="symbol"/>.</returns>
    public static string GetFullyQualifiedMetadataName(this ISymbol symbol)
    {
        return symbol.ToDisplayString(_fullyQualifiedMetadataFormat);
    }

    /// <summary>
    /// Calculates the effective accessibility for a given symbol.
    /// </summary>
    /// <param name="symbol">The <see cref="ISymbol"/> instance to check.</param>
    /// <returns>The effective accessibility for <paramref name="symbol"/>.</returns>
    public static Accessibility GetEffectiveAccessibility(this ISymbol symbol)
    {
        // Start by assuming it's visible
        Accessibility visibility = Accessibility.Public;

        // Handle special cases
        switch (symbol.Kind)
        {
            case SymbolKind.Alias: return Accessibility.Private;
            case SymbolKind.Parameter: return symbol.ContainingSymbol.GetEffectiveAccessibility();
            case SymbolKind.TypeParameter: return Accessibility.Private;
        }

        // Traverse the symbol hierarchy to determine the effective accessibility
        while (symbol is not null && symbol.Kind != SymbolKind.Namespace)
        {
            switch (symbol.DeclaredAccessibility)
            {
                case Accessibility.NotApplicable:
                case Accessibility.Private:
                    return Accessibility.Private;
                case Accessibility.Internal:
                case Accessibility.ProtectedAndInternal:
                    visibility = Accessibility.Internal;
                    break;
            }

            symbol = symbol.ContainingSymbol;
        }

        return visibility;
    }

    /// <summary>
    /// Checks if the symbol is an error type.
    /// </summary>
    /// <param name="symbol">The <see cref="ISymbol"/> instance to check.</param>
    /// <returns><see langword="true"/> if the symbol is an error type; otherwise, <see langword="false"/>.</returns>
    public static bool IsErrorType(this ISymbol symbol) => symbol switch
    {
        IErrorTypeSymbol => true,

        IFieldSymbol { Type.TypeKind: TypeKind.Error } => true,
        IPropertySymbol { Type.TypeKind: TypeKind.Error } => true,
        IMethodSymbol { ReturnType.TypeKind: TypeKind.Error } => true,
        IParameterSymbol { Type.TypeKind: TypeKind.Error } => true,
        IEventSymbol { Type.TypeKind: TypeKind.Error } => true,

        ITypeSymbol { TypeKind: TypeKind.Error } => true,

        _ when symbol.Kind is SymbolKind.ErrorType => true,
        _ => false
    };
}
