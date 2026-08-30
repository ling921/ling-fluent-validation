namespace Ling.FluentValidation.Validators;

/// <summary>
/// Represents a validator that checks for a minimum length of a collection.
/// </summary>
/// <typeparam name="T">The type of the object being validated.</typeparam>
/// <typeparam name="TProperty">The type of the property being validated.</typeparam>
public class CollectionMinimumLengthValidator<T, TProperty> : CollectionLengthValidator<T, TProperty>
    where TProperty : IEnumerable
{
    /// <inheritdoc/>
    public override string Name => "Ling_CollectionMinimumLengthValidator";

    /// <summary>
    /// Initializes a new instance with the specified minimum length.
    /// </summary>
    /// <param name="min">The minimum allowed length.</param>
    public CollectionMinimumLengthValidator(int min) : base(min, -1)
    {
    }
}
