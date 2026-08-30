using Ling.FluentValidation.Analyzers.Diagnostics;
using Ling.FluentValidation.Analyzers.Infrastructure;
using Ling.FluentValidation.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;

namespace Ling.FluentValidation.Analyzers;

/// <summary>
/// The analyzer for migrating attributes from 'System.ComponentModel.DataAnnotations' to 'Ling.FluentValidation.Annotations'.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class MigrateAttributeAnalyzer : AttributeDiagnosticAnalyzer
{
    /// <inheritdoc/>
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => [
        DiagnosticDescriptors.UseLingValidationAttributeFix
    ];

    /// <inheritdoc/>
    public override ImmutableArray<string> TargetAttributeFullyQualifiedMetadataNames => [
        Constants.SystemAllowedValuesAttributeFullyQualifiedMetadataName,
        Constants.SystemBase64StringAttributeFullyQualifiedMetadataName,
        Constants.SystemCompareAttributeFullyQualifiedMetadataName,
        Constants.SystemCreditCardAttributeFullyQualifiedMetadataName,
        Constants.SystemDeniedValuesAttributeFullyQualifiedMetadataName,
        Constants.SystemEmailAddressAttributeFullyQualifiedMetadataName,
        Constants.SystemEnumDataTypeAttributeFullyQualifiedMetadataName,
        Constants.SystemFileExtensionsAttributeFullyQualifiedMetadataName,
        Constants.SystemLengthAttributeFullyQualifiedMetadataName,
        Constants.SystemMaxLengthAttributeFullyQualifiedMetadataName,
        Constants.SystemMinLengthAttributeFullyQualifiedMetadataName,
        Constants.SystemPhoneAttributeFullyQualifiedMetadataName,
        Constants.SystemRangeAttributeFullyQualifiedMetadataName,
        Constants.SystemRegularExpressionAttributeFullyQualifiedMetadataName,
        Constants.SystemRequiredAttributeFullyQualifiedMetadataName,
        Constants.SystemStringLengthAttributeFullyQualifiedMetadataName,
        Constants.SystemUrlAttributeFullyQualifiedMetadataName,
    ];

    /// <inheritdoc/>
    public override AttributeTargets AttributeTargets => AttributeTargets.Property | AttributeTargets.Field;

    /// <inheritdoc/>
    protected override void AnalyzePropertyAttribute(SyntaxNodeAnalysisContext context, PropertyDeclarationSyntax propertyDeclarationSyntax, IPropertySymbol propertySymbol, ImmutableArray<AttributeInfo> attributes)
    {
        AnalyzeAttributeCore(context, propertyDeclarationSyntax, propertySymbol, attributes);
    }

    /// <inheritdoc/>
    protected override void AnalyzeFieldAttribute(SyntaxNodeAnalysisContext context, FieldDeclarationSyntax fieldDeclarationSyntax, IFieldSymbol fieldSymbol, ImmutableArray<AttributeInfo> attributes)
    {
        AnalyzeAttributeCore(context, fieldDeclarationSyntax, fieldSymbol, attributes);
    }

    private void AnalyzeAttributeCore(
        SyntaxNodeAnalysisContext context,
        MemberDeclarationSyntax syntax,
        ISymbol symbol,
        ImmutableArray<AttributeInfo> attributes)
    {
        var typeSymbol = symbol is IPropertySymbol propertySymbol
            ? propertySymbol.Type
            : ((IFieldSymbol)symbol).Type;

        foreach (var (fullQualifiedMetadataName, attributeData) in attributes)
        {
            // Ignore attributes that have error diagnostics.
            var attributeSyntax = (AttributeSyntax)attributeData.ApplicationSyntaxReference!.GetSyntax()!;
            if (context.SemanticModel.GetDiagnostics(attributeSyntax.Span, context.CancellationToken).Any(d => d.Severity == DiagnosticSeverity.Error))
            {
                continue;
            }

            // CodeFix ignore attributes that have a message resource name or type.
            if (attributeData.GetNamedArgument("ErrorMessageResourceName") is { Value: string { Length: > 0 } } ||
                attributeData.GetNamedArgument("ErrorMessageResourceType") is { Value: INamedTypeSymbol })
            {
                continue;
            }

            var builder = ImmutableDictionary.CreateBuilder<string, string?>();
            builder.Add("OriginalAttribute", fullQualifiedMetadataName);

            switch (fullQualifiedMetadataName)
            {
                case Constants.SystemAllowedValuesAttributeFullyQualifiedMetadataName:
                    builder.Add("NewAttribute", Constants.AllowedValuesAttributeFullyQualifiedMetadataName);
                    break;

                case Constants.SystemBase64StringAttributeFullyQualifiedMetadataName:
                    builder.Add("NewAttribute", Constants.Base64StringAttributeFullyQualifiedMetadataName);
                    break;

                case Constants.SystemCompareAttributeFullyQualifiedMetadataName:
                    builder.Add("NewAttribute", Constants.CompareAttributeFullyQualifiedMetadataName);
                    break;

                case Constants.SystemCreditCardAttributeFullyQualifiedMetadataName:
                    builder.Add("NewAttribute", Constants.CreditCardAttributeFullyQualifiedMetadataName);
                    break;

                case Constants.SystemDeniedValuesAttributeFullyQualifiedMetadataName:
                    builder.Add("NewAttribute", Constants.DeniedValuesAttributeFullyQualifiedMetadataName);
                    break;

                case Constants.SystemEmailAddressAttributeFullyQualifiedMetadataName:
                    builder.Add("NewAttribute", Constants.EmailAddressAttributeFullyQualifiedMetadataName);
                    break;

                case Constants.SystemEnumDataTypeAttributeFullyQualifiedMetadataName:
                    if (typeSymbol.SpecialType == SpecialType.System_String)
                    {
                        builder.Add("NewAttribute", Constants.EnumNameAttributeFullyQualifiedMetadataName);
                    }
                    else
                    {
                        builder.Add("NewAttribute", Constants.EnumAttributeFullyQualifiedMetadataName);
                    }
                    break;

                case Constants.SystemFileExtensionsAttributeFullyQualifiedMetadataName:
                    builder.Add("NewAttribute", Constants.FileExtensionsAttributeFullyQualifiedMetadataName);
                    break;

                case Constants.SystemLengthAttributeFullyQualifiedMetadataName:
                    builder.Add("NewAttribute", Constants.LengthAttributeFullyQualifiedMetadataName);
                    break;

                case Constants.SystemMaxLengthAttributeFullyQualifiedMetadataName:
                    builder.Add("NewAttribute", Constants.MaximumLengthAttributeFullyQualifiedMetadataName);
                    break;

                case Constants.SystemMinLengthAttributeFullyQualifiedMetadataName:
                    builder.Add("NewAttribute", Constants.MinimumLengthAttributeFullyQualifiedMetadataName);
                    break;

                case Constants.SystemPhoneAttributeFullyQualifiedMetadataName:
                    builder.Add("NewAttribute", Constants.PhoneAttributeFullyQualifiedMetadataName);
                    break;

                case Constants.SystemRangeAttributeFullyQualifiedMetadataName:
                    var minimumIsExclusive = attributeData.GetNamedArgument("MinimumIsExclusive") is { Value: true };
                    var maximumIsExclusive = attributeData.GetNamedArgument("MaximumIsExclusive") is { Value: true };
                    if (minimumIsExclusive && maximumIsExclusive)
                    {
                        builder.Add("NewAttribute", Constants.ExclusiveBetweenAttributeFullyQualifiedMetadataName);
                    }
                    else if (!minimumIsExclusive && !maximumIsExclusive)
                    {
                        builder.Add("NewAttribute", Constants.InclusiveBetweenAttributeFullyQualifiedMetadataName);
                    }
                    else
                    {
                        builder.Add("NewAttribute", minimumIsExclusive ? Constants.GreaterThanAttributeFullyQualifiedMetadataName : Constants.GreaterThanOrEqualToAttributeFullyQualifiedMetadataName);
                        builder.Add("NewAttribute_1", maximumIsExclusive ? Constants.LessThanAttributeFullyQualifiedMetadataName : Constants.LessThanOrEqualToAttributeFullyQualifiedMetadataName);
                    }
                    break;

                case Constants.SystemRegularExpressionAttributeFullyQualifiedMetadataName:
                    builder.Add("NewAttribute", Constants.MatchesAttributeFullyQualifiedMetadataName);
                    break;

                case Constants.SystemRequiredAttributeFullyQualifiedMetadataName:
                    // By default, 'AllowEmptyStrings' is 'false'.
                    if (typeSymbol.IsStringType() &&
                        attributeData.GetNamedArgument("AllowEmptyStrings") is not { Value: true })
                    {
                        builder.Add("NewAttribute", Constants.NotEmptyAttributeFullyQualifiedMetadataName);
                    }
                    else
                    {
                        builder.Add("NewAttribute", Constants.NotNullAttributeFullyQualifiedMetadataName);
                    }
                    break;

                case Constants.SystemStringLengthAttributeFullyQualifiedMetadataName:
                    if (attributeData.GetNamedArgument("MinimumLength") is { Value: int })
                    {
                        builder.Add("NewAttribute", Constants.LengthAttributeFullyQualifiedMetadataName);
                    }
                    else
                    {
                        builder.Add("NewAttribute", Constants.MaximumLengthAttributeFullyQualifiedMetadataName);
                    }
                    break;

                case Constants.SystemUrlAttributeFullyQualifiedMetadataName:
                    builder.Add("NewAttribute", Constants.UrlAttributeFullyQualifiedMetadataName);
                    break;

                default:
                    continue;
            }

            var properties = builder.ToImmutable();
            var diagnostic = Diagnostic.Create(
                descriptor: DiagnosticDescriptors.UseLingValidationAttributeFix,
                location: attributeSyntax.GetLocation(),
                properties: properties,
                properties.TryGetValue("NewAttribute", out var v1) ? v1 : string.Empty,
                attributeData.AttributeClass!.Name);
            context.ReportDiagnostic(diagnostic);
        }
    }
}
