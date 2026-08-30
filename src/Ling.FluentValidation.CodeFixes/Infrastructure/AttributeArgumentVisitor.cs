using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using System.Collections.Immutable;

namespace Ling.FluentValidation.CodeFixes.Infrastructure;

/// <summary>
/// Represents a visitor for attribute arguments.
/// </summary>
internal sealed class AttributeArgumentVisitor
{
    /// <summary>
    /// Represents the constructor arguments.
    /// </summary>
    private readonly ImmutableArray<AttributeArgumentSyntax> _constructorArguments;

    /// <summary>
    /// Represents the named arguments.
    /// </summary>
    private readonly ImmutableDictionary<string, AttributeArgumentSyntax> _namedArgumentDictionary;

    /// <summary>
    /// Gets the count of constructor arguments.
    /// </summary>
    public int ConstructorArgumentCount => _constructorArguments.Length;

    /// <summary>
    /// Gets the count of named arguments.
    /// </summary>
    public int NamedArgumentCount => _namedArgumentDictionary.Count;

    /// <summary>
    /// Gets the constructor argument at the specified index.
    /// </summary>
    /// <param name="index">The index of the argument.</param>
    /// <returns>The argument at the specified index.</returns>
    public AttributeArgumentSyntax this[int index] => _constructorArguments[index];

    /// <summary>
    /// Gets the named argument with the specified name.
    /// </summary>
    /// <param name="name">The name of the argument.</param>
    /// <returns>The argument with the specified name, or null if it doesn't exist.</returns>
    public AttributeArgumentSyntax? this[string name] => _namedArgumentDictionary.GetValueOrDefault(name);

    /// <summary>
    /// Initializes a new instance of the <see cref="AttributeArgumentVisitor"/> class.
    /// </summary>
    /// <param name="syntax">The attribute argument list syntax.</param>
    public AttributeArgumentVisitor(AttributeArgumentListSyntax? syntax)
    {
        if (syntax is { Arguments.Count: > 0 })
        {
            var builder1 = ImmutableArray.CreateBuilder<AttributeArgumentSyntax>(syntax.Arguments.Count);
            var builder2 = ImmutableDictionary.CreateBuilder<string, AttributeArgumentSyntax>();

            foreach (var argumentSyntax in syntax.Arguments)
            {
                if (argumentSyntax.NameEquals is null)
                {
                    builder1.Add(argumentSyntax);
                }
                else
                {
                    builder2.Add(argumentSyntax.NameEquals.Name.Identifier.ValueText, argumentSyntax);
                }
            }

            _constructorArguments = builder1.ToImmutable();
            _namedArgumentDictionary = builder2.ToImmutableDictionary();
        }
        else
        {
            _constructorArguments = [];
            _namedArgumentDictionary = ImmutableDictionary<string, AttributeArgumentSyntax>.Empty;
        }
    }

    /// <summary>
    /// Converts the constructor arguments to an attribute argument list syntax.
    /// </summary>
    /// <returns>The attribute argument list syntax, or null if there are no constructor arguments.</returns>
    public AttributeArgumentListSyntax? ToConstructorArgumentList()
    {
        return _constructorArguments.Length > 0
            ? SyntaxFactory.AttributeArgumentList(SyntaxFactory.SeparatedList(_constructorArguments))
            : null;
    }

    /// <summary>
    /// Converts the specified named arguments to an attribute non-named argument list syntax.
    /// </summary>
    /// <param name="names">The names of the arguments to convert.</param>
    /// <returns>The attribute argument list syntax, or null if there are no named arguments.</returns>
    public AttributeArgumentListSyntax? ToConstructorArgumentListWithNamedArguments(params string[] names)
    {
        if (names?.Length > 0)
        {
            var list = new List<AttributeArgumentSyntax>();
            foreach (var name in names)
            {
                var argumentSyntax = _namedArgumentDictionary.GetValueOrDefault(name);
                if (argumentSyntax is not null)
                {
                    list.Add(argumentSyntax.WithNameEquals(null));
                }
            }
            if (list.Count > 0)
            {
                return SyntaxFactory.AttributeArgumentList(SyntaxFactory.SeparatedList(list));
            }
        }

        return null;
    }
}
