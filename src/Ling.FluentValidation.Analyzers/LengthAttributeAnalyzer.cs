using Ling.FluentValidation.Analyzers.Diagnostics;
using Ling.FluentValidation.Analyzers.Infrastructure;
using Ling.FluentValidation.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;

namespace Ling.FluentValidation.Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class LengthAttributeAnalyzer : AttributeDiagnosticAnalyzer
{
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => [
            DiagnosticDescriptors.AttributeShouldUsedOnPropertyOfStringOrEnumerableError,
            DiagnosticDescriptors.AttributeShouldUsedOnPropertyOfOneType,
            DiagnosticDescriptors.LengthAttributeMinEqualToMax
        ];

    public override ImmutableArray<string> TargetAttributeFullyQualifiedMetadataNames => [
            Constants.LengthAttributeFullyQualifiedMetadataName,
            Constants.MaximumLengthAttributeFullyQualifiedMetadataName,
            Constants.MinimumLengthAttributeFullyQualifiedMetadataName,

            Constants.SystemLengthAttributeFullyQualifiedMetadataName,
            Constants.SystemMaxLengthAttributeFullyQualifiedMetadataName,
            Constants.SystemMinLengthAttributeFullyQualifiedMetadataName
        ];

    public override AttributeTargets AttributeTargets => AttributeTargets.Property | AttributeTargets.Field;

    protected override void AnalyzePropertyAttribute(SyntaxNodeAnalysisContext context, PropertyDeclarationSyntax propertyDeclarationSyntax, IPropertySymbol propertySymbol, ImmutableArray<AttributeInfo> attributes)
    {
        AnalyzeAttributeCore(context, propertyDeclarationSyntax, propertySymbol, attributes);
    }

    protected override void AnalyzeFieldAttribute(SyntaxNodeAnalysisContext context, FieldDeclarationSyntax fieldDeclarationSyntax, IFieldSymbol fieldSymbol, ImmutableArray<AttributeInfo> attributes)
    {
        AnalyzeAttributeCore(context, fieldDeclarationSyntax, fieldSymbol, attributes);
    }

    private void AnalyzeAttributeCore(
        SyntaxNodeAnalysisContext context,
        MemberDeclarationSyntax syntax,
        ISymbol symbol,
        //ITypeSymbol propertyOrFieldTypeSymbol,
        ImmutableArray<AttributeInfo> attributes)
    {
        var typeSymbol = symbol is IPropertySymbol propertySymbol
            ? propertySymbol.Type
            : ((IFieldSymbol)symbol).Type;
        var isStringOrEnumerable = typeSymbol.IsStringType() || typeSymbol.IsEnumerableType();

        foreach (var (fullQualifiedMetadataName, attributeData) in attributes)
        {
            if (!isStringOrEnumerable)
            {
                var attributeSyntax = (AttributeSyntax)attributeData.ApplicationSyntaxReference!.GetSyntax()!;
                var diagnostic = Diagnostic.Create(
                    DiagnosticDescriptors.AttributeShouldUsedOnPropertyOfStringOrEnumerableError,
                    attributeSyntax.GetLocation(),
                    attributeData.AttributeClass!.Name);
                context.ReportDiagnostic(diagnostic);
            }

            if (fullQualifiedMetadataName == Constants.LengthAttributeFullyQualifiedMetadataName &&
                attributeData.ConstructorArguments.Length == 2 &&
                attributeData.ConstructorArguments[0] is { Value: int min } &&
                attributeData.ConstructorArguments[1] is { Value: int max })
            {
                if (min == max)
                {
                    context.ReportDiagnostic(Diagnostic.Create(
                        DiagnosticDescriptors.LengthAttributeMinEqualToMax,
                        attributeData.ApplicationSyntaxReference!.GetSyntax().GetLocation(),
                        attributeData.AttributeClass!.Name));
                }
            }
        }
    }
}
