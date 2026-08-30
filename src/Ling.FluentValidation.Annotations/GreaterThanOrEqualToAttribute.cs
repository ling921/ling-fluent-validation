namespace Ling.FluentValidation.Annotations;

/// <summary>
/// Used to validate that a property or field is greater than or equal to a value.
/// <para>
/// This will generate <c>RuleFor(x => x.PropertyOrField).GreaterThanOrEqualTo(value)...</c>
/// </para>
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public sealed class GreaterThanOrEqualToAttribute : CompareValidationBaseAttribute
{
    /// <inheritdoc />
    public GreaterThanOrEqualToAttribute(int value) : base(value) { }

    /// <inheritdoc />
    public GreaterThanOrEqualToAttribute(long value) : base(value) { }

    /// <inheritdoc />
    public GreaterThanOrEqualToAttribute(uint value) : base(value) { }

    /// <inheritdoc />
    public GreaterThanOrEqualToAttribute(ulong value) : base(value) { }

    /// <inheritdoc />
    public GreaterThanOrEqualToAttribute(byte value) : base(value) { }

    /// <inheritdoc />
    public GreaterThanOrEqualToAttribute(sbyte value) : base(value) { }

    /// <inheritdoc />
    public GreaterThanOrEqualToAttribute(float value) : base(value) { }

    /// <inheritdoc />
    public GreaterThanOrEqualToAttribute(double value) : base(value) { }

    /// <inheritdoc />
    public GreaterThanOrEqualToAttribute(bool value) : base(value) { }

    /// <inheritdoc />
    public GreaterThanOrEqualToAttribute(string value) : base(value) { }

    /// <inheritdoc />
    public GreaterThanOrEqualToAttribute(object value) : base(value) { }
}
