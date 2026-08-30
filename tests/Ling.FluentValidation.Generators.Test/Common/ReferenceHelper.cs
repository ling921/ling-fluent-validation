namespace Ling.FluentValidation.Generators.Test.Common;

internal static class ReferenceHelper
{
    public static IEnumerable<MetadataReference> GetRequiredReferences()
    {
        yield return Libs.netstandard;
        yield return Libs.SystemRuntime;
        yield return Libs.SystemLinqExpressions;
        yield return Libs.SystemComponentModelAnnotations;
        yield return Libs.SystemCollections;
        yield return Libs.SystemObjectModel;
        yield return Libs.SystemCollectionsConcurrent;
        yield return Libs.SystemTextRegularExpressions;
        yield return MetadataReference.CreateFromFile(typeof(global::FluentValidation.IValidator).Assembly.Location);
        yield return MetadataReference.CreateFromFile(typeof(IRuleBuilderExtensions).Assembly.Location);
        yield return MetadataReference.CreateFromFile(typeof(global::Ling.FluentValidation.Annotations.GenerateValidatorAttribute).Assembly.Location);
    }

    public static string TargetFramework =>
#if NETCOREAPP3_1
        "netcoreapp3.1";
#elif NET5_0
        "net5.0";
#elif NET6_0
        "net6.0";
#elif NET7_0
        "net7.0";
#elif NET8_0
        "net8.0";
#elif NET9_0
        "net9.0";
#elif NET10_0
        "net10.0";
#else
        "netstandard2.0";
#endif
}
