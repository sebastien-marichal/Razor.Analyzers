namespace Razor.Analyzers.ClassLib.Example;

public class TestClass
{
    public string? Model { get; set; }

    void Method()
    {
        // Should not raise sine Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper is not in the Assembly 
        this.Model = "test";
    }
}