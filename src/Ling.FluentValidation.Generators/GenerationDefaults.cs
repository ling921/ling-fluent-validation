using System.Collections.Immutable;

namespace Ling.FluentValidation.Generators;

internal static class GenerationDefaults
{
    /// <summary>
    /// All the fully qualified attribute names target on properties or fields for code gneration.
    /// </summary>
    public static readonly ImmutableArray<string> MemberAttributeFullyQualifiedMetadataNames = [
            Constants.AllowedValuesAttributeFullyQualifiedMetadataName,
            Constants.Base64StringAttributeFullyQualifiedMetadataName,
            Constants.CompareAttributeFullyQualifiedMetadataName,
            Constants.CreditCardAttributeFullyQualifiedMetadataName,
            Constants.DeniedValuesAttributeFullyQualifiedMetadataName,
            Constants.EmailAddressAttributeFullyQualifiedMetadataName,
            Constants.EmptyAttributeFullyQualifiedMetadataName,
            Constants.EnumAttributeFullyQualifiedMetadataName,
            Constants.EnumNameAttributeFullyQualifiedMetadataName,
            Constants.GenericEnumNameAttributeFullyQualifiedMetadataName,
            Constants.EqualAttributeFullyQualifiedMetadataName,
            Constants.ExclusiveBetweenAttributeFullyQualifiedMetadataName,
            Constants.FileExtensionsAttributeFullyQualifiedMetadataName,
            Constants.GreaterThanAttributeFullyQualifiedMetadataName,
            Constants.GreaterThanOrEqualToAttributeFullyQualifiedMetadataName,
            Constants.InclusiveBetweenAttributeFullyQualifiedMetadataName,
            Constants.LengthAttributeFullyQualifiedMetadataName,
            Constants.MaximumLengthAttributeFullyQualifiedMetadataName,
            Constants.MinimumLengthAttributeFullyQualifiedMetadataName,
            Constants.LessThanAttributeFullyQualifiedMetadataName,
            Constants.LessThanOrEqualToAttributeFullyQualifiedMetadataName,
            Constants.MatchesAttributeFullyQualifiedMetadataName,
            Constants.NotEmptyAttributeFullyQualifiedMetadataName,
            Constants.NotEqualAttributeFullyQualifiedMetadataName,
            Constants.NotNullAttributeFullyQualifiedMetadataName,
            Constants.NullAttributeFullyQualifiedMetadataName,
            Constants.PhoneAttributeFullyQualifiedMetadataName,
            Constants.UrlAttributeFullyQualifiedMetadataName,

            Constants.SystemAllowedValuesAttributeFullyQualifiedMetadataName,
            Constants.SystemBase64StringAttributeFullyQualifiedMetadataName,
            Constants.SystemCompareAttributeFullyQualifiedMetadataName,
            Constants.SystemCreditCardAttributeFullyQualifiedMetadataName,
            Constants.SystemDeniedValuesAttributeFullyQualifiedMetadataName,
            Constants.SystemEmailAddressAttributeFullyQualifiedMetadataName,
            Constants.SystemEnumDataTypeAttributeFullyQualifiedMetadataName,
            Constants.SystemFileExtensionsAttributeFullyQualifiedMetadataName,
            Constants.SystemLengthAttributeFullyQualifiedMetadataName,
            Constants.SystemMaxLengthAttributeFullyQualifiedMetadataName,
            Constants.SystemMinLengthAttributeFullyQualifiedMetadataName,
            Constants.SystemPhoneAttributeFullyQualifiedMetadataName,
            Constants.SystemRangeAttributeFullyQualifiedMetadataName,
            Constants.SystemRegularExpressionAttributeFullyQualifiedMetadataName,
            Constants.SystemRequiredAttributeFullyQualifiedMetadataName,
            Constants.SystemStringLengthAttributeFullyQualifiedMetadataName,
            Constants.SystemUrlAttributeFullyQualifiedMetadataName
        ];
}
