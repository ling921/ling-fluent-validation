namespace Ling.FluentValidation.Test;

public sealed class LingValidatorOptionsTests
{
    [Fact]
    public void RegisterTranslations_AddsMessagesWithoutReplacingLanguageManager()
    {
        var previous = ValidatorOptions.Global.LanguageManager;
        try
        {
            var culture = new CultureInfo("zh-CN");
            ValidatorOptions.Global.LanguageManager = new global::FluentValidation.Resources.LanguageManager
            {
                Culture = culture,
                Enabled = false,
            };

            var registered = LingValidatorOptions.RegisterTranslations();

            var languageManager = Assert.IsType<global::FluentValidation.Resources.LanguageManager>(ValidatorOptions.Global.LanguageManager);
            Assert.True(registered);
            Assert.Same(culture, languageManager.Culture);
            Assert.False(languageManager.Enabled);
            languageManager.Enabled = true;
            Assert.NotEmpty(languageManager.GetString("Ling_PhoneValidator", culture));
        }
        finally
        {
            ValidatorOptions.Global.LanguageManager = previous;
        }
    }

    [Fact]
    public void RegisterTranslations_PreservesUnsupportedCustomManager()
    {
        var previous = ValidatorOptions.Global.LanguageManager;
        try
        {
            var custom = new CustomLanguageManager();
            ValidatorOptions.Global.LanguageManager = custom;

            var registered = LingValidatorOptions.RegisterTranslations();

            Assert.False(registered);
            Assert.Same(custom, ValidatorOptions.Global.LanguageManager);
        }
        finally
        {
            ValidatorOptions.Global.LanguageManager = previous;
        }
    }

    [Fact]
    public void TranslationCatalog_ContainsOnlyCompleteEntries()
    {
        Assert.Equal(410, LingValidatorTranslations.All.Count);
        Assert.All(
            LingValidatorTranslations.All.GroupBy(static translation => translation.Language),
            static language => Assert.Equal(10, language.Count()));
        Assert.All(LingValidatorTranslations.All, static translation =>
        {
            Assert.False(string.IsNullOrWhiteSpace(translation.Language));
            Assert.False(string.IsNullOrWhiteSpace(translation.Key));
            Assert.False(string.IsNullOrWhiteSpace(translation.Message));
        });
    }

    private sealed class CustomLanguageManager : global::FluentValidation.Resources.ILanguageManager
    {
        public bool Enabled { get; set; } = true;

        public CultureInfo? Culture { get; set; }

        public string GetString(string key, CultureInfo? culture = null) => key;
    }
}
