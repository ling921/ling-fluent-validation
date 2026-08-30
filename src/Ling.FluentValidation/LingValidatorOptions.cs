using FluentValidation;
using FluentValidation.Resources;
using System.Diagnostics;
using System.Threading;

namespace Ling.FluentValidation;

/// <summary>
/// Configures process-wide defaults used by Ling.FluentValidation runtime rules.
/// </summary>
public static class LingValidatorOptions
{
    private static int _customLanguageManagerWarningWritten;

    /// <summary>
    /// Registers Ling validator translations with the current FluentValidation language manager.
    /// </summary>
    /// <returns><see langword="true"/> when the current manager supports direct registration; otherwise <see langword="false"/>.</returns>
    public static bool RegisterTranslations()
    {
        var current = ValidatorOptions.Global.LanguageManager;
        if (current is global::FluentValidation.Resources.LanguageManager languageManager)
        {
            LingValidatorTranslations.AddTo(languageManager);
            return true;
        }

        if (Interlocked.Exchange(ref _customLanguageManagerWarningWritten, 1) == 0)
        {
            Trace.TraceWarning(
                "Ling.FluentValidation could not register its translations because ValidatorOptions.Global.LanguageManager " +
                "does not derive from FluentValidation.Resources.LanguageManager. Import LingValidatorTranslations.All " +
                "into the custom ILanguageManager implementation.");
        }

        return false;
    }
}
