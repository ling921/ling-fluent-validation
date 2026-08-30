using Ling.FluentValidation.Analyzers.Diagnostics;
using Ling.FluentValidation.Analyzers.Infrastructure;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;

namespace Ling.FluentValidation.Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class EnumNameAttributeValidationAnalyzer : AttributeDiagnosticAnalyzer
{
    /// <inheritdoc/>
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => [
        DiagnosticDescriptors.TypeShouldImplementAnotherType
    ];

    /// <inheritdoc/>
    public override ImmutableArray<string> TargetAttributeFullyQualifiedMetadataNames => [
        Constants.EnumNameAttributeFullyQualifiedMetadataName,
        Constants.GenericEnumNameAttributeFullyQualifiedMetadataName
    ];

    /// <inheritdoc/>
    public override AttributeTargets AttributeTargets => AttributeTargets.Property | AttributeTargets.Field;

    /// <inheritdoc/>
    protected override void AnalyzePropertyAttribute(SyntaxNodeAnalysisContext context, PropertyDeclarationSyntax propertyDeclarationSyntax, IPropertySymbol propertySymbol, ImmutableArray<AttributeInfo> attributes)
    {
        AnalyzeAttributeCore(context, propertyDeclarationSyntax, propertySymbol.Type, attributes);
    }

    /// <inheritdoc/>
    protected override void AnalyzeFieldAttribute(SyntaxNodeAnalysisContext context, FieldDeclarationSyntax fieldDeclarationSyntax, IFieldSymbol fieldSymbol, ImmutableArray<AttributeInfo> attributes)
    {
        AnalyzeAttributeCore(context, fieldDeclarationSyntax, fieldSymbol.Type, attributes);
    }

    private void AnalyzeAttributeCore(
        SyntaxNodeAnalysisContext context,
        MemberDeclarationSyntax syntax,
        ITypeSymbol propertyOrFieldTypeSymbol,
        ImmutableArray<AttributeInfo> attributes)
    {
        foreach (var (_, attributeData) in attributes)
        {
            var enumTypeSymbol = attributeData.AttributeClass!.IsGenericType
                ? (INamedTypeSymbol)attributeData.AttributeClass.TypeArguments[0]
                : (INamedTypeSymbol)attributeData.ConstructorArguments[0].Value!;
            if (enumTypeSymbol.TypeKind != TypeKind.Enum)
            {
                var attributeSyntax = (AttributeSyntax)attributeData.ApplicationSyntaxReference!.GetSyntax()!;
                var location = attributeData.AttributeClass.IsGenericType
                    ? attributeSyntax.Name.GetLocation()
                    : ((TypeOfExpressionSyntax)attributeSyntax.ArgumentList!.Arguments[0].Expression).Type.GetLocation();

                var info = Diagnostic.Create(
                        DiagnosticDescriptors.TypeShouldImplementAnotherType,
                        location,
                        enumTypeSymbol.Name,
                        "System.Enum");
                context.ReportDiagnostic(info);
            }
        }
    }
}
