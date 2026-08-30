namespace Ling.FluentValidation.Test.ValidatorsTests;

public sealed class UrlValidatorTests : ValidatorTestBase
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("http://www.example.com")]
    [InlineData("https://www.example.com")]
    [InlineData("https://sub.example.com")]
    [InlineData("http://www.example.com:8080")]
    [InlineData("http://www.example.com/path")]
    [InlineData("http://www.example.com/path?query=1")]
    [InlineData("ftp://www.example.com")]
    [InlineData("http://info")]
    [InlineData("file://sample.txt")]
    [InlineData("www.example.com")]
    [InlineData("example")]
    public void Is_Valid_Test(string? url)
    {
        var testInstance = new TestClass
        {
            Url = url
        };

        var context = new ValidationContext(testInstance);
        var result = new UrlValidator<TestClass>().IsValid(context, url);
        var msResult = new UrlAttribute().IsValid(url);

        Assert.Equal(msResult, result);
    }

    [Fact]
    public override void Default_Message_Test()
    {
        Instance.Url = "example";
        Validator.RuleFor(x => x.Url)
            .Url();

        var result = Validator.Validate(Instance);

        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal("'Url' is not a valid URL.", result.Errors[0].ErrorMessage);
    }

    [Fact]
    public override void Custom_Message_Test()
    {
        Instance.Url = "example";
        Validator.RuleFor(x => x.Url)
            .Url()
            .WithMessage("'{PropertyName}' is not valid.");

        var result = Validator.Validate(Instance);

        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal("'Url' is not valid.", result.Errors[0].ErrorMessage);
    }
}
