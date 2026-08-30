namespace Ling.FluentValidation.Validators;

/// <summary>
/// Represents a validator that checks if a string is a valid base64 string.
/// <para>
/// The leading and trailing whitespaces do not affect the validation result,
/// which is the same as the Base64StringAttribute verification behavior in .NET 8.0+.
/// </para>
/// </summary>
/// <typeparam name="T">The type of the object being validated.</typeparam>
public class Base64StringValidator<T> : PropertyValidator<T, string?>
{
    /// <summary>
    /// Gets a value indicating whether the validator supports base64 strings without trailing padding.
    /// </summary>
    public bool SupportUnpadded { get; }

    /// <inheritdoc/>
    public override string Name => "Ling_Base64StringValidator";

    /// <summary>
    /// Initializes a new instance.
    /// </summary>
    /// <param name="supportUnpadded">Whether the validator supports base64 strings without trailing padding.</param>
    public Base64StringValidator(bool supportUnpadded = false)
    {
        SupportUnpadded = supportUnpadded;
    }

    /// <inheritdoc/>
    public override bool IsValid(ValidationContext<T> context, string? value)
    {
        if (value is null)
        {
            return true;
        }
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        value = value.Trim();
#if NETSTANDARD2_0
        if (SupportUnpadded && !value.EndsWith("="))
#else
        if (SupportUnpadded && !value.EndsWith('='))
#endif
        {
            value = UnpaddingBase64Text(value);
        }

#if NET8_0_OR_GREATER
        return System.Buffers.Text.Base64.IsValid(value);
#else
        return IsValidBase64Text(value);
#endif
    }

    /// <inheritdoc/>
    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        LingValidatorOptions.RegisterTranslations();
        return Localized(errorCode, Name);
    }

    /// <summary>
    /// Unpadding base64 text.
    /// </summary>
    /// <param name="value">The base64 text.</param>
    /// <returns>The unpadded base64 text.</returns>
    protected static string UnpaddingBase64Text(string value)
    {
        var padding = value.Length % 4;
        return padding == 0 ? value : value.PadRight(value.Length + (4 - padding), '=');
    }

#if !NET8_0_OR_GREATER
    /// <summary>
    /// Checks if a string is a valid base64 text.
    /// </summary>
    /// <param name="value">The base64 text.</param>
    /// <returns><see langword="true"/> if the base64 text is valid; otherwise, <see langword="false"/>.</returns>
    protected static bool IsValidBase64Text(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return false;
        }

        if (value.Length % 4 != 0)
        {
            return false;
        }

        try
        {
            _ = Convert.FromBase64String(value);

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
#endif
}
