namespace Ling.FluentValidation.Test.ValidatorsTests;

public sealed class FileExtensionsValidatorTests : ValidatorTestBase
{
    [Theory]
    [InlineData(null, "png,jpg,jpeg,gif", true)]
    [InlineData("", "png,jpg,jpeg,gif", false)]
    [InlineData("sample.jpg", "png,jpg,jpeg,gif", true)]
    [InlineData("sample.jpg", "png, jpg, jpeg, gif", true)]
    [InlineData("sample.JPG", ".png, .jpg, .jpeg, .gif", true)]
    [InlineData("sample.cs", "png,jpg,jpeg,gif", false)]
    public void Is_Valid_Test(string? fileName, string extensions, bool excepted)
    {
        var testInstance = new TestClass
        {
            FileName = fileName
        };

        var validator = new FileExtensionsValidator<TestClass>(extensions);
        var context = new ValidationContext(testInstance);
        var result = validator.IsValid(context, fileName);

        Assert.Equal(excepted, result);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("png,,jpg")]
    [InlineData("png, ")]
    public void Invalid_Extensions_Throw(string extensions)
    {
        Assert.Throws<ArgumentException>(() => new FileExtensionsValidator<TestClass>(extensions));
    }

    [Fact]
    public override void Default_Message_Test()
    {
        Instance.FileName = "sample.cs";
        Validator.RuleFor(x => x.FileName)
            .FileExtensions("png,jpg,jpeg,gif");

        var result = Validator.Validate(Instance);

        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal("'File Name' only accepts files with the following extensions: .png, .jpg, .jpeg, .gif.", result.Errors[0].ErrorMessage);
    }

    [Fact]
    public override void Custom_Message_Test()
    {
        Instance.FileName = "sample.cs";
        Validator.RuleFor(x => x.FileName)
            .FileExtensions("png,jpg,jpeg,gif")
            .WithMessage("'{PropertyName}' is not support, supported extensions are: {Extensions}.");

        var result = Validator.Validate(Instance);

        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal("'File Name' is not support, supported extensions are: .png, .jpg, .jpeg, .gif.", result.Errors[0].ErrorMessage);
    }
}
