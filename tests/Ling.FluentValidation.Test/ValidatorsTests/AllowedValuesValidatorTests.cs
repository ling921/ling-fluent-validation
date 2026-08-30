namespace Ling.FluentValidation.Test.ValidatorsTests;

public sealed class AllowedValuesValidatorTests : ValidatorTestBase
{
    [Theory]
    [InlineData(new int[] { 1, 2, 3 }, 1, true)]
    [InlineData(new int[] { 1, 2, 3 }, 0, false)]
    [InlineData(new int[] { 1, 2, 3 }, 4, false)]
    public void Is_Valid_Test(int[] allowedValues, int number, bool excepted)
    {
        var testInstance = new TestClass
        {
            Number = number
        };

        var validator = new AllowedValuesValidator<TestClass, int>(allowedValues);
        var context = new ValidationContext(testInstance);
        var result = validator.IsValid(context, number);

        Assert.Equal(excepted, result);
    }

    [Fact]
    public void Empty_Allowed_Values_Throws()
    {
        Assert.Throws<ArgumentException>(() =>
            new AllowedValuesValidator<TestClass, int>([]));
    }

    [Fact]
    public override void Default_Message_Test()
    {
        Instance.Number = 0;
        Validator.RuleFor(x => x.Number)
            .AllowedValues([1, 2, 3]);

        var result = Validator.Validate(Instance);

        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal("'Number' does not equal any of the following values: 1, 2, 3.", result.Errors[0].ErrorMessage);
    }

    [Fact]
    public override void Custom_Message_Test()
    {
        Instance.Number = 0;
        Validator.RuleFor(x => x.Number)
            .AllowedValues([1, 2, 3])
            .WithMessage("'{PropertyName}' is not valid. Allowed values are: {Values}.");

        var result = Validator.Validate(Instance);

        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal("'Number' is not valid. Allowed values are: 1, 2, 3.", result.Errors[0].ErrorMessage);
    }
}
