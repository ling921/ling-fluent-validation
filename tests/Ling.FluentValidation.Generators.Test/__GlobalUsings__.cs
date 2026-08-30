global using Ling.FluentValidation.Generators.Test.Common;
global using Microsoft.CodeAnalysis;
global using Microsoft.CodeAnalysis.CSharp;
global using System.Text;

#if NETCOREAPP3_1
global using Libs = Basic.Reference.Assemblies.NetCoreApp31.References;
#elif NET5_0
global using Libs = Basic.Reference.Assemblies.Net50.References;
#elif NET6_0
global using Libs = Basic.Reference.Assemblies.Net60.References;
#elif NET7_0
global using Libs = Basic.Reference.Assemblies.Net70.References;
#elif NET8_0
global using Libs = Basic.Reference.Assemblies.Net80.References;
#else
global using Libs = Basic.Reference.Assemblies.Net80.References;
#endif
