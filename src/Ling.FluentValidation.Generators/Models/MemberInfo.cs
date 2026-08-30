using Microsoft.CodeAnalysis;
using System.Collections.Immutable;

namespace Ling.FluentValidation.Generators.Models;

/// <summary>
/// A model representing a member and its associated attributes.
/// </summary>
/// <param name="MemberName">The member name.</param>
/// <param name="Attributes">The attributes for the member.</param>
internal sealed record MemberInfo(
    string MemberName,
    ITypeSymbol MemberType,
    ImmutableArray<AttributeData> Attributes)
{
    public MemberInfo(ISymbol symbol)
        : this(
            symbol.Name,
            GetType(symbol) ?? throw new InvalidOperationException($"Unable to get type for {symbol.Name}."),
            symbol.GetAttributes())
    {
    }

    private static ITypeSymbol? GetType(ISymbol symbol) => symbol switch
    {
        IFieldSymbol field => field.Type,
        IPropertySymbol property => property.Type,
        IMethodSymbol method => method.ReturnType,
        _ => null
    };
}

internal enum MemberKind
{
    None,
    Field,
    Property,
    Method
}
