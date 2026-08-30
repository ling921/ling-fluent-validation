namespace Ling.FluentValidation.Test.ValidatorsTests;

public sealed class DeniedValuesValidatorTests : ValidatorTestBase
{
    [Theory]
    [InlineData(new int[] { 1, 2, 3 }, 1, false)]
    [InlineData(new int[] { 1, 2, 3 }, 0, true)]
    [InlineData(new int[] { 1, 2, 3 }, 4, true)]
    [InlineData(new int[0], 1, true)]
    public void Is_Valid_Test(int[] deniedValues, int number, bool excepted)
    {
        var testInstance = new TestClass
        {
            Number = number
        };

        var validator = new DeniedValuesValidator<TestClass, int>(deniedValues);
        var context = new ValidationContext(testInstance);
        var result = validator.IsValid(context, number);

        Assert.Equal(excepted, result);
    }

    [Fact]
    public override void Default_Message_Test()
    {
        Instance.Number = 1;
        Validator.RuleFor(x => x.Number)
            .DeniedValues([1, 2, 3]);

        var result = Validator.Validate(Instance);

        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal("'Number' equals one of the following values: 1, 2, 3.", result.Errors[0].ErrorMessage);
    }

    [Fact]
    public override void Custom_Message_Test()
    {
        Instance.Number = 1;
        Validator.RuleFor(x => x.Number)
            .DeniedValues([1, 2, 3])
            .WithMessage("'{PropertyName}' is not valid. Not allowed values are: {Values}.");

        var result = Validator.Validate(Instance);

        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal("'Number' is not valid. Not allowed values are: 1, 2, 3.", result.Errors[0].ErrorMessage);
    }
}
