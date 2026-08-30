namespace Ling.FluentValidation.Sample.Models;

public sealed class ShippingAddress
{
    public string CountryCode { get; init; } = string.Empty;

    public string PostalCode { get; init; } = string.Empty;
}
