namespace Ling.FluentValidation.Test.Common;

public sealed class TestClass
{
    public string? PhoneNumber { get; set; }
    public string? Url { get; set; }
    public string? Base64Text { get; set; }
    public string? FileName { get; set; }
    public int Number { get; set; }
    public int[]? Numbers { get; set; }
}

public sealed class TestClassValidator : AbstractValidator<TestClass>;
