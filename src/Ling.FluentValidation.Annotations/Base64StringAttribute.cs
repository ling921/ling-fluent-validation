namespace Ling.FluentValidation.Annotations;

/// <summary>
/// Used to validate that a property or field is base64 string.
/// <para>
/// This will generate <c>RuleFor(x => x.PropertyOrField).Base64String()...</c>
/// </para>
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public sealed class Base64StringAttribute : ValidationBaseAttribute
{
    /// <summary>
    /// Gets a value indicating whether the validator supports base64 strings without trailing padding.
    /// </summary>
    public bool SupportUnpadded { get; }

    /// <summary>
    /// Initializes a new instance.
    /// </summary>
    public Base64StringAttribute()
    {
    }

    /// <summary>
    /// Initializes a new instance.
    /// </summary>
    /// <param name="supportUnpadded">Whether the validator supports base64 strings without trailing padding.</param>
    public Base64StringAttribute(bool supportUnpadded)
    {
        SupportUnpadded = supportUnpadded;
    }
}
