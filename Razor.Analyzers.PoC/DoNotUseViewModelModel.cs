using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Operations;

namespace Razor.Analyzers.PoC;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class DoNotUseViewModelModel : ViewFeatureAnalyzerBase
{
    private const string DiagnosticId = "SM0001";

    private static readonly DiagnosticDescriptor Rule = new(DiagnosticId, "XXX", "{0}", "Razor", DiagnosticSeverity.Warning, true);

    public DoNotUseViewModelModel() : base(Rule) { }

    protected override void InitializeWorker(ViewFeaturesAnalyzerContext analyzerContext)
    {
        analyzerContext.Context.RegisterSyntaxNodeAction(context =>
        {
            if (context.Node is MemberAccessExpressionSyntax memberAccess
                && memberAccess.Name.Identifier.ToString().Contains("Model"))
            {
                context.ReportDiagnostic(Diagnostic.Create(
                    SupportedDiagnostic,
                    memberAccess.Name.Identifier.GetLocation(), memberAccess.Name.Identifier.ToString()));
            }
        }, SyntaxKind.SimpleMemberAccessExpression);
    }
}