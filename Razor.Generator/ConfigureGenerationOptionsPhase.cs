using Microsoft.AspNetCore.Razor.Language;

namespace Razor.Generator;

public class ConfigureGenerationOptionsPhase : IRazorEnginePhase
{
    public Action<RazorCodeGenerationOptionsBuilder> Configure { get; }

    public RazorEngine Engine { get; set; }

    public ConfigureGenerationOptionsPhase()
    {
        Configure = builder =>
        {
            builder.RootNamespace = "Razor.Generator";
            builder.UseEnhancedLinePragma = true;
        };
    }

    public ConfigureGenerationOptionsPhase(Action<RazorCodeGenerationOptionsBuilder> configure)
    {
        Configure = configure;
    }

    public void Execute(RazorCodeDocument codeDocument)
    {
        codeDocument.SetParserOptions(RazorParserOptions.Create(builder =>
        {
            builder.ParseLeadingDirectives = false;
        }, FileKinds.Component));
        codeDocument.SetCodeGenerationOptions(RazorCodeGenerationOptions.Create(Configure));
    }
}