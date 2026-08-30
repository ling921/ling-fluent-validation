namespace Ling.FluentValidation.Annotations;

/// <summary>
/// Used to validate that a property or field is enum value.
/// <para>
/// This will generate <c>RuleFor(x => x.PropertyOrField).IsInEnum()...</c>
/// </para>
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public sealed class EnumAttribute : ValidationBaseAttribute;
