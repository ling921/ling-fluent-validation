# Ling.FluentValidation.Annotations

[English](https://github.com/ling921/fluent-validation-generator/blob/master/src/Ling.FluentValidation.Annotations/README.md) | 简体中文 | [项目文档](https://github.com/ling921/fluent-validation-generator/blob/master/README.zh-CN.md)

`Ling.FluentValidation.Annotations` 提供用于标记 DTO 和描述校验规则的纯声明 API。它不依赖 FluentValidation，因此共享的 Contracts 项目不会被迫引入校验运行时依赖。

该包还包含 Roslyn 分析器和 Code Fix，可以在编辑和构建阶段报告无效的规则用法。

## 安装

在定义 Request 和 DTO 类型的项目中安装：

```shell
dotnet add package Ling.FluentValidation.Annotations --prerelease
```

## 声明校验契约

验证器生成必须显式启用。包含规则的类型应标记 `[GenerateValidator]`：

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

如果类型声明了规则却没有标记，分析器会报告诊断，并提供添加 `[GenerateValidator]` 的 Code Fix。

## 规则

Annotations 提供值型、类型无关的规则，包括：

- null 和空值检查；
- 长度和正则表达式检查；
- 相等、大小比较、闭区间和开区间；
- 允许值和拒绝值；
- Email、Phone、URL、Credit Card、Base64、文件扩展名、枚举和枚举名称检查。

生成器也识别标准 `System.ComponentModel.DataAnnotations` 校验特性。Ling 特性可以配置规则错误消息、错误代码、严重级别、显示名称以及受支持的条件成员。

依赖服务、其他验证器或应用行为的规则应通过接收项目中的 partial 验证器实现，而不应写入契约程序集。

## 选择来源程序集

负责接收生成验证器的项目需要显式选择外部契约程序集：

```csharp
using Ling.FluentValidation.Annotations;
using MyApplication.Contracts;

[assembly: GenerateValidatorsFromAssemblyContaining(typeof(CreateOrderRequest))]
```

在 .NET 7 及以上版本中，可以使用泛型形式省略 `typeof`：

```csharp
[assembly: GenerateValidatorsFromAssemblyContaining<CreateOrderRequest>]
```

## 配置生成验证器

在接收代码的程序集上应用配置：

```csharp
[assembly: ValidatorGenerationOptions(
    Namespace = "MyApplication.Validation",
    Visibility = GeneratedValidatorVisibility.Public,
    IsSealed = true,
    ClassLevelCascadeMode = ValidationCascadeMode.Continue,
    RuleLevelCascadeMode = ValidationCascadeMode.Stop)]
```

## 目标框架

- `netstandard2.0` 提供基础声明 API。
- `net7.0` 增加仅用于简化 `typeof` 写法的泛型特性。

较新的应用会自动选择兼容资产。

## 相关包

- `Ling.FluentValidation` 提供源代码生成器、运行时规则实现和 FluentValidation 集成。

## 协议

[MIT](https://github.com/ling921/fluent-validation-generator/blob/master/LICENSE)
