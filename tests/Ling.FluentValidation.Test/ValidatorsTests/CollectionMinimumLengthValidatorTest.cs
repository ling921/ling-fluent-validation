namespace Ling.FluentValidation.Test.ValidatorsTests;

public sealed class CollectionMinimumLengthValidatorTest : ValidatorTestBase
{
    [Theory]
    [InlineData(new int[] { 1, 2, 3 }, 3, true)]
    [InlineData(new int[] { 1, 2, 3 }, 1, true)]
    [InlineData(new int[] { 1, 2, 3 }, 2, true)]
    [InlineData(new int[] { 1, 2, 3 }, 4, false)]
    [InlineData(null, 10, true)]
    [InlineData(new int[0], 0, true)]
    [InlineData(new int[0], 1, false)]
    public void Is_Valid_Test(int[]? values, int min, bool excepted)
    {
        var testInstance = new TestClass
        {
            Numbers = values
        };

        var validator = new CollectionMinimumLengthValidator<TestClass, int[]>(min);
        var context = new ValidationContext(testInstance);
        var result = validator.IsValid(context, values);

        Assert.Equal(excepted, result);
    }

    [Fact]
    public override void Default_Message_Test()
    {
        Instance.Numbers = [1, 2, 3];
        Validator.RuleFor(x => x.Numbers)
            .MinimumLength(4);

        var result = Validator.Validate(Instance);

        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal("'Numbers' must have at least 4 items. You entered 3 items.", result.Errors[0].ErrorMessage);
    }

    [Fact]
    public override void Custom_Message_Test()
    {
        Instance.Numbers = [1, 2, 3];
        Validator.RuleFor(x => x.Numbers)
            .MinimumLength(4)
            .WithMessage("'{PropertyName}' is not valid. It must have no less than {MinLength} items. You entered {TotalLength} items.");

        var result = Validator.Validate(Instance);

        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal("'Numbers' is not valid. It must have no less than 4 items. You entered 3 items.", result.Errors[0].ErrorMessage);
    }
}
