namespace Ling.FluentValidation.Annotations;

/// <summary>
/// Represents the base class for validation attributes.
/// </summary>
public abstract class ValidationBaseAttribute : Attribute
{
    /// <summary>
    /// Gets or inits the custom property name to use within the error message.
    /// </summary>
    public string? PropertyName { get; init; }

    /// <summary>
    /// Gets or inits the custom error message to use when validation fails. Only applies to the rule that directly precedes it.
    /// </summary>
    public virtual string? ErrorMessage { get; init; }

    /// <summary>
    /// Gets or inits the custom error code to use if validation fails.
    /// </summary>
    public string? ErrorCode { get; init; }

    /// <summary>
    /// Gets or inits the custom severity that should be stored alongside the validation message when validation fails for this rule.
    /// </summary>
    public ValidationSeverity Severity { get; init; }

    /// <summary>
    /// Gets or inits the custom when clause to whether this rule should be applied or not.
    /// <para>
    /// For example:
    /// <code>
    /// public class User
    /// {
    ///     [Length(6, 12, When = nameof(IsNotSpecialName))]
    ///     public string Name { get; set; }
    ///
    ///     internal bool IsNotSpecialName()
    ///     {
    ///         return Name != "admin";
    ///     }
    /// }
    /// </code>
    /// When clause can be accessable field, property or method which return <c>bool</c>, or with <c>!</c> operator.
    /// </para>
    /// </summary>
    public string? When { get; init; }
}

/// <summary>
/// Represents the base class for validation attributes for comparison.
/// </summary>
public abstract class CompareValidationBaseAttribute : ValidationBaseAttribute
{
    /// <summary>
    /// Gets the value to compare.
    /// </summary>
    public object Value { get; }

    /// <inheritdoc cref="CompareValidationBaseAttribute(object)"/>
    protected CompareValidationBaseAttribute(int value)
    {
        Value = value;
    }

    /// <inheritdoc cref="CompareValidationBaseAttribute(object)"/>
    protected CompareValidationBaseAttribute(long value)
    {
        Value = value;
    }

    /// <inheritdoc cref="CompareValidationBaseAttribute(object)"/>
    protected CompareValidationBaseAttribute(uint value)
    {
        Value = value;
    }

    /// <inheritdoc cref="CompareValidationBaseAttribute(object)"/>
    protected CompareValidationBaseAttribute(ulong value)
    {
        Value = value;
    }

    /// <inheritdoc cref="CompareValidationBaseAttribute(object)"/>
    protected CompareValidationBaseAttribute(byte value)
    {
        Value = value;
    }

    /// <inheritdoc cref="CompareValidationBaseAttribute(object)"/>
    protected CompareValidationBaseAttribute(sbyte value)
    {
        Value = value;
    }

    /// <inheritdoc cref="CompareValidationBaseAttribute(object)"/>
    protected CompareValidationBaseAttribute(float value)
    {
        Value = value;
    }

    /// <inheritdoc cref="CompareValidationBaseAttribute(object)"/>
    protected CompareValidationBaseAttribute(double value)
    {
        Value = value;
    }

    /// <inheritdoc cref="CompareValidationBaseAttribute(object)"/>
    protected CompareValidationBaseAttribute(bool value)
    {
        Value = value;
    }

    /// <inheritdoc cref="CompareValidationBaseAttribute(object)"/>
    protected CompareValidationBaseAttribute(string value)
    {
        Value = value;
    }

    /// <summary>
    /// Initializes a new instance.
    /// </summary>
    /// <param name="value">The value to compare.</param>
    protected CompareValidationBaseAttribute(object value)
    {
        Value = value;
    }
}
