namespace Ling.FluentValidation;

/// <summary>
/// Extension methods that provide the default set of validators.
/// </summary>
public static class IRuleBuilderExtensions
{
    static IRuleBuilderExtensions()
    {
        LingValidatorOptions.RegisterTranslations();
    }

    /// <summary>
	/// Defines a phone number validator on the current rule builder for string properties.
	/// Validation will fail if the value returned by the lambda is not a valid phone number.
	/// </summary>
	/// <typeparam name="T">Type of object being validated.</typeparam>
	/// <param name="ruleBuilder">The rule builder on which the validator should be defined.</param>
	/// <returns>The same rule builder with the phone number validator applied.</returns>
	public static IRuleBuilderOptions<T, string?> Phone<T>(this IRuleBuilder<T, string?> ruleBuilder)
    {
        var validator = new PhoneValidator<T>();
        return ruleBuilder.SetValidator(validator);
    }

    /// <summary>
	/// Defines a URL validator on the current rule builder for string properties.
	/// Validation will fail if the value returned by the lambda is not a valid URL.
	/// </summary>
	/// <typeparam name="T">Type of object being validated.</typeparam>
	/// <param name="ruleBuilder">The rule builder on which the validator should be defined.</param>
	/// <returns>The same rule builder with the URL validator applied.</returns>
	public static IRuleBuilderOptions<T, string?> Url<T>(this IRuleBuilder<T, string?> ruleBuilder)
    {
        var validator = new UrlValidator<T>();
        return ruleBuilder.SetValidator(validator);
    }

    /// <summary>
    /// Defines a length validator on the current rule builder for collection properties.
    /// Validation will fail if the collection length is outside the specified range.
    /// </summary>
    /// <typeparam name="T">Type of object being validated.</typeparam>
    /// <typeparam name="TProperty">Type of property being validated.</typeparam>
    /// <param name="ruleBuilder">The rule builder on which the validator should be defined.</param>
    /// <param name="min">The minimum allowed length.</param>
    /// <param name="max">The maximum allowed length.</param>
    /// <returns>The same rule builder with the length validator applied.</returns>
    public static IRuleBuilderOptions<T, TProperty?> Length<T, TProperty>(
        this IRuleBuilder<T, TProperty?> ruleBuilder,
        int min,
        int max)
        where TProperty : IEnumerable
    {
        var validator = new CollectionLengthValidator<T, TProperty>(min, max);
        return ruleBuilder.SetValidator(validator);
    }

    /// <summary>
    /// Defines an exact length validator on the current rule builder for collection properties.
    /// Validation will fail if the collection length is not exactly the specified length.
    /// </summary>
    /// <typeparam name="T">Type of object being validated.</typeparam>
    /// <typeparam name="TProperty">Type of property being validated.</typeparam>
    /// <param name="ruleBuilder">The rule builder on which the validator should be defined.</param>
    /// <param name="length">The exact length expected for the collection.</param>
    /// <returns>The same rule builder with the exact length validator applied.</returns>
    public static IRuleBuilderOptions<T, TProperty?> ExactLength<T, TProperty>(
        this IRuleBuilder<T, TProperty?> ruleBuilder,
        int length)
        where TProperty : IEnumerable
    {
        var validator = new CollectionExactLengthValidator<T, TProperty>(length);
        return ruleBuilder.SetValidator(validator);
    }

    /// <summary>
    /// Defines a maximum length validator on the current rule builder for collection properties.
    /// Validation will fail if the collection length exceeds the specified maximum length.
    /// </summary>
    /// <typeparam name="T">Type of object being validated.</typeparam>
    /// <typeparam name="TProperty">Type of property being validated.</typeparam>
    /// <param name="ruleBuilder">The rule builder on which the validator should be defined.</param>
    /// <param name="length">The maximum allowed length.</param>
    /// <returns>The same rule builder with the maximum length validator applied.</returns>
    public static IRuleBuilderOptions<T, TProperty?> MaximumLength<T, TProperty>(
        this IRuleBuilder<T, TProperty?> ruleBuilder,
        int length)
        where TProperty : IEnumerable
    {
        var validator = new CollectionMaximumLengthValidator<T, TProperty>(length);
        return ruleBuilder.SetValidator(validator);
    }

    /// <summary>
    /// Defines a minimum length validator on the current rule builder for collection properties.
    /// Validation will fail if the collection length is less than the specified minimum length.
    /// </summary>
    /// <typeparam name="T">Type of object being validated.</typeparam>
    /// <typeparam name="TProperty">Type of property being validated.</typeparam>
    /// <param name="ruleBuilder">The rule builder on which the validator should be defined.</param>
    /// <param name="length">The minimum allowed length.</param>
    /// <returns>The same rule builder with the minimum length validator applied.</returns>
    public static IRuleBuilderOptions<T, TProperty?> MinimumLength<T, TProperty>(
        this IRuleBuilder<T, TProperty?> ruleBuilder,
        int length)
        where TProperty : IEnumerable
    {
        var validator = new CollectionMinimumLengthValidator<T, TProperty>(length);
        return ruleBuilder.SetValidator(validator);
    }

    /// <summary>
    /// Defines an allowed values validator on the current rule builder.
	/// Validation will fail if the value returned by the lambda is not one of the allowed values.
    /// </summary>
    /// <typeparam name="T">Type of object being validated.</typeparam>
    /// <typeparam name="TProperty">Type of property being validated.</typeparam>
    /// <param name="ruleBuilder">The rule builder on which the validator should be defined.</param>
    /// <param name="values">Allowed values.</param>
    /// <returns>The same rule builder with the allowed values validator applied.</returns>
    public static IRuleBuilderOptions<T, TProperty?> AllowedValues<T, TProperty>(
        this IRuleBuilder<T, TProperty?> ruleBuilder,
        params TProperty[] values)
    {
        var validator = new AllowedValuesValidator<T, TProperty>(values);
        return ruleBuilder.SetValidator(validator);
    }

    /// <summary>
    /// Defines an allowed values validator on the current rule builder.
	/// Validation will fail if the value returned by the lambda is not one of the allowed values.
    /// </summary>
    /// <typeparam name="T">Type of object being validated.</typeparam>
    /// <typeparam name="TProperty">Type of property being validated.</typeparam>
    /// <param name="ruleBuilder">The rule builder on which the validator should be defined.</param>
    /// <param name="values">Allowed values.</param>
    /// <param name="comparer">Comparer used to compare values.</param>
    /// <returns>The same rule builder with the allowed values validator applied.</returns>
    public static IRuleBuilderOptions<T, TProperty?> AllowedValues<T, TProperty>(
        this IRuleBuilder<T, TProperty?> ruleBuilder,
        IEnumerable<TProperty> values,
        IEqualityComparer<TProperty?>? comparer = null)
    {
        var validator = new AllowedValuesValidator<T, TProperty>(values, comparer);
        return ruleBuilder.SetValidator(validator);
    }

    /// <summary>
    /// Defines a disallowed values validator on the current rule builder.
	/// Validation will fail if the value returned by the lambda is one of the allowed values.
    /// </summary>
    /// <typeparam name="T">Type of object being validated.</typeparam>
    /// <typeparam name="TProperty">Type of property being validated.</typeparam>
    /// <param name="ruleBuilder">The rule builder on which the validator should be defined.</param>
    /// <param name="values">Allowed values.</param>
    /// <returns>The same rule builder with the disallowed values validator applied.</returns>
    public static IRuleBuilderOptions<T, TProperty?> DeniedValues<T, TProperty>(
        this IRuleBuilder<T, TProperty?> ruleBuilder,
        params TProperty[] values)
    {
        var validator = new DeniedValuesValidator<T, TProperty>(values);
        return ruleBuilder.SetValidator(validator);
    }

    /// <summary>
    /// Defines a disallowed values validator on the current rule builder.
	/// Validation will fail if the value returned by the lambda is one of the allowed values.
    /// </summary>
    /// <typeparam name="T">Type of object being validated.</typeparam>
    /// <typeparam name="TProperty">Type of property being validated.</typeparam>
    /// <param name="ruleBuilder">The rule builder on which the validator should be defined.</param>
    /// <param name="values">Allowed values.</param>
    /// <param name="comparer">Comparer used to compare values.</param>
    /// <returns>The same rule builder with the disallowed values validator applied.</returns>
    public static IRuleBuilderOptions<T, TProperty?> DeniedValues<T, TProperty>(
        this IRuleBuilder<T, TProperty?> ruleBuilder,
        IEnumerable<TProperty> values,
        IEqualityComparer<TProperty?>? comparer = null)
    {
        var validator = new DeniedValuesValidator<T, TProperty>(values, comparer);
        return ruleBuilder.SetValidator(validator);
    }

    /// <summary>
    /// Defines a base64 string validator on the current rule builder.
    /// Validation will fail if the value returned by the lambda is not a valid base64 string.
    /// </summary>
    /// <typeparam name="T">Type of object being validated.</typeparam>
    /// <param name="ruleBuilder">The rule builder on which the validator should be defined.</param>
    /// <param name="supportUnpadded">Whether the validator supports base64 strings without trailing padding.</param>
    /// <returns>The same rule builder with the base64 string validator applied.</returns>
    public static IRuleBuilderOptions<T, string?> Base64String<T>(
        this IRuleBuilder<T, string?> ruleBuilder,
        bool supportUnpadded = false)
    {
        var validator = new Base64StringValidator<T>(supportUnpadded);
        return ruleBuilder.SetValidator(validator);
    }

    /// <summary>
    /// Defines a file extensions validator on the current rule builder.
    /// Validation will fail if the file name returned by the lambda doesn't have any of the allowed extensions.
    /// </summary>
    /// <typeparam name="T">Type of object being validated.</typeparam>
    /// <param name="ruleBuilder">The rule builder on which the validator should be defined.</param>
    /// <param name="extensions">
    /// The allowed extensions, separated by comma.
    /// <para>
    /// For example: '<c>png,jpg,jpeg,gif</c>'
    /// </para>
    /// </param>
    /// <returns>The same rule builder with the file extensions validator applied.</returns>
    public static IRuleBuilderOptions<T, string?> FileExtensions<T>(
        this IRuleBuilder<T, string?> ruleBuilder,
        string extensions)
    {
        var validator = new FileExtensionsValidator<T>(extensions);
        return ruleBuilder.SetValidator(validator);
    }
}
