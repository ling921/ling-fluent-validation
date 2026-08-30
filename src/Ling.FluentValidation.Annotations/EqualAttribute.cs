namespace Ling.FluentValidation.Annotations;

/// <summary>
/// Used to validate that a property or field is equals to a value.
/// <para>
/// This will generate <c>RuleFor(x => x.PropertyOrField).Equal(value)...</c>
/// </para>
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public sealed class EqualAttribute : CompareValidationBaseAttribute
{
    /// <inheritdoc/>
    public EqualAttribute(int value) : base(value) { }

    /// <inheritdoc/>
    public EqualAttribute(long value) : base(value) { }

    /// <inheritdoc/>
    public EqualAttribute(uint value) : base(value) { }

    /// <inheritdoc/>
    public EqualAttribute(ulong value) : base(value) { }

    /// <inheritdoc/>
    public EqualAttribute(byte value) : base(value) { }

    /// <inheritdoc/>
    public EqualAttribute(sbyte value) : base(value) { }

    /// <inheritdoc/>
    public EqualAttribute(float value) : base(value) { }

    /// <inheritdoc/>
    public EqualAttribute(double value) : base(value) { }

    /// <inheritdoc/>
    public EqualAttribute(bool value) : base(value) { }

    /// <inheritdoc/>
    public EqualAttribute(string value) : base(value) { }

    /// <inheritdoc/>
    public EqualAttribute(object value) : base(value) { }
}
