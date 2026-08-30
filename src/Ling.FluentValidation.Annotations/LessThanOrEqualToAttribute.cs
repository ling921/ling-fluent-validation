namespace Ling.FluentValidation.Annotations;

/// <summary>
/// Used to validate that a property or field is less than or equal to a value.
/// <para>
/// This will generate <c>RuleFor(x => x.PropertyOrField).LessThanOrEqualTo(value)...</c>
/// </para>
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public sealed class LessThanOrEqualToAttribute : CompareValidationBaseAttribute
{
    /// <inheritdoc />
    public LessThanOrEqualToAttribute(int value) : base(value) { }

    /// <inheritdoc />
    public LessThanOrEqualToAttribute(long value) : base(value) { }

    /// <inheritdoc />
    public LessThanOrEqualToAttribute(uint value) : base(value) { }

    /// <inheritdoc />
    public LessThanOrEqualToAttribute(ulong value) : base(value) { }

    /// <inheritdoc />
    public LessThanOrEqualToAttribute(byte value) : base(value) { }

    /// <inheritdoc />
    public LessThanOrEqualToAttribute(sbyte value) : base(value) { }

    /// <inheritdoc />
    public LessThanOrEqualToAttribute(float value) : base(value) { }

    /// <inheritdoc />
    public LessThanOrEqualToAttribute(double value) : base(value) { }

    /// <inheritdoc />
    public LessThanOrEqualToAttribute(bool value) : base(value) { }

    /// <inheritdoc />
    public LessThanOrEqualToAttribute(string value) : base(value) { }

    /// <inheritdoc />
    public LessThanOrEqualToAttribute(object value) : base(value) { }
}
