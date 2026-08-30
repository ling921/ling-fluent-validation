using Ling.FluentValidation.Annotations;

namespace Ling.FluentValidation.Sample.Models;

[GenerateValidator]
public sealed class CreateAccountRequest
{
    [NotEmpty]
    [EmailAddress]
    public string Email { get; init; } = string.Empty;

    [Length(3, 32)]
    [Matches("^[a-zA-Z0-9_.-]+$")]
    public string UserName { get; init; } = string.Empty;

    [Length(12, 128)]
    public string Password { get; init; } = string.Empty;

    [Compare(nameof(Password), ErrorMessage = "Passwords must match.")]
    public string ConfirmPassword { get; init; } = string.Empty;

    [Enum]
    public AccountTier Tier { get; init; }

    [EnumName<AccountTier>(CaseSensitive = false)]
    public string TierName { get; init; } = string.Empty;

    [AllowedValues("en-US", "zh-CN")]
    public string PreferredCulture { get; init; } = "en-US";

    [FileExtensions("jpg,jpeg,png")]
    public string? AvatarFileName { get; init; }

    public ShippingAddress ShippingAddress { get; init; } = new();
}
