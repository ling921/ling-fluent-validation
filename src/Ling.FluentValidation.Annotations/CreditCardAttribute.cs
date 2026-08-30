namespace Ling.FluentValidation.Annotations;

/// <summary>
/// Used to validate that a property or field is credit card.
/// <para>
/// This will generate <c>RuleFor(x => x.PropertyOrField).CreditCard()...</c>
/// </para>
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public sealed class CreditCardAttribute : ValidationBaseAttribute;
