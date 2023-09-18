using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Razor.Analyzers.PoC;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class DoNotUseViewModelModel : ViewFeatureAnalyzerBase
{
    private const string DiagnosticId = "SM0001";

    private static readonly DiagnosticDescriptor Rule = new(DiagnosticId, "XXX", "{0}", "Razor", DiagnosticSeverity.Warning, true);

    private const bool NormalizePahts = false;

    public DoNotUseViewModelModel() : base(Rule) { }

    protected override void InitializeWorker(ViewFeaturesAnalyzerContext analyzerContext)
    {
        analyzerContext.Context.RegisterSyntaxNodeAction(context =>
        {
            if (context.Node is InvocationExpressionSyntax invocation
                && invocation.Expression is { } expression
                && expression is IdentifierNameSyntax identifierName
                && identifierName.Identifier.ValueText == "RaiseHere")
            {
                var location = identifierName.Identifier.GetLocation();
                var lineSpan = location.GetMappedLineSpan();
                if (NormalizePahts)
                {
                    location = Location.Create(Path.GetFullPath(lineSpan.Path), location.SourceSpan, lineSpan.Span);
                }
                else
                {
                    location = Location.Create(lineSpan.Path, location.SourceSpan, lineSpan.Span);
                }

                context.ReportDiagnostic(Diagnostic.Create(Rule, location, $"Reported thanks to {location}"));
            }
        }, SyntaxKind.InvocationExpression);
    }
}