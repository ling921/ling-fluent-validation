using Ling.FluentValidation.Analyzers.Diagnostics;
using Ling.FluentValidation.Analyzers.Infrastructure;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;

namespace Ling.FluentValidation.Analyzers;

/// <summary>
/// Validates uses of <see cref="Constants.AllowedValuesAttributeFullyQualifiedMetadataName"/>.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class AllowedValuesAttributeAnalyzer : AttributeDiagnosticAnalyzer
{
    /// <inheritdoc/>
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics =>
        [DiagnosticDescriptors.AllowedValuesAttributeShouldHaveAtLeastOneValue];

    /// <inheritdoc/>
    public override ImmutableArray<string> TargetAttributeFullyQualifiedMetadataNames =>
        [Constants.AllowedValuesAttributeFullyQualifiedMetadataName];

    /// <inheritdoc/>
    public override AttributeTargets AttributeTargets => AttributeTargets.Property | AttributeTargets.Field;

    /// <inheritdoc/>
    protected override void AnalyzePropertyAttribute(
        SyntaxNodeAnalysisContext context,
        PropertyDeclarationSyntax propertyDeclarationSyntax,
        IPropertySymbol propertySymbol,
        ImmutableArray<AttributeInfo> attributes) => Analyze(context, attributes);

    /// <inheritdoc/>
    protected override void AnalyzeFieldAttribute(
        SyntaxNodeAnalysisContext context,
        FieldDeclarationSyntax fieldDeclarationSyntax,
        IFieldSymbol fieldSymbol,
        ImmutableArray<AttributeInfo> attributes) => Analyze(context, attributes);

    private static void Analyze(SyntaxNodeAnalysisContext context, ImmutableArray<AttributeInfo> attributes)
    {
        foreach (var (_, attributeData) in attributes)
        {
            if (attributeData.ConstructorArguments.Length != 1 ||
                attributeData.ConstructorArguments[0].Values.Length == 0)
            {
                context.ReportDiagnostic(Diagnostic.Create(
                    DiagnosticDescriptors.AllowedValuesAttributeShouldHaveAtLeastOneValue,
                    attributeData.ApplicationSyntaxReference!.GetSyntax().GetLocation()));
            }
        }
    }
}
