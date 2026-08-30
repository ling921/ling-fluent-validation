namespace Ling.FluentValidation.Validators;

/// <summary>
/// Represents a validator that checks if a value is one of the allowed values.
/// </summary>
/// <typeparam name="T">The type of the object being validated.</typeparam>
/// <typeparam name="TProperty">The type of the property being validated.</typeparam>
public class AllowedValuesValidator<T, TProperty> : PropertyValidator<T, TProperty?>
{
    /// <inheritdoc/>
    public override string Name => "Ling_AllowedValuesValidator";

    /// <summary>
    /// Gets the allowed values.
    /// </summary>
    protected virtual TProperty[] AllowedValues { get; }

    /// <summary>
    /// Gets the value equality comparer.
    /// </summary>
    protected virtual IEqualityComparer<TProperty?>? Comparer { get; }

    /// <summary>
    /// Initializes a new instance with the specified allowed values.
    /// </summary>
    /// <param name="values">Allowed values.</param>
    /// <param name="comparer">The comparer used to compare values. Defaults to <see cref="EqualityComparer{T}.Default"/>.</param>
    public AllowedValuesValidator(IEnumerable<TProperty> values, IEqualityComparer<TProperty?>? comparer = null)
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(values);
#else
        if (values is null)
        {
            throw new ArgumentNullException(nameof(values));
        }
#endif

        AllowedValues = values is TProperty[] array ? array : values.ToArray();
        if (AllowedValues.Length == 0)
        {
            throw new ArgumentException("At least one allowed value is required.", nameof(values));
        }
        Comparer = comparer;
    }

    /// <inheritdoc/>
    public override bool IsValid(ValidationContext<T> context, TProperty? value)
    {
        if (value is null || AllowedValues.Contains(value, Comparer ?? EqualityComparer<TProperty?>.Default))
        {
            return true;
        }

        context.MessageFormatter.AppendArgument("Values", string.Join(", ", AllowedValues));

        return false;
    }

    /// <inheritdoc/>
    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return Localized(errorCode, Name);
    }
}
