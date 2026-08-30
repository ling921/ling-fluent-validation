namespace Ling.FluentValidation.Validators;

/// <summary>
/// Represents a validator that checks if a string is a valid phone number.
/// <para>
/// The verification rule is consistent with 'System.ComponentModel.DataAnnotations.PhoneAttribute'.
/// </para>
/// </summary>
/// <typeparam name="T">The type of the object being validated.</typeparam>
public class PhoneValidator<T> : PropertyValidator<T, string?>
{
    /// <inheritdoc/>
    public override string Name => "Ling_PhoneValidator";

    /// <inheritdoc/>
    public override bool IsValid(ValidationContext<T> context, string? value)
    {
        if (value is null)
        {
            return true;
        }

#if NET5_0_OR_GREATER
        var potentialPhoneNumber = value!.Replace("+", string.Empty).AsSpan().TrimEnd();
#else
        var potentialPhoneNumber = value!.Replace("+", string.Empty).TrimEnd();
#endif
        potentialPhoneNumber = RemoveExtension(potentialPhoneNumber);
        bool flag = false;
        var readOnlySpan = potentialPhoneNumber;
        for (int i = 0; i < readOnlySpan.Length; i++)
        {
            char c = readOnlySpan[i];
            if (char.IsDigit(c))
            {
                flag = true;
                break;
            }
        }
        if (!flag)
        {
            return false;
        }
        var readOnlySpan2 = potentialPhoneNumber;
        for (int j = 0; j < readOnlySpan2.Length; j++)
        {
            char c2 = readOnlySpan2[j];
            if (!char.IsDigit(c2) && !char.IsWhiteSpace(c2) && !"-.()".Contains(c2))
            {
                return false;
            }
        }
        return true;
    }

    /// <inheritdoc/>
    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        LingValidatorOptions.RegisterTranslations();
        return Localized(errorCode, Name);
    }

#if NET5_0_OR_GREATER
    private static ReadOnlySpan<char> RemoveExtension(ReadOnlySpan<char> potentialPhoneNumber)
#else
    private static string RemoveExtension(string potentialPhoneNumber)
#endif
    {
        int num = potentialPhoneNumber.LastIndexOf("ext.", StringComparison.OrdinalIgnoreCase);
        if (num >= 0)
        {
            var potentialExtension = potentialPhoneNumber[(num + "ext.".Length)..];
            if (MatchesExtension(potentialExtension))
            {
                return potentialPhoneNumber[..num];
            }
        }
        num = potentialPhoneNumber.LastIndexOf("ext", StringComparison.OrdinalIgnoreCase);
        if (num >= 0)
        {
            var potentialExtension2 = potentialPhoneNumber[(num + "ext".Length)..];
            if (MatchesExtension(potentialExtension2))
            {
                return potentialPhoneNumber[..num];
            }
        }
        num = potentialPhoneNumber.LastIndexOf("x", StringComparison.OrdinalIgnoreCase);
        if (num >= 0)
        {
            var potentialExtension3 = potentialPhoneNumber[(num + "x".Length)..];
            if (MatchesExtension(potentialExtension3))
            {
                return potentialPhoneNumber[..num];
            }
        }
        return potentialPhoneNumber;
    }

#if NET5_0_OR_GREATER
    private static bool MatchesExtension(ReadOnlySpan<char> potentialExtension)
#else
    private static bool MatchesExtension(string potentialExtension)
#endif
    {
        potentialExtension = potentialExtension.TrimStart();
        if (potentialExtension.Length == 0)
        {
            return false;
        }
        var readOnlySpan = potentialExtension;
        for (int i = 0; i < readOnlySpan.Length; i++)
        {
            char c = readOnlySpan[i];
            if (!char.IsDigit(c))
            {
                return false;
            }
        }
        return true;
    }
}
