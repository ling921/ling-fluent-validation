# Ling.FluentValidation.Annotations

[Project documentation](https://github.com/ling921/fluent-validation-generator#readme) | [简体中文](https://github.com/ling921/fluent-validation-generator/blob/master/src/Ling.FluentValidation.Annotations/README.zh-CN.md)

`Ling.FluentValidation.Annotations` contains the declaration-only API for marking DTOs and describing validation rules. It does not depend on FluentValidation, so shared Contracts projects do not acquire a validation-runtime dependency.

The package also includes Roslyn analyzers and code fixes that report invalid rule usage while editing and building.

## Installation

Install the package in the project that owns request and DTO types:

```shell
dotnet add package Ling.FluentValidation.Annotations --prerelease
```

## Declare a validation contract

Generation is explicit. A type with rules must be marked with `[GenerateValidator]`:

```csharp
using Ling.FluentValidation.Annotations;

namespace MyApplication.Contracts;

[GenerateValidator]
public sealed record CreateOrderRequest
{
    [NotEmpty]
    [Length(3, 100)]
    public string Name { get; init; } = string.Empty;

    [InclusiveBetween(1, 100)]
    public int Quantity { get; init; }
}
```

The analyzer reports rules placed on an unmarked type and offers a code fix that adds `[GenerateValidator]`.

## Rules

Annotations cover value-based, type-independent rules, including:

- null and empty checks;
- length and regular-expression checks;
- equality, ordering, and inclusive or exclusive ranges;
- allowed and denied values;
- email, phone, URL, credit card, Base64, file extension, enum, and enum-name checks.

Standard `System.ComponentModel.DataAnnotations` validation attributes are also recognized by the generator. Rule-specific error messages, codes, severity, display names, and supported conditional members can be configured on Ling attributes.

Rules that depend on services, another validator, or application behavior belong in a partial validator in the receiving project rather than in the contract assembly.

## Select a source assembly

The project that receives generated validators explicitly selects external contract assemblies:

```csharp
using Ling.FluentValidation.Annotations;
using MyApplication.Contracts;

[assembly: GenerateValidatorsFromAssemblyContaining(typeof(CreateOrderRequest))]
```

On .NET 7 or later, the generic form can be used to omit `typeof`:

```csharp
[assembly: GenerateValidatorsFromAssemblyContaining<CreateOrderRequest>]
```

## Configure generated validators

Apply options to the receiving assembly:

```csharp
[assembly: ValidatorGenerationOptions(
    Namespace = "MyApplication.Validation",
    Visibility = GeneratedValidatorVisibility.Public,
    IsSealed = true,
    ClassLevelCascadeMode = ValidationCascadeMode.Continue,
    RuleLevelCascadeMode = ValidationCascadeMode.Stop)]
```

## Target frameworks

- `netstandard2.0` provides the base declaration API.
- `net7.0` adds generic attribute forms that only simplify `typeof` syntax.

Newer applications automatically select the compatible asset.

## Related packages

- `Ling.FluentValidation` provides the source generator, runtime rule implementations, and FluentValidation integration.

## License

[MIT](https://github.com/ling921/fluent-validation-generator/blob/master/LICENSE)
