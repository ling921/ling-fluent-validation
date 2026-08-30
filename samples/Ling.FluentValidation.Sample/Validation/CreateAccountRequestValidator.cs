using FluentValidation;
using Ling.FluentValidation.Sample.Models;

namespace Ling.FluentValidation.Sample.Validation;

public sealed partial class CreateAccountRequestValidator
{
    partial void ConfigureAdditionalRules()
    {
        RuleFor(request => request.Password)
            .Must(static password => password.Any(char.IsUpper) && password.Any(char.IsDigit))
            .WithMessage("Password must contain an uppercase letter and a digit.");

        RuleFor(request => request.ShippingAddress)
            .SetValidator(new ShippingAddressValidator());
    }
}
