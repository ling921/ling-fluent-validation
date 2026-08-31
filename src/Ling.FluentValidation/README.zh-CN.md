# Ling.FluentValidation

[English](https://github.com/ling921/ling-fluent-validation/blob/master/src/Ling.FluentValidation/README.md) | 简体中文 | [项目文档](https://github.com/ling921/ling-fluent-validation/blob/master/README.zh-CN.md)

`Ling.FluentValidation` 提供源代码生成器、FluentValidation 规则扩展和兼容的运行时验证器。生成器以仅编译期资产的方式分发；DTO 声明则位于零 FluentValidation 依赖的 `Ling.FluentValidation.Annotations` 包中。

## 安装

在负责承载生成验证器的 Application 或 Validation 项目中安装主包：

```xml
<PackageReference Include="Ling.FluentValidation" Version="..." />
```

主包会传递引用 `Ling.FluentValidation.Annotations`，包括其中的分析器和 Code Fix 资产。不过 Contracts 项目仍应直接引用 Annotations 包，并且不应引用本运行时包。

## 从契约程序集生成

```csharp
using Ling.FluentValidation.Annotations;
using MyApplication.Contracts;

[assembly: GenerateValidatorsFromAssemblyContaining(typeof(CreateOrderRequest))]
[assembly: ValidatorGenerationOptions(
    Namespace = "MyApplication.Validation",
    Visibility = GeneratedValidatorVisibility.Public)]
```

生成器只读取显式选择的引用程序集。当前程序集内已标记的 DTO 会自动参与生成。

## 扩展生成验证器

生成的验证器是 partial 类型，并提供无参构造函数和 `ConfigureAdditionalRules`：

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

需要依赖服务时，可以添加一个调用生成构造函数的构造函数：

```csharp
public CreateOrderRequestValidator(IOrderPolicy policy) : this()
{
    RuleFor(request => request.Name)
        .MustAsync(policy.IsAllowedAsync);
}
```

## 注册生成验证器

每个接收生成代码的程序集都会得到一个 `GeneratedValidatorRegistry`，其中包含目标类型到验证器类型的映射。当项目引用 DI abstractions 时，还会生成符合惯例的扩展方法：

```csharp
var services = new ServiceCollection();
services.AddGeneratedValidators();
```

注册过程使用生成的具体类型，不会扫描程序集。

其他程序集还可以直接获取全部生成验证器类型：

```csharp
IReadOnlyList<Type> validatorTypes =
    GeneratedValidatorRegistry.ValidatorTypes;

IReadOnlyDictionary<Type, Type> validatorsByTargetType =
    GeneratedValidatorRegistry.ValidatorTypesByTargetType;
```

返回的集合只读，并且在对应生成程序集内保持稳定。

## 本地化

首次使用 Ling 规则扩展时，如果当前管理器继承自 `FluentValidation.Resources.LanguageManager`，库会直接注册 Ling 消息，不会替换管理器或修改设置：

```csharp
ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("zh-CN");
```

自行实现的 `ILanguageManager` 可以导入 `LingValidatorTranslations.All` 中的全部条目；无法自动注册时会通过 `Trace` 输出一次警告。

## 支持框架

运行时资产覆盖：

- .NET Standard 2.0 和 2.1
- .NET Core 3.1
- .NET 5 至 .NET 10

每个目标框架均使用经过测试且有上界的 FluentValidation 主版本范围。

## 相关包

- `Ling.FluentValidation.Annotations` 包含零 FluentValidation 依赖的声明、分析器和 Code Fix。

## 协议

[MIT](https://github.com/ling921/ling-fluent-validation/blob/master/LICENSE)
