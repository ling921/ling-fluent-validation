# Ling.FluentValidation

[English](README.md) | 简体中文

`Ling.FluentValidation` 根据 Request 和 DTO 类型上的校验特性生成强类型 [FluentValidation](https://github.com/FluentValidation/FluentValidation) 验证器。契约程序集无需依赖 FluentValidation，验证器则生成到真正负责校验的 Application 或 Validation 程序集中。

## NuGet 包

| 包 | 用途 |
| --- | --- |
| [`Ling.FluentValidation.Annotations`](src/Ling.FluentValidation.Annotations/README.zh-CN.md) [![NuGet](https://img.shields.io/nuget/v/Ling.FluentValidation.Annotations.svg)](https://www.nuget.org/packages/Ling.FluentValidation.Annotations/) | 面向契约项目的零 FluentValidation 依赖特性、诊断和 Code Fix。 |
| [`Ling.FluentValidation`](src/Ling.FluentValidation/README.zh-CN.md) [![NuGet](https://img.shields.io/nuget/v/Ling.FluentValidation.svg)](https://www.nuget.org/packages/Ling.FluentValidation/) | 源代码生成器、FluentValidation 运行时验证器和规则扩展。 |

## 功能

- 使用 `[GenerateValidator]` 显式参与验证器生成。
- 支持跨程序集生成，且不会扫描所有项目引用。
- Contracts 程序集不需要依赖 FluentValidation。
- 同时支持 Ling 校验特性和标准 DataAnnotations。
- 支持 class、record、嵌套类型和继承成员。
- 通过 partial 验证器添加自定义规则和依赖注入构造函数。
- 生成确定性的验证器注册表，并可选生成 DI 注册方法。
- 提供编译期诊断和补充生成标记的 Code Fix。
- 运行时资产覆盖 .NET Core 3.1、.NET 5–10 和 .NET Standard 2.0/2.1。

## 快速开始

定义 DTO 的项目只引用 Annotations 包：

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

在需要承载验证器的项目中安装主包。源代码生成器作为编译器资产包含在主包中，不会成为运行时依赖：

```xml
<PackageReference Include="Ling.FluentValidation" Version="..." />
```

显式选择契约程序集：

```csharp
using Ling.FluentValidation.Annotations;
using MyApplication.Contracts;

[assembly: GenerateValidatorsFromAssemblyContaining(typeof(CreateOrderRequest))]
```

默认情况下，生成器会在 `MyApplication.Contracts.Validators` 中创建 `internal sealed partial` 验证器。可以在接收代码的程序集中修改命名空间和可见性：

```csharp
[assembly: ValidatorGenerationOptions(
    Namespace = "MyApplication.Validation",
    Visibility = GeneratedValidatorVisibility.Public)]
```

## 自定义规则

不适合放在契约中的规则可以通过生成验证器的另一个 partial 部分补充：

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

如果规则需要服务依赖，请增加一个使用 `this()` 调用生成无参构造函数的构造函数。

## 依赖注入

当项目中存在 `Microsoft.Extensions.DependencyInjection.Abstractions` 时，生成器还会生成符合惯例的 DI 扩展方法，使用具体类型映射注册验证器，无需程序集扫描：

```csharp
var services = new ServiceCollection();
services.AddGeneratedValidators();
```

其他程序集可以直接获取生成的验证器类型，无需反射扫描：

```csharp
IReadOnlyList<Type> validatorTypes =
    GeneratedValidatorRegistry.ValidatorTypes;

IReadOnlyDictionary<Type, Type> validatorsByTargetType =
    GeneratedValidatorRegistry.ValidatorTypesByTargetType;
```

每个验证程序集分别拥有自己的注册表；如果应用包含多个验证程序集，应显式调用各程序集公开的注册表。

## 本地化

首次使用 Ling 规则扩展时，如果当前语言管理器继承自 `FluentValidation.Resources.LanguageManager`，库会直接向该实例注册 Ling 消息，不会替换实例或修改其设置。区域仍按 FluentValidation 的常规方式配置：

```csharp
ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("zh-CN");
```

如果应用使用自行实现的 `ILanguageManager`，请自行导入 `LingValidatorTranslations.All`。无法自动注册时，库会通过 `Trace` 输出一次警告。

## 设计

- `Annotations` 负责声明和设计期诊断，但不依赖 FluentValidation。
- `Ling.FluentValidation` 负责运行时行为，并将源代码生成器作为仅编译期资产内置。生成器从源码及显式选择的引用程序集元数据读取规则，并将验证器生成到当前编译中。

只有当前程序集和通过 `GenerateValidatorsFromAssemblyContaining` 显式选择的程序集会参与生成，从而保证结果稳定，并避免无关引用意外产生验证代码。

## 示例与开发

- 跨程序集示例：[`samples/Ling.FluentValidation.Sample`](samples/README.md)
- 构建：`dotnet build Ling.FluentValidation.sln`
- 在全部受支持运行时上测试：`dotnet test Ling.FluentValidation.sln -c Release`

## 参与贡献

欢迎提交 Issue 或 Pull Request。行为变更应同时包含对应测试。

## 协议

[MIT](LICENSE)
