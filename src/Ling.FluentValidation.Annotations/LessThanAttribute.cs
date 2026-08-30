namespace Ling.FluentValidation.Annotations;

/// <summary>
/// Used to validate that a property or field is less than a value.
/// <para>
/// This will generate <c>RuleFor(x => x.PropertyOrField).LessThan(value)...</c>
/// </para>
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public sealed class LessThanAttribute : CompareValidationBaseAttribute
{
    /// <inheritdoc />
    public LessThanAttribute(int value) : base(value) { }

    /// <inheritdoc />
    public LessThanAttribute(long value) : base(value) { }

    /// <inheritdoc />
    public LessThanAttribute(uint value) : base(value) { }

    /// <inheritdoc />
    public LessThanAttribute(ulong value) : base(value) { }

    /// <inheritdoc />
    public LessThanAttribute(byte value) : base(value) { }

    /// <inheritdoc />
    public LessThanAttribute(sbyte value) : base(value) { }

    /// <inheritdoc />
    public LessThanAttribute(float value) : base(value) { }

    /// <inheritdoc />
    public LessThanAttribute(double value) : base(value) { }

    /// <inheritdoc />
    public LessThanAttribute(bool value) : base(value) { }

    /// <inheritdoc />
    public LessThanAttribute(string value) : base(value) { }

    /// <inheritdoc />
    public LessThanAttribute(object value) : base(value) { }
}
