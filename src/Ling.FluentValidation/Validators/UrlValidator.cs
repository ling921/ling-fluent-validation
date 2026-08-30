using System.Text.RegularExpressions;

namespace Ling.FluentValidation.Validators;

/// <summary>
/// Represents a validator that checks if a string is a valid URL.
/// <para>
/// The verification rule is consistent with 'System.ComponentModel.DataAnnotations.UrlAttribute'.
/// </para>
/// </summary>
/// <typeparam name="T">The type of the object being validated.</typeparam>
public partial class UrlValidator<T> : PropertyValidator<T, string?>
{
#if NET7_0_OR_GREATER
    [GeneratedRegex("^(https?|ftp)://.*$", RegexOptions.IgnoreCase | RegexOptions.Compiled)]
    protected static partial Regex UrlRegex();
#else
    /// <summary>
    /// The regular expression used to validate URLs.
    /// </summary>
    protected static readonly Regex UrlRegex = new("^(https?|ftp)://.*$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
#endif

    /// <inheritdoc/>
    public override string Name => "Ling_UrlValidator";

    /// <inheritdoc/>
    public override bool IsValid(ValidationContext<T> context, string? value)
    {
#if NET7_0_OR_GREATER
        return value is null || UrlRegex().IsMatch(value);
#else
        return value is null || UrlRegex.IsMatch(value);
#endif
    }

    /// <inheritdoc/>
    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        LingValidatorOptions.RegisterTranslations();
        return Localized(errorCode, Name);
    }
}
