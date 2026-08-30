namespace Ling.FluentValidation.Test.Common;

public abstract class ValidatorTestBase
{
    protected readonly TestClass Instance = new();
    protected readonly TestClassValidator Validator = new();
    protected ValidationContext ValidationContext => new(Instance);

    protected ValidatorTestBase()
    {
        ValidatorOptions.Global.LanguageManager = new LanguageManager
        {
            Culture = new CultureInfo("en-US")
        };
    }

    public abstract void Default_Message_Test();
    public abstract void Custom_Message_Test();
}
