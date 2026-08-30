using Microsoft.CodeAnalysis;

namespace Ling.FluentValidation.Extensions;

/// <summary>
/// Extension methods for the <see cref="AttributeData"/> type.
/// </summary>
internal static class AttributeDataExtensions
{
    /// <summary>
    /// Gets the constructor argument.
    /// </summary>
    /// <param name="attributeData">The <see cref="AttributeData"/> instance.</param>
    /// <param name="index">The argument index.</param>
    /// <returns>The constructor argument if exists; otherwise, <see langword="default"/>.</returns>
    public static TypedConstant GetConstructorArgument(this AttributeData attributeData, int index)
    {
        return index >= 0 && attributeData.ConstructorArguments.Length > index
            ? attributeData.ConstructorArguments[index]
            : default;
    }

    /// <summary>
    /// Gets the named argument.
    /// </summary>
    /// <param name="attributeData">The <see cref="AttributeData"/> instance.</param>
    /// <param name="name">The argument name.</param>
    /// <returns>The named argument if exists; otherwise, <see langword="default"/>.</returns>
    public static TypedConstant GetNamedArgument(this AttributeData attributeData, string name)
    {
        return attributeData.NamedArguments
            .FirstOrDefault(x => x.Key == name)
            .Value;
    }
}
