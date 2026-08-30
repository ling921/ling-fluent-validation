namespace Ling.FluentValidation.Validators;

/// <summary>
/// Represents a validator that checks the length of a collection.
/// </summary>
/// <typeparam name="T">The type of the object being validated.</typeparam>
/// <typeparam name="TProperty">The type of the property being validated.</typeparam>
public class CollectionLengthValidator<T, TProperty> : PropertyValidator<T, TProperty?>
    where TProperty : IEnumerable
{
    /// <summary>
    /// Gets the minimum length allowed.
    /// </summary>
    protected virtual int Min { get; }

    /// <summary>
    /// Gets the maximum length allowed.
    /// </summary>
    protected virtual int Max { get; }

    /// <inheritdoc/>
    public override string Name => "Ling_CollectionLengthValidator";

    /// <summary>
    /// Initializes a new instance with the specified minimum and maximum lengths.
    /// </summary>
    /// <param name="min">The minimum allowed length.</param>
    /// <param name="max">The maximum allowed length. Use -1 for no maximum length.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the maximum length is less than the minimum length.</exception>
    public CollectionLengthValidator(int min, int max)
    {
        Min = min;
        Max = max;

        if (max != -1 && max < min)
        {
            throw new ArgumentOutOfRangeException(nameof(max), "Max should be larger than min.");
        }
    }

    /// <inheritdoc/>
    public override bool IsValid(ValidationContext<T> context, TProperty? value)
    {
        if (value is null)
        {
            return true;
        }

        var length = Count(value, Min, Max);

        if (length < Min || (length > Max && Max != -1))
        {
            context.MessageFormatter
                .AppendArgument("MinLength", Min)
                .AppendArgument("MaxLength", Max)
                .AppendArgument("TotalLength", length);

            return false;
        }

        return true;
    }

    /// <inheritdoc/>
    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return Localized(errorCode, Name);
    }

    private static int Count(TProperty value, int min, int max)
    {
        if (value is Array array)
        {
            return array.Length;
        }
        else if (value is ICollection collection)
        {
            return collection.Count;
        }

        var count = 0;
        var enumerator = value.GetEnumerator();
        try
        {
            while (enumerator.MoveNext())
            {
                count++;

                // No more elements are needed to decide the result.
                if ((max == -1 && count >= min) || (max != -1 && count > max))
                {
                    break;
                }
            }
        }
        finally
        {
            (enumerator as IDisposable)?.Dispose();
        }

        return count;
    }
}
