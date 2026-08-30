using Ling.FluentValidation;
using Ling.FluentValidation.Annotations;
using Ling.FluentValidation.Sample.Models;

[assembly: GenerateValidatorsFromAssemblyContaining(typeof(CreateAccountRequest))]
[assembly: ValidatorGenerationOptions(
    Namespace = "Ling.FluentValidation.Sample.Validation",
    Visibility = GeneratedValidatorVisibility.Public)]
