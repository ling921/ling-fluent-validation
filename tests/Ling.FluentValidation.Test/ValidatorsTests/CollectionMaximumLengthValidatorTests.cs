namespace Ling.FluentValidation.Test.ValidatorsTests;

public sealed class CollectionMaximumLengthValidatorTests : ValidatorTestBase
{
    [Theory]
    [InlineData(new int[] { 1, 2, 3 }, 3, true)]
    [InlineData(new int[] { 1, 2, 3 }, 1, false)]
    [InlineData(new int[] { 1, 2, 3 }, 2, false)]
    [InlineData(new int[] { 1, 2, 3 }, 4, true)]
    [InlineData(null, 10, true)]
    [InlineData(new int[0], 0, true)]
    [InlineData(new int[0], 1, true)]
    public void Is_Valid_Test(int[]? values, int max, bool excepted)
    {
        var testInstance = new TestClass
        {
            Numbers = values
        };

        var validator = new CollectionMaximumLengthValidator<TestClass, int[]>(max);
        var context = new ValidationContext(testInstance);
        var result = validator.IsValid(context, values);

        Assert.Equal(excepted, result);
    }

    [Fact]
    public override void Default_Message_Test()
    {
        Instance.Numbers = [1, 2, 3];
        Validator.RuleFor(x => x.Numbers)
            .MaximumLength(2);

        var result = Validator.Validate(Instance);

        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal("'Numbers' must have 2 items or fewer. You entered 3 items.", result.Errors[0].ErrorMessage);
    }

    [Fact]
    public override void Custom_Message_Test()
    {
        Instance.Numbers = [1, 2, 3];
        Validator.RuleFor(x => x.Numbers)
            .MaximumLength(2)
            .WithMessage("'{PropertyName}' is not valid. It must have no more than {MaxLength} items. You entered {TotalLength} items.");

        var result = Validator.Validate(Instance);

        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal("'Numbers' is not valid. It must have no more than 2 items. You entered 3 items.", result.Errors[0].ErrorMessage);
    }
}
