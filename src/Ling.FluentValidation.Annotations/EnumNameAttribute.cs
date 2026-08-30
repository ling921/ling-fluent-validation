namespace Ling.FluentValidation.Annotations;

/// <summary>
/// Used to validate that a property or field is enum name.
/// <para>
/// This will generate <c>RuleFor(x => x.PropertyOrField).IsEnumName(enumType, caseSensitive)...</c>
/// </para>
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public sealed class EnumNameAttribute : ValidationBaseAttribute
{
    /// <summary>
    /// Gets the enum type.
    /// </summary>
    public Type EnumType { get; }

    /// <summary>
    /// Gets or sets a value indicating whether the enum name is case sensitive.
    /// </summary>
    public bool CaseSensitive { get; set; } = true;

    /// <summary>
    /// Initializes a new instance.
    /// </summary>
    /// <param name="enumType">The enum type.</param>
    public EnumNameAttribute(Type enumType)
    {
        EnumType = enumType;
    }
}

#if NET7_0_OR_GREATER

/// <summary>
/// Used to validate that a property or field is enum name.
/// <para>
/// This will generate RuleFor(x => x.PropertyOrField).IsEnumName(enumType, caseSensitive)...
/// </para>
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public sealed class EnumNameAttribute<TEnum> : ValidationBaseAttribute where TEnum : struct, Enum
{
    /// <summary>
    /// Gets or sets a value indicating whether the enum name is case sensitive.
    /// </summary>
    public bool CaseSensitive { get; set; } = true;
}

#endif
