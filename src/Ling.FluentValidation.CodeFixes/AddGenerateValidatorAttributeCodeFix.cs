using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Immutable;
using System.Composition;

namespace Ling.FluentValidation.CodeFixes;

[ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(AddGenerateValidatorAttributeCodeFix)), Shared]
public sealed class AddGenerateValidatorAttributeCodeFix : CodeFixProvider
{
    public override ImmutableArray<string> FixableDiagnosticIds => [DiagnosticIds.ValidationTypeMustBeMarkedId];

    public override FixAllProvider GetFixAllProvider() => WellKnownFixAllProviders.BatchFixer;

    public override async Task RegisterCodeFixesAsync(CodeFixContext context)
    {
        var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);
        var declaration = root?.FindToken(context.Span.Start).Parent?.AncestorsAndSelf().OfType<TypeDeclarationSyntax>().FirstOrDefault();
        if (declaration is null) return;

        context.RegisterCodeFix(
            CodeAction.Create(
                "Add GenerateValidatorAttribute",
                token => AddAttributeAsync(context.Document, declaration, token),
                nameof(AddGenerateValidatorAttributeCodeFix)),
            context.Diagnostics);
    }

    private static async Task<Document> AddAttributeAsync(Document document, TypeDeclarationSyntax declaration, CancellationToken token)
    {
        var text = await document.GetTextAsync(token).ConfigureAwait(false);
        var line = text.Lines.GetLineFromPosition(declaration.SpanStart);
        var indentation = text.ToString(TextSpan.FromBounds(line.Start, declaration.SpanStart));
        var lineBreakSpan = TextSpan.FromBounds(line.End, line.EndIncludingLineBreak);
        var lineBreak = lineBreakSpan.Length > 0 ? text.ToString(lineBreakSpan) : Environment.NewLine;
        var insertion = $"[global::Ling.FluentValidation.Annotations.GenerateValidator]{lineBreak}{indentation}";
        return document.WithText(text.WithChanges(new TextChange(new TextSpan(declaration.SpanStart, 0), insertion)));
    }
}
