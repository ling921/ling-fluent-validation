using FluentValidation.Resources;
using System.Collections.ObjectModel;

namespace Ling.FluentValidation;

/// <summary>
/// Provides all translations used by Ling validators.
/// </summary>
public static class LingValidatorTranslations
{
    private static readonly IReadOnlyList<Resources.LanguageTranslation> TranslationList =
        new ReadOnlyCollection<Resources.LanguageTranslation>(
            new List<Resources.LanguageTranslation>(new Resources.LanguageManager().Translations));

    /// <summary>
    /// Gets all available translations. Custom <see cref="ILanguageManager"/> implementations can use this
    /// collection to import Ling validator messages.
    /// </summary>
    public static IReadOnlyList<Resources.LanguageTranslation> All => TranslationList;

    /// <summary>
    /// Adds every Ling translation to a FluentValidation language manager.
    /// </summary>
    /// <param name="languageManager">The language manager to update.</param>
    public static void AddTo(global::FluentValidation.Resources.LanguageManager languageManager)
    {
        if (languageManager is null)
        {
            throw new ArgumentNullException(nameof(languageManager));
        }

        foreach (var translation in TranslationList)
        {
            languageManager.AddTranslation(translation.Language, translation.Key, translation.Message);
        }
    }
}
