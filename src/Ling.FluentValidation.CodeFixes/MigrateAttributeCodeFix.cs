using Ling.FluentValidation.CodeFixes.Infrastructure;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Immutable;
using System.Composition;

namespace Ling.FluentValidation.CodeFixes;

[ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(MigrateAttributeCodeFix)), Shared]
public sealed class MigrateAttributeCodeFix : CodeFixProvider
{
    /// <inheritdoc/>
    public override ImmutableArray<string> FixableDiagnosticIds => [
        DiagnosticIds.UseLingValidationAttributeFixId
    ];

    /// <inheritdoc/>
    public override FixAllProvider? GetFixAllProvider()
    {
        return WellKnownFixAllProviders.BatchFixer;
    }

    /// <inheritdoc/>
    public override async Task RegisterCodeFixesAsync(CodeFixContext context)
    {
        var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);

        var diagnostic = context.Diagnostics[0];
        var diagnosticSpan = diagnostic.Location.SourceSpan;
        var attributeSyntax = root!.FindToken(diagnosticSpan.Start).Parent!.AncestorsAndSelf()
            .OfType<AttributeSyntax>().First();

        var (oldNode, newNode) = AnalyzeNode(attributeSyntax, diagnostic.Properties, context.CancellationToken);

        if (newNode is null)
        {
            return;
        }

        context.RegisterCodeFix(
            CodeAction.Create(
                title: string.Format(SR.ReplaceWith, diagnostic.Properties["NewAttribute"]),
                createChangedDocument: token => ReplaceNodeAsync(
                    document: context.Document,
                    oldNode: oldNode,
                    newNode: newNode,
                    cancellationToken: token),
                equivalenceKey: nameof(MigrateAttributeCodeFix)),
            diagnostic);
    }

    private static (SyntaxNode, SyntaxNode?) AnalyzeNode(
        AttributeSyntax oldAttributeSyntax,
        ImmutableDictionary<string, string?> diagnosticProperties,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var oldAttributeName = diagnosticProperties["OriginalAttribute"];
        var newAttributeName = diagnosticProperties["NewAttribute"]!;

        var visitor = new AttributeArgumentVisitor(oldAttributeSyntax.ArgumentList);

        var newAttributeSyntax = SyntaxFactory
            .Attribute(oldAttributeSyntax.CreateNameSyntax(newAttributeName));

        // Only keep attributes parameters with 'ErrorMessage'
        switch (oldAttributeName)
        {
            case Constants.SystemAllowedValuesAttributeFullyQualifiedMetadataName:
            case Constants.SystemCompareAttributeFullyQualifiedMetadataName:
            case Constants.SystemDeniedValuesAttributeFullyQualifiedMetadataName:
            case Constants.SystemLengthAttributeFullyQualifiedMetadataName:
            case Constants.SystemMaxLengthAttributeFullyQualifiedMetadataName:
            case Constants.SystemMinLengthAttributeFullyQualifiedMetadataName:
            case Constants.SystemRegularExpressionAttributeFullyQualifiedMetadataName:
                return (oldAttributeSyntax, newAttributeSyntax.WithArgumentList(visitor.ToConstructorArgumentList()).AddArgument(visitor["ErrorMessage"]));

            case Constants.SystemBase64StringAttributeFullyQualifiedMetadataName:
            case Constants.SystemCreditCardAttributeFullyQualifiedMetadataName:
            case Constants.SystemEmailAddressAttributeFullyQualifiedMetadataName:
            case Constants.SystemPhoneAttributeFullyQualifiedMetadataName:
            case Constants.SystemRequiredAttributeFullyQualifiedMetadataName:
            case Constants.SystemUrlAttributeFullyQualifiedMetadataName:
                return (oldAttributeSyntax, newAttributeSyntax.AddArgument(visitor["ErrorMessage"]));

            case Constants.SystemEnumDataTypeAttributeFullyQualifiedMetadataName:
                return newAttributeName == Constants.EnumNameAttributeFullyQualifiedMetadataName
                    ? (oldAttributeSyntax, newAttributeSyntax.WithArgumentList(visitor.ToConstructorArgumentList()).AddArgument(visitor["ErrorMessage"]))
                    : (oldAttributeSyntax, newAttributeSyntax.AddArgument(visitor["ErrorMessage"]));

            case Constants.SystemFileExtensionsAttributeFullyQualifiedMetadataName:
                return visitor["Extensions"] is null
                    ? (oldAttributeSyntax, newAttributeSyntax.AddArgument(visitor["ErrorMessage"]))
                    : (oldAttributeSyntax, newAttributeSyntax.WithArgumentList(visitor.ToConstructorArgumentListWithNamedArguments("Extensions")).AddArgument(visitor["ErrorMessage"]));

            case Constants.SystemRangeAttributeFullyQualifiedMetadataName:
                switch (newAttributeName)
                {
                    case Constants.ExclusiveBetweenAttributeFullyQualifiedMetadataName:
                    case Constants.InclusiveBetweenAttributeFullyQualifiedMetadataName:
                        return (oldAttributeSyntax, newAttributeSyntax.WithArgumentList(visitor.ToConstructorArgumentList()).AddArgument(visitor["ErrorMessage"]));

                    case Constants.GreaterThanAttributeFullyQualifiedMetadataName:
                    case Constants.GreaterThanOrEqualToAttributeFullyQualifiedMetadataName:
                        var parent = (AttributeListSyntax)oldAttributeSyntax.Parent!;
                        var nodes = new SyntaxNodeOrTokenList();
                        var separator = SyntaxFactory.Token(SyntaxKind.CommaToken);
                        foreach (var node in parent.Attributes)
                        {
                            if (node == oldAttributeSyntax)
                            {
                                var newAttributeSyntax_1 = SyntaxFactory
                                    .Attribute(oldAttributeSyntax.CreateNameSyntax(diagnosticProperties["NewAttribute_1"]!));
                                var argumentList_0 = SyntaxFactory.AttributeArgumentList(SyntaxFactory.SeparatedList(new[] { visitor[0] }));
                                var argumentList_1 = SyntaxFactory.AttributeArgumentList(SyntaxFactory.SeparatedList(new[] { visitor[1] }));
                                if (visitor["ErrorMessage"] is AttributeArgumentSyntax errorMessageArgument)
                                {
                                    argumentList_0 = argumentList_0.AddArguments(errorMessageArgument);
                                    argumentList_1 = argumentList_1.AddArguments(errorMessageArgument);
                                }
                                nodes = nodes.Add(newAttributeSyntax.WithArgumentList(argumentList_0));
                                nodes = nodes.Add(separator);
                                nodes = nodes.Add(newAttributeSyntax_1.WithArgumentList(argumentList_1));
                            }
                            else
                            {
                                nodes = nodes.Add(node);
                            }
                        }
                        var newParent = SyntaxFactory.AttributeList(SyntaxFactory.SeparatedList<AttributeSyntax>(nodes))
                            .WithTriviaFrom(parent);
                        return (parent, newParent);

                    default:
                        return (oldAttributeSyntax, null);
                }

            case Constants.SystemStringLengthAttributeFullyQualifiedMetadataName:
                if (visitor["MinimumLength"] is AttributeArgumentSyntax minimumLengthArgument)
                {
                    var argumentList = SyntaxFactory.AttributeArgumentList(SyntaxFactory.SeparatedList(new[] { minimumLengthArgument, visitor[0] }));
                    if (visitor["ErrorMessage"] is AttributeArgumentSyntax errorMessageArgument)
                    {
                        argumentList = argumentList.AddArguments(errorMessageArgument);
                    }
                    return (oldAttributeSyntax, newAttributeSyntax.WithArgumentList(argumentList));
                }
                else
                {
                    return (oldAttributeSyntax, newAttributeSyntax.WithArgumentList(visitor.ToConstructorArgumentList()).AddArgument(visitor["ErrorMessage"]));
                }

            default:
                return (oldAttributeSyntax, null);
        }
    }

    private static async Task<Document> ReplaceNodeAsync(
        Document document,
        SyntaxNode oldNode,
        SyntaxNode newNode,
        CancellationToken cancellationToken)
    {
        var root = await document.GetSyntaxRootAsync(cancellationToken).ConfigureAwait(false);
        var newRoot = root!.ReplaceNode(oldNode, newNode)!;
        return document.WithSyntaxRoot(newRoot);
    }
}
