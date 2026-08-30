using Microsoft.CodeAnalysis;

#pragma warning disable IDE0090 // Use 'new DiagnosticDescriptor(...)'

namespace Ling.FluentValidation.Analyzers.Diagnostics;

/// <summary>
/// A container for all <see cref="DiagnosticDescriptor"/> instances for errors reported by analyzers in this project.
/// </summary>
public static class DiagnosticDescriptors
{
    /// <summary>Gets the diagnostic reported when a DTO with rules lacks GenerateValidatorAttribute.</summary>
    public static readonly DiagnosticDescriptor ValidationTypeMustBeMarked = new(
        DiagnosticIds.ValidationTypeMustBeMarkedId,
        "Validation type must be explicitly marked",
        "Type '{0}' declares validation rules and must be marked with GenerateValidatorAttribute",
        "Usage",
        DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "Validator generation is explicit and requires GenerateValidatorAttribute.",
        helpLinkUri: "https://github.com/ling921/fluent-validation-generator");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> indicating when the validator should inherit 'IValidator&lt;T&gt;'.
    /// <para>
    /// Format: <c>"The validator type '{0}' should inherit 'IValidator&lt;T&gt;'"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor ValidatorShouldInheritIValidatorError = new DiagnosticDescriptor(
        id: DiagnosticIds.ValidatorShouldInheritIValidatorId,
        title: "The validator should inherit 'IValidator<T>'",
        messageFormat: "The validator type '{0}' should inherit 'IValidator<T>'",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "The validator should inherit 'IValidator<T>'.",
        helpLinkUri: "https://github.com/ling921/fluent-validation-generator");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> indicating when the validator included is not compatible.
    /// <para>
    /// Format: <c>"The type '{0}' should be assignable to type '{1}' that targeted by validator type '{2}'"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor IncludeValidatorUncompatibleError = new DiagnosticDescriptor(
        id: DiagnosticIds.IncludeValidatorUncompatibleId,
        title: "The validator included is not compatible",
        messageFormat: "The type '{0}' should be assignable to type '{1}' that validated by type '{2}'",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "The validator included is not compatible.",
        helpLinkUri: "https://github.com/ling921/fluent-validation-generator");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> indicating when an attribute should used on specific type property or field.
    /// <para>
    /// Format: <c>"Attribute '{0}' should used on property or field of '{1}' type"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor AttributeShouldUsedOnPropertyOfOneType = new DiagnosticDescriptor(
        id: DiagnosticIds.AttributeShouldUsedOnPropertyOfSpecificTypeId,
        title: "Attribute should used on specific type property or field",
        messageFormat: "Attribute '{0}' should used on property or field of '{1}' type",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "Attribute should used on specific type property or field.",
        helpLinkUri: "https://github.com/ling921/fluent-validation-generator");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> indicating when an attribute should used on specific type property or field.
    /// <para>
    /// Format: <c>"Attribute '{0}' should used on property or field of types: '{1}' or '{2}'"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor AttributeShouldUsedOnPropertyOfTwoTypes = new DiagnosticDescriptor(
        id: DiagnosticIds.AttributeShouldUsedOnPropertyOfSpecificTypeId,
        title: "Attribute should used on specific type property or field",
        messageFormat: "Attribute '{0}' should used on property or field of types: '{1}' or '{2}'",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "Attribute should used on specific type property or field.",
        helpLinkUri: "https://github.com/ling921/fluent-validation-generator");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> indicating when an attribute should used on specific type property or field.
    /// <para>
    /// Format: <c>Attribute '{0}' should used on property or field of 'string' type or implement 'IEnumerable&lt;T&gt;'</c>
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor AttributeShouldUsedOnPropertyOfStringOrEnumerableError = new DiagnosticDescriptor(
        id: DiagnosticIds.AttributeShouldUsedOnPropertyOfSpecificTypeId,
        title: "Attribute should used on specific type property or field",
        messageFormat: "Attribute '{0}' should used on property or field of 'string' type or implement 'IEnumerable<T>'",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "Attribute should used on specific type property or field.",
        helpLinkUri: "https://github.com/ling921/fluent-validation-generator");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> indicating when an attribute should used on specific type property or field.
    /// <para>
    /// Format: <c>"Attribute '{0}' should used on property or field that implements IComparable and IComparable&lt;{1}&gt;"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor AttributeShouldUsedOnPropertyImplimentIComparableError = new DiagnosticDescriptor(
        id: DiagnosticIds.AttributeShouldUsedOnPropertyOfSpecificTypeId,
        title: "Attribute should used on specific type property or field",
        messageFormat: "Attribute '{0}' should used on property or field that implements IComparable and IComparable<{1}>",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "Attribute should used on specific type property or field.",
        helpLinkUri: "https://github.com/ling921/fluent-validation-generator");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> indicating when a attribute parameter should be assignable to property or field.
    /// <para>
    /// Format: <c>"Attribute '{0}' with parameter '{1}' should be assignable to property or field '{2}'"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor ValidationAttributeParameterShouldAssignableToPropertyError = new DiagnosticDescriptor(
        id: DiagnosticIds.ValidationAttributeParameterShouldAssignableToPropertyId,
        title: "Attribute parameter should be assignable to property or field",
        messageFormat: "Attribute '{0}' with parameter '{1}' should be assignable to property or field '{2}'",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "Attribute parameter should be assignable to property or field.",
        helpLinkUri: "https://github.com/ling921/fluent-validation-generator");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> indicating when a validator type should inherit PropertyValidator or AbstractValidator.
    /// <para>
    /// Format: <c>"'{0}' should implement PropertyValidator&lt;{1}, {2}&gt; or AbstractValidator&lt;{2}&gt;"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor ValidatorShouldInheritPropertyValidatorOrAbstractValidatorError = new DiagnosticDescriptor(
        id: DiagnosticIds.ValidatorShouldInheritPropertyValidatorOrAbstractValidatorId,
        title: "Validator type should inherit PropertyValidator or AbstractValidator",
        messageFormat: "'{0}' should implement PropertyValidator<{1}, {2}> or AbstractValidator<{2}>",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "The validator type should inherit PropertyValidator<T, TProperty> or AbstractValidator<TProperty>.",
        helpLinkUri: "https://github.com/ling921/fluent-validation-generator");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> indicating when a type should have public parameterless constructor.
    /// <para>
    /// Format: <c>"The type '{0}' should have public parameterless constructor"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor TypeShouldHavePublicParameterlessConstructor = new DiagnosticDescriptor(
        id: DiagnosticIds.TypeShouldHavePublicParameterlessConstructorId,
        title: "Type should have public parameterless instance constructor",
        messageFormat: "The type '{0}' should have public parameterless instance constructor",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "The type should have public parameterless instance constructor.",
        helpLinkUri: "https://github.com/ling921/fluent-validation-generator");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> indicating when a type should implement another type.
    /// <para>
    /// Format: <c>"The type '{0}' should implement type '{1}'"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor TypeShouldImplementAnotherType = new DiagnosticDescriptor(
        id: DiagnosticIds.TypeShouldImplementAnotherTypeId,
        title: "Type should implement another type",
        messageFormat: "The type '{0}' should implement type '{1}'",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "The type should implement another type.",
        helpLinkUri: "https://github.com/ling921/fluent-validation-generator");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> indicating when a type should implement one of two types.
    /// <para>
    /// Format: <c>"The type '{0}' should implement type '{1}' or '{2}'"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor TypeShouldImplementOneOfTwoTypes = new DiagnosticDescriptor(
        id: DiagnosticIds.TypeShouldImplementOneOfTwoTypesId,
        title: "Type should implement one of two types",
        messageFormat: "The type '{0}' should implement type '{1}' or '{2}'",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "The type should implement one of two types.",
        helpLinkUri: "https://github.com/ling921/fluent-validation-generator");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> indicating when a type should assignable to another type.
    /// <para>
    /// Format: <c>"The type '{0}' should assignable to type '{1}'"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor TypeShouldAssignableToAnotherType = new DiagnosticDescriptor(
        id: DiagnosticIds.TypeShouldAssignableToAnotherTypeId,
        title: "Type should assignable to another type",
        messageFormat: "The type '{0}' should assignable to type '{1}'",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "The type should assignable to another type.",
        helpLinkUri: "https://github.com/ling921/fluent-validation-generator");

    /// <summary>
    /// Gets a diagnostic indicating that an attribute target cannot be handled by the source generator.
    /// </summary>
    public static readonly DiagnosticDescriptor AttributeTargetCannotBeGenerated = new DiagnosticDescriptor(
        id: DiagnosticIds.AttributeTargetCannotBeGeneratedId,
        title: "Attribute target cannot be handled by the source generator",
        messageFormat: "Attribute '{0}' cannot generate validation code for '{1}': {2}",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "Validation attributes must target accessible instance members on non-generic classes.",
        helpLinkUri: "https://github.com/ling921/fluent-validation-generator");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> indicating when AllowedValuesAttribute should have at least one value.
    /// <para>
    /// Format: <c>"AllowedValuesAttribute should have at least one value"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor AllowedValuesAttributeShouldHaveAtLeastOneValue = new DiagnosticDescriptor(
        id: DiagnosticIds.AllowedValuesAttributeShouldHaveAtLeastOneValueId,
        title: "AllowedValuesAttribute should have at least one value",
        messageFormat: "Attribute 'AllowedValuesAttribute' should have at least one value",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true,
        description: "AllowedValuesAttribute should have at least one value.",
        helpLinkUri: "https://github.com/ling921/fluent-validation-generator");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> indicating when DeniedValuesAttribute should have at least one value.
    /// <para>
    /// Format: <c>"DeniedValuesAttribute should have at least one value"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor DeniedValuesAttributeShouldHaveAtLeastOneValue = new DiagnosticDescriptor(
        id: DiagnosticIds.DeniedValuesAttributeShouldHaveAtLeastOneValueId,
        title: "DeniedValuesAttribute should have at least one value",
        messageFormat: "Attribute 'DeniedValuesAttribute' should have at least one value",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true,
        description: "DeniedValuesAttribute should have at least one value.",
        helpLinkUri: "https://github.com/ling921/fluent-validation-generator");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> indicating when CompareAttribute should specify property.
    /// <para>
    /// Format: <c>"'{0}' should be a property or field of '{1}' type"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor CompareAttributeShouldSpecifyProperty = new DiagnosticDescriptor(
        id: DiagnosticIds.CompareAttributeShouldSpecifyPropertyId,
        title: "CompareAttribute should specify property",
        messageFormat: "'{0}' should be a property or field of '{1}' type",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "CompareAttribute should specify property.",
        helpLinkUri: "https://github.com/ling921/fluent-validation-generator");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> indicating when CompareAttribute should specify same type property.
    /// <para>
    /// Format: <c>"Property or field '{0}' to compared should be type '{1}'"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor CompareAttributeShouldSpecifySameTypeProperty = new DiagnosticDescriptor(
        id: DiagnosticIds.CompareAttributeShouldSpecifySameTypePropertyId,
        title: "CompareAttribute should specify same type property",
        messageFormat: "Property or field '{0}' to compared should be type '{1}'",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "CompareAttribute should specify same type property.",
        helpLinkUri: "https://github.com/ling921/fluent-validation-generator");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> indicating when LengthAttribute should used on countable property.
    /// <para>
    /// Format: <c>"Attribute '{0}' should used on property or field with type 'System.String' or 'IEnumerable&lt;T&gt;'"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor LengthAttributeShouldUsedOnStringOrEnumerableProperty = new DiagnosticDescriptor(
        id: DiagnosticIds.LengthAttributeShouldUsedOnStringOrEnumerablePropertyId,
        title: "LengthAttribute should used on countable property",
        messageFormat: "Attribute '{0}' should used on property or field with type 'System.String' or 'IEnumerable<T>'",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "LengthAttribute should used on countable property.",
        helpLinkUri: "https://github.com/ling921/fluent-validation-generator");

    /// <summary>
    /// Gets a diagnostic indicating that equal minimum and maximum length values
    /// can use the exact-length constructor.
    /// </summary>
    public static readonly DiagnosticDescriptor LengthAttributeMinEqualToMax = new DiagnosticDescriptor(
        id: DiagnosticIds.LengthAttributeMinEqualToMaxId,
        title: "LengthAttribute can use the exact-length constructor",
        messageFormat: "Attribute '{0}' has equal minimum and maximum values; use the exact-length constructor",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Info,
        isEnabledByDefault: true,
        description: "Equal minimum and maximum values can be expressed with the exact-length constructor.",
        helpLinkUri: "https://github.com/ling921/fluent-validation-generator");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> indicating when using attribute of namespace 'System.ComponentModel.DataAnnotations'.
    /// <para>
    /// Format: <c>"Use '{0}' instead of '{1}'"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor UseLingValidationAttributeFix = new DiagnosticDescriptor(
        id: DiagnosticIds.UseLingValidationAttributeFixId,
        title: "Use attribute of namespace 'Ling.FluentValidation.Annotations' instead of 'System.ComponentModel.DataAnnotations'",
        messageFormat: "Use '{0}' instead of '{1}'",
        category: "Refactoring",
        defaultSeverity: DiagnosticSeverity.Info,
        isEnabledByDefault: true,
        description: "Use attribute of namespace 'Ling.FluentValidation.Annotations' instead of 'System.ComponentModel.DataAnnotations'.",
        helpLinkUri: "https://github.com/ling921/fluent-validation-generator");
}
