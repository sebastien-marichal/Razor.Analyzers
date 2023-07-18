using Microsoft.AspNetCore.Razor.Language;
using Razor.Generator;

var fileInfo = new FileInfo("../../../Counter.razor");

var fileSystem = RazorProjectFileSystem.Create(fileInfo.Directory.FullName);

var engine = RazorProjectEngine.Create(RazorConfiguration.Default, fileSystem, builder => { builder.Phases.Insert(0, new ConfigureGenerationOptionsPhase()); });
RazorCodeDocument codeDocument = engine.Process(fileSystem.GetItem(fileInfo.FullName));
RazorCSharpDocument csharpDocument = codeDocument.GetCSharpDocument();

File.WriteAllText($"{fileInfo.Name}.g.syntaxtree", codeDocument.GetDocumentIntermediateNode().GetTreeString());
File.WriteAllText($"{fileInfo.Name}.g.cs", csharpDocument.GeneratedCode);