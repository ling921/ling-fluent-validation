using FluentValidation;
using Ling.FluentValidation.Sample.Models;

namespace Ling.FluentValidation.Sample.Validation;

public sealed class ShippingAddressValidator : AbstractValidator<ShippingAddress>
{
    public ShippingAddressValidator()
    {
        RuleFor(address => address.CountryCode)
            .NotEmpty()
            .Length(2)
            .WithMessage("Country code must contain two characters.");

        RuleFor(address => address.PostalCode)
            .NotEmpty()
            .MaximumLength(12);
    }
}
