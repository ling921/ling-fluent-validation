# Samples

The sample is split into two assemblies:

- `Ling.FluentValidation.Sample.Contracts` references only the dependency-free annotations package and marks its request DTO explicitly;
- `Ling.FluentValidation.Sample` selects the contracts assembly, receives the generated validator, and adds custom and nested-object rules through a partial class;
- the application registers generated validators through `services.AddGeneratedValidators()`, resolves one through DI, and prints automatically localized validation failures.

Run the sample from the repository root:

```shell
dotnet run --project samples/Ling.FluentValidation.Sample/Ling.FluentValidation.Sample.csproj
```
