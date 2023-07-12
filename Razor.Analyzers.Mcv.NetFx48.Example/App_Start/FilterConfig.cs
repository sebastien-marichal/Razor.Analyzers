using System.Web;
using System.Web.Mvc;

namespace Razor.Analyzers.Mcv.NetFx48.Example
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}