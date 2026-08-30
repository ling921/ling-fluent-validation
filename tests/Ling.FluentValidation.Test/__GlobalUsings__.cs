global using FluentValidation;
global using FluentValidation.Resources;
global using Ling.FluentValidation.Test.Common;
global using Ling.FluentValidation.Validators;
global using System.ComponentModel.DataAnnotations;
global using System.Globalization;
#if NETCOREAPP3_1
global using ValidationContext = Ling.FluentValidation.Validators.ValidationContext<Ling.FluentValidation.Test.Common.TestClass>;
#else
global using ValidationContext = FluentValidation.ValidationContext<Ling.FluentValidation.Test.Common.TestClass>;
#endif
