namespace Ling.FluentValidation.Annotations;

/// <summary>
/// Used to validate that a property or field is not empty.
/// <para>
/// This will generate <c>RuleFor(x => x.PropertyOrField).NotEmpty()...</c>
/// </para>
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public sealed class NotEmptyAttribute : ValidationBaseAttribute;
