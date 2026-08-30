using Ling.FluentValidation.Analyzers.Diagnostics;
using Ling.FluentValidation.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;

namespace Ling.FluentValidation.Analyzers;

/// <summary>Requires DTOs containing Ling validation rules to opt in explicitly.</summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class GenerateValidatorMarkerAnalyzer : DiagnosticAnalyzer
{
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics =>
        [DiagnosticDescriptors.ValidationTypeMustBeMarked];

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();
        context.RegisterSyntaxNodeAction(Analyze, SyntaxKind.ClassDeclaration, SyntaxKind.RecordDeclaration);
    }

    private static void Analyze(SyntaxNodeAnalysisContext context)
    {
        if (context.Node is not TypeDeclarationSyntax declaration ||
            context.SemanticModel.GetDeclaredSymbol(declaration, context.CancellationToken) is not INamedTypeSymbol type)
        {
            return;
        }

        if (type.GetAttributes().Any(static attribute =>
            attribute.AttributeClass?.GetFullyQualifiedMetadataName() == Constants.GenerateValidatorAttributeFullyQualifiedMetadataName))
        {
            return;
        }

        var hasRule = type.GetMembers().Any(static member => member.GetAttributes().Any(static attribute =>
            IsSupportedRuleAttribute(attribute.AttributeClass?.GetFullyQualifiedMetadataName())));
        if (!hasRule) return;

        context.ReportDiagnostic(Diagnostic.Create(
            DiagnosticDescriptors.ValidationTypeMustBeMarked,
            declaration.Identifier.GetLocation(),
            type.Name));
    }

    private static bool IsSupportedRuleAttribute(string? metadataName)
    {
        if (metadataName?.StartsWith(Constants.AttributeNamespace + ".", StringComparison.Ordinal) == true)
        {
            return true;
        }

        return metadataName is
            Constants.SystemAllowedValuesAttributeFullyQualifiedMetadataName or
            Constants.SystemBase64StringAttributeFullyQualifiedMetadataName or
            Constants.SystemCompareAttributeFullyQualifiedMetadataName or
            Constants.SystemCreditCardAttributeFullyQualifiedMetadataName or
            Constants.SystemDeniedValuesAttributeFullyQualifiedMetadataName or
            Constants.SystemEmailAddressAttributeFullyQualifiedMetadataName or
            Constants.SystemEnumDataTypeAttributeFullyQualifiedMetadataName or
            Constants.SystemFileExtensionsAttributeFullyQualifiedMetadataName or
            Constants.SystemLengthAttributeFullyQualifiedMetadataName or
            Constants.SystemMaxLengthAttributeFullyQualifiedMetadataName or
            Constants.SystemMinLengthAttributeFullyQualifiedMetadataName or
            Constants.SystemPhoneAttributeFullyQualifiedMetadataName or
            Constants.SystemRangeAttributeFullyQualifiedMetadataName or
            Constants.SystemRegularExpressionAttributeFullyQualifiedMetadataName or
            Constants.SystemRequiredAttributeFullyQualifiedMetadataName or
            Constants.SystemStringLengthAttributeFullyQualifiedMetadataName or
            Constants.SystemUrlAttributeFullyQualifiedMetadataName;
    }
}
