using Ling.FluentValidation.Extensions;
using Microsoft.CodeAnalysis;
using System.Collections;
using System.Collections.Immutable;
using static Microsoft.CodeAnalysis.SymbolDisplayTypeQualificationStyle;

namespace Ling.FluentValidation.Generators.Models;

/// <summary>
/// A model describing the hierarchy info for a specific type.
/// </summary>
/// <param name="FilenameHint">The filename hint for the current type.</param>
/// <param name="MetadataName">The metadata name for the current type.</param>
/// <param name="Namespace">Gets the namespace for the current type.</param>
/// <param name="Hierarchy">Gets the sequence of type definitions containing the current type.</param>
internal sealed partial record HierarchyInfo(
    string FilenameHint,
    string MetadataName,
    string Namespace,
    EquatableArray<TypeInfo> Hierarchy)
{
    /// <summary>
    /// Creates a new <see cref="HierarchyInfo"/> instance from a given <see cref="INamedTypeSymbol"/>.
    /// </summary>
    /// <param name="typeSymbol">The input <see cref="INamedTypeSymbol"/> instance to gather info for.</param>
    /// <returns>A <see cref="HierarchyInfo"/> instance describing <paramref name="typeSymbol"/>.</returns>
    public static HierarchyInfo From(INamedTypeSymbol typeSymbol)
    {
        var builder = ImmutableArray.CreateBuilder<TypeInfo>();

        for (INamedTypeSymbol? parent = typeSymbol;
             parent is not null;
             parent = parent.ContainingType)
        {
            builder.Add(new TypeInfo(parent));
        }

        var hierarchy = builder.ToImmutable();
        return new(
            typeSymbol.GetFullyQualifiedMetadataName(),
            string.Join("_", hierarchy.Reverse().Select(static item => item.QualifiedName)),
            typeSymbol.ContainingNamespace.ToDisplayString(new(typeQualificationStyle: NameAndContainingTypesAndNamespaces)),
            hierarchy);
    }
}
