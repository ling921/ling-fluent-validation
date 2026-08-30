namespace Ling.FluentValidation.Annotations;

/// <summary>
/// Used to validate that a property or field is not any of the denied values.
/// <para>
/// This will generate <c>RuleFor(x => x.PropertyOrField).DeniedValues(Values)...</c>
/// </para>
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public sealed class DeniedValuesAttribute : ValidationBaseAttribute
{
    /// <summary>
    /// Gets the denied values.
    /// </summary>
    public object?[] Values { get; }

    /// <summary>
    /// Initializes a new instance.
    /// </summary>
    /// <param name="values">The denied values.</param>
    public DeniedValuesAttribute(params object?[] values)
    {
        Values = values;
    }
}
