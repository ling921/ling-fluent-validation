namespace Ling.FluentValidation.Annotations;

/// <summary>
/// Marks a class or record for validator generation.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public sealed class GenerateValidatorAttribute : Attribute;
