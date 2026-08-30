using Ling.FluentValidation.Analyzers.Diagnostics;
using Ling.FluentValidation.Analyzers.Infrastructure;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;

namespace Ling.FluentValidation.Analyzers;

/// <summary>
/// Validates that member validation attributes target code the generator can access.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class ValidationAttributeTargetAnalyzer : AttributeDiagnosticAnalyzer
{
    /// <inheritdoc/>
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics =>
        [DiagnosticDescriptors.AttributeTargetCannotBeGenerated];

    /// <inheritdoc/>
    public override ImmutableArray<string> TargetAttributeFullyQualifiedMetadataNames =>
    [
        Constants.AllowedValuesAttributeFullyQualifiedMetadataName,
        Constants.Base64StringAttributeFullyQualifiedMetadataName,
        Constants.CompareAttributeFullyQualifiedMetadataName,
        Constants.CreditCardAttributeFullyQualifiedMetadataName,
        Constants.DeniedValuesAttributeFullyQualifiedMetadataName,
        Constants.EmailAddressAttributeFullyQualifiedMetadataName,
        Constants.EmptyAttributeFullyQualifiedMetadataName,
        Constants.EnumAttributeFullyQualifiedMetadataName,
        Constants.EnumNameAttributeFullyQualifiedMetadataName,
        Constants.GenericEnumNameAttributeFullyQualifiedMetadataName,
        Constants.EqualAttributeFullyQualifiedMetadataName,
        Constants.ExclusiveBetweenAttributeFullyQualifiedMetadataName,
        Constants.FileExtensionsAttributeFullyQualifiedMetadataName,
        Constants.GreaterThanAttributeFullyQualifiedMetadataName,
        Constants.GreaterThanOrEqualToAttributeFullyQualifiedMetadataName,
        Constants.InclusiveBetweenAttributeFullyQualifiedMetadataName,
        Constants.LengthAttributeFullyQualifiedMetadataName,
        Constants.LessThanAttributeFullyQualifiedMetadataName,
        Constants.LessThanOrEqualToAttributeFullyQualifiedMetadataName,
        Constants.MatchesAttributeFullyQualifiedMetadataName,
        Constants.MaximumLengthAttributeFullyQualifiedMetadataName,
        Constants.MinimumLengthAttributeFullyQualifiedMetadataName,
        Constants.NotEmptyAttributeFullyQualifiedMetadataName,
        Constants.NotEqualAttributeFullyQualifiedMetadataName,
        Constants.NotNullAttributeFullyQualifiedMetadataName,
        Constants.NullAttributeFullyQualifiedMetadataName,
        Constants.PhoneAttributeFullyQualifiedMetadataName,
        Constants.UrlAttributeFullyQualifiedMetadataName,
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
    protected override void AnalyzePropertyAttribute(
        SyntaxNodeAnalysisContext context,
        PropertyDeclarationSyntax propertyDeclarationSyntax,
        IPropertySymbol propertySymbol,
        ImmutableArray<AttributeInfo> attributes) => Analyze(context, propertySymbol, attributes);

    /// <inheritdoc/>
    protected override void AnalyzeFieldAttribute(
        SyntaxNodeAnalysisContext context,
        FieldDeclarationSyntax fieldDeclarationSyntax,
        IFieldSymbol fieldSymbol,
        ImmutableArray<AttributeInfo> attributes) => Analyze(context, fieldSymbol, attributes);

    private static void Analyze(SyntaxNodeAnalysisContext context, ISymbol member, ImmutableArray<AttributeInfo> attributes)
    {
        var reason = GetUnsupportedReason(member);
        if (reason is null)
        {
            return;
        }

        foreach (var (_, attributeData) in attributes)
        {
            context.ReportDiagnostic(Diagnostic.Create(
                DiagnosticDescriptors.AttributeTargetCannotBeGenerated,
                attributeData.ApplicationSyntaxReference!.GetSyntax().GetLocation(),
                attributeData.AttributeClass!.Name,
                member.Name,
                reason));
        }
    }

    private static string? GetUnsupportedReason(ISymbol member)
    {
        if (member.IsStatic)
        {
            return "static members are not supported";
        }

        if (member is IPropertySymbol { IsIndexer: true })
        {
            return "indexers are not supported";
        }

        if (member.DeclaredAccessibility is Accessibility.Private or Accessibility.Protected or Accessibility.ProtectedAndInternal)
        {
            return "the member is not accessible from the generated validator namespace";
        }

        for (var type = member.ContainingType; type is not null; type = type.ContainingType)
        {
            if (type.Arity > 0)
            {
                return "generic containing types are not supported";
            }

            if (type.DeclaredAccessibility is Accessibility.Private or Accessibility.Protected or Accessibility.ProtectedAndInternal)
            {
                return "the containing type is not accessible from the generated validator namespace";
            }
        }

        return null;
    }
}
