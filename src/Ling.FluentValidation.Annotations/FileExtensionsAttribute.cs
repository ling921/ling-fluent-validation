namespace Ling.FluentValidation.Annotations;

/// <summary>
/// Used to validate that a file name extension is one of the allowed extensions.
/// <para>
/// The extesions default to '<c>png,jpg,jpeg,gif</c>',
/// which is the same as the FileExtensionsAttribute verification behavior in .NET 8.0+.
/// </para>
/// <para>
/// This will generate <c>RuleFor(x => x.PropertyOrField).FileExtensions(Extensions)...</c>
/// </para>
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public sealed class FileExtensionsAttribute : ValidationBaseAttribute
{
    private readonly string? _extensions;

    /// <summary>
    /// Gets the allowed extensions.
    /// </summary>
    public string Extensions => string.IsNullOrWhiteSpace(_extensions) ? "png,jpg,jpeg,gif" : _extensions!;

    /// <summary>
    /// Initializes a new instance.
    /// </summary>
    public FileExtensionsAttribute()
    {
    }

    /// <summary>
    /// Initializes a new instance with specified extensions.
    /// </summary>
    /// <param name="extensions">
    /// The allowed extensions, separated by comma.
    /// <para>
    /// For example: '<c>png,jpg,jpeg,gif</c>'
    /// </para>
    /// </param>
    public FileExtensionsAttribute(string extensions)
    {
        _extensions = extensions;
    }
}
