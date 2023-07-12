// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Razor.Analyzers.PoC;

public abstract class ViewFeatureAnalyzerBase : DiagnosticAnalyzer
{
    protected ViewFeatureAnalyzerBase(DiagnosticDescriptor diagnosticDescriptor)
    {
        SupportedDiagnostic = diagnosticDescriptor;
        SupportedDiagnostics = ImmutableArray.Create(SupportedDiagnostic);
    }

    protected DiagnosticDescriptor SupportedDiagnostic { get; }

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; }

    public sealed override void Initialize(AnalysisContext context)
    {
        context.EnableConcurrentExecution();
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze |
                                               GeneratedCodeAnalysisFlags.ReportDiagnostics);

        context.RegisterCompilationStartAction(compilationContext =>
        {
            var analyzerContext = new ViewFeaturesAnalyzerContext(compilationContext);

            // Only do work if we can locate IHtmlHelper.
            // Help identity if we are in Mvc application project
            if (analyzerContext.HtmlHelperType == null)
            {
                return;
            }

            InitializeWorker(analyzerContext);
        });
    }

    protected abstract void InitializeWorker(ViewFeaturesAnalyzerContext analyzerContext);
}