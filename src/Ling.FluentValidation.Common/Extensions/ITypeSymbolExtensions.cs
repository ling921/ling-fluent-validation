using Microsoft.CodeAnalysis;

namespace Ling.FluentValidation.Extensions;

/// <summary>
/// Extension methods for the <see cref="ITypeSymbol"/> type.
/// </summary>
internal static class ITypeSymbolExtensions
{
    /// <summary>
    /// Checks if the type is string type.
    /// </summary>
    /// <param name="typeSymbol">The <see cref="ITypeSymbol"/> instance to check.</param>
    /// <returns><see langword="true"/> if the type is string type; otherwise, <see langword="false"/>.</returns>
    public static bool IsStringType(this ITypeSymbol typeSymbol) => typeSymbol.SpecialType is SpecialType.System_String;

    /// <summary>
    /// Checks if the type is enumerable type.
    /// </summary>
    /// <param name="typeSymbol">The <see cref="ITypeSymbol"/> instance to check.</param>
    /// <returns><see langword="true"/> if the type is enumerable type; otherwise, <see langword="false"/>.</returns>
    public static bool IsEnumerableType(this ITypeSymbol typeSymbol)
    {
        if (typeSymbol is IArrayTypeSymbol)
        {
            return true;
        }

        foreach (var interfaceTypeSymbol in typeSymbol.AllInterfaces)
        {
            switch (interfaceTypeSymbol.SpecialType)
            {
                case SpecialType.System_Array:
                case SpecialType.System_Collections_Generic_IList_T:
                case SpecialType.System_Collections_Generic_ICollection_T:
                case SpecialType.System_Collections_Generic_IReadOnlyList_T:
                case SpecialType.System_Collections_Generic_IReadOnlyCollection_T:
                case SpecialType.System_Collections_IEnumerable:
                case SpecialType.System_Collections_Generic_IEnumerable_T:
                    return true;

                default:
                    break;
            }
        }

        return false;
    }

    /// <summary>
    /// Gets the nullable underlying type.
    /// </summary>
    /// <param name="typeSymbol">The <see cref="ITypeSymbol"/> instance to get.</param>
    /// <returns>The nullable underlying type, if it exists; otherwise, <see langword="null"/>.</returns>
    public static ITypeSymbol? GetNullableUnderlyingType(this ITypeSymbol typeSymbol)
    {
        if (typeSymbol is INamedTypeSymbol namedTypeSymbol &&
            namedTypeSymbol.IsValueType &&
            namedTypeSymbol.OriginalDefinition.SpecialType == SpecialType.System_Nullable_T)
        {
            return namedTypeSymbol.TypeArguments[0];
        }
        return null;
    }
}
