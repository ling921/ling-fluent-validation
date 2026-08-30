using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Ling.FluentValidation.Annotations;

/// <summary>
/// Used to validate that a property or fieldmatches a regular expression.
/// <para>
/// This will generate <c>RuleFor(x => x.PropertyOrField).Matches(pattern)...</c>
/// </para>
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public sealed class MatchesAttribute : ValidationBaseAttribute
{
    /// <summary>
    /// The regular expression pattern.
    /// </summary>
    public string Pattern { get; }

    /// <summary>
    /// The regular expression matching options.
    /// </summary>
    public RegexOptions Options { get; init; }

    /// <summary>
    /// Initializes a new instance.
    /// </summary>
    /// <param name="pattern">The regular expression pattern.</param>
    public MatchesAttribute([StringSyntax(StringSyntaxAttribute.Regex)] string pattern)
    {
        Pattern = pattern;
    }
}
