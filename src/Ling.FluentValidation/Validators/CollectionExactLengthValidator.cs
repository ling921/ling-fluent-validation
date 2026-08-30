namespace Ling.FluentValidation.Validators;

/// <summary>
/// Represents a validator that checks for an exact length of a collection.
/// </summary>
/// <typeparam name="T">The type of the object being validated.</typeparam>
/// <typeparam name="TProperty">The type of the property being validated.</typeparam>
public class CollectionExactLengthValidator<T, TProperty> : CollectionLengthValidator<T, TProperty>
    where TProperty : IEnumerable
{
    /// <inheritdoc/>
    public override string Name => "Ling_CollectionExactLengthValidator";

    /// <summary>
    /// Initializes a new instance with the specified exact length.
    /// </summary>
    /// <param name="length">The exact length allowed.</param>
    public CollectionExactLengthValidator(int length) : base(length, length)
    {
    }
}
