namespace Ling.FluentValidation;

internal static class DiagnosticIds
{
    /// <summary>
    /// The diagnostic id for <see cref="ValidatorShouldInheritIValidatorError"/>.
    /// </summary>
    public const string ValidatorShouldInheritIValidatorId = "LFV0001";

    /// <summary>
    /// The diagnostic id for <see cref="IncludeValidatorUncompatibleError"/>.
    /// </summary>
    public const string IncludeValidatorUncompatibleId = "LFV0002";

    /// <summary>
    /// The diagnostic id for <see cref="AttributeShouldUsedOnPropertyOfTypeError"/>.
    /// </summary>
    public const string AttributeShouldUsedOnPropertyOfSpecificTypeId = "LFV0003";

    /// <summary>
    /// The diagnostic id for <see cref="ValidationAttributeParameterShouldAssignableToPropertyError"/>.
    /// </summary>
    public const string ValidationAttributeParameterShouldAssignableToPropertyId = "LFV0004";

    /// <summary>
    /// The diagnostic id for <see cref="ValidatorShouldInheritPropertyValidatorOrAbstractValidatorError"/>.
    /// </summary>
    public const string ValidatorShouldInheritPropertyValidatorOrAbstractValidatorId = "LFV0005";

    /// <summary>
    /// The diagnostic id for type should have a public parameterless constructor.
    /// </summary>
    public const string TypeShouldHavePublicParameterlessConstructorId = "LFV0006";

    /// <summary>
    /// The diagnostic id for type should implement another type.
    /// </summary>
    public const string TypeShouldImplementAnotherTypeId = "LFV0007";

    /// <summary>
    /// The diagnostic id for type should implement one of two types.
    /// </summary>
    public const string TypeShouldImplementOneOfTwoTypesId = "LFV0008";

    /// <summary>
    /// The diagnostic id for type should be assignable to another type.
    /// </summary>
    public const string TypeShouldAssignableToAnotherTypeId = "LFV0009";

    /// <summary>
    /// The diagnostic id for an attribute target that cannot be handled by the source generator.
    /// </summary>
    public const string AttributeTargetCannotBeGeneratedId = "LFV0010";

    /// <summary>The diagnostic id for rules declared on an unmarked DTO.</summary>
    public const string ValidationTypeMustBeMarkedId = "LFV0011";

    #region Diagnostic Ids for specific attribute

    // Each attribute owns 20 diagnostic ids.

    /// <summary>
    /// The diagnostic id for 'AllowedValuesAttribute' should have at least one value.
    /// </summary>
    public const string AllowedValuesAttributeShouldHaveAtLeastOneValueId = "LFV5101";

    /// <summary>
    /// The diagnostic id for 'DeniedValuesAttribute' should have at least one value.
    /// </summary>
    public const string DeniedValuesAttributeShouldHaveAtLeastOneValueId = "LFV5121";

    /// <summary>
    /// The diagnostic id for 'CompareAttribute' should specify property id.
    /// </summary>
    public const string CompareAttributeShouldSpecifyPropertyId = "LFV5141";

    /// <summary>
    /// The diagnostic id for 'CompareAttribute' should specify same type property id.
    /// </summary>
    public const string CompareAttributeShouldSpecifySameTypePropertyId = "LFV5142";

    /// <summary>
    /// The diagnostic id for 'LengthAttribute' should specify property id.
    /// </summary>
    public const string LengthAttributeShouldUsedOnStringOrEnumerablePropertyId = "LFV5161";

    /// <summary>
    /// The diagnostic id for 'LengthAttribute' parameter should be greater than or equal to zero.
    /// </summary>
    public const string LengthAttributeParameterShouldGreaterThanOrEqualToZeroId = "LFV5162";

    /// <summary>
    /// The diagnostic id for 'LengthAttribute' parameter 'min' should be less than or equal to 'max'.
    /// </summary>
    public const string LengthAttributeMinShouldBeLessThanMaxId = "LFV5163";

    /// <summary>
    /// The diagnostic id for 'LengthAttribute' parameter 'min' is equals to 'max'.
    /// </summary>
    public const string LengthAttributeMinEqualToMaxId = "LFV5164";

    /// <summary>
    /// The diagnostic id for 'RangeAttribute' should used on comparable property.
    /// </summary>
    public const string RangeAttributeShouldUsedOnComparablePropertyId = "LFV5181";

    /// <summary>
    /// The diagnostic id for 'RangeAttribute' parameter 'min' should be less than or equal to 'max'.
    /// </summary>
    public const string RangeAttributeMinShouldBeLessThanMaxId = "LFV5182";

    /// <summary>
    /// The diagnostic id for 'RangeAttribute' parameter 'min' is equals to 'max'.
    /// </summary>
    public const string RangeAttributeMinEqualToMaxId = "LFV5183";

    #endregion

    /// <summary>
    /// The diagnostic id for <see cref="UseLingValidationAttributeFix"/>.
    /// </summary>
    public const string UseLingValidationAttributeFixId = "LFV9001";
}
