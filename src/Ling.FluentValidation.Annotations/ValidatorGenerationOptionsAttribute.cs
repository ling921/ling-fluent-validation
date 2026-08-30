namespace Ling.FluentValidation.Annotations;

/// <summary>
/// Configures validators generated into the current assembly.
/// </summary>
[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
public sealed class ValidatorGenerationOptionsAttribute : Attribute
{
    /// <summary>Gets or sets an override namespace for every generated validator.</summary>
    public string? Namespace { get; init; }

    /// <summary>Gets or sets generated validator visibility.</summary>
    public GeneratedValidatorVisibility Visibility { get; init; } = GeneratedValidatorVisibility.Internal;

    /// <summary>Gets or sets whether generated validators are sealed.</summary>
    public bool IsSealed { get; init; } = true;

    /// <summary>Gets or sets the class-level cascade mode.</summary>
    public ValidationCascadeMode ClassLevelCascadeMode { get; init; }

    /// <summary>Gets or sets the rule-level cascade mode.</summary>
    public ValidationCascadeMode RuleLevelCascadeMode { get; init; }
}

/// <summary>Generated validator visibility.</summary>
public enum GeneratedValidatorVisibility
{
    /// <summary>Generate an internal validator.</summary>
    Internal,

    /// <summary>Generate a public validator when its target type is public.</summary>
    Public,
}

/// <summary>A FluentValidation-independent cascade-mode value.</summary>
public enum ValidationCascadeMode
{
    /// <summary>Do not override FluentValidation defaults.</summary>
    Default,

    /// <summary>Continue after a validation failure.</summary>
    Continue,

    /// <summary>Stop after a validation failure.</summary>
    Stop,
}
