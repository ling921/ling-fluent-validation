namespace Ling.FluentValidation.Annotations;

/// <summary>
/// Used to validate that a property or field is phone number.
/// <para>
/// This will generate <c>RuleFor(x => x.PropertyOrField).Phone()...</c>
/// </para>
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public sealed class PhoneAttribute : ValidationBaseAttribute;
