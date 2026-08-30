namespace Ling.FluentValidation.Annotations;

/// <summary>
/// Selects a referenced assembly whose marked DTOs should have validators generated in the current assembly.
/// </summary>
[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
public sealed class GenerateValidatorsFromAssemblyContainingAttribute : Attribute
{
    /// <summary>Gets a type from the source assembly.</summary>
    public Type MarkerType { get; }

    /// <summary>Initializes the attribute with a type from the source assembly.</summary>
    public GenerateValidatorsFromAssemblyContainingAttribute(Type markerType)
    {
        MarkerType = markerType;
    }
}

#if NET7_0_OR_GREATER

/// <summary>
/// Selects a referenced assembly without requiring a <c>typeof</c> expression.
/// </summary>
[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
public sealed class GenerateValidatorsFromAssemblyContainingAttribute<TMarker> : Attribute;

#endif
