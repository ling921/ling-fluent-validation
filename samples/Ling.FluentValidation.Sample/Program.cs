using FluentValidation;
using Ling.FluentValidation.Sample;
using Ling.FluentValidation.Sample.Models;
using Ling.FluentValidation.Sample.Validation;
using Microsoft.Extensions.DependencyInjection;

var invalidRequest = new CreateAccountRequest
{
    Email = "not-an-email",
    UserName = "x!",
    Password = "weak-password",
    ConfirmPassword = "different-password",
    Tier = (AccountTier)99,
    TierName = "Enterprise",
    PreferredCulture = "fr-FR",
    AvatarFileName = "avatar.exe",
    ShippingAddress = new ShippingAddress(),
};

var validRequest = new CreateAccountRequest
{
    Email = "user@example.com",
    UserName = "sample.user",
    Password = "StrongPassword1",
    ConfirmPassword = "StrongPassword1",
    Tier = AccountTier.Professional,
    TierName = nameof(AccountTier.Professional),
    PreferredCulture = "zh-CN",
    AvatarFileName = "avatar.png",
    ShippingAddress = new ShippingAddress
    {
        CountryCode = "CN",
        PostalCode = "518000",
    },
};

var services = new ServiceCollection();
services.AddGeneratedValidators();
await using var serviceProvider = services.BuildServiceProvider();
var validator = serviceProvider.GetRequiredService<IValidator<CreateAccountRequest>>();
var generatedValidatorTypes = GeneratedValidatorRegistry.ValidatorTypes;

Console.WriteLine($"Generated validators: {generatedValidatorTypes.Count}");
Console.WriteLine();

await PrintValidationResultAsync("Invalid request", validator, invalidRequest);
await PrintValidationResultAsync("Valid request", validator, validRequest);

static async Task PrintValidationResultAsync(
    string title,
    IValidator<CreateAccountRequest> validator,
    CreateAccountRequest request)
{
    var result = await validator.ValidateAsync(request);

    Console.WriteLine($"{title}: {(result.IsValid ? "valid" : "invalid")}");
    foreach (var error in result.Errors)
    {
        Console.WriteLine($"  {error.PropertyName}: {error.ErrorMessage}");
    }

    Console.WriteLine();
}
