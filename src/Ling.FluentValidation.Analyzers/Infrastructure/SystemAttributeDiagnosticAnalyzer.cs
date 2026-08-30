using Ling.FluentValidation.Analyzers.Diagnostics;
using Ling.FluentValidation.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;

namespace Ling.FluentValidation.Analyzers.Infrastructure;

/// <summary>
/// The base analyzer class for attributes under the 'System.ComponentModel.DataAnnotations' namespace.
/// </summary>
public abstract class SystemAttributeDiagnosticAnalyzer : MemberAttributeDiagnosticAnalyzer
{
    /// <summary>
    /// The full qualified metadata names of the attribute that should be used on the property or field.
    /// <para>
    /// For example, <c>"System.ComponentModel.DataAnnotations.RequiredAttribute"</c>.
    /// </para>
    /// </summary>
    public abstract string TargetAttributeFullyQualifiedMetadataName { get; }

    /// <inheritdoc/>
    public sealed override ImmutableArray<string> TargetAttributeFullyQualifiedMetadataNames => [
        TargetAttributeFullyQualifiedMetadataName
    ];

    /// <summary>
    /// The additional supported diagnostics.
    /// </summary>
    public virtual ImmutableArray<DiagnosticDescriptor> AdditionalSupportedDiagnostics => [];

    /// <inheritdoc/>
    public sealed override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => [
        ..AdditionalSupportedDiagnostics,
        DiagnosticDescriptors.UseLingValidationAttributeFix
    ];

    /// <inheritdoc/>
    protected sealed override void AnalyzeNode(
        SyntaxNodeAnalysisContext context,
        ISymbol memberSymbol,
        ITypeSymbol memberTypeSymbol,
        AttributeData attributeData,
        ImmutableArray<INamedTypeSymbol> namedTypeSymbols)
    {
        if (!RunAdditionalAnalyze(context, memberSymbol, memberTypeSymbol, attributeData, namedTypeSymbols))
        {
            return;
        }

        // CodeFix ignore attributes that have a message resource name or type.
        if (attributeData.GetNamedArgument("ErrorMessageResourceName") is { Value: string { Length: > 0 } } ||
            attributeData.GetNamedArgument("ErrorMessageResourceType") is { Value: INamedTypeSymbol })
        {
            return;
        }

        var attributeSyntax = (AttributeSyntax)attributeData.ApplicationSyntaxReference!.GetSyntax()!;
        var propertiesBuilder = ImmutableDictionary.CreateBuilder<string, string?>();
        propertiesBuilder.Add("OriginalAttribute", TargetAttributeFullyQualifiedMetadataName);

        BuildCodeFixParameters(propertiesBuilder, attributeData, memberTypeSymbol);

        var properties = propertiesBuilder.ToImmutable();
        var diagnostic = Diagnostic.Create(
            descriptor: DiagnosticDescriptors.UseLingValidationAttributeFix,
            location: attributeSyntax.GetLocation(),
            properties: properties,
            properties.TryGetValue("NewAttribute", out var v1) ? v1 : string.Empty,
            attributeData.AttributeClass!.Name);
        context.ReportDiagnostic(diagnostic);
    }

    /// <summary>
    /// Runs additional analyze.
    /// </summary>/// <param name="context">The syntax node analysis context.</param>
    /// <param name="memberSymbol">Property or field symbol.</param>
    /// <param name="memberTypeSymbol">Property or field type symbol.</param>
    /// <param name="attributeData">The attribute data that is used to analyze the node.</param>
    /// <param name="namedTypeSymbols">The named type symbols from <see cref="GetNamedTypeSymbolsForAnalysis(SyntaxNodeAnalysisContext)"/>.</param>
    /// <returns>Returns <see langword="true"/> to continue code fix, otherwise <see langword="false"/>.</returns>
    protected virtual bool RunAdditionalAnalyze(
        SyntaxNodeAnalysisContext context,
        ISymbol memberSymbol,
        ITypeSymbol memberTypeSymbol,
        AttributeData attributeData,
        ImmutableArray<INamedTypeSymbol> namedTypeSymbols)
    {
        return true;
    }

    /// <summary>
    /// Fill parameters for the code fix.
    /// </summary>
    /// <param name="builder">The code fix parameters builder.</param>
    /// <param name="attributeData">The attribute data.</param>
    /// <param name="memberTypeSymbol">Property or field type symbol.</param>
    protected abstract void BuildCodeFixParameters(
        ImmutableDictionary<string, string?>.Builder builder,
        AttributeData attributeData,
        ITypeSymbol memberTypeSymbol);
}
