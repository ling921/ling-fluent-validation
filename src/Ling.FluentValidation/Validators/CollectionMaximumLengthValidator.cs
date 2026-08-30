namespace Ling.FluentValidation.Validators;

/// <summary>
/// Represents a validator that checks for a maximum length of a collection.
/// </summary>
/// <typeparam name="T">The type of the object being validated.</typeparam>
/// <typeparam name="TProperty">The type of the property being validated.</typeparam>
public class CollectionMaximumLengthValidator<T, TProperty> : CollectionLengthValidator<T, TProperty>
    where TProperty : IEnumerable
{
    /// <inheritdoc/>
    public override string Name => "Ling_CollectionMaximumLengthValidator";

    /// <summary>
    /// Initializes a new instance with the specified maximum length.
    /// </summary>
    /// <param name="max">The maximum allowed length.</param>
    public CollectionMaximumLengthValidator(int max) : base(0, max)
    {
    }
}
