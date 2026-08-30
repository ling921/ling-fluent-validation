namespace Ling.FluentValidation.Annotations;

/// <summary>
/// Used to validate that a property or field is equal to another property or field.
/// <para>
/// This will generate <c>RuleFor(x => x.PropertyOrField).Equal(x => x.OtherProperty)...</c>
/// </para>
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public sealed class CompareAttribute : ValidationBaseAttribute
{
    /// <summary>
    /// Gets the name of the property or field to compare with.
    /// </summary>
    public string OtherProperty { get; }

    /// <summary>
    /// Initializes a new instance.
    /// </summary>
    /// <param name="otherProperty">
    /// The name of the property or field to compare with.
    /// <para>
    /// The other property or field must be non-static, non-private and of the same type as the property or field being validated.
    /// </para>
    /// <para>
    /// It is recommended to use the <c>nameof()</c> operator to specify the name of the property or field.
    /// For example: <c>nameof(MyProperty)</c>
    /// </para>
    /// </param>
    public CompareAttribute(string otherProperty)
    {
        OtherProperty = otherProperty;
    }
}
