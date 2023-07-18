using System.Reflection;
using Microsoft.AspNetCore.Razor.Language.Intermediate;

namespace Razor.Generator;

public static class DocumentIntermediateNodeExtensions
{
    public static string GetTreeString(this DocumentIntermediateNode node)
    {
        return typeof(IntermediateNode).GetProperty("Tree", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(node) as string ?? string.Empty;
    }
}