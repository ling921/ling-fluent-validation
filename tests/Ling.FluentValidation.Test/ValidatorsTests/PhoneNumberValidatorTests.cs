namespace Ling.FluentValidation.Test.ValidatorsTests;

public sealed class PhoneNumberValidatorTests : ValidatorTestBase
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("1234567890")]
    [InlineData("123-456-7890")]
    [InlineData("(123) 456-7890")]
    [InlineData("+1 123-456-7890")]
    [InlineData("+86 1234567890")]
    [InlineData("+44 (0)1234 567 890")]
    [InlineData("123-456-ABCD")]
    [InlineData("123-456-789@")]
    [InlineData("phone123")]
    [InlineData("abcdef")]
    [InlineData("12--34")]
    [InlineData("123(456")]
    [InlineData("++123")]
    public void Is_Valid_Test(string? phoneNumber)
    {
        var testInstance = new TestClass
        {
            PhoneNumber = phoneNumber
        };

        var context = new ValidationContext(testInstance);
        var result = new PhoneValidator<TestClass>().IsValid(context, phoneNumber);
        var msResult = new PhoneAttribute().IsValid(phoneNumber);

        Assert.Equal(msResult, result);
    }

    [Fact]
    public override void Default_Message_Test()
    {
        Instance.PhoneNumber = "abcdef";
        Validator.RuleFor(x => x.PhoneNumber)
            .Phone();

        var result = Validator.Validate(Instance);

        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal("'Phone Number' is not a valid phone number.", result.Errors[0].ErrorMessage);
    }

    [Fact]
    public override void Custom_Message_Test()
    {
        Instance.PhoneNumber = "abcdef";
        Validator.RuleFor(x => x.PhoneNumber)
            .Phone()
            .WithMessage("'{PropertyName}' is not valid.");

        var result = Validator.Validate(Instance);

        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal("'Phone Number' is not valid.", result.Errors[0].ErrorMessage);
    }
}
