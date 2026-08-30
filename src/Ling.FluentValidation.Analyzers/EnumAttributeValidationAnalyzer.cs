using Ling.FluentValidation.Analyzers.Diagnostics;
using Ling.FluentValidation.Analyzers.Infrastructure;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;
using System.Xml.Linq;

namespace Ling.FluentValidation.Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class EnumAttributeValidationAnalyzer : AttributeDiagnosticAnalyzer
{
    /// <inheritdoc/>
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => [
        DiagnosticDescriptors.AttributeShouldUsedOnPropertyOfOneType
    ];

    /// <inheritdoc/>
    public override ImmutableArray<string> TargetAttributeFullyQualifiedMetadataNames => [
        Constants.EnumAttributeFullyQualifiedMetadataName
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
            if (propertyOrFieldTypeSymbol.TypeKind != TypeKind.Enum &&
                !(propertyOrFieldTypeSymbol is INamedTypeSymbol { TypeArguments: { Length: 1 } Arguments } && Arguments[0].TypeKind == TypeKind.Enum))
            {
                var attributeSyntax = (AttributeSyntax)attributeData.ApplicationSyntaxReference!.GetSyntax()!;

                var info = Diagnostic.Create(
                        DiagnosticDescriptors.AttributeShouldUsedOnPropertyOfOneType,
                        attributeSyntax.GetLocation(),
                        attributeData.AttributeClass!.Name,
                        "enum");
                context.ReportDiagnostic(info);
            }
        }
    }
}
