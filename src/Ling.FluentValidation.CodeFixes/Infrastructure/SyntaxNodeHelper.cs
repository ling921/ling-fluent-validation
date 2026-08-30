using Ling.FluentValidation.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Ling.FluentValidation.CodeFixes.Infrastructure;

/// <summary>
/// Contains helper methods for working with <see cref="SyntaxNode"/> instances.
/// </summary>
internal static class SyntaxNodeHelper
{
    /// <summary>
    /// Checks if a given <see cref="SyntaxNode"/> is using the specified namespace.
    /// </summary>
    /// <param name="syntaxNode">The <see cref="SyntaxNode"/> to check.</param>
    /// <param name="namespace">The namespace to check for.</param>
    /// <returns>True if the <see cref="SyntaxNode"/> is using the specified namespace, false otherwise.</returns>
    public static bool IsUsingNamespace(this SyntaxNode syntaxNode, string @namespace)
    {
        var root = (CompilationUnitSyntax)syntaxNode.SyntaxTree.GetRoot();

        return root.Usings.Any(u => u.Name?.ToString() == @namespace);
    }

    /// <summary>
    /// Creates a <see cref="NameSyntax"/> instance from a given name string.
    /// <para>
    /// If the name string contains a namespace that is already being used, the name will ignore namespace part.
    /// </para>
    /// <para>
    /// If the name string ends with '<c>Attribute</c>', the name will ignore the '<c>Attribute</c>' part.
    /// </para>
    /// </summary>
    /// <param name="syntaxNode">The <see cref="SyntaxNode"/> to use for creating the <see cref="NameSyntax"/> instance.</param>
    /// <param name="name">The name string to create the <see cref="NameSyntax"/> instance from.</param>
    /// <returns>The created <see cref="NameSyntax"/> instance.</returns>
    public static NameSyntax CreateNameSyntax(this SyntaxNode syntaxNode, string name)
    {
        var root = (CompilationUnitSyntax)syntaxNode.SyntaxTree.GetRoot();
        var usingNamespaces = root.Usings.Select(u => u.Name?.ToString()).ToList();

        var pos = name.Length - 1;
        while (pos >= 0)
        {
            if (name[pos] == '.' && usingNamespaces.Contains(name.Substring(0, pos)))
            {
                break;
            }
            pos--;
        }

        return ParseName(name.TrimEnd("Attribute")!, pos + 1, false);
    }

    /// <summary>
    /// Adds an argument to an attribute syntax.
    /// If the argument syntax is null, the original attribute syntax is returned.
    /// </summary>
    /// <param name="attributeSyntax">The attribute syntax to add the argument to.</param>
    /// <param name="argumentSyntax">The argument syntax to add.</param>
    /// <returns>The attribute syntax with the added argument.</returns>
    public static AttributeSyntax AddArgument(this AttributeSyntax attributeSyntax, AttributeArgumentSyntax? argumentSyntax)
    {
        if (argumentSyntax is not null)
        {
            var argumentList = attributeSyntax.ArgumentList is null
                ? AttributeArgumentList(SeparatedList(new[] { argumentSyntax }))
                : attributeSyntax.ArgumentList.AddArguments(argumentSyntax);

            return attributeSyntax.WithArgumentList(argumentList);
        }

        return attributeSyntax;
    }
}
