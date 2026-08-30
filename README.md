# Ling.FluentValidation

English | [简体中文](README.zh-CN.md)

`Ling.FluentValidation` turns validation attributes on request and DTO types into strongly typed [FluentValidation](https://github.com/FluentValidation/FluentValidation) validators. Contracts can remain independent of FluentValidation, while validators are generated in the application or validation assembly that owns them.

## Packages

| Package | Purpose |
| --- | --- |
| [`Ling.FluentValidation.Annotations`](src/Ling.FluentValidation.Annotations/README.md) [![NuGet](https://img.shields.io/nuget/v/Ling.FluentValidation.Annotations.svg)](https://www.nuget.org/packages/Ling.FluentValidation.Annotations/) | Dependency-free attributes, diagnostics, and code fixes for contract projects. |
| [`Ling.FluentValidation`](src/Ling.FluentValidation/README.md) [![NuGet](https://img.shields.io/nuget/v/Ling.FluentValidation.svg)](https://www.nuget.org/packages/Ling.FluentValidation/) | Source generator, FluentValidation runtime validators, and rule extensions. |

## Features

- Explicit validator generation with `[GenerateValidator]`.
- Cross-assembly generation without scanning every project reference.
- Contract assemblies with no FluentValidation dependency.
- Ling validation attributes and standard DataAnnotations support.
- Class, record, nested type, and inherited-member support.
- Partial generated validators for custom rules and dependency-injected constructors.
- A generated registry for deterministic validator discovery and optional DI registration.
- Compile-time diagnostics and a code fix for missing generation markers.
- .NET Core 3.1, .NET 5–10, and .NET Standard 2.0/2.1 runtime assets.

## Quick start

Reference only the annotations package from the project that owns the DTOs:

```xml
<PackageReference Include="Ling.FluentValidation.Annotations" Version="..." />
```

```csharp
using Ling.FluentValidation.Annotations;

namespace MyApplication.Contracts;

[GenerateValidator]
public sealed class CreateOrderRequest
{
    [NotEmpty]
    [Length(3, 100)]
    public string Name { get; init; } = string.Empty;
}
```

Install the main package in the project that should contain the validators. The source generator is included as a compiler asset and does not become a runtime dependency:

```xml
<PackageReference Include="Ling.FluentValidation" Version="..." />
```

Select the contract assembly explicitly:

```csharp
using Ling.FluentValidation.Annotations;
using MyApplication.Contracts;

[assembly: GenerateValidatorsFromAssemblyContaining(typeof(CreateOrderRequest))]
```

The generator creates an `internal sealed partial` validator in `MyApplication.Contracts.Validators` by default. Configure a different namespace or visibility on the receiving assembly:

```csharp
[assembly: ValidatorGenerationOptions(
    Namespace = "MyApplication.Validation",
    Visibility = GeneratedValidatorVisibility.Public)]
```

## Custom rules

Add rules that do not belong in a contract through another part of the generated validator:

```csharp
using FluentValidation;
using MyApplication.Contracts;

namespace MyApplication.Validation;

public sealed partial class CreateOrderRequestValidator
{
    partial void ConfigureAdditionalRules()
    {
        RuleFor(request => request.Name)
            .Must(name => !name.StartsWith("test-"));
    }
}
```

For rules that require services, add another constructor that calls the generated parameterless constructor with `this()`.

## Dependency injection

When `Microsoft.Extensions.DependencyInjection.Abstractions` is available, the generator also emits a conventional DI extension that registers concrete validator mappings without assembly scanning:

```csharp
var services = new ServiceCollection();
services.AddGeneratedValidators();
```

Other assemblies can obtain the generated validator types without reflection:

```csharp
IReadOnlyList<Type> validatorTypes =
    GeneratedValidatorRegistry.ValidatorTypes;

IReadOnlyDictionary<Type, Type> validatorsByTargetType =
    GeneratedValidatorRegistry.ValidatorTypesByTargetType;
```

Each validation assembly owns its registry, so applications with multiple validation assemblies call the registry exposed by each one explicitly.

## Localization

The first Ling rule extension registers Ling messages directly in the current FluentValidation language manager when it derives from `FluentValidation.Resources.LanguageManager`; the manager instance and its settings are never replaced. Set the culture normally through FluentValidation:

```csharp
ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("zh-CN");
```

For a custom `ILanguageManager` implementation, import `LingValidatorTranslations.All` yourself. The library writes a one-time `Trace` warning when automatic registration is unavailable.

## Design

- `Annotations` owns declarations and design-time diagnostics, but has no FluentValidation dependency.
- `Ling.FluentValidation` owns runtime behavior and embeds the source generator as a compiler-only asset. The generator reads source and explicitly selected referenced assembly metadata, then emits validators into the current compilation.

Only types in the current assembly and assemblies selected with `GenerateValidatorsFromAssemblyContaining` participate in generation. This keeps generation deterministic and prevents accidental validation code from unrelated references.

## Samples and development

- Cross-assembly sample: [`samples/Ling.FluentValidation.Sample`](samples/README.md)
- Build: `dotnet build Ling.FluentValidation.sln`
- Test all supported runtimes: `dotnet test Ling.FluentValidation.sln -c Release`

## Contributing

Contributions are welcome. Please open an issue or pull request and include tests for behavior changes.

## License

[MIT](LICENSE)
