namespace Ling.FluentValidation.Annotations;

/// <summary>
/// Used to validate that a property or field is between two values, exclusive minimum and maximum.
/// <para>
/// This will generate <c>RuleFor(x => x.PropertyOrField).ExclusiveBetween(minValue, maxValue)...</c>
/// </para>
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public sealed class ExclusiveBetweenAttribute : ValidationBaseAttribute
{
    /// <summary>
    /// The exclusive minimum value.
    /// </summary>
    public object MinValue { get; }

    /// <summary>
    /// The exclusive maximum value.
    /// </summary>
    public object MaxValue { get; }

    /// <inheritdoc cref="ExclusiveBetweenAttribute(object, object)" />
    public ExclusiveBetweenAttribute(int minValue, int maxValue)
    {
        MinValue = minValue;
        MaxValue = maxValue;
    }

    /// <inheritdoc cref="ExclusiveBetweenAttribute(object, object)" />
    public ExclusiveBetweenAttribute(long minValue, long maxValue)
    {
        MinValue = minValue;
        MaxValue = maxValue;
    }

    /// <inheritdoc cref="ExclusiveBetweenAttribute(object, object)" />
    public ExclusiveBetweenAttribute(uint minValue, uint maxValue)
    {
        MinValue = minValue;
        MaxValue = maxValue;
    }

    /// <inheritdoc cref="ExclusiveBetweenAttribute(object, object)" />
    public ExclusiveBetweenAttribute(ulong minValue, ulong maxValue)
    {
        MinValue = minValue;
        MaxValue = maxValue;
    }

    /// <inheritdoc cref="ExclusiveBetweenAttribute(object, object)" />
    public ExclusiveBetweenAttribute(byte minValue, byte maxValue)
    {
        MinValue = minValue;
        MaxValue = maxValue;
    }

    /// <inheritdoc cref="ExclusiveBetweenAttribute(object, object)" />
    public ExclusiveBetweenAttribute(sbyte minValue, sbyte maxValue)
    {
        MinValue = minValue;
        MaxValue = maxValue;
    }

    /// <inheritdoc cref="ExclusiveBetweenAttribute(object, object)" />
    public ExclusiveBetweenAttribute(float minValue, float maxValue)
    {
        MinValue = minValue;
        MaxValue = maxValue;
    }

    /// <inheritdoc cref="ExclusiveBetweenAttribute(object, object)" />
    public ExclusiveBetweenAttribute(double minValue, double maxValue)
    {
        MinValue = minValue;
        MaxValue = maxValue;
    }

    /// <inheritdoc cref="ExclusiveBetweenAttribute(object, object)" />
    public ExclusiveBetweenAttribute(bool minValue, bool maxValue)
    {
        MinValue = minValue;
        MaxValue = maxValue;
    }

    /// <inheritdoc cref="ExclusiveBetweenAttribute(object, object)" />
    public ExclusiveBetweenAttribute(string minValue, string maxValue)
    {
        MinValue = minValue;
        MaxValue = maxValue;
    }

    /// <summary>
    /// Initializes a new instance.
    /// </summary>
    /// <param name="minValue">The exclusive minimum value.</param>
    /// <param name="maxValue">The exclusive maximum value.</param>
    public ExclusiveBetweenAttribute(object minValue, object maxValue)
    {
        MinValue = minValue;
        MaxValue = maxValue;
    }
}
