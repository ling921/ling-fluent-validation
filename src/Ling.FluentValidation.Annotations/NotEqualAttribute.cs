namespace Ling.FluentValidation.Annotations;

/// <summary>
/// Used to validate that a property or field is not equals to a value.
/// <para>
/// This will generate <c>RuleFor(x => x.PropertyOrField).NotEqual(value)...</c>
/// </para>
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public sealed class NotEqualAttribute : CompareValidationBaseAttribute
{
    /// <inheritdoc />
    public NotEqualAttribute(int value) : base(value) { }

    /// <inheritdoc />
    public NotEqualAttribute(long value) : base(value) { }

    /// <inheritdoc />
    public NotEqualAttribute(uint value) : base(value) { }

    /// <inheritdoc />
    public NotEqualAttribute(ulong value) : base(value) { }

    /// <inheritdoc />
    public NotEqualAttribute(byte value) : base(value) { }

    /// <inheritdoc />
    public NotEqualAttribute(sbyte value) : base(value) { }

    /// <inheritdoc />
    public NotEqualAttribute(float value) : base(value) { }

    /// <inheritdoc />
    public NotEqualAttribute(double value) : base(value) { }

    /// <inheritdoc />
    public NotEqualAttribute(bool value) : base(value) { }

    /// <inheritdoc />
    public NotEqualAttribute(string value) : base(value) { }

    /// <inheritdoc />
    public NotEqualAttribute(object value) : base(value) { }
}
