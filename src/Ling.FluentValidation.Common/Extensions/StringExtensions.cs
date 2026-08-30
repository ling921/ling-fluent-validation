using System.Diagnostics.CodeAnalysis;

namespace Ling.FluentValidation.Extensions;

internal static class StringExtensions
{
    [return: NotNullIfNotNull(nameof(str))]
    public static string? TrimStart(this string? str, string value)
    {
        if (str is null || string.IsNullOrEmpty(value)) return str;

        var v = str;

        while (v.StartsWith(value))
        {
            v = v.Substring(value.Length);
        }
        return v;
    }

    [return: NotNullIfNotNull(nameof(str))]
    public static string? TrimEnd(this string? str, string value)
    {
        if (str is null || string.IsNullOrEmpty(value)) return str;

        var v = str;

        while (v.EndsWith(value))
        {
            v = v.Substring(0, v.Length - value.Length);
        }
        return v;
    }
}
