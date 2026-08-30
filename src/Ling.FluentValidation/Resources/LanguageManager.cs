namespace Ling.FluentValidation.Resources;

/// <summary>
/// Allows the default error message translations to be managed for Ling.FluentValidation.
/// <para>
/// This class is extended from <see cref="global::FluentValidation.Resources.LanguageManager"/>.
/// </para>
/// </summary>
public class LanguageManager : global::FluentValidation.Resources.LanguageManager
{
    private readonly List<LanguageTranslation> _translations = new();

    /// <summary>
    /// Gets the translations registered by this language manager.
    /// </summary>
    public IReadOnlyList<LanguageTranslation> Translations => _translations.AsReadOnly();

    /// <summary>
    /// Initializes a new instance with external translations.
    /// </summary>
    public LanguageManager()
    {
        AddEnglishLanguageTranslation();
        AddAlbanianLanguageTranslation();
        AddArabicLanguageTranslation();
        AddBengaliLanguageTranslation();
        AddBosnianLanguageTranslation();
        AddChineseSimplifiedLanguageTranslation();
        AddChineseTraditionalLanguageTranslation();
        AddCroatianLanguageTranslation();
        AddCzechLanguageTranslation();
        AddDanishLanguageTranslation();
        AddDutchLanguageTranslation();
        AddFinnishLanguageTranslation();
        AddFrenchLanguageTranslation();
        AddGermanLanguageTranslation();
        AddGeorgianLanguageTranslation();
        AddGreekLanguageTranslation();
        AddHebrewLanguageTranslation();
        AddHindiLanguageTranslation();
        AddHungarianLanguageTranslation();
        AddIcelandicLanguageTranslation();
        AddItalianLanguageTranslation();
        AddIndonesianLanguageTranslation();
        AddJapaneseLanguageTranslation();
        AddKoreanLanguageTranslation();
        AddMacedonianLanguageTranslation();
        AddNorwegianBokmalLanguageTranslation();
        AddPersianLanguageTranslation();
        AddPolishLanguageTranslation();
        AddPortugueseLanguageTranslation();
        AddPortugueseBrazilLanguageTranslation();
        AddRomanianLanguageTranslation();
        AddRussianLanguageTranslation();
        AddSlovakLanguageTranslation();
        AddSlovenianLanguageTranslation();
        AddSpanishLanguageTranslation();
        AddSerbianLanguageTranslation();
        AddSwedishLanguageTranslation();
        AddTurkishLanguageTranslation();
        AddUkrainianLanguageTranslation();
        AddVietnameseLanguageTranslation();
        AddWelshLanguageTranslation();
    }

    /// <summary>
    /// Adds a translation and records it in the public Ling translation catalog.
    /// </summary>
    public new void AddTranslation(string language, string key, string message)
    {
        base.AddTranslation(language, key, message);
        _translations.Add(new LanguageTranslation(language, key, message));
    }

    private void AddTranslations(
        string language,
        string phone,
        string url,
        string collectionLength,
        string collectionExactLength,
        string collectionMinimumLength,
        string collectionMaximumLength,
        string allowedValues,
        string deniedValues,
        string base64,
        string fileExtensions)
    {
        AddTranslation(language, "Ling_PhoneValidator", phone);
        AddTranslation(language, "Ling_UrlValidator", url);
        AddTranslation(language, "Ling_CollectionLengthValidator", collectionLength);
        AddTranslation(language, "Ling_CollectionExactLengthValidator", collectionExactLength);
        AddTranslation(language, "Ling_CollectionMinimumLengthValidator", collectionMinimumLength);
        AddTranslation(language, "Ling_CollectionMaximumLengthValidator", collectionMaximumLength);
        AddTranslation(language, "Ling_AllowedValuesValidator", allowedValues);
        AddTranslation(language, "Ling_DeniedValuesValidator", deniedValues);
        AddTranslation(language, "Ling_Base64StringValidator", base64);
        AddTranslation(language, "Ling_FileExtensionsValidator", fileExtensions);
    }

    /// <summary>
    /// Adds translation for the English language.
    /// </summary>
    protected virtual void AddEnglishLanguageTranslation()
    {
        AddTranslation("en", "Ling_PhoneValidator", "'{PropertyName}' is not a valid phone number.");
        AddTranslation("en", "Ling_UrlValidator", "'{PropertyName}' is not a valid URL.");
        AddTranslation("en", "Ling_CollectionLengthValidator", "'{PropertyName}' must have {MinLength}-{MaxLength} items. You entered {TotalLength} items.");
        AddTranslation("en", "Ling_CollectionExactLengthValidator", "'{PropertyName}' must have {MaxLength} items. You entered {TotalLength} items.");
        AddTranslation("en", "Ling_CollectionMinimumLengthValidator", "'{PropertyName}' must have at least {MinLength} items. You entered {TotalLength} items.");
        AddTranslation("en", "Ling_CollectionMaximumLengthValidator", "'{PropertyName}' must have {MaxLength} items or fewer. You entered {TotalLength} items.");
        AddTranslation("en", "Ling_AllowedValuesValidator", "'{PropertyName}' does not equal any of the following values: {Values}.");
        AddTranslation("en", "Ling_DeniedValuesValidator", "'{PropertyName}' equals one of the following values: {Values}.");
        AddTranslation("en", "Ling_Base64StringValidator", "'{PropertyName}' is not a valid Base64 encoding.");
        AddTranslation("en", "Ling_FileExtensionsValidator", "'{PropertyName}' only accepts files with the following extensions: {Extensions}.");
    }

    /// <summary>
    /// Adds translation for the Albanian language.
    /// </summary>
    protected virtual void AddAlbanianLanguageTranslation()
    {
        AddTranslation("sq", "Ling_PhoneValidator", "'{PropertyName}' nuk është një numër i vlefshem.");
        AddTranslation("sq", "Ling_UrlValidator", "'{PropertyName}' nuk është një URL e vlefshem.");
        AddTranslation("sq", "Ling_CollectionLengthValidator", "'{PropertyName}' duhet të ketë {MinLength}-{MaxLength} elemente. Ju keni shtypur {TotalLength} elemente.");
        AddTranslation("sq", "Ling_CollectionExactLengthValidator", "'{PropertyName}' duhet të ketë {MaxLength} elemente. Ju keni shtypur {TotalLength} elemente.");
        AddTranslation("sq", "Ling_CollectionMinimumLengthValidator", "'{PropertyName}' duhet të ketë të paktën {MinLength} elemente. Ju keni shtypur {TotalLength} elemente.");
        AddTranslation("sq", "Ling_CollectionMaximumLengthValidator", "'{PropertyName}' duhet të ketë {MaxLength} elemente ose më pak. Ju keni shtypur {TotalLength} elemente.");
        AddTranslation("sq", "Ling_AllowedValuesValidator", "'{PropertyName}' duhet të keni njëse vlerët: {Values}.");
        AddTranslation("sq", "Ling_DeniedValuesValidator", "'{PropertyName}' nuk duhet të keni njëse vlerët: {Values}.");
        AddTranslation("sq", "Ling_Base64StringValidator", "'{PropertyName}' nuk është një vlerë baze64 i vlefshem.");
        AddTranslation("sq", "Ling_FileExtensionsValidator", "'{PropertyName}' duhet të keni njëse ekstensionet e vlefshme: {Extensions}.");
    }

    /// <summary>
    /// Adds translation for the Arabic language.
    /// </summary>
    protected virtual void AddArabicLanguageTranslation()
    {
        AddTranslation("ar", "Ling_PhoneValidator", "'{PropertyName}' ليس رقم هاتف صحيح.");
        AddTranslation("ar", "Ling_UrlValidator", "'{PropertyName}' ليس عنوان URL صحيح.");
        AddTranslation("ar", "Ling_CollectionLengthValidator", "'{PropertyName}' يجب أن يحتوي على {MinLength}-{MaxLength} عنصرًا. تم إدخال {TotalLength} عنصرًا.");
        AddTranslation("ar", "Ling_CollectionExactLengthValidator", "'{PropertyName}' يجب أن يحتوي على {MaxLength} عنصرًا فقط. تم إدخال {TotalLength} عنصرًا.");
        AddTranslation("ar", "Ling_CollectionMinimumLengthValidator", "'{PropertyName}' يجب أن يحتوي على على الأقل {MinLength} عنصرًا. تم إدخال {TotalLength} عنصرًا.");
        AddTranslation("ar", "Ling_CollectionMaximumLengthValidator", "'{PropertyName}' يجب أن يحتوي على على الأكثر من {MaxLength} عنصرًا. تم إدخال {TotalLength} عنصرًا.");
        AddTranslation("ar", "Ling_AllowedValuesValidator", "'{PropertyName}' يجب أن يكون {Values}.");
        AddTranslation("ar", "Ling_DeniedValuesValidator", "'{PropertyName}' يجب أن لا يكون {Values}.");
        AddTranslation("ar", "Ling_Base64StringValidator", "'{PropertyName}' ليس رمز Base64 صحيح.");
        AddTranslation("ar", "Ling_FileExtensionsValidator", "'{PropertyName}' يجب أن يحتوي على {Extensions}.");
    }

    /// <summary>
    /// Adds translation for the Bengali language.
    /// </summary>
    protected virtual void AddBengaliLanguageTranslation()
    {
        AddTranslation("bn", "Ling_PhoneValidator", "'{PropertyName}' একটি ভুল নম্বর নয়।");
        AddTranslation("bn", "Ling_UrlValidator", "'{PropertyName}' একটি ভুল URL নয়।");
        AddTranslation("bn", "Ling_CollectionLengthValidator", "'{PropertyName}' {MinLength}-{MaxLength} এর মধ্যে আইটেম হয়। আপনি {TotalLength} আইটেম ইনপুট করেছেন।");
        AddTranslation("bn", "Ling_CollectionExactLengthValidator", "'{PropertyName}' {MaxLength} এর সাধারণ আইটেম হয়। আপনি {TotalLength} আইটেম ইনপুট করেছেন।");
        AddTranslation("bn", "Ling_CollectionMinimumLengthValidator", "'{PropertyName}' একটি মিনিমাম {MinLength} আইটেম থাকতে হয়। আপনি {TotalLength} আইটেম ইনপুট করেছেন।");
        AddTranslation("bn", "Ling_CollectionMaximumLengthValidator", "'{PropertyName}' একটি মেক্সিমাম {MaxLength} আইটেম থাকতে পারে। আপনি {TotalLength} আইটেম ইনপুট করেছেন।");
        AddTranslation("bn", "Ling_AllowedValuesValidator", "'{PropertyName}' {Values} এর মধ্যে অথবা না.");
        AddTranslation("bn", "Ling_DeniedValuesValidator", "'{PropertyName}' {Values} এর মধ্যে অথবা না.");
        AddTranslation("bn", "Ling_Base64StringValidator", "'{PropertyName}' একটি ভুল Base64 নয়।");
        AddTranslation("bn", "Ling_FileExtensionsValidator", "'{PropertyName}' {Extensions} এর মধ্যে অথবা না.");
    }

    /// <summary>
    /// Adds translation for the Bosnian language.
    /// </summary>
    protected virtual void AddBosnianLanguageTranslation()
    {
        AddTranslation("bs", "Ling_PhoneValidator", "'{PropertyName}' nije validan broj telefona.");
        AddTranslation("bs", "Ling_UrlValidator", "'{PropertyName}' nije validan URL.");
        AddTranslation("bs", "Ling_CollectionLengthValidator", "'{PropertyName}' mora imati {MinLength}-{MaxLength} stavki. Uneseno je {TotalLength} stavki.");
        AddTranslation("bs", "Ling_CollectionExactLengthValidator", "'{PropertyName}' mora imati {MaxLength} stavki. Uneseno je {TotalLength} stavki.");
        AddTranslation("bs", "Ling_CollectionMinimumLengthValidator", "'{PropertyName}' mora imati bare {MinLength} stavki. Uneseno je {TotalLength} stavki.");
        AddTranslation("bs", "Ling_CollectionMaximumLengthValidator", "'{PropertyName}' mora imati {MaxLength} ili manje stavki. Uneseno je {TotalLength} stavki.");
        AddTranslation("bs", "Ling_AllowedValuesValidator", "'{PropertyName}' mora biti jedan od sljedecih: {Values}.");
        AddTranslation("bs", "Ling_DeniedValuesValidator", "'{PropertyName}' ne smije biti jedan od sljedecih: {Values}.");
        AddTranslation("bs", "Ling_Base64StringValidator", "'{PropertyName}' nije validan Base64 string.");
        AddTranslation("bs", "Ling_FileExtensionsValidator", "'{PropertyName}' mora biti jedan od sljedecih: {Extensions}.");
    }

    /// <summary>
    /// Adds translation for the Chinese simplified language.
    /// </summary>
    protected virtual void AddChineseSimplifiedLanguageTranslation()
    {
        AddTranslation("zh-CN", "Ling_PhoneValidator", "'{PropertyName}' 不是有效的电话号码。");
        AddTranslation("zh-CN", "Ling_UrlValidator", "'{PropertyName}' 不是有效的 URL。");
        AddTranslation("zh-CN", "Ling_CollectionLengthValidator", "'{PropertyName}' 必须有 {MinLength} 到 {MaxLength} 项。您输入了 {TotalLength} 项。");
        AddTranslation("zh-CN", "Ling_CollectionExactLengthValidator", "'{PropertyName}' 必须有 {MaxLength} 项。您输入了 {TotalLength} 项。");
        AddTranslation("zh-CN", "Ling_CollectionMinimumLengthValidator", "'{PropertyName}' 必须有至少 {MinLength} 项。您输入了 {TotalLength} 项。");
        AddTranslation("zh-CN", "Ling_CollectionMaximumLengthValidator", "'{PropertyName}' 必须不能超过 {MaxLength} 项。您输入了 {TotalLength} 项。");
        AddTranslation("zh-CN", "Ling_AllowedValuesValidator", "'{PropertyName}' 必须是以下值之一：{Values}。");
        AddTranslation("zh-CN", "Ling_DeniedValuesValidator", "'{PropertyName}' 不能是以下值之一：{Values}。");
        AddTranslation("zh-CN", "Ling_Base64StringValidator", "'{PropertyName}' 不是有效的 Base64 字符串。");
        AddTranslation("zh-CN", "Ling_FileExtensionsValidator", "'{PropertyName}' 只能接受以下扩展名：{Extensions}。");
    }

    /// <summary>
    /// Adds translation for the Chinese traditional language.
    /// </summary>
    protected virtual void AddChineseTraditionalLanguageTranslation()
    {
        AddTranslation("zh-TW", "Ling_PhoneValidator", "'{PropertyName}' 不是有效的電話號碼。");
        AddTranslation("zh-TW", "Ling_UrlValidator", "'{PropertyName}' 不是有效的 URL。");
        AddTranslation("zh-TW", "Ling_CollectionLengthValidator", "'{PropertyName}' 必須有 {MinLength} 到 {MaxLength} 個。您輸入了 {TotalLength} 個。");
        AddTranslation("zh-TW", "Ling_CollectionExactLengthValidator", "'{PropertyName}' 必須有 {MaxLength} 個。您輸入了 {TotalLength} 個。");
        AddTranslation("zh-TW", "Ling_CollectionMinimumLengthValidator", "'{PropertyName}' 必須有至少 {MinLength} 個。您輸入了 {TotalLength} 個。");
        AddTranslation("zh-TW", "Ling_CollectionMaximumLengthValidator", "'{PropertyName}' 必須不能超過 {MaxLength} 個。您輸入了 {TotalLength} 個。");
        AddTranslation("zh-TW", "Ling_AllowedValuesValidator", "'{PropertyName}' 必須是以下值之一：{Values}。");
        AddTranslation("zh-TW", "Ling_DeniedValuesValidator", "'{PropertyName}' 不能是以下值之一：{Values}。");
        AddTranslation("zh-TW", "Ling_Base64StringValidator", "'{PropertyName}' 不是有效的 Base64 字串。");
        AddTranslation("zh-TW", "Ling_FileExtensionsValidator", "'{PropertyName}' 只能接受以下擴展名：{Extensions}。");
    }

    /// <summary>
    /// Adds translation for the Croatian language.
    /// </summary>
    protected virtual void AddCroatianLanguageTranslation()
    {
        AddTranslation("hr", "Ling_PhoneValidator", "'{PropertyName}' nije valjan telefonski broj.");
        AddTranslation("hr", "Ling_UrlValidator", "'{PropertyName}' nije valjan URL.");
        AddTranslation("hr", "Ling_CollectionLengthValidator", "'{PropertyName}' mora imati {MinLength}-{MaxLength} stavki. Uneseno je {TotalLength} stavki.");
        AddTranslation("hr", "Ling_CollectionExactLengthValidator", "'{PropertyName}' mora imati {MaxLength} stavki. Uneseno je {TotalLength} stavki.");
        AddTranslation("hr", "Ling_CollectionMinimumLengthValidator", "'{PropertyName}' mora imati bare {MinLength} stavki. Uneseno je {TotalLength} stavki.");
        AddTranslation("hr", "Ling_CollectionMaximumLengthValidator", "'{PropertyName}' mora imati {MaxLength} ili manje stavki. Uneseno je {TotalLength} stavki.");
        AddTranslation("hr", "Ling_AllowedValuesValidator", "'{PropertyName}' mora biti jedan od sljedecih: {Values}.");
        AddTranslation("hr", "Ling_DeniedValuesValidator", "'{PropertyName}' ne smije biti jedan od sljedecih: {Values}.");
        AddTranslation("hr", "Ling_Base64StringValidator", "'{PropertyName}' nije valjan Base64 string.");
        AddTranslation("hr", "Ling_FileExtensionsValidator", "'{PropertyName}' mora biti jedan od sljedecih: {Extensions}.");
    }

    /// <summary>
    /// Adds translation for the Czech language.
    /// </summary>
    protected virtual void AddCzechLanguageTranslation()
    {
        AddTranslation("cs", "Ling_PhoneValidator", "'{PropertyName}' neni platný telefonový číslo.");
        AddTranslation("cs", "Ling_UrlValidator", "'{PropertyName}' neni platný URL.");
        AddTranslation("cs", "Ling_CollectionLengthValidator", "'{PropertyName}' musí být {MinLength}-{MaxLength} polozď. Zadali jste {TotalLength} polozď.");
        AddTranslation("cs", "Ling_CollectionExactLengthValidator", "'{PropertyName}' musí být {MaxLength} polozď. Zadali jste {TotalLength} polozď.");
        AddTranslation("cs", "Ling_CollectionMinimumLengthValidator", "'{PropertyName}' musí být {MinLength} polozď. Zadali jste {TotalLength} polozď.");
        AddTranslation("cs", "Ling_CollectionMaximumLengthValidator", "'{PropertyName}' nemusí být {MaxLength} polozď. Zadali jste {TotalLength} polozď.");
        AddTranslation("cs", "Ling_AllowedValuesValidator", "'{PropertyName}' musí být jedně z {Values}.");
        AddTranslation("cs", "Ling_DeniedValuesValidator", "'{PropertyName}' nemusí být jedně z {Values}.");
        AddTranslation("cs", "Ling_Base64StringValidator", "'{PropertyName}' neni platný Base64 string.");
        AddTranslation("cs", "Ling_FileExtensionsValidator", "'{PropertyName}' musí být jedně z {Extensions}.");
    }

    /// <summary>
    /// Adds translation for the Danish language.
    /// </summary>
    protected virtual void AddDanishLanguageTranslation()
    {
        AddTranslations("da", "'{PropertyName}' er ikke et gyldigt telefonnummer.", "'{PropertyName}' er ikke en gyldig URL.", "'{PropertyName}' skal have mellem {MinLength} og {MaxLength} elementer. Du angav {TotalLength} elementer.", "'{PropertyName}' skal have {MaxLength} elementer. Du angav {TotalLength} elementer.", "'{PropertyName}' skal have mindst {MinLength} elementer. Du angav {TotalLength} elementer.", "'{PropertyName}' må højst have {MaxLength} elementer. Du angav {TotalLength} elementer.", "'{PropertyName}' skal være en af følgende værdier: {Values}.", "'{PropertyName}' må ikke være en af følgende værdier: {Values}.", "'{PropertyName}' er ikke en gyldig Base64-kodning.", "'{PropertyName}' accepterer kun følgende filtypenavne: {Extensions}.");
    }

    /// <summary>
    /// Adds translation for the Dutch language.
    /// </summary>
    protected virtual void AddDutchLanguageTranslation()
    {
        AddTranslations("nl", "'{PropertyName}' is geen geldig telefoonnummer.", "'{PropertyName}' is geen geldige URL.", "'{PropertyName}' moet tussen {MinLength} en {MaxLength} items bevatten. U hebt {TotalLength} items ingevoerd.", "'{PropertyName}' moet {MaxLength} items bevatten. U hebt {TotalLength} items ingevoerd.", "'{PropertyName}' moet minstens {MinLength} items bevatten. U hebt {TotalLength} items ingevoerd.", "'{PropertyName}' mag maximaal {MaxLength} items bevatten. U hebt {TotalLength} items ingevoerd.", "'{PropertyName}' moet een van de volgende waarden zijn: {Values}.", "'{PropertyName}' mag niet een van de volgende waarden zijn: {Values}.", "'{PropertyName}' is geen geldige Base64-codering.", "'{PropertyName}' accepteert alleen de volgende extensies: {Extensions}.");
    }

    /// <summary>
    /// Adds translation for the Finnish language.
    /// </summary>
    protected virtual void AddFinnishLanguageTranslation()
    {
        AddTranslations("fi", "'{PropertyName}' ei ole kelvollinen puhelinnumero.", "'{PropertyName}' ei ole kelvollinen URL-osoite.", "'{PropertyName}'-kentässä on oltava {MinLength}–{MaxLength} kohdetta. Annoit {TotalLength} kohdetta.", "'{PropertyName}'-kentässä on oltava {MaxLength} kohdetta. Annoit {TotalLength} kohdetta.", "'{PropertyName}'-kentässä on oltava vähintään {MinLength} kohdetta. Annoit {TotalLength} kohdetta.", "'{PropertyName}'-kentässä saa olla enintään {MaxLength} kohdetta. Annoit {TotalLength} kohdetta.", "'{PropertyName}'-kentän on oltava jokin seuraavista arvoista: {Values}.", "'{PropertyName}'-kenttä ei saa olla mikään seuraavista arvoista: {Values}.", "'{PropertyName}' ei ole kelvollinen Base64-koodaus.", "'{PropertyName}' hyväksyy vain seuraavat tiedostopäätteet: {Extensions}.");
    }

    /// <summary>
    /// Adds translation for the French language.
    /// </summary>
    protected virtual void AddFrenchLanguageTranslation()
    {
        AddTranslations("fr", "'{PropertyName}' n'est pas un numéro de téléphone valide.", "'{PropertyName}' n'est pas une URL valide.", "'{PropertyName}' doit contenir entre {MinLength} et {MaxLength} éléments. Vous avez saisi {TotalLength} éléments.", "'{PropertyName}' doit contenir {MaxLength} éléments. Vous avez saisi {TotalLength} éléments.", "'{PropertyName}' doit contenir au moins {MinLength} éléments. Vous avez saisi {TotalLength} éléments.", "'{PropertyName}' doit contenir au plus {MaxLength} éléments. Vous avez saisi {TotalLength} éléments.", "'{PropertyName}' doit être l'une des valeurs suivantes : {Values}.", "'{PropertyName}' ne doit pas être l'une des valeurs suivantes : {Values}.", "'{PropertyName}' n'est pas un encodage Base64 valide.", "'{PropertyName}' accepte uniquement les extensions suivantes : {Extensions}.");
    }

    /// <summary>
    /// Adds translation for the German language.
    /// </summary>
    protected virtual void AddGermanLanguageTranslation()
    {
        AddTranslations("de", "'{PropertyName}' ist keine gültige Telefonnummer.", "'{PropertyName}' ist keine gültige URL.", "'{PropertyName}' muss zwischen {MinLength} und {MaxLength} Elemente enthalten. Sie haben {TotalLength} Elemente eingegeben.", "'{PropertyName}' muss {MaxLength} Elemente enthalten. Sie haben {TotalLength} Elemente eingegeben.", "'{PropertyName}' muss mindestens {MinLength} Elemente enthalten. Sie haben {TotalLength} Elemente eingegeben.", "'{PropertyName}' darf höchstens {MaxLength} Elemente enthalten. Sie haben {TotalLength} Elemente eingegeben.", "'{PropertyName}' muss einer der folgenden Werte sein: {Values}.", "'{PropertyName}' darf keiner der folgenden Werte sein: {Values}.", "'{PropertyName}' ist keine gültige Base64-Codierung.", "'{PropertyName}' akzeptiert nur folgende Dateiendungen: {Extensions}.");
    }

    /// <summary>
    /// Adds translation for the Georgian language.
    /// </summary>
    protected virtual void AddGeorgianLanguageTranslation()
    {
        AddTranslations("ka", "'{PropertyName}' არ არის სწორი ტელეფონის ნომერი.", "'{PropertyName}' არ არის სწორი URL.", "'{PropertyName}' უნდა შეიცავდეს {MinLength}-დან {MaxLength}-მდე ელემენტს. შეყვანილია {TotalLength}.", "'{PropertyName}' უნდა შეიცავდეს {MaxLength} ელემენტს. შეყვანილია {TotalLength}.", "'{PropertyName}' უნდა შეიცავდეს სულ მცირე {MinLength} ელემენტს. შეყვანილია {TotalLength}.", "'{PropertyName}' უნდა შეიცავდეს არაუმეტეს {MaxLength} ელემენტს. შეყვანილია {TotalLength}.", "'{PropertyName}' უნდა იყოს ერთ-ერთი მნიშვნელობა: {Values}.", "'{PropertyName}' არ უნდა იყოს ერთ-ერთი მნიშვნელობა: {Values}.", "'{PropertyName}' არ არის სწორი Base64 კოდირება.", "'{PropertyName}' იღებს მხოლოდ შემდეგ გაფართოებებს: {Extensions}.");
    }

    /// <summary>
    /// Adds translation for the Greek language.
    /// </summary>
    protected virtual void AddGreekLanguageTranslation()
    {
        AddTranslations("el", "Το '{PropertyName}' δεν είναι έγκυρος αριθμός τηλεφώνου.", "Το '{PropertyName}' δεν είναι έγκυρη διεύθυνση URL.", "Το '{PropertyName}' πρέπει να έχει {MinLength} έως {MaxLength} στοιχεία. Εισαγάγατε {TotalLength}.", "Το '{PropertyName}' πρέπει να έχει {MaxLength} στοιχεία. Εισαγάγατε {TotalLength}.", "Το '{PropertyName}' πρέπει να έχει τουλάχιστον {MinLength} στοιχεία. Εισαγάγατε {TotalLength}.", "Το '{PropertyName}' πρέπει να έχει έως {MaxLength} στοιχεία. Εισαγάγατε {TotalLength}.", "Το '{PropertyName}' πρέπει να είναι μία από τις τιμές: {Values}.", "Το '{PropertyName}' δεν πρέπει να είναι μία από τις τιμές: {Values}.", "Το '{PropertyName}' δεν είναι έγκυρη κωδικοποίηση Base64.", "Το '{PropertyName}' δέχεται μόνο τις επεκτάσεις: {Extensions}.");
    }

    /// <summary>
    /// Adds translation for the Hebrew language.
    /// </summary>
    protected virtual void AddHebrewLanguageTranslation()
    {
        AddTranslations("he", "'{PropertyName}' אינו מספר טלפון חוקי.", "'{PropertyName}' אינו URL חוקי.", "'{PropertyName}' חייב להכיל בין {MinLength} ל-{MaxLength} פריטים. הוזנו {TotalLength} פריטים.", "'{PropertyName}' חייב להכיל {MaxLength} פריטים. הוזנו {TotalLength} פריטים.", "'{PropertyName}' חייב להכיל לפחות {MinLength} פריטים. הוזנו {TotalLength} פריטים.", "'{PropertyName}' יכול להכיל לכל היותר {MaxLength} פריטים. הוזנו {TotalLength} פריטים.", "'{PropertyName}' חייב להיות אחד מהערכים: {Values}.", "'{PropertyName}' אינו יכול להיות אחד מהערכים: {Values}.", "'{PropertyName}' אינו קידוד Base64 חוקי.", "'{PropertyName}' מקבל רק את הסיומות: {Extensions}.");
    }

    /// <summary>
    /// Adds translation for the Hindi language.
    /// </summary>
    protected virtual void AddHindiLanguageTranslation()
    {
        AddTranslations("hi", "'{PropertyName}' मान्य फ़ोन नंबर नहीं है।", "'{PropertyName}' मान्य URL नहीं है।", "'{PropertyName}' में {MinLength} से {MaxLength} आइटम होने चाहिए। आपने {TotalLength} आइटम दर्ज किए।", "'{PropertyName}' में {MaxLength} आइटम होने चाहिए। आपने {TotalLength} आइटम दर्ज किए।", "'{PropertyName}' में कम से कम {MinLength} आइटम होने चाहिए। आपने {TotalLength} आइटम दर्ज किए।", "'{PropertyName}' में अधिकतम {MaxLength} आइटम हो सकते हैं। आपने {TotalLength} आइटम दर्ज किए।", "'{PropertyName}' इनमें से एक मान होना चाहिए: {Values}।", "'{PropertyName}' इनमें से कोई मान नहीं होना चाहिए: {Values}।", "'{PropertyName}' मान्य Base64 एन्कोडिंग नहीं है।", "'{PropertyName}' केवल ये एक्सटेंशन स्वीकार करता है: {Extensions}।");
    }

    /// <summary>
    /// Adds translation for the Hungarian language.
    /// </summary>
    protected virtual void AddHungarianLanguageTranslation()
    {
        AddTranslations("hu", "A(z) '{PropertyName}' nem érvényes telefonszám.", "A(z) '{PropertyName}' nem érvényes URL.", "A(z) '{PropertyName}' elemszáma {MinLength} és {MaxLength} között legyen. A megadott elemszám: {TotalLength}.", "A(z) '{PropertyName}' elemszáma {MaxLength} legyen. A megadott elemszám: {TotalLength}.", "A(z) '{PropertyName}' legalább {MinLength} elemet tartalmazzon. A megadott elemszám: {TotalLength}.", "A(z) '{PropertyName}' legfeljebb {MaxLength} elemet tartalmazhat. A megadott elemszám: {TotalLength}.", "A(z) '{PropertyName}' értéke a következők egyike legyen: {Values}.", "A(z) '{PropertyName}' értéke nem lehet a következők egyike: {Values}.", "A(z) '{PropertyName}' nem érvényes Base64-kódolás.", "A(z) '{PropertyName}' csak a következő kiterjesztéseket fogadja el: {Extensions}.");
    }

    /// <summary>
    /// Adds translation for the Icelandic language.
    /// </summary>
    protected virtual void AddIcelandicLanguageTranslation()
    {
        AddTranslations("is", "'{PropertyName}' er ekki gilt símanúmer.", "'{PropertyName}' er ekki gild vefslóð.", "'{PropertyName}' verður að hafa {MinLength}–{MaxLength} atriði. Þú slóst inn {TotalLength} atriði.", "'{PropertyName}' verður að hafa {MaxLength} atriði. Þú slóst inn {TotalLength} atriði.", "'{PropertyName}' verður að hafa að minnsta kosti {MinLength} atriði. Þú slóst inn {TotalLength} atriði.", "'{PropertyName}' má hafa mest {MaxLength} atriði. Þú slóst inn {TotalLength} atriði.", "'{PropertyName}' verður að vera eitt af eftirfarandi gildum: {Values}.", "'{PropertyName}' má ekki vera eitt af eftirfarandi gildum: {Values}.", "'{PropertyName}' er ekki gild Base64-kóðun.", "'{PropertyName}' samþykkir aðeins eftirfarandi skráarendingar: {Extensions}.");
    }

    /// <summary>
    /// Adds translation for the Italian language.
    /// </summary>
    protected virtual void AddItalianLanguageTranslation()
    {
        AddTranslations("it", "'{PropertyName}' non è un numero di telefono valido.", "'{PropertyName}' non è un URL valido.", "'{PropertyName}' deve contenere da {MinLength} a {MaxLength} elementi. Sono stati inseriti {TotalLength} elementi.", "'{PropertyName}' deve contenere {MaxLength} elementi. Sono stati inseriti {TotalLength} elementi.", "'{PropertyName}' deve contenere almeno {MinLength} elementi. Sono stati inseriti {TotalLength} elementi.", "'{PropertyName}' può contenere al massimo {MaxLength} elementi. Sono stati inseriti {TotalLength} elementi.", "'{PropertyName}' deve essere uno dei seguenti valori: {Values}.", "'{PropertyName}' non deve essere uno dei seguenti valori: {Values}.", "'{PropertyName}' non è una codifica Base64 valida.", "'{PropertyName}' accetta solo le seguenti estensioni: {Extensions}.");
    }

    /// <summary>
    /// Adds translation for the Indonesian language.
    /// </summary>
    protected virtual void AddIndonesianLanguageTranslation()
    {
        AddTranslations("id", "'{PropertyName}' bukan nomor telepon yang valid.", "'{PropertyName}' bukan URL yang valid.", "'{PropertyName}' harus memiliki {MinLength} hingga {MaxLength} item. Anda memasukkan {TotalLength} item.", "'{PropertyName}' harus memiliki {MaxLength} item. Anda memasukkan {TotalLength} item.", "'{PropertyName}' harus memiliki sedikitnya {MinLength} item. Anda memasukkan {TotalLength} item.", "'{PropertyName}' boleh memiliki paling banyak {MaxLength} item. Anda memasukkan {TotalLength} item.", "'{PropertyName}' harus merupakan salah satu nilai berikut: {Values}.", "'{PropertyName}' tidak boleh merupakan salah satu nilai berikut: {Values}.", "'{PropertyName}' bukan enkode Base64 yang valid.", "'{PropertyName}' hanya menerima ekstensi berikut: {Extensions}.");
    }

    /// <summary>
    /// Adds translation for the Japanese language.
    /// </summary>
    protected virtual void AddJapaneseLanguageTranslation()
    {
        AddTranslations("ja", "'{PropertyName}' は有効な電話番号ではありません。", "'{PropertyName}' は有効な URL ではありません。", "'{PropertyName}' は {MinLength}～{MaxLength} 個の項目が必要です。{TotalLength} 個入力されています。", "'{PropertyName}' は {MaxLength} 個の項目が必要です。{TotalLength} 個入力されています。", "'{PropertyName}' は少なくとも {MinLength} 個の項目が必要です。{TotalLength} 個入力されています。", "'{PropertyName}' は {MaxLength} 個以下である必要があります。{TotalLength} 個入力されています。", "'{PropertyName}' は次の値のいずれかである必要があります: {Values}。", "'{PropertyName}' は次の値のいずれでもない必要があります: {Values}。", "'{PropertyName}' は有効な Base64 エンコードではありません。", "'{PropertyName}' では次の拡張子のみ使用できます: {Extensions}。");
    }

    /// <summary>
    /// Adds translation for the Korean language.
    /// </summary>
    protected virtual void AddKoreanLanguageTranslation()
    {
        AddTranslations("ko", "'{PropertyName}'은(는) 유효한 전화번호가 아닙니다.", "'{PropertyName}'은(는) 유효한 URL이 아닙니다.", "'{PropertyName}'에는 {MinLength}개에서 {MaxLength}개의 항목이 있어야 합니다. {TotalLength}개를 입력했습니다.", "'{PropertyName}'에는 {MaxLength}개의 항목이 있어야 합니다. {TotalLength}개를 입력했습니다.", "'{PropertyName}'에는 최소 {MinLength}개의 항목이 있어야 합니다. {TotalLength}개를 입력했습니다.", "'{PropertyName}'에는 최대 {MaxLength}개의 항목이 있어야 합니다. {TotalLength}개를 입력했습니다.", "'{PropertyName}'은(는) 다음 값 중 하나여야 합니다: {Values}.", "'{PropertyName}'은(는) 다음 값 중 하나이면 안 됩니다: {Values}.", "'{PropertyName}'은(는) 유효한 Base64 인코딩이 아닙니다.", "'{PropertyName}'에서는 다음 확장자만 허용됩니다: {Extensions}.");
    }

    /// <summary>
    /// Adds translation for the Macedonian language.
    /// </summary>
    protected virtual void AddMacedonianLanguageTranslation()
    {
        AddTranslations("mk", "'{PropertyName}' не е важечки телефонски број.", "'{PropertyName}' не е важечка URL-адреса.", "'{PropertyName}' мора да има од {MinLength} до {MaxLength} ставки. Внесовте {TotalLength} ставки.", "'{PropertyName}' мора да има {MaxLength} ставки. Внесовте {TotalLength} ставки.", "'{PropertyName}' мора да има најмалку {MinLength} ставки. Внесовте {TotalLength} ставки.", "'{PropertyName}' може да има најмногу {MaxLength} ставки. Внесовте {TotalLength} ставки.", "'{PropertyName}' мора да биде една од вредностите: {Values}.", "'{PropertyName}' не смее да биде една од вредностите: {Values}.", "'{PropertyName}' не е важечко Base64 кодирање.", "'{PropertyName}' ги прифаќа само наставките: {Extensions}.");
    }

    /// <summary>
    /// Adds translation for the Norwegian bokmal language.
    /// </summary>
    protected virtual void AddNorwegianBokmalLanguageTranslation()
    {
        AddTranslations("nb", "'{PropertyName}' er ikke et gyldig telefonnummer.", "'{PropertyName}' er ikke en gyldig URL.", "'{PropertyName}' må ha mellom {MinLength} og {MaxLength} elementer. Du skrev inn {TotalLength} elementer.", "'{PropertyName}' må ha {MaxLength} elementer. Du skrev inn {TotalLength} elementer.", "'{PropertyName}' må ha minst {MinLength} elementer. Du skrev inn {TotalLength} elementer.", "'{PropertyName}' kan ha maksimalt {MaxLength} elementer. Du skrev inn {TotalLength} elementer.", "'{PropertyName}' må være en av følgende verdier: {Values}.", "'{PropertyName}' kan ikke være en av følgende verdier: {Values}.", "'{PropertyName}' er ikke en gyldig Base64-koding.", "'{PropertyName}' godtar bare følgende filendelser: {Extensions}.");
    }

    /// <summary>
    /// Adds translation for the Persian language.
    /// </summary>
    protected virtual void AddPersianLanguageTranslation()
    {
        AddTranslations("fa", "'{PropertyName}' شماره تلفن معتبری نیست.", "'{PropertyName}' نشانی URL معتبری نیست.", "'{PropertyName}' باید بین {MinLength} تا {MaxLength} مورد داشته باشد. شما {TotalLength} مورد وارد کرده‌اید.", "'{PropertyName}' باید {MaxLength} مورد داشته باشد. شما {TotalLength} مورد وارد کرده‌اید.", "'{PropertyName}' باید حداقل {MinLength} مورد داشته باشد. شما {TotalLength} مورد وارد کرده‌اید.", "'{PropertyName}' باید حداکثر {MaxLength} مورد داشته باشد. شما {TotalLength} مورد وارد کرده‌اید.", "'{PropertyName}' باید یکی از مقادیر زیر باشد: {Values}.", "'{PropertyName}' نباید یکی از مقادیر زیر باشد: {Values}.", "'{PropertyName}' کدگذاری Base64 معتبری نیست.", "'{PropertyName}' فقط پسوندهای زیر را می‌پذیرد: {Extensions}.");
    }

    /// <summary>
    /// Adds translation for the Polish language.
    /// </summary>
    protected virtual void AddPolishLanguageTranslation()
    {
        AddTranslations("pl", "'{PropertyName}' nie jest prawidłowym numerem telefonu.", "'{PropertyName}' nie jest prawidłowym adresem URL.", "'{PropertyName}' musi zawierać od {MinLength} do {MaxLength} elementów. Wprowadzono {TotalLength}.", "'{PropertyName}' musi zawierać {MaxLength} elementów. Wprowadzono {TotalLength}.", "'{PropertyName}' musi zawierać co najmniej {MinLength} elementów. Wprowadzono {TotalLength}.", "'{PropertyName}' może zawierać najwyżej {MaxLength} elementów. Wprowadzono {TotalLength}.", "'{PropertyName}' musi być jedną z wartości: {Values}.", "'{PropertyName}' nie może być jedną z wartości: {Values}.", "'{PropertyName}' nie jest prawidłowym kodowaniem Base64.", "'{PropertyName}' akceptuje tylko rozszerzenia: {Extensions}.");
    }

    /// <summary>
    /// Adds translation for the Portuguese language.
    /// </summary>
    protected virtual void AddPortugueseLanguageTranslation()
    {
        AddTranslations("pt", "'{PropertyName}' não é um número de telefone válido.", "'{PropertyName}' não é um URL válido.", "'{PropertyName}' deve conter entre {MinLength} e {MaxLength} itens. Introduziu {TotalLength} itens.", "'{PropertyName}' deve conter {MaxLength} itens. Introduziu {TotalLength} itens.", "'{PropertyName}' deve conter pelo menos {MinLength} itens. Introduziu {TotalLength} itens.", "'{PropertyName}' deve conter no máximo {MaxLength} itens. Introduziu {TotalLength} itens.", "'{PropertyName}' deve ser um dos seguintes valores: {Values}.", "'{PropertyName}' não deve ser um dos seguintes valores: {Values}.", "'{PropertyName}' não é uma codificação Base64 válida.", "'{PropertyName}' aceita apenas as extensões: {Extensions}.");
    }

    /// <summary>
    /// Adds translation for the Portuguese (Brazil) language.
    /// </summary>
    protected virtual void AddPortugueseBrazilLanguageTranslation()
    {
        AddTranslations("pt-BR", "'{PropertyName}' não é um número de telefone válido.", "'{PropertyName}' não é uma URL válida.", "'{PropertyName}' deve conter entre {MinLength} e {MaxLength} itens. Você informou {TotalLength} itens.", "'{PropertyName}' deve conter {MaxLength} itens. Você informou {TotalLength} itens.", "'{PropertyName}' deve conter pelo menos {MinLength} itens. Você informou {TotalLength} itens.", "'{PropertyName}' deve conter no máximo {MaxLength} itens. Você informou {TotalLength} itens.", "'{PropertyName}' deve ser um dos seguintes valores: {Values}.", "'{PropertyName}' não pode ser um dos seguintes valores: {Values}.", "'{PropertyName}' não é uma codificação Base64 válida.", "'{PropertyName}' aceita apenas as extensões: {Extensions}.");
    }

    /// <summary>
    /// Adds translation for the Romanian language.
    /// </summary>
    protected virtual void AddRomanianLanguageTranslation()
    {
        AddTranslations("ro", "'{PropertyName}' nu este un număr de telefon valid.", "'{PropertyName}' nu este un URL valid.", "'{PropertyName}' trebuie să conțină între {MinLength} și {MaxLength} elemente. Ați introdus {TotalLength}.", "'{PropertyName}' trebuie să conțină {MaxLength} elemente. Ați introdus {TotalLength}.", "'{PropertyName}' trebuie să conțină cel puțin {MinLength} elemente. Ați introdus {TotalLength}.", "'{PropertyName}' poate conține cel mult {MaxLength} elemente. Ați introdus {TotalLength}.", "'{PropertyName}' trebuie să fie una dintre valorile: {Values}.", "'{PropertyName}' nu trebuie să fie una dintre valorile: {Values}.", "'{PropertyName}' nu este o codificare Base64 validă.", "'{PropertyName}' acceptă doar extensiile: {Extensions}.");
    }

    /// <summary>
    /// Adds translation for the Russian language.
    /// </summary>
    protected virtual void AddRussianLanguageTranslation()
    {
        AddTranslations("ru", "'{PropertyName}' не является допустимым номером телефона.", "'{PropertyName}' не является допустимым URL.", "'{PropertyName}' должно содержать от {MinLength} до {MaxLength} элементов. Введено: {TotalLength}.", "'{PropertyName}' должно содержать {MaxLength} элементов. Введено: {TotalLength}.", "'{PropertyName}' должно содержать не менее {MinLength} элементов. Введено: {TotalLength}.", "'{PropertyName}' должно содержать не более {MaxLength} элементов. Введено: {TotalLength}.", "'{PropertyName}' должно быть одним из значений: {Values}.", "'{PropertyName}' не должно быть одним из значений: {Values}.", "'{PropertyName}' не является допустимой кодировкой Base64.", "'{PropertyName}' принимает только расширения: {Extensions}.");
    }

    /// <summary>
    /// Adds translation for the Slovak language.
    /// </summary>
    protected virtual void AddSlovakLanguageTranslation()
    {
        AddTranslations("sk", "'{PropertyName}' nie je platné telefónne číslo.", "'{PropertyName}' nie je platná URL adresa.", "'{PropertyName}' musí obsahovať {MinLength} až {MaxLength} položiek. Zadali ste {TotalLength}.", "'{PropertyName}' musí obsahovať {MaxLength} položiek. Zadali ste {TotalLength}.", "'{PropertyName}' musí obsahovať aspoň {MinLength} položiek. Zadali ste {TotalLength}.", "'{PropertyName}' môže obsahovať najviac {MaxLength} položiek. Zadali ste {TotalLength}.", "'{PropertyName}' musí byť jedna z hodnôt: {Values}.", "'{PropertyName}' nesmie byť jedna z hodnôt: {Values}.", "'{PropertyName}' nie je platné kódovanie Base64.", "'{PropertyName}' prijíma iba prípony: {Extensions}.");
    }

    /// <summary>
    /// Adds translation for the Slovenian language.
    /// </summary>
    protected virtual void AddSlovenianLanguageTranslation()
    {
        AddTranslations("sl", "'{PropertyName}' ni veljavna telefonska številka.", "'{PropertyName}' ni veljaven URL.", "'{PropertyName}' mora vsebovati od {MinLength} do {MaxLength} elementov. Vnesli ste {TotalLength}.", "'{PropertyName}' mora vsebovati {MaxLength} elementov. Vnesli ste {TotalLength}.", "'{PropertyName}' mora vsebovati vsaj {MinLength} elementov. Vnesli ste {TotalLength}.", "'{PropertyName}' lahko vsebuje največ {MaxLength} elementov. Vnesli ste {TotalLength}.", "'{PropertyName}' mora biti ena od vrednosti: {Values}.", "'{PropertyName}' ne sme biti ena od vrednosti: {Values}.", "'{PropertyName}' ni veljavno kodiranje Base64.", "'{PropertyName}' sprejema samo končnice: {Extensions}.");
    }

    /// <summary>
    /// Adds translation for the Spanish language.
    /// </summary>
    protected virtual void AddSpanishLanguageTranslation()
    {
        AddTranslations("es", "'{PropertyName}' no es un número de teléfono válido.", "'{PropertyName}' no es una URL válida.", "'{PropertyName}' debe contener entre {MinLength} y {MaxLength} elementos. Introdujo {TotalLength} elementos.", "'{PropertyName}' debe contener {MaxLength} elementos. Introdujo {TotalLength} elementos.", "'{PropertyName}' debe contener al menos {MinLength} elementos. Introdujo {TotalLength} elementos.", "'{PropertyName}' debe contener como máximo {MaxLength} elementos. Introdujo {TotalLength} elementos.", "'{PropertyName}' debe ser uno de los siguientes valores: {Values}.", "'{PropertyName}' no debe ser uno de los siguientes valores: {Values}.", "'{PropertyName}' no es una codificación Base64 válida.", "'{PropertyName}' solo acepta las extensiones: {Extensions}.");
    }

    /// <summary>
    /// Adds translation for the Serbian language.
    /// </summary>
    protected virtual void AddSerbianLanguageTranslation()
    {
        AddTranslations("sr", "'{PropertyName}' nije važeći broj telefona.", "'{PropertyName}' nije važeći URL.", "'{PropertyName}' mora imati od {MinLength} do {MaxLength} stavki. Uneli ste {TotalLength}.", "'{PropertyName}' mora imati {MaxLength} stavki. Uneli ste {TotalLength}.", "'{PropertyName}' mora imati najmanje {MinLength} stavki. Uneli ste {TotalLength}.", "'{PropertyName}' može imati najviše {MaxLength} stavki. Uneli ste {TotalLength}.", "'{PropertyName}' mora biti jedna od vrednosti: {Values}.", "'{PropertyName}' ne sme biti jedna od vrednosti: {Values}.", "'{PropertyName}' nije važeće Base64 kodiranje.", "'{PropertyName}' prihvata samo ekstenzije: {Extensions}.");
    }

    /// <summary>
    /// Adds translation for the Swedish language.
    /// </summary>
    protected virtual void AddSwedishLanguageTranslation()
    {
        AddTranslations("sv", "'{PropertyName}' är inte ett giltigt telefonnummer.", "'{PropertyName}' är inte en giltig URL.", "'{PropertyName}' måste ha mellan {MinLength} och {MaxLength} objekt. Du angav {TotalLength} objekt.", "'{PropertyName}' måste ha {MaxLength} objekt. Du angav {TotalLength} objekt.", "'{PropertyName}' måste ha minst {MinLength} objekt. Du angav {TotalLength} objekt.", "'{PropertyName}' får ha högst {MaxLength} objekt. Du angav {TotalLength} objekt.", "'{PropertyName}' måste vara ett av följande värden: {Values}.", "'{PropertyName}' får inte vara ett av följande värden: {Values}.", "'{PropertyName}' är inte en giltig Base64-kodning.", "'{PropertyName}' accepterar endast följande filändelser: {Extensions}.");
    }

    /// <summary>
    /// Adds translation for the Turkish language.
    /// </summary>
    protected virtual void AddTurkishLanguageTranslation()
    {
        AddTranslations("tr", "'{PropertyName}' geçerli bir telefon numarası değil.", "'{PropertyName}' geçerli bir URL değil.", "'{PropertyName}' {MinLength} ile {MaxLength} arasında öğe içermelidir. {TotalLength} öğe girdiniz.", "'{PropertyName}' {MaxLength} öğe içermelidir. {TotalLength} öğe girdiniz.", "'{PropertyName}' en az {MinLength} öğe içermelidir. {TotalLength} öğe girdiniz.", "'{PropertyName}' en fazla {MaxLength} öğe içermelidir. {TotalLength} öğe girdiniz.", "'{PropertyName}' şu değerlerden biri olmalıdır: {Values}.", "'{PropertyName}' şu değerlerden biri olmamalıdır: {Values}.", "'{PropertyName}' geçerli bir Base64 kodlaması değil.", "'{PropertyName}' yalnızca şu uzantıları kabul eder: {Extensions}.");
    }

    /// <summary>
    /// Adds translation for the Ukrainian language.
    /// </summary>
    protected virtual void AddUkrainianLanguageTranslation()
    {
        AddTranslations("uk", "'{PropertyName}' не є дійсним номером телефону.", "'{PropertyName}' не є дійсною URL-адресою.", "'{PropertyName}' має містити від {MinLength} до {MaxLength} елементів. Введено: {TotalLength}.", "'{PropertyName}' має містити {MaxLength} елементів. Введено: {TotalLength}.", "'{PropertyName}' має містити щонайменше {MinLength} елементів. Введено: {TotalLength}.", "'{PropertyName}' має містити не більше {MaxLength} елементів. Введено: {TotalLength}.", "'{PropertyName}' має бути одним зі значень: {Values}.", "'{PropertyName}' не має бути одним зі значень: {Values}.", "'{PropertyName}' не є дійсним кодуванням Base64.", "'{PropertyName}' приймає лише розширення: {Extensions}.");
    }

    /// <summary>
    /// Adds translation for the Vietnamese language.
    /// </summary>
    protected virtual void AddVietnameseLanguageTranslation()
    {
        AddTranslations("vi", "'{PropertyName}' không phải là số điện thoại hợp lệ.", "'{PropertyName}' không phải là URL hợp lệ.", "'{PropertyName}' phải có từ {MinLength} đến {MaxLength} mục. Bạn đã nhập {TotalLength} mục.", "'{PropertyName}' phải có {MaxLength} mục. Bạn đã nhập {TotalLength} mục.", "'{PropertyName}' phải có ít nhất {MinLength} mục. Bạn đã nhập {TotalLength} mục.", "'{PropertyName}' chỉ được có tối đa {MaxLength} mục. Bạn đã nhập {TotalLength} mục.", "'{PropertyName}' phải là một trong các giá trị: {Values}.", "'{PropertyName}' không được là một trong các giá trị: {Values}.", "'{PropertyName}' không phải là mã hóa Base64 hợp lệ.", "'{PropertyName}' chỉ chấp nhận các phần mở rộng: {Extensions}.");
    }

    /// <summary>
    /// Adds translation for the Welsh language.
    /// </summary>
    protected virtual void AddWelshLanguageTranslation()
    {
        AddTranslations("cy", "Nid yw '{PropertyName}' yn rhif ffôn dilys.", "Nid yw '{PropertyName}' yn URL dilys.", "Rhaid i '{PropertyName}' gynnwys rhwng {MinLength} a {MaxLength} eitem. Rhoesoch {TotalLength} eitem.", "Rhaid i '{PropertyName}' gynnwys {MaxLength} eitem. Rhoesoch {TotalLength} eitem.", "Rhaid i '{PropertyName}' gynnwys o leiaf {MinLength} eitem. Rhoesoch {TotalLength} eitem.", "Gall '{PropertyName}' gynnwys dim mwy na {MaxLength} eitem. Rhoesoch {TotalLength} eitem.", "Rhaid i '{PropertyName}' fod yn un o'r gwerthoedd: {Values}.", "Ni chaiff '{PropertyName}' fod yn un o'r gwerthoedd: {Values}.", "Nid yw '{PropertyName}' yn amgodiad Base64 dilys.", "Mae '{PropertyName}' yn derbyn yr estyniadau hyn yn unig: {Extensions}.");
    }
}
