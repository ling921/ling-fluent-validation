namespace Ling.FluentValidation.Test.ValidatorsTests;

public sealed class CollectionLengthValidatorTests : ValidatorTestBase
{
    [Theory]
    [InlineData(new int[] { 1, 2, 3 }, 0, 4, true)]
    [InlineData(new int[] { 1, 2, 3 }, 1, 3, true)]
    [InlineData(new int[] { 1, 2, 3 }, 1, 2, false)]
    [InlineData(new int[] { 1, 2, 3 }, 2, 2, false)]
    [InlineData(new int[] { 1, 2, 3 }, 4, 5, false)]
    [InlineData(null, 10, 20, true)]
    [InlineData(new int[0], 10, 20, false)]
    public void Is_Valid_Test(int[]? values, int min, int max, bool excepted)
    {
        var testInstance = new TestClass
        {
            Numbers = values
        };

        var validator = new CollectionLengthValidator<TestClass, int[]>(min, max);
        var context = new ValidationContext(testInstance);
        var result = validator.IsValid(context, values);

        Assert.Equal(excepted, result);
    }

    [Fact]
    public override void Default_Message_Test()
    {
        Instance.Numbers = [1, 2, 3];
        Validator.RuleFor(x => x.Numbers)
            .Length(1, 2);

        var result = Validator.Validate(Instance);

        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal("'Numbers' must have 1-2 items. You entered 3 items.", result.Errors[0].ErrorMessage);
    }

    [Fact]
    public override void Custom_Message_Test()
    {
        Instance.Numbers = [1, 2, 3];
        Validator.RuleFor(x => x.Numbers)
            .Length(1, 2)
            .WithMessage("'{PropertyName}' is not valid. It must have {MinLength}-{MaxLength} items. You entered {TotalLength} items.");

        var result = Validator.Validate(Instance);

        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal("'Numbers' is not valid. It must have 1-2 items. You entered 3 items.", result.Errors[0].ErrorMessage);
    }
}
