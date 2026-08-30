namespace Ling.FluentValidation.Annotations;

/// <summary>
/// Used to validate that a property or field is between two values, inclusive minimum and maximum.
/// <para>
/// This will generate <c>RuleFor(x => x.PropertyOrField).InclusiveBetween(minValue, maxValue)...</c>
/// </para>
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public sealed class InclusiveBetweenAttribute : ValidationBaseAttribute
{
    /// <summary>
    /// The inclusive minimum value.
    /// </summary>
    public object MinValue { get; }

    /// <summary>
    /// The inclusive maximum value.
    /// </summary>
    public object MaxValue { get; }

    /// <inheritdoc cref="InclusiveBetweenAttribute(object, object)" />
    public InclusiveBetweenAttribute(int minValue, int maxValue)
    {
        MinValue = minValue;
        MaxValue = maxValue;
    }

    /// <inheritdoc cref="InclusiveBetweenAttribute(object, object)" />
    public InclusiveBetweenAttribute(long minValue, long maxValue)
    {
        MinValue = minValue;
        MaxValue = maxValue;
    }

    /// <inheritdoc cref="InclusiveBetweenAttribute(object, object)" />
    public InclusiveBetweenAttribute(uint minValue, uint maxValue)
    {
        MinValue = minValue;
        MaxValue = maxValue;
    }

    /// <inheritdoc cref="InclusiveBetweenAttribute(object, object)" />
    public InclusiveBetweenAttribute(ulong minValue, ulong maxValue)
    {
        MinValue = minValue;
        MaxValue = maxValue;
    }

    /// <inheritdoc cref="InclusiveBetweenAttribute(object, object)" />
    public InclusiveBetweenAttribute(byte minValue, byte maxValue)
    {
        MinValue = minValue;
        MaxValue = maxValue;
    }

    /// <inheritdoc cref="InclusiveBetweenAttribute(object, object)" />
    public InclusiveBetweenAttribute(sbyte minValue, sbyte maxValue)
    {
        MinValue = minValue;
        MaxValue = maxValue;
    }

    /// <inheritdoc cref="InclusiveBetweenAttribute(object, object)" />
    public InclusiveBetweenAttribute(float minValue, float maxValue)
    {
        MinValue = minValue;
        MaxValue = maxValue;
    }

    /// <inheritdoc cref="InclusiveBetweenAttribute(object, object)" />
    public InclusiveBetweenAttribute(double minValue, double maxValue)
    {
        MinValue = minValue;
        MaxValue = maxValue;
    }

    /// <inheritdoc cref="InclusiveBetweenAttribute(object, object)" />
    public InclusiveBetweenAttribute(bool minValue, bool maxValue)
    {
        MinValue = minValue;
        MaxValue = maxValue;
    }

    /// <inheritdoc cref="InclusiveBetweenAttribute(object, object)" />
    public InclusiveBetweenAttribute(string minValue, string maxValue)
    {
        MinValue = minValue;
        MaxValue = maxValue;
    }

    /// <summary>
    /// Initializes a new instance.
    /// </summary>
    /// <param name="minValue">The inclusive minimum value.</param>
    /// <param name="maxValue">The inclusive maximum value.</param>
    public InclusiveBetweenAttribute(object minValue, object maxValue)
    {
        MinValue = minValue;
        MaxValue = maxValue;
    }
}
