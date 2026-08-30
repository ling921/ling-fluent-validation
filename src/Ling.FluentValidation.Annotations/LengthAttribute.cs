namespace Ling.FluentValidation.Annotations;

/// <summary>
/// Used to validate that length of a <c>string</c> or <c>IEnumerable&lt;T&gt;</c> property is in range.
/// <para>
/// This will generate <c>RuleFor(x => x.PropertyOrField).Length(min, max)...</c>
/// </para>
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public sealed class LengthAttribute : ValidationBaseAttribute
{
    /// <summary>
    /// Gets the minimum length.
    /// </summary>
    public int Minimum { get; }

    /// <summary>
    /// Gets the maximum length.
    /// </summary>
    public int Maximum { get; }

    /// <summary>
    /// Initializes a new instance.
    /// </summary>
    /// <param name="min">The minimum length.</param>
    /// <param name="max">The maximum length.</param>
    public LengthAttribute(int min, int max)
    {
        Minimum = min;
        Maximum = max;
    }

    /// <summary>
    /// Initializes a new instance.
    /// </summary>
    /// <param name="exactLength">The exact length.</param>
    public LengthAttribute(int exactLength)
    {
        Minimum = exactLength;
        Maximum = exactLength;
    }
}

/// <summary>
/// Used to validate that length of a <c>string</c> or <c>IEnumerable&lt;T&gt;</c> property is less or equal to maximum.
/// <para>
/// This will generate RuleFor(x => x.PropertyOrField).MaximumLength(max)...
/// </para>
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public sealed class MaximumLengthAttribute : ValidationBaseAttribute
{
    /// <summary>
    /// Gets the maximum length.
    /// </summary>
    public int MaxLength { get; }

    /// <summary>
    /// Initializes a new instance.
    /// </summary>
    /// <param name="maxLength">The maximum length.</param>
    public MaximumLengthAttribute(int maxLength)
    {
        MaxLength = maxLength;
    }
}

/// <summary>
/// Used to validate that length of a <c>string</c> or <c>IEnumerable&lt;T&gt;</c> property is greater or equal to minimum.
/// <para>
/// This will generate RuleFor(x => x.PropertyOrField).MinimumLength(min)...
/// </para>
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public sealed class MinimumLengthAttribute : ValidationBaseAttribute
{
    /// <summary>
    /// Gets the minimum length.
    /// </summary>
    public int MinLength { get; }

    /// <summary>
    /// Initializes a new instance.
    /// </summary>
    /// <param name="minLength">The minimum length.</param>
    public MinimumLengthAttribute(int minLength)
    {
        MinLength = minLength;
    }
}
