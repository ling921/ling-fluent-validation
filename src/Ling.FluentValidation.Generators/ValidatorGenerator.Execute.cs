using Ling.FluentValidation.Extensions;
using Ling.FluentValidation.Generators.Helpers;
using Ling.FluentValidation.Generators.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Collections.Immutable;

namespace Ling.FluentValidation.Generators;

partial class ValidatorGenerator
{
    private static class Execute
    {
        private const string CASCADE_MODE_CONTINUE = "Ling.FluentValidation.Annotations.ValidationCascadeMode.Continue";
        private const string CASCADE_MODE_STOP = "Ling.FluentValidation.Annotations.ValidationCascadeMode.Stop";
        private const string VISIBILITY_PUBLIC = "Ling.FluentValidation.Annotations.GeneratedValidatorVisibility.Public";

        public static GenerationSpec CreateGenerationSpec(
            Compilation compilation,
            INamedTypeSymbol classSymbol,
            Location location,
            bool supportsSplitCascadeModes)
        {
            var assemblySymbol = compilation.Assembly;
            var targetNamespace = classSymbol.ContainingNamespace.IsGlobalNamespace
                ? "Validators"
                : $"{classSymbol.ContainingNamespace.ToDisplayString()}.Validators";
            var @namespace = targetNamespace;
            string? classLevelCascadeMode = null;
            string? ruleLevelCascadeMode = null;
            var canBePublic = IsEffectivelyPublic(classSymbol);
            var keyword = "internal";
            var isSealed = true;
            var members = ImmutableArray.CreateBuilder<MemberInfo>();

            // Find generation options on the assembly receiving generated code.
            foreach (var attribute in assemblySymbol.GetAttributes())
            {
                var attributeTypeName = attribute.AttributeClass!.GetFullyQualifiedMetadataName();
                if (attributeTypeName == Constants.ValidatorGenerationOptionsAttributeFullyQualifiedMetadataName)
                {
                    var namespaceTypeConstant = attribute.NamedArguments.FirstOrDefault(x => x.Key == "Namespace").Value;
                    if (!namespaceTypeConstant.IsNull)
                    {
                        @namespace = (string)namespaceTypeConstant.Value!;
                    }

                    var visibilityTypedConstant = attribute.NamedArguments.FirstOrDefault(x => x.Key == "Visibility").Value;
                    if (!visibilityTypedConstant.IsNull)
                    {
                        keyword = visibilityTypedConstant.ToCSharpString() == VISIBILITY_PUBLIC && canBePublic ? "public" : "internal";
                    }

                    var isSealedTypedConstant = attribute.NamedArguments.FirstOrDefault(x => x.Key == "IsSealed").Value;
                    if (!isSealedTypedConstant.IsNull)
                    {
                        isSealed = (bool)isSealedTypedConstant.Value!;
                    }

                    var classModeTypedConstant = attribute.NamedArguments.FirstOrDefault(x => x.Key == "ClassLevelCascadeMode").Value;
                    if (!classModeTypedConstant.IsNull)
                    {
                        classLevelCascadeMode = classModeTypedConstant.ToCSharpString() switch
                        {
                            CASCADE_MODE_CONTINUE => "global::FluentValidation.CascadeMode.Continue",
                            CASCADE_MODE_STOP => "global::FluentValidation.CascadeMode.Stop",
                            _ => null,
                        };
                    }

                    var ruleModeTypedConstant = attribute.NamedArguments.FirstOrDefault(x => x.Key == "RuleLevelCascadeMode").Value;
                    if (!ruleModeTypedConstant.IsNull)
                    {
                        ruleLevelCascadeMode = ruleModeTypedConstant.ToCSharpString() switch
                        {
                            CASCADE_MODE_CONTINUE => "global::FluentValidation.CascadeMode.Continue",
                            CASCADE_MODE_STOP => "global::FluentValidation.CascadeMode.Stop",
                            _ => null,
                        };
                    }

                    break;
                }
            }

            // Include accessible rules declared on the target and its base types.
            var memberNames = new HashSet<string>(StringComparer.Ordinal);
            for (INamedTypeSymbol? current = classSymbol; current is not null && current.SpecialType == SpecialType.None; current = current.BaseType)
            {
                foreach (var member in current.GetMembers())
                {
                    if (member is IPropertySymbol or IFieldSymbol &&
                        memberNames.Add(member.Name) &&
                        GetUnsupportedMemberReason(compilation, member) is null)
                    {
                        var info = new MemberInfo(member);
                        if (info.Attributes.Any(a => GenerationDefaults.MemberAttributeFullyQualifiedMetadataNames.Contains(a.AttributeClass!.GetFullyQualifiedMetadataName())))
                        {
                            members.Add(info);
                        }
                    }
                }
            }

            // Find the possible when clauses
            var maybeWhenClauses = ImmutableDictionary.CreateBuilder<string, MemberKind>();
            for (INamedTypeSymbol? current = classSymbol; current is not null && current.SpecialType == SpecialType.None; current = current.BaseType)
            {
                foreach (var member in current.GetMembers().Where(m => compilation.IsSymbolAccessibleWithin(m, compilation.Assembly)))
                {
                    if (member is IFieldSymbol { Type.SpecialType: SpecialType.System_Boolean, IsStatic: false }) maybeWhenClauses[member.Name] = MemberKind.Field;
                    else if (member is IPropertySymbol { Type.SpecialType: SpecialType.System_Boolean, IsStatic: false }) maybeWhenClauses[member.Name] = MemberKind.Property;
                    else if (member is IMethodSymbol { ReturnType.SpecialType: SpecialType.System_Boolean, Parameters.Length: 0, IsStatic: false, MethodKind: not MethodKind.PropertyGet }) maybeWhenClauses[member.Name] = MemberKind.Method;
                }
            }

            return new GenerationSpec(
                assemblySymbol.Name,
                ImmutableArray.Create(compilation.ReferencedAssemblyNames.ToArray()),
                HierarchyInfo.From(classSymbol),
                @namespace,
                keyword,
                isSealed,
                classLevelCascadeMode,
                ruleLevelCascadeMode,
                supportsSplitCascadeModes,
                ImmutableArray<string>.Empty,
                members.ToImmutable(),
                maybeWhenClauses.ToImmutable(),
                location);
        }

        public static string? GetUnsupportedTypeReason(Compilation compilation, INamedTypeSymbol typeSymbol)
        {
            for (INamedTypeSymbol? current = typeSymbol; current is not null; current = current.ContainingType)
            {
                if (current.Arity > 0)
                {
                    return "generic containing types are not supported";
                }

                if (!compilation.IsSymbolAccessibleWithin(current, compilation.Assembly))
                {
                    return "the containing type is not accessible from the generated validator namespace";
                }
            }

            return null;
        }

        private static bool IsEffectivelyPublic(INamedTypeSymbol typeSymbol)
        {
            for (INamedTypeSymbol? current = typeSymbol; current is not null; current = current.ContainingType)
            {
                if (current.DeclaredAccessibility != Accessibility.Public)
                {
                    return false;
                }
            }

            return true;
        }

        public static string? GetUnsupportedMemberReason(Compilation compilation, ISymbol member)
        {
            if (member.IsStatic)
            {
                return "static members are not supported";
            }

            if (member is IPropertySymbol { IsIndexer: true })
            {
                return "indexers are not supported";
            }

            if (!compilation.IsSymbolAccessibleWithin(member, compilation.Assembly))
            {
                return "the member is not accessible from the generated validator namespace";
            }

            return GetUnsupportedTypeReason(compilation, member.ContainingType);
        }

        public static string GetGeneratedValidatorsClassText(
            ImmutableArray<GenerationSpec> items,
            bool referencedDI)
        {
            var @namespace = "Ling.FluentValidation";
            if (items.FirstOrDefault() is { AssemblyName: { Length: > 0 } assemblyName })
            {
                @namespace = assemblyName;
            }

            var cb = new CodeBuilder("""
                // <auto-generated/>

                #pragma warning disable
                #nullable enable annotations

                """);
            cb.AppendFormatLine("namespace {0}", @namespace);
            cb.OpenBrace();
            cb.AppendLine("/// <summary>");
            cb.AppendLine("/// Auto-generated validator types.");
            cb.AppendLine("/// </summary>");
            cb.AppendLine("public static class GeneratedValidatorRegistry");
            cb.OpenBrace();

            cb.AppendLine("/// <summary>");
            cb.AppendLine("/// Backing collection for all generated validator types.");
            cb.AppendLine("/// </summary>");
            cb.AppendLine("private static readonly global::System.Collections.Generic.IReadOnlyList<global::System.Type> _validatorTypes = global::System.Array.AsReadOnly(new global::System.Type[]");
            cb.OpenBrace();
            foreach (var item in items)
            {
                cb.AppendFormatLine("typeof({0}),", item.GenerateClassFullName);
            }
            cb.CloseBrace(textAfterBrace: ")", appendSemicolon: true);

            cb.AppendLine();

            cb.AppendLine("/// <summary>");
            cb.AppendLine("/// Gets every validator type generated into this assembly.");
            cb.AppendLine("/// </summary>");
            cb.AppendLine("public static global::System.Collections.Generic.IReadOnlyList<global::System.Type> ValidatorTypes => _validatorTypes;");

            cb.AppendLine();

            cb.AppendLine("/// <summary>");
            cb.AppendLine("/// Gets the generated validator type for each validated target type.");
            cb.AppendLine("/// </summary>");
            cb.AppendLine("public static global::System.Collections.Generic.IReadOnlyDictionary<global::System.Type, global::System.Type> ValidatorTypesByTargetType { get; } = new global::System.Collections.ObjectModel.ReadOnlyDictionary<global::System.Type, global::System.Type>(new global::System.Collections.Generic.Dictionary<global::System.Type, global::System.Type>");
            cb.OpenBrace();
            foreach (var item in items)
            {
                cb.AppendFormatLine("[typeof({0})] = typeof({1}),", item.TargetClassFullName, item.GenerateClassFullName);
            }
            cb.CloseBrace(textAfterBrace: ")", appendSemicolon: true);

            if (referencedDI)
            {
                cb.AppendLine();
                cb.CloseBrace();
                cb.AppendLine();

                cb.AppendLine("/// <summary>");
                cb.AppendLine("/// Dependency injection extensions for validators generated into this assembly.");
                cb.AppendLine("/// </summary>");
                cb.AppendLine("public static class GeneratedValidatorServiceCollectionExtensions");
                cb.OpenBrace();
                cb.AppendLine("/// <summary>");
                cb.AppendLine("/// Adds validators generated into this assembly to a service collection.");
                cb.AppendLine("/// </summary>");
                cb.AppendLine("/// <param name=\"services\">The service collection.</param>");
                cb.AppendLine("/// <returns>The same service collection for chaining.</returns>");
                cb.AppendLine("public static global::Microsoft.Extensions.DependencyInjection.IServiceCollection AddGeneratedValidators(this global::Microsoft.Extensions.DependencyInjection.IServiceCollection services)");
                cb.OpenBrace();
                foreach (var item in items)
                {
                    if (!item.HierarchyInfo.Hierarchy[0].IsAbstract)
                    {
                        cb.AppendFormatLine("global::Microsoft.Extensions.DependencyInjection.Extensions.ServiceCollectionDescriptorExtensions.TryAddTransient<global::FluentValidation.IValidator<{0}>, {1}>(services);", item.TargetClassFullName, item.GenerateClassFullName);
                    }
                }
                cb.AppendLine();
                cb.AppendLine("return services;");
                cb.CloseBrace();
            }

            return cb.ToString();
        }

        public static string GetValidatorClassText(GenerationSpec spec)
        {
            var keywords = spec.Keyword;
            if (spec.IsSealed)
            {
                keywords += " sealed";
            }

            var targetClassFullName = spec.TargetClassFullName;

            var cb = new CodeBuilder("""
                // <auto-generated/>

                #pragma warning disable
                #nullable enable annotations

                using global::FluentValidation;
                using global::Ling.FluentValidation;


                """);
            cb.AppendFormatLine("namespace {0}", spec.Namespace);
            cb.OpenBrace();

            cb.AppendLine("/// <summary>");
            cb.AppendFormatLine("/// Validator for <see cref=\"{0}\"/>.", targetClassFullName);
            cb.AppendLine("/// </summary>");
            cb.AppendFormatLine("{0} partial class {1} : global::FluentValidation.AbstractValidator<{2}>", keywords, spec.GenerateClassName, targetClassFullName);
            cb.OpenBrace();

            cb.AppendLine("/// <summary>");
            cb.AppendLine("/// Initializes a new instance of the validator class.");
            cb.AppendLine("/// </summary>");
            cb.AppendFormatLine("public {0}()", spec.GenerateClassName);
            cb.OpenBrace();

            if (spec.SupportsSplitCascadeModes)
            {
                if (spec.ClassLevelCascadeMode is not null)
                {
                    cb.AppendFormatLine("ClassLevelCascadeMode = {0};", spec.ClassLevelCascadeMode);
                }
                if (spec.RuleLevelCascadeMode is not null)
                {
                    cb.AppendFormatLine("RuleLevelCascadeMode = {0};", spec.RuleLevelCascadeMode);
                }
            }
            else if ((spec.RuleLevelCascadeMode ?? spec.ClassLevelCascadeMode) is { } cascadeMode)
            {
                if (cascadeMode == "global::FluentValidation.CascadeMode.Stop")
                {
                    cascadeMode = "global::FluentValidation.CascadeMode.StopOnFirstFailure";
                }
                cb.AppendFormatLine("CascadeMode = {0};", cascadeMode);
            }
            if (spec.ClassLevelCascadeMode is not null || spec.RuleLevelCascadeMode is not null)
            {
                cb.AppendLine();
            }

            if (spec.Includes.Length > 0)
            {
                foreach (var include in spec.Includes)
                {
                    cb.AppendFormatLine("Include(new {0}());", include);
                }
                if (spec.Members.Length > 0)
                {
                    cb.AppendLine();
                }
            }

            for (var i = 0; i < spec.Members.Length; i++)
            {
                var property = spec.Members[i];
                for (var j = 0; j < property.Attributes.Length; j++)
                {
                    var attribute = property.Attributes[j];
                    var attributeTypeQualifiedName = attribute.AttributeClass?.GetFullyQualifiedMetadataName();

                    ValidationAttributeInfoBase? attributeInfo = attributeTypeQualifiedName switch
                    {
                        Constants.AllowedValuesAttributeFullyQualifiedMetadataName => new LingAllowedValuesAttributeInfo(property.MemberName, attribute, spec.MaybeWhenClauses),
                        Constants.Base64StringAttributeFullyQualifiedMetadataName => new LingBase64StringAttributeInfo(property.MemberName, attribute, spec.MaybeWhenClauses),
                        Constants.CompareAttributeFullyQualifiedMetadataName => new LingCompareAttributeInfo(property.MemberName, attribute, spec.MaybeWhenClauses),
                        Constants.CreditCardAttributeFullyQualifiedMetadataName => new LingCreditCardAttributeInfo(property.MemberName, attribute, spec.MaybeWhenClauses),
                        Constants.DeniedValuesAttributeFullyQualifiedMetadataName => new LingDeniedValuesAttributeInfo(property.MemberName, attribute, spec.MaybeWhenClauses),
                        Constants.EmailAddressAttributeFullyQualifiedMetadataName => new LingEmailAddressAttributeInfo(property.MemberName, attribute, spec.MaybeWhenClauses),
                        Constants.EmptyAttributeFullyQualifiedMetadataName => new LingEmptyAttributeInfo(property.MemberName, attribute, spec.MaybeWhenClauses),
                        Constants.EnumAttributeFullyQualifiedMetadataName => new LingEnumAttributeInfo(property.MemberName, attribute, spec.MaybeWhenClauses),
                        Constants.EnumNameAttributeFullyQualifiedMetadataName => new LingEnumNameAttributeInfo(property.MemberName, attribute, spec.MaybeWhenClauses),
                        Constants.GenericEnumNameAttributeFullyQualifiedMetadataName => new LingEnumNameAttributeInfo(property.MemberName, attribute, spec.MaybeWhenClauses),
                        Constants.EqualAttributeFullyQualifiedMetadataName => new LingEqualAttributeInfo(property.MemberName, attribute, spec.MaybeWhenClauses),
                        Constants.ExclusiveBetweenAttributeFullyQualifiedMetadataName => new LingExclusiveBetweenAttributeInfo(property.MemberName, attribute, spec.MaybeWhenClauses),
                        Constants.FileExtensionsAttributeFullyQualifiedMetadataName => new LingFileExtensionsAttributeInfo(property.MemberName, attribute, spec.MaybeWhenClauses),
                        Constants.GreaterThanAttributeFullyQualifiedMetadataName => new LingGreaterThanAttributeInfo(property.MemberName, attribute, spec.MaybeWhenClauses),
                        Constants.GreaterThanOrEqualToAttributeFullyQualifiedMetadataName => new LingGreaterThanOrEqualToAttributeInfo(property.MemberName, attribute, spec.MaybeWhenClauses),
                        Constants.InclusiveBetweenAttributeFullyQualifiedMetadataName => new LingInclusiveBetweenAttributeInfo(property.MemberName, attribute, spec.MaybeWhenClauses),
                        Constants.LengthAttributeFullyQualifiedMetadataName => new LingLengthAttributeInfo(property.MemberName, attribute, spec.MaybeWhenClauses),
                        Constants.LessThanAttributeFullyQualifiedMetadataName => new LingLessThanAttributeInfo(property.MemberName, attribute, spec.MaybeWhenClauses),
                        Constants.LessThanOrEqualToAttributeFullyQualifiedMetadataName => new LingLessThanOrEqualToAttributeInfo(property.MemberName, attribute, spec.MaybeWhenClauses),
                        Constants.MatchesAttributeFullyQualifiedMetadataName => new LingMatchesAttributeInfo(property.MemberName, attribute, spec.MaybeWhenClauses),
                        Constants.MaximumLengthAttributeFullyQualifiedMetadataName => new LingMaximumLengthAttributeInfo(property.MemberName, attribute, spec.MaybeWhenClauses),
                        Constants.MinimumLengthAttributeFullyQualifiedMetadataName => new LingMinimumLengthAttributeInfo(property.MemberName, attribute, spec.MaybeWhenClauses),
                        Constants.NotEmptyAttributeFullyQualifiedMetadataName => new LingNotEmptyAttributeInfo(property.MemberName, attribute, spec.MaybeWhenClauses),
                        Constants.NotEqualAttributeFullyQualifiedMetadataName => new LingNotEqualAttributeInfo(property.MemberName, attribute, spec.MaybeWhenClauses),
                        Constants.NotNullAttributeFullyQualifiedMetadataName => new LingNotNullAttributeInfo(property.MemberName, attribute, spec.MaybeWhenClauses),
                        Constants.NullAttributeFullyQualifiedMetadataName => new LingNullAttributeInfo(property.MemberName, attribute, spec.MaybeWhenClauses),
                        Constants.PhoneAttributeFullyQualifiedMetadataName => new LingPhoneAttributeInfo(property.MemberName, attribute, spec.MaybeWhenClauses),
                        Constants.UrlAttributeFullyQualifiedMetadataName => new LingUrlAttributeInfo(property.MemberName, attribute, spec.MaybeWhenClauses),

                        Constants.SystemAllowedValuesAttributeFullyQualifiedMetadataName => new SystemAllowedValuesAttributeInfo(property.MemberName, attribute),
                        Constants.SystemBase64StringAttributeFullyQualifiedMetadataName => new SystemBase64StringAttributeInfo(property.MemberName, attribute),
                        Constants.SystemCompareAttributeFullyQualifiedMetadataName => new SystemCompareAttributeInfo(property.MemberName, attribute),
                        Constants.SystemCreditCardAttributeFullyQualifiedMetadataName => new SystemCreditCardAttributeInfo(property.MemberName, attribute),
                        Constants.SystemDeniedValuesAttributeFullyQualifiedMetadataName => new SystemDeniedValuesAttributeInfo(property.MemberName, attribute),
                        Constants.SystemEmailAddressAttributeFullyQualifiedMetadataName => new SystemEmailAddressAttributeInfo(property.MemberName, attribute),
                        Constants.SystemEnumDataTypeAttributeFullyQualifiedMetadataName => new SystemEnumDataTypeAttributeInfo(property, attribute),
                        Constants.SystemFileExtensionsAttributeFullyQualifiedMetadataName => new SystemFileExtensionsAttributeInfo(property.MemberName, attribute),
                        Constants.SystemLengthAttributeFullyQualifiedMetadataName => new SystemLengthAttributeInfo(property.MemberName, attribute),
                        Constants.SystemMaxLengthAttributeFullyQualifiedMetadataName => new SystemMaxLengthAttributeInfo(property.MemberName, attribute),
                        Constants.SystemMinLengthAttributeFullyQualifiedMetadataName => new SystemMinLengthAttributeInfo(property.MemberName, attribute),
                        Constants.SystemPhoneAttributeFullyQualifiedMetadataName => new SystemPhoneAttributeInfo(property.MemberName, attribute),
                        Constants.SystemRangeAttributeFullyQualifiedMetadataName => new SystemRangeAttributeInfo(property.MemberName, attribute),
                        Constants.SystemRegularExpressionAttributeFullyQualifiedMetadataName => new SystemRegularExpressionAttributeInfo(property.MemberName, attribute),
                        Constants.SystemRequiredAttributeFullyQualifiedMetadataName => new SystemRequiredAttributeInfo(property, attribute),
                        Constants.SystemStringLengthAttributeFullyQualifiedMetadataName => new SystemStringLengthAttributeInfo(property.MemberName, attribute),
                        Constants.SystemUrlAttributeFullyQualifiedMetadataName => new SystemUrlAttributeInfo(property.MemberName, attribute),
                        _ => null
                    };

                    attributeInfo?.AppendTo(cb);

                    if (i != spec.Members.Length - 1 && j == property.Attributes.Length - 1)
                    {
                        cb.AppendLine();
                    }
                }
            }

            cb.AppendLine();
            cb.AppendLine("ConfigureAdditionalRules();");
            cb.CloseBrace();
            cb.AppendLine();
            cb.AppendLine("partial void ConfigureAdditionalRules();");

            return cb.ToString();
        }
    }
}
