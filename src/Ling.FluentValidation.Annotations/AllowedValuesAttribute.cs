namespace Ling.FluentValidation.Annotations;

/// <summary>
/// Used to validate that a property or field is one of the allowed values.
/// <para>
/// This will generate <c>RuleFor(x => x.PropertyOrField).AllowedValues(Values)...</c>
/// </para>
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public sealed class AllowedValuesAttribute : ValidationBaseAttribute
{
    /// <summary>
    /// Gets the allowed values.
    /// </summary>
    public object?[] Values { get; }

    /// <summary>
    /// Initializes a new instance.
    /// </summary>
    /// <param name="values">The allowed values.</param>
    public AllowedValuesAttribute(params object?[] values)
    {
        Values = values;
    }
}
