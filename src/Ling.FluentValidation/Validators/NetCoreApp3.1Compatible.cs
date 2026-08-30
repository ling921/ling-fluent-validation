#if NETCOREAPP3_1
#pragma warning disable IDE0060,RCS1163

using FluentValidation.Internal;
using FluentValidation.Resources;

namespace Ling.FluentValidation.Validators;

/// <summary>
/// Represents context information for a validation operation.
/// </summary>
/// <typeparam name="T">The type of the object being validated.</typeparam>
public sealed class ValidationContext<T> : global::FluentValidation.ValidationContext<T>
{
    /// <summary>
    /// Gets the message formatter.
    /// </summary>
    internal MessageFormatter MessageFormatter { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationContext{T}"/> class.
    /// </summary>
    /// <param name="instanceToValidate"></param>
    public ValidationContext(T instanceToValidate) : base(instanceToValidate)
    {
        MessageFormatter = ValidatorOptions.Global.MessageFormatterFactory();
    }
}

/// <summary>
/// Represents a validator that checks if a property is valid.
/// <para>
/// This class is only used for netcoreapp3.1 and is compatible with subsequent FluentValidation versions.
/// </para>
/// </summary>
/// <typeparam name="T">The type of the object being validated.</typeparam>
/// <typeparam name="TProperty">The type of the property being validated.</typeparam>
public abstract class PropertyValidator<T, TProperty> : PropertyValidator
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PropertyValidator{T, TProperty}"/> class.
    /// </summary>
    protected PropertyValidator() : base(string.Empty)
    {
    }

    /// <summary>
    /// The name of the validator. This is usually the type name without any generic parameters. This is used as the default Error Code for the validator.
    /// </summary>
    public abstract string Name { get; }

    /// <summary>
    /// Validates a specific property value.
    /// </summary>
    /// <param name="context">The validation context. The parent object can be obtained from here.</param>
    /// <param name="value">The current property value to validate</param>
    /// <returns>True if valid, otherwise false.</returns>
    public abstract bool IsValid(ValidationContext<T> context, TProperty value);

    /// <inheritdoc/>
    protected override bool IsValid(PropertyValidatorContext context)
    {
        var value = context.PropertyValue is TProperty v ? v : default;
        var context2 = new ValidationContext<T>((T)context.InstanceToValidate);

        var result = IsValid(context2, value!);
        foreach (var (name, text) in context2.MessageFormatter.PlaceholderValues)
        {
            context.MessageFormatter.AppendArgument(name, text);
        }

        if (Options.ErrorMessageSource is not StaticStringSource stringSource ||
            string.IsNullOrWhiteSpace(stringSource.GetString(context)))
        {
            Options.ErrorMessageSource = new LanguageStringSource(ctx => Options.ErrorCodeSource?.GetString(ctx), Name);
        }

        return result;
    }

    /// <summary>
    /// Returns the default error message template for this validator, when not overridden.
    /// </summary>
    /// <param name="errorCode">The currently configured error code for the validator.</param>
    /// <returns>The default error message template.</returns>
    protected virtual string GetDefaultMessageTemplate(string errorCode)
    {
        return Localized(errorCode, Name);
    }

    /// <summary>
    /// Retrieves a localized string from the LanguageManager.
    /// If an ErrorCode is defined for this validator, the error code is used as the key.
    /// If no ErrorCode is defined (or the language manager doesn't have a translation for the error code)
    /// then the fallback key is used instead.
    /// </summary>
    /// <param name="errorCode">The currently configured error code for the validator.</param>
    /// <param name="fallbackKey">The fallback key to use for translation, if no ErrorCode is available.</param>
    /// <returns>The translated error message template.</returns>
    protected string Localized(string errorCode, string fallbackKey)
    {
        return fallbackKey;
    }
}
#pragma warning restore IDE0060,RCS1163
#endif
