using Ling.FluentValidation.Generators.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Text.RegularExpressions;

namespace Ling.FluentValidation.Generators.Models;

internal abstract class ValidationAttributeInfoBase
{
    public string MemberName { get; }
    public AttributeData AttributeData { get; }

    protected TypedConstant this[int index] => index < AttributeData.ConstructorArguments.Length
        ? AttributeData.ConstructorArguments[index]
        : default;

    protected TypedConstant this[string key] => AttributeData.NamedArguments.FirstOrDefault(x => x.Key == key).Value;

    protected ValidationAttributeInfoBase(string memberName, AttributeData attributeData)
    {
        MemberName = memberName;
        AttributeData = attributeData;
    }

    public void AppendTo(CodeBuilder builder)
    {
        builder.AppendFormatLine("RuleFor(x => x.{0})", EscapeIdentifier(MemberName));
        builder.IncreaseIndentLevel();
        AppendRuleTo(builder);
        AppendExtraTo(builder);
        builder.AppendLine(";");
        builder.DecreaseIndentLevel();
    }

    protected abstract void AppendRuleTo(CodeBuilder builder);
    protected abstract void AppendExtraTo(CodeBuilder builder);

    protected static string EscapeIdentifier(string identifier)
    {
        return SyntaxFacts.GetKeywordKind(identifier) != SyntaxKind.None ||
               SyntaxFacts.GetContextualKeywordKind(identifier) != SyntaxKind.None
            ? "@" + identifier
            : identifier;
    }

    protected string ResolveArrayValues(string values)
    {
        if (values.StartsWith("{"))
        {
            return Regex.Replace(values, @"^\{{1,2}(.*?)\}{1,2}$", "$1");
        }
        return values;
    }
}
