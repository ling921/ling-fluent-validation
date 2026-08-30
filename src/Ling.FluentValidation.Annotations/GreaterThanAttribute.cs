namespace Ling.FluentValidation.Annotations;

/// <summary>
/// Used to validate that a property or field is greater than a value.
/// <para>
/// This will generate <c>RuleFor(x => x.PropertyOrField).GreaterThan(value)...</c>
/// </para>
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public sealed class GreaterThanAttribute : CompareValidationBaseAttribute
{
    /// <inheritdoc />
    public GreaterThanAttribute(int value) : base(value) { }

    /// <inheritdoc />
    public GreaterThanAttribute(long value) : base(value) { }

    /// <inheritdoc />
    public GreaterThanAttribute(uint value) : base(value) { }

    /// <inheritdoc />
    public GreaterThanAttribute(ulong value) : base(value) { }

    /// <inheritdoc />
    public GreaterThanAttribute(byte value) : base(value) { }

    /// <inheritdoc />
    public GreaterThanAttribute(sbyte value) : base(value) { }

    /// <inheritdoc />
    public GreaterThanAttribute(float value) : base(value) { }

    /// <inheritdoc />
    public GreaterThanAttribute(double value) : base(value) { }

    /// <inheritdoc />
    public GreaterThanAttribute(bool value) : base(value) { }

    /// <inheritdoc />
    public GreaterThanAttribute(string value) : base(value) { }

    /// <inheritdoc />
    public GreaterThanAttribute(object value) : base(value) { }
}
