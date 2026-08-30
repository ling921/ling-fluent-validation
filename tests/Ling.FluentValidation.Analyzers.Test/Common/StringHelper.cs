namespace Ling.FluentValidation.Test.Common;

internal static class StringHelper
{
    public static string EnsureEndsWith(this string input, string suffix)
    {
        return input.EndsWith(suffix) ? input : input + suffix;
    }
}
