namespace Ling.FluentValidation.Annotations;

/// <summary>
/// Used to validate that a property or field is url.
/// <para>
/// This will generate <c>RuleFor(x => x.PropertyOrField).Url()...</c>
/// </para>
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public sealed class UrlAttribute : ValidationBaseAttribute;
