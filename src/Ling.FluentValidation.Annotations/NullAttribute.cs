namespace Ling.FluentValidation.Annotations;

/// <summary>
/// Used to validate that a property or field is null.
/// <para>
/// This will generate <c>RuleFor(x => x.PropertyOrField).Null()...</c>
/// </para>
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public sealed class NullAttribute : ValidationBaseAttribute;
