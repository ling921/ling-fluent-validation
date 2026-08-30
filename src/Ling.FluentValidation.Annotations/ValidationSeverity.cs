namespace Ling.FluentValidation.Annotations;

/// <summary>A FluentValidation-independent validation severity.</summary>
public enum ValidationSeverity
{
    /// <summary>An error.</summary>
    Error,

    /// <summary>A warning.</summary>
    Warning,

    /// <summary>Informational validation output.</summary>
    Info,
}
