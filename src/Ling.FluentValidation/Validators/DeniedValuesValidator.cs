namespace Ling.FluentValidation.Validators;

/// <summary>
/// Represents a validator that checks if a value is not one of the denied values.
/// </summary>
/// <typeparam name="T">The type of the object being validated.</typeparam>
/// <typeparam name="TProperty">The type of the property being validated.</typeparam>
public class DeniedValuesValidator<T, TProperty> : PropertyValidator<T, TProperty?>
{
    /// <inheritdoc/>
    public override string Name => "Ling_DeniedValuesValidator";

    /// <summary>
    /// Gets the denied values.
    /// </summary>
    protected virtual TProperty[] DeniedValues { get; }

    /// <summary>
    /// Gets the value equality comparer.
    /// </summary>
    protected virtual IEqualityComparer<TProperty?>? Comparer { get; }

    /// <summary>
    /// Initializes a new instance with the specified denied values.
    /// </summary>
    /// <param name="values">Disallowed values.</param>
    /// <param name="comparer">The comparer used to compare values. Defaults to <see cref="EqualityComparer{T}.Default"/>.</param>
    public DeniedValuesValidator(IEnumerable<TProperty> values, IEqualityComparer<TProperty?>? comparer = null)
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(values);
#else
        if (values is null)
        {
            throw new ArgumentNullException(nameof(values));
        }
#endif

        DeniedValues = values is TProperty[] array ? array : values.ToArray();
        Comparer = comparer;
    }

    /// <inheritdoc/>
    public override bool IsValid(ValidationContext<T> context, TProperty? value)
    {
        if (value is null || !DeniedValues.Contains(value, Comparer ?? EqualityComparer<TProperty?>.Default))
        {
            return true;
        }

        context.MessageFormatter.AppendArgument("Values", string.Join(", ", DeniedValues));

        return false;
    }

    /// <inheritdoc/>
    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return Localized(errorCode, Name);
    }
}
