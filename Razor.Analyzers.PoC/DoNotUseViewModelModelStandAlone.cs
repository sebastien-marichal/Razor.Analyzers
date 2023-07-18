using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Operations;

namespace Razor.Analyzers.PoC;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class DoNotUseViewModelModelStandAlone : DiagnosticAnalyzer
{
    private const string DiagnosticId = "SM0005";

    private static readonly DiagnosticDescriptor Rule = new(DiagnosticId, "XXX",
        @"[SM0005] [{0}] ""{1}"" member access",
        "Razor", DiagnosticSeverity.Warning,
        true);

    public override void Initialize(AnalysisContext context)
    {
        context.EnableConcurrentExecution();
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze |
                                               GeneratedCodeAnalysisFlags.ReportDiagnostics);

        FromSyntaxNode(context);
        FromCompilationStart(context);
    }

    private static void FromCompilationStart(AnalysisContext context) =>
        context.RegisterCompilationStartAction(ctx =>
        {
            ctx.RegisterSyntaxNodeAction(analysisContext => ReportDiagnostic(analysisContext, "CompilationStart"),
                SyntaxKind.SimpleMemberAccessExpression);
        });

    private static void FromSyntaxNode(AnalysisContext context) =>
        context.RegisterSyntaxNodeAction(analysisContext => ReportDiagnostic(analysisContext, "SyntaxNode"),
            SyntaxKind.SimpleMemberAccessExpression);

    private static void ReportDiagnostic(SyntaxNodeAnalysisContext ctx, string actionName)
    {
        if (ctx.Node is MemberAccessExpressionSyntax memberAccess &&
            memberAccess.Name.Identifier.ToString().Contains("Model"))
        {
            ctx.ReportDiagnostic(Diagnostic.Create(Rule, memberAccess.Name.Identifier.GetLocation(), actionName,
                $"FilePath: [{memberAccess.Name.Identifier.GetLocation().GetMappedLineSpan()}] - {memberAccess.Name.Identifier.ToString()}"));
        }
    }

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = ImmutableArray.Create(Rule);
}