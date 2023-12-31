﻿// See https://aka.ms/new-console-template for more information

using System.Collections.Immutable;
using System.Reflection;
using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.MSBuild;
using Razor.Analyzers.PoC;

var map = new Dictionary<string, string>
          {
              {
                  "Event.razor",
                  @"@page ""/event""
<h1>@currentHeading</h1>
<p>
    <label>
        New title
        <input @bind=""newHeading"" />
    </label>
    <button @onclick=""UpdateHeading"">
        Update heading
    </button>
</p>

<p>
    <label>
        <input type=""checkbox"" @onchange=""CheckChanged"" />
        @checkedMessage
    </label>
</p>
"
              },
              {
                  "Event.razor.cs",
                  @"namespace Razor.Empty;

                  public partial class Event
                  {
                  private string currentHeading = ""Initial heading"";
                  private string? newHeading;
                  private string checkedMessage = ""Not changed yet"";

                  public string Model { get; set; }  
                  private void UpdateHeading()
                  {
                  currentHeading = $""{newHeading}!!!"";
                  Model = """";
                  }

              private void CheckChanged()
              {
              checkedMessage = $""Last changed at {DateTime.Now}"";
          }
}
"
              },
              {
                  "Other.cshtml",
                  @"@{
                  ViewData[""Title""] = ""Home Page"";
                  ViewData.Model.Test = ""toto""; // Noncompliant: don't use Model
              }"
              }
          };


MSBuildLocator.RegisterDefaults();
using var workspace = MSBuildWorkspace.Create();
workspace.WorkspaceFailed += (_, failure) => Console.WriteLine(failure.Diagnostic);

var analyzers = ImmutableArray.Create<DiagnosticAnalyzer>(new DoNotUseViewModelModel(), new DoNotUseViewModelModelStandAlone());

const string root = "C:/src/work/Razor.Analyzers/Razor.Empty/";
foreach (var pair in map)
{
    File.WriteAllText(Path.Join(root, pair.Key), pair.Value);
}

var project = await workspace.OpenProjectAsync("C:/src/work/Razor.Analyzers/Razor.Empty/Razor.Empty.csproj");
var compilation = (await project.GetCompilationAsync()).WithAnalyzers(analyzers);
var analyzerDiagnostics = await compilation.GetAnalyzerDiagnosticsAsync();
var allDiagnostics = await compilation.GetAllDiagnosticsAsync();
var sourceGeneratedDocuments = await project.GetSourceGeneratedDocumentsAsync();

foreach (var key in map.Keys)
{
    File.Delete(Path.Join(root, key));
}

Console.WriteLine("Done!");

ImmutableArray<DiagnosticAnalyzer> GetAnalyzers() =>
    Assembly
        .LoadFrom("C:/src/work/Razor.Analyzers/Razor.Analyzers.PoC/bin/Debug/netstandard2.0/Razor.Analyzers.PoC.dll")
        .GetTypes().Where(x => !x.IsAbstract && typeof(DiagnosticAnalyzer).IsAssignableFrom(x))
        .Select(Activator.CreateInstance)
        .Cast<DiagnosticAnalyzer>()
        .ToImmutableArray();
