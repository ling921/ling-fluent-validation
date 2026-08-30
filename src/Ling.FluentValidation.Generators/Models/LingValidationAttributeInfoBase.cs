using Ling.FluentValidation.Extensions;
using Ling.FluentValidation.Generators.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Collections.Immutable;

namespace Ling.FluentValidation.Generators.Models;

internal abstract class LingValidationAttributeInfoBase(string memberName, AttributeData attributeData, ImmutableDictionary<string, MemberKind> maybeWhenClauses) : ValidationAttributeInfoBase(memberName, attributeData)
{
    public ImmutableDictionary<string, MemberKind> MaybeWhenClauses { get; } = maybeWhenClauses;

    protected override void AppendExtraTo(CodeBuilder builder)
    {
        if (this["When"].Value is string whenClauseKey)
        {
            var notSymbol = string.Empty;
            if (whenClauseKey.StartsWith("!"))
            {
                notSymbol = "!";
                whenClauseKey = whenClauseKey.Substring(1);
            }

            MaybeWhenClauses.TryGetValue(whenClauseKey, out var memberKind);
            var parentheses = memberKind switch
            {
                MemberKind.Field => string.Empty,
                MemberKind.Property => string.Empty,
                MemberKind.Method => "()",
                _ => "()"
            };

            builder.AppendLine();
            builder.AppendFormat(".When(x => {0}x.{1}{2})", notSymbol, EscapeIdentifier(whenClauseKey), parentheses);
        }

        if (this["PropertyName"] is { IsNull: false, Value: string { Length: > 0 } } propertyName)
        {
            builder.AppendLine();
            builder.AppendFormat(".WithName({0})", propertyName.ToCSharpString());
        }

        if (this["ErrorMessage"] is { IsNull: false, Value: string { Length: > 0 } } errorMessage)
        {
            builder.AppendLine();
            builder.AppendFormat(".WithMessage({0})", errorMessage.ToCSharpString());
        }

        if (this["ErrorCode"] is { IsNull: false, Value: string { Length: > 0 } } errorCode)
        {
            builder.AppendLine();
            builder.AppendFormat(".WithErrorCode({0})", errorCode.ToCSharpString());
        }

        if (this["Severity"] is { IsNull: false } severity)
        {
            builder.AppendLine();
            var value = severity.ToCSharpString().Replace("Ling.FluentValidation.Annotations.ValidationSeverity.", "global::FluentValidation.Severity.");
            builder.AppendFormat(".WithSeverity({0})", value);
        }
    }
}

internal sealed class LingAllowedValuesAttributeInfo(string memberName, AttributeData attributeData, ImmutableDictionary<string, MemberKind> maybeWhenClauses)
    : LingValidationAttributeInfoBase(memberName, attributeData, maybeWhenClauses)
{
    protected override void AppendRuleTo(CodeBuilder builder)
    {
        var values = this[0].ToCSharpString();
        builder.AppendFormat(".AllowedValues({0})", ResolveArrayValues(values));
    }
}

internal sealed class LingBase64StringAttributeInfo(string memberName, AttributeData attributeData, ImmutableDictionary<string, MemberKind> maybeWhenClauses)
    : LingValidationAttributeInfoBase(memberName, attributeData, maybeWhenClauses)
{
    protected override void AppendRuleTo(CodeBuilder builder)
    {
        if (AttributeData.ConstructorArguments.Length > 0)
        {
            builder.AppendFormat(".Base64String({0})", this[0].ToCSharpString());
        }
        else
        {
            builder.Append(".Base64String()");
        }
    }
}

internal sealed class LingCompareAttributeInfo(string memberName, AttributeData attributeData, ImmutableDictionary<string, MemberKind> maybeWhenClauses)
    : LingValidationAttributeInfoBase(memberName, attributeData, maybeWhenClauses)
{
    protected override void AppendRuleTo(CodeBuilder builder)
    {
        builder.AppendFormat(".Equal(x => x.{0})", EscapeIdentifier((string)this[0].Value!));
    }
}

internal sealed class LingCreditCardAttributeInfo(string memberName, AttributeData attributeData, ImmutableDictionary<string, MemberKind> maybeWhenClauses)
    : LingValidationAttributeInfoBase(memberName, attributeData, maybeWhenClauses)
{
    protected override void AppendRuleTo(CodeBuilder builder)
    {
        builder.Append(".CreditCard()");
    }
}

internal sealed class LingDeniedValuesAttributeInfo(string memberName, AttributeData attributeData, ImmutableDictionary<string, MemberKind> maybeWhenClauses)
    : LingValidationAttributeInfoBase(memberName, attributeData, maybeWhenClauses)
{
    protected override void AppendRuleTo(CodeBuilder builder)
    {
        var values = this[0].ToCSharpString();
        builder.AppendFormat(".DeniedValues({0})", ResolveArrayValues(values));
    }
}

internal sealed class LingEmailAddressAttributeInfo(string memberName, AttributeData attributeData, ImmutableDictionary<string, MemberKind> maybeWhenClauses)
    : LingValidationAttributeInfoBase(memberName, attributeData, maybeWhenClauses)
{
    protected override void AppendRuleTo(CodeBuilder builder)
    {
        builder.Append(".EmailAddress()");
    }
}

internal sealed class LingEmptyAttributeInfo(string memberName, AttributeData attributeData, ImmutableDictionary<string, MemberKind> maybeWhenClauses)
    : LingValidationAttributeInfoBase(memberName, attributeData, maybeWhenClauses)
{
    protected override void AppendRuleTo(CodeBuilder builder)
    {
        builder.Append(".Empty()");
    }
}

internal sealed class LingEnumAttributeInfo(string memberName, AttributeData attributeData, ImmutableDictionary<string, MemberKind> maybeWhenClauses)
    : LingValidationAttributeInfoBase(memberName, attributeData, maybeWhenClauses)
{
    protected override void AppendRuleTo(CodeBuilder builder)
    {
        builder.Append(".IsInEnum()");
    }
}

internal sealed class LingEnumNameAttributeInfo(string memberName, AttributeData attributeData, ImmutableDictionary<string, MemberKind> maybeWhenClauses)
    : LingValidationAttributeInfoBase(memberName, attributeData, maybeWhenClauses)
{
    protected override void AppendRuleTo(CodeBuilder builder)
    {
        var enumTypeSymbol = AttributeData.AttributeClass!.IsGenericType
            ? (INamedTypeSymbol)AttributeData.AttributeClass.TypeArguments[0]
            : (INamedTypeSymbol)this[0].Value!;

        if (this["CaseSensitive"] is { IsNull: false } caseSensitive)
        {
            builder.AppendFormat(".IsEnumName(typeof({0}), {1})", enumTypeSymbol.GetFullyQualifiedName(), caseSensitive.ToCSharpString());
        }
        else
        {
            builder.AppendFormat(".IsEnumName(typeof({0}))", enumTypeSymbol.GetFullyQualifiedName());
        }
    }
}

internal sealed class LingEqualAttributeInfo(string memberName, AttributeData attributeData, ImmutableDictionary<string, MemberKind> maybeWhenClauses)
    : LingValidationAttributeInfoBase(memberName, attributeData, maybeWhenClauses)
{
    protected override void AppendRuleTo(CodeBuilder builder)
    {
        builder.AppendFormat(".Equal({0})", this[0].ToCSharpString());
    }
}

internal sealed class LingExclusiveBetweenAttributeInfo(string memberName, AttributeData attributeData, ImmutableDictionary<string, MemberKind> maybeWhenClauses)
    : LingValidationAttributeInfoBase(memberName, attributeData, maybeWhenClauses)
{
    protected override void AppendRuleTo(CodeBuilder builder)
    {
        builder.AppendFormat(".ExclusiveBetween({0}, {1})", this[0].ToCSharpString(), this[1].ToCSharpString());
    }
}

internal sealed class LingFileExtensionsAttributeInfo(string memberName, AttributeData attributeData, ImmutableDictionary<string, MemberKind> maybeWhenClauses)
    : LingValidationAttributeInfoBase(memberName, attributeData, maybeWhenClauses)
{
    protected override void AppendRuleTo(CodeBuilder builder)
    {
        if (this[0] is not { Value: string extensions } || string.IsNullOrWhiteSpace(extensions))
        {
            builder.Append(".FileExtensions(\"png,jpg,jpeg,gif\")");
            return;
        }
        builder.AppendFormat(".FileExtensions({0})", this[0].ToCSharpString());
    }
}

internal sealed class LingGreaterThanAttributeInfo(string memberName, AttributeData attributeData, ImmutableDictionary<string, MemberKind> maybeWhenClauses)
    : LingValidationAttributeInfoBase(memberName, attributeData, maybeWhenClauses)
{
    protected override void AppendRuleTo(CodeBuilder builder)
    {
        builder.AppendFormat(".GreaterThan({0})", this[0].ToCSharpString());
    }
}

internal sealed class LingGreaterThanOrEqualToAttributeInfo(string memberName, AttributeData attributeData, ImmutableDictionary<string, MemberKind> maybeWhenClauses)
    : LingValidationAttributeInfoBase(memberName, attributeData, maybeWhenClauses)
{
    protected override void AppendRuleTo(CodeBuilder builder)
    {
        builder.AppendFormat(".GreaterThanOrEqualTo({0})", this[0].ToCSharpString());
    }
}

internal sealed class LingInclusiveBetweenAttributeInfo(string memberName, AttributeData attributeData, ImmutableDictionary<string, MemberKind> maybeWhenClauses)
    : LingValidationAttributeInfoBase(memberName, attributeData, maybeWhenClauses)
{
    protected override void AppendRuleTo(CodeBuilder builder)
    {
        builder.AppendFormat(".InclusiveBetween({0}, {1})", this[0].ToCSharpString(), this[1].ToCSharpString());
    }
}

internal sealed class LingLengthAttributeInfo(string memberName, AttributeData attributeData, ImmutableDictionary<string, MemberKind> maybeWhenClauses)
    : LingValidationAttributeInfoBase(memberName, attributeData, maybeWhenClauses)
{
    protected override void AppendRuleTo(CodeBuilder builder)
    {
        _ = AttributeData.ConstructorArguments.Length switch
        {
            1 => builder.AppendFormat(".Length({0})", this[0].ToCSharpString()),
            2 => builder.AppendFormat(".Length({0}, {1})", this[0].ToCSharpString(), this[1].ToCSharpString()),
            _ => builder
        };
    }
}

internal sealed class LingMaximumLengthAttributeInfo(string memberName, AttributeData attributeData, ImmutableDictionary<string, MemberKind> maybeWhenClauses)
    : LingValidationAttributeInfoBase(memberName, attributeData, maybeWhenClauses)
{
    protected override void AppendRuleTo(CodeBuilder builder)
    {
        builder.AppendFormat(".MaximumLength({0})", this[0].ToCSharpString());
    }
}

internal sealed class LingMinimumLengthAttributeInfo(string memberName, AttributeData attributeData, ImmutableDictionary<string, MemberKind> maybeWhenClauses)
    : LingValidationAttributeInfoBase(memberName, attributeData, maybeWhenClauses)
{
    protected override void AppendRuleTo(CodeBuilder builder)
    {
        builder.AppendFormat(".MinimumLength({0})", this[0].ToCSharpString());
    }
}

internal sealed class LingLessThanAttributeInfo(string memberName, AttributeData attributeData, ImmutableDictionary<string, MemberKind> maybeWhenClauses)
    : LingValidationAttributeInfoBase(memberName, attributeData, maybeWhenClauses)
{
    protected override void AppendRuleTo(CodeBuilder builder)
    {
        builder.AppendFormat(".LessThan({0})", this[0].ToCSharpString());
    }
}

internal sealed class LingLessThanOrEqualToAttributeInfo(string memberName, AttributeData attributeData, ImmutableDictionary<string, MemberKind> maybeWhenClauses)
    : LingValidationAttributeInfoBase(memberName, attributeData, maybeWhenClauses)
{
    protected override void AppendRuleTo(CodeBuilder builder)
    {
        builder.AppendFormat(".LessThanOrEqualTo({0})", this[0].ToCSharpString());
    }
}

internal sealed class LingMatchesAttributeInfo(string memberName, AttributeData attributeData, ImmutableDictionary<string, MemberKind> maybeWhenClauses)
    : LingValidationAttributeInfoBase(memberName, attributeData, maybeWhenClauses)
{
    protected override void AppendRuleTo(CodeBuilder builder)
    {
        if (this["Options"] is { IsNull: false } options)
        {
            var optionsText = options.ToCSharpString()
                .Replace("System.Text.RegularExpressions.RegexOptions.None | ", string.Empty)
                .Replace("System.Text.RegularExpressions.RegexOptions", "global::System.Text.RegularExpressions.RegexOptions");

            builder.AppendFormat(".Matches({0}, {1})", this[0].ToCSharpString(), optionsText);
        }
        else
        {
            builder.AppendFormat(".Matches({0})", this[0].ToCSharpString());
        }
    }
}

internal sealed class LingNotEmptyAttributeInfo(string memberName, AttributeData attributeData, ImmutableDictionary<string, MemberKind> maybeWhenClauses)
    : LingValidationAttributeInfoBase(memberName, attributeData, maybeWhenClauses)
{
    protected override void AppendRuleTo(CodeBuilder builder)
    {
        builder.Append(".NotEmpty()");
    }
}

internal sealed class LingNotEqualAttributeInfo(string memberName, AttributeData attributeData, ImmutableDictionary<string, MemberKind> maybeWhenClauses)
    : LingValidationAttributeInfoBase(memberName, attributeData, maybeWhenClauses)
{
    protected override void AppendRuleTo(CodeBuilder builder)
    {
        builder.AppendFormat(".NotEqual({0})", this[0].ToCSharpString());
    }
}

internal sealed class LingNotNullAttributeInfo(string memberName, AttributeData attributeData, ImmutableDictionary<string, MemberKind> maybeWhenClauses)
    : LingValidationAttributeInfoBase(memberName, attributeData, maybeWhenClauses)
{
    protected override void AppendRuleTo(CodeBuilder builder)
    {
        builder.Append(".NotNull()");
    }
}

internal sealed class LingNullAttributeInfo(string memberName, AttributeData attributeData, ImmutableDictionary<string, MemberKind> maybeWhenClauses)
    : LingValidationAttributeInfoBase(memberName, attributeData, maybeWhenClauses)
{
    protected override void AppendRuleTo(CodeBuilder builder)
    {
        builder.Append(".Null()");
    }
}

internal sealed class LingPhoneAttributeInfo(string memberName, AttributeData attributeData, ImmutableDictionary<string, MemberKind> maybeWhenClauses)
    : LingValidationAttributeInfoBase(memberName, attributeData, maybeWhenClauses)
{
    protected override void AppendRuleTo(CodeBuilder builder)
    {
        builder.Append(".Phone()");
    }
}

internal sealed class LingUrlAttributeInfo(string memberName, AttributeData attributeData, ImmutableDictionary<string, MemberKind> maybeWhenClauses)
    : LingValidationAttributeInfoBase(memberName, attributeData, maybeWhenClauses)
{
    protected override void AppendRuleTo(CodeBuilder builder)
    {
        builder.AppendFormat(".Url()");
    }
}
