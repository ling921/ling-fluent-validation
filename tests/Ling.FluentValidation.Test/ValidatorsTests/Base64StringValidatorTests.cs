namespace Ling.FluentValidation.Test.ValidatorsTests;

public sealed class Base64StringValidatorTests : ValidatorTestBase
{
    [Theory]
    [InlineData(null, false, true)]
    [InlineData(null, true, true)]
    [InlineData("", false, false)]
    [InlineData("", true, false)]
    [InlineData("  ", false, false)]
    [InlineData("  ", true, false)]
    [InlineData("Rmx1ZW50VmFsaWRhdGlvbg==", false, true)]
    [InlineData("Rmx1ZW50VmFsaWRhdGlvbg==", true, true)]
    [InlineData("Rmx1ZW50VmFsaWRhdGlvbg=", false, false)]
    [InlineData("Rmx1ZW50VmFsaWRhdGlvbg=", true, false)]
    [InlineData("Rmx1ZW50VmFsaWRhdGlvbg", false, false)]
    [InlineData("Rmx1ZW50VmFsaWRhdGlvbg", true, true)]
    [InlineData("Rmx1ZW50VmFsaWRhdGlvbg== ", false, true)]
    [InlineData(" Rmx1ZW50VmFsaWRhdGlvbg==", false, true)]
    public void Is_Valid_Test(string? base64Text, bool supportUnpadded, bool excepted)
    {
        var testInstance = new TestClass
        {
            Base64Text = base64Text
        };

        var validator = new Base64StringValidator<TestClass>(supportUnpadded);
        var context = new ValidationContext(testInstance);
        var result = validator.IsValid(context, base64Text);

        Assert.Equal(excepted, result);
    }

    [Fact]
    public override void Default_Message_Test()
    {
        Instance.Base64Text = string.Empty;
        Validator.RuleFor(x => x.Base64Text)
            .Base64String();

        var result = Validator.Validate(Instance);

        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal("'Base64 Text' is not a valid Base64 encoding.", result.Errors[0].ErrorMessage);
    }

    [Fact]
    public override void Custom_Message_Test()
    {
        Instance.Base64Text = string.Empty;
        Validator.RuleFor(x => x.Base64Text)
            .Base64String()
            .WithMessage("'{PropertyName}' is not valid, only base64 string is supported.");

        var result = Validator.Validate(Instance);

        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal("'Base64 Text' is not valid, only base64 string is supported.", result.Errors[0].ErrorMessage);
    }
}
