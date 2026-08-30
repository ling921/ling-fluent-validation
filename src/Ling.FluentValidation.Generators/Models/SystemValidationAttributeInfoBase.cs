using Ling.FluentValidation.Extensions;
using Ling.FluentValidation.Generators.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Ling.FluentValidation.Generators.Models;

internal abstract class SystemValidationAttributeInfoBase(string memberName, AttributeData attributeData)
    : ValidationAttributeInfoBase(memberName, attributeData)
{
    protected override void AppendExtraTo(CodeBuilder builder)
    {
        if (this["ErrorMessage"] is { IsNull: false, Value: string { Length: > 0 } } errorMessage)
        {
            builder.AppendLine();
            builder.AppendFormat(".WithMessage({0})", errorMessage.ToCSharpString());
        }
    }
}

internal sealed class SystemAllowedValuesAttributeInfo(string memberName, AttributeData attributeData)
    : SystemValidationAttributeInfoBase(memberName, attributeData)
{
    protected override void AppendRuleTo(CodeBuilder builder)
    {
        var values = this[0].ToCSharpString();
        builder.AppendFormat(".AllowedValues({0})", ResolveArrayValues(values));
    }
}

internal sealed class SystemBase64StringAttributeInfo(string memberName, AttributeData attributeData)
    : SystemValidationAttributeInfoBase(memberName, attributeData)
{
    protected override void AppendRuleTo(CodeBuilder builder)
    {
        builder.AppendFormat(".Base64String()");
    }
}

internal sealed class SystemCompareAttributeInfo(string memberName, AttributeData attributeData)
    : SystemValidationAttributeInfoBase(memberName, attributeData)
{
    protected override void AppendRuleTo(CodeBuilder builder)
    {
        builder.AppendFormat(".Equal(x => x.{0})", EscapeIdentifier((string)this[0].Value!));
    }
}

internal sealed class SystemCreditCardAttributeInfo(string memberName, AttributeData attributeData)
    : SystemValidationAttributeInfoBase(memberName, attributeData)
{
    protected override void AppendRuleTo(CodeBuilder builder)
    {
        builder.AppendFormat(".CreditCard()");
    }
}

internal sealed class SystemDeniedValuesAttributeInfo(string memberName, AttributeData attributeData)
    : SystemValidationAttributeInfoBase(memberName, attributeData)
{
    protected override void AppendRuleTo(CodeBuilder builder)
    {
        var values = this[0].ToCSharpString();
        builder.AppendFormat(".DeniedValues({0})", ResolveArrayValues(values));
    }
}

internal sealed class SystemEmailAddressAttributeInfo(string memberName, AttributeData attributeData)
    : SystemValidationAttributeInfoBase(memberName, attributeData)
{
    protected override void AppendRuleTo(CodeBuilder builder)
    {
        builder.AppendFormat(".EmailAddress()");
    }
}

internal sealed class SystemEnumDataTypeAttributeInfo(MemberInfo memberInfo, AttributeData attributeData)
    : SystemValidationAttributeInfoBase(memberInfo.MemberName, attributeData)
{
    private readonly ITypeSymbol _memberType = memberInfo.MemberType;

    protected override void AppendRuleTo(CodeBuilder builder)
    {
        if (_memberType.SpecialType == SpecialType.System_String)
        {
            var enumTypeSymbol = (INamedTypeSymbol)this[0].Value!;
            builder.AppendFormat(".IsEnumName(typeof({0}))", enumTypeSymbol.GetFullyQualifiedName());
        }
        else
        {
            builder.Append(".IsInEnum()");
        }
    }
}

internal sealed class SystemFileExtensionsAttributeInfo(string memberName, AttributeData attributeData)
    : SystemValidationAttributeInfoBase(memberName, attributeData)
{
    protected override void AppendRuleTo(CodeBuilder builder)
    {
        if (this["Extensions"] is not { Value: string } extensions)
        {
            builder.Append(".FileExtensions(\"png,jpg,jpeg,gif\")");
            return;
        }
        builder.AppendFormat(".FileExtensions({0})", extensions.ToCSharpString());
    }
}

internal sealed class SystemLengthAttributeInfo(string memberName, AttributeData attributeData)
    : SystemValidationAttributeInfoBase(memberName, attributeData)
{
    protected override void AppendRuleTo(CodeBuilder builder)
    {
        var minLength = this[0].ToCSharpString();
        var maxLength = this[1].ToCSharpString();

        if (minLength == maxLength)
        {
            builder.AppendFormat(".Length({0})", minLength);
        }
        else
        {
            builder.AppendFormat(".Length({0}, {1})", minLength, maxLength);
        }
    }
}

internal sealed class SystemMaxLengthAttributeInfo(string memberName, AttributeData attributeData)
    : SystemValidationAttributeInfoBase(memberName, attributeData)
{
    protected override void AppendRuleTo(CodeBuilder builder)
    {
        builder.AppendFormat(".MaximumLength({0})", this[0].ToCSharpString());
    }
}

internal sealed class SystemMinLengthAttributeInfo(string memberName, AttributeData attributeData)
    : SystemValidationAttributeInfoBase(memberName, attributeData)
{
    protected override void AppendRuleTo(CodeBuilder builder)
    {
        builder.AppendFormat(".MinimumLength({0})", this[0].ToCSharpString());
    }
}

internal sealed class SystemPhoneAttributeInfo(string memberName, AttributeData attributeData)
    : SystemValidationAttributeInfoBase(memberName, attributeData)
{
    protected override void AppendRuleTo(CodeBuilder builder)
    {
        builder.AppendFormat(".Phone()");
    }
}

internal sealed class SystemRangeAttributeInfo(string memberName, AttributeData attributeData)
    : SystemValidationAttributeInfoBase(memberName, attributeData)
{
    protected override void AppendRuleTo(CodeBuilder builder)
    {
        var minExclusive = this["MinimumIsExclusive"] is { Value: true };
        var maxExclusive = this["MaximumIsExclusive"] is { Value: true };

        if (minExclusive && maxExclusive)
        {
            builder.AppendFormat(".ExclusiveBetween({0}, {1})", this[0].ToCSharpString(), this[1].ToCSharpString());
        }
        else if (!minExclusive && !maxExclusive)
        {
            builder.AppendFormat(".InclusiveBetween({0}, {1})", this[0].ToCSharpString(), this[1].ToCSharpString());
        }
        else if (minExclusive)
        {
            builder.AppendFormatLine(".GreaterThan({0})", this[0].ToCSharpString());
            builder.AppendFormat(".LessThanOrEqualTo({0})", this[1].ToCSharpString());
        }
        else
        {
            builder.AppendFormatLine(".GreaterThanOrEqualTo({0})", this[0].ToCSharpString());
            builder.AppendFormat(".LessThan({0})", this[1].ToCSharpString());
        }
    }
}

internal sealed class SystemRegularExpressionAttributeInfo(string memberName, AttributeData attributeData)
    : SystemValidationAttributeInfoBase(memberName, attributeData)
{
    protected override void AppendRuleTo(CodeBuilder builder)
    {
        builder.AppendFormat(".Matches({0})", this[0].ToCSharpString());
    }
}

internal sealed class SystemRequiredAttributeInfo(MemberInfo memberInfo, AttributeData attributeData)
    : SystemValidationAttributeInfoBase(memberInfo.MemberName, attributeData)
{
    private readonly ITypeSymbol _memberType = memberInfo.MemberType;

    protected override void AppendRuleTo(CodeBuilder builder)
    {
        // By default, 'AllowEmptyStrings' is 'false'.
        if (_memberType.SpecialType == SpecialType.System_String &&
            this["AllowEmptyStrings"] is not { Value: true })
        {
            builder.Append(".NotEmpty()");
        }
        else
        {
            builder.Append(".NotNull()");
        }
    }
}

internal sealed class SystemStringLengthAttributeInfo(string memberName, AttributeData attributeData)
    : SystemValidationAttributeInfoBase(memberName, attributeData)
{
    protected override void AppendRuleTo(CodeBuilder builder)
    {
        if (this["MinimumLength"] is { Value: int minLength })
        {
            builder.AppendFormat(".Length({0}, {1})", minLength, this[0].ToCSharpString());
        }
        else
        {
            builder.AppendFormat(".MaximumLength({0})", this[0].ToCSharpString());
        }
    }
}

internal sealed class SystemUrlAttributeInfo(string memberName, AttributeData attributeData)
    : SystemValidationAttributeInfoBase(memberName, attributeData)
{
    protected override void AppendRuleTo(CodeBuilder builder)
    {
        builder.AppendFormat(".Url()");
    }
}
