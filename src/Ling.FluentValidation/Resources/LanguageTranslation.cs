namespace Ling.FluentValidation.Resources;

/// <summary>
/// Describes one localized validator message.
/// </summary>
public sealed class LanguageTranslation
{
    /// <summary>
    /// Initializes a translation.
    /// </summary>
    public LanguageTranslation(string language, string key, string message)
    {
        Language = language;
        Key = key;
        Message = message;
    }

    /// <summary>Gets the culture name.</summary>
    public string Language { get; }

    /// <summary>Gets the FluentValidation message key.</summary>
    public string Key { get; }

    /// <summary>Gets the localized message template.</summary>
    public string Message { get; }
}
