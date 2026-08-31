# Ling.FluentValidation

[Project documentation](https://github.com/ling921/ling-fluent-validation#readme) | [简体中文](https://github.com/ling921/ling-fluent-validation/blob/master/src/Ling.FluentValidation/README.zh-CN.md)

`Ling.FluentValidation` provides the source generator, FluentValidation rule extensions, and compatible runtime validators. The generator is shipped as a compiler-only asset, while DTO declarations live in the dependency-free `Ling.FluentValidation.Annotations` package.

## Installation

Install the package in the application or validation project that should own generated validators:

```xml
<PackageReference Include="Ling.FluentValidation" Version="..." />
```

The package references `Ling.FluentValidation.Annotations` transitively, including its analyzer and code-fix assets. Contract projects should nevertheless reference `Ling.FluentValidation.Annotations` directly and should not reference this runtime package.

## Generate from a contract assembly

```csharp
using Ling.FluentValidation.Annotations;
using MyApplication.Contracts;

[assembly: GenerateValidatorsFromAssemblyContaining(typeof(CreateOrderRequest))]
[assembly: ValidatorGenerationOptions(
    Namespace = "MyApplication.Validation",
    Visibility = GeneratedValidatorVisibility.Public)]
```

Only explicitly selected referenced assemblies are examined. Marked DTOs in the current assembly are generated automatically.

## Extend a generated validator

Generated validators are partial and expose a parameterless constructor plus `ConfigureAdditionalRules`:

```csharp
using FluentValidation;
using MyApplication.Contracts;

namespace MyApplication.Validation;

public sealed partial class CreateOrderRequestValidator
{
    partial void ConfigureAdditionalRules()
    {
        RuleFor(request => request.Name)
            .Must(name => !name.StartsWith("test-"))
            .WithMessage("Reserved order name.");
    }
}
```

For rules with dependencies, add a constructor that chains to the generated constructor:

```csharp
public CreateOrderRequestValidator(IOrderPolicy policy) : this()
{
    RuleFor(request => request.Name)
        .MustAsync(policy.IsAllowedAsync);
}
```

## Register generated validators

Every receiving assembly gets a `GeneratedValidatorRegistry` containing target-to-validator mappings. When DI abstractions are referenced, a conventional extension method is generated too:

```csharp
var services = new ServiceCollection();
services.AddGeneratedValidators();
```

Registration uses generated concrete types and does not scan assemblies.

Other assemblies can retrieve the generated validator types directly:

```csharp
IReadOnlyList<Type> validatorTypes =
    GeneratedValidatorRegistry.ValidatorTypes;

IReadOnlyDictionary<Type, Type> validatorsByTargetType =
    GeneratedValidatorRegistry.ValidatorTypesByTargetType;
```

The returned collection is read-only and stable for the generated assembly.

## Localization

The first Ling rule extension adds Ling messages directly to the current manager when it derives from `FluentValidation.Resources.LanguageManager`. It never replaces the manager or changes its settings:

```csharp
ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("zh-CN");
```

Custom `ILanguageManager` implementations can import every entry from `LingValidatorTranslations.All`; a one-time `Trace` warning is emitted when automatic registration is unavailable.

## Supported frameworks

Runtime assets are provided for:

- .NET Standard 2.0 and 2.1
- .NET Core 3.1
- .NET 5 through .NET 10

The package uses a tested FluentValidation major-version range for each target framework.

## Related packages

- `Ling.FluentValidation.Annotations` contains dependency-free declarations, analyzers, and code fixes.

## License

[MIT](https://github.com/ling921/ling-fluent-validation/blob/master/LICENSE)
