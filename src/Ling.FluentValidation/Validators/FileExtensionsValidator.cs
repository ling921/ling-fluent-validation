namespace Ling.FluentValidation.Validators;

/// <summary>
/// Represents a validator that checks if a file name string has a valid extension.
/// </summary>
/// <typeparam name="T">The type of the object being validated.</typeparam>
public class FileExtensionsValidator<T> : PropertyValidator<T, string?>
{
    private readonly HashSet<string> _extensions;
    private readonly string _extensionsFormatted;

    /// <summary>
    /// Gets the file extensions.
    /// </summary>
    public string Extensions { get; }

    /// <inheritdoc/>
    public override string Name => "Ling_FileExtensionsValidator";

    /// <summary>
    /// Initializes a new instance with the specified extensions.
    /// </summary>
    /// <param name="extensions">
    /// The allowed extensions, separated by commas.
    /// <para>
    /// For example: '<c>png,jpg,jpeg,gif</c>'
    /// </para>
    /// </param>
    /// <exception cref="ArgumentException">The value is empty or contains an empty extension.</exception>
    public FileExtensionsValidator(string extensions)
    {
        if (string.IsNullOrWhiteSpace(extensions))
        {
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(extensions));
        }

        var parsedExtensions = extensions
            .Split(',')
            .Select(extension => extension.Trim().TrimStart('.'))
            .ToArray();

        if (parsedExtensions.Any(string.IsNullOrWhiteSpace))
        {
            throw new ArgumentException("Extensions cannot contain an empty value.", nameof(extensions));
        }

        Extensions = extensions;
        _extensions = new HashSet<string>(parsedExtensions.Select(extension => "." + extension), StringComparer.OrdinalIgnoreCase);
        _extensionsFormatted = string.Join(", ", _extensions);
    }

    /// <inheritdoc/>
    public override bool IsValid(ValidationContext<T> context, string? value)
    {
        if (value is null)
        {
            return true;
        }

        if (_extensions.Contains(Path.GetExtension(value)))
        {
            return true;
        }

        context.MessageFormatter.AppendArgument("Extensions", _extensionsFormatted);

        return false;
    }

    /// <inheritdoc/>
    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        LingValidatorOptions.RegisterTranslations();
        return Localized(errorCode, Name);
    }
}
