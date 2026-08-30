using Ling.FluentValidation.CodeFixes.Infrastructure;

namespace Ling.FluentValidation.Test.CodeFixesTests.Infrastructure;

public sealed class SyntaxNodeHelperTests
{
    private readonly SyntaxTree _syntaxTree = CSharpSyntaxTree.ParseText("""
        using Ling.FluentValidation.Annotations;

        namespace TestProject
        {
            public class Test
            {
            }
        }
        """);

    [Theory]
    [InlineData("Test", false)]
    [InlineData("Ling.FluentValidation.Annotations", true)]
    public void IsUsingNamespace_Test(string name, bool expected)
    {
        Assert.Equal(expected, _syntaxTree.GetRoot().IsUsingNamespace(name));
    }

    [Theory]
    [InlineData("Test", "Test")]
    [InlineData("Ling.FluentValidation.Annotations.Test", "Test")]
    [InlineData("Ling.FluentValidation.Annotations.TestAttribute", "Test")]
    public void CreateNameSyntax_Test(string name, string expected)
    {
        var nameSyntax = _syntaxTree.GetRoot().CreateNameSyntax(name);

        Assert.Equal(expected, nameSyntax.ToString());
    }
}
