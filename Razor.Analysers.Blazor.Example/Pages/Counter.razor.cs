using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.JSInterop;
using Razor.Analysers.Blazor.Example;
using Razor.Analysers.Blazor.Example.Shared;

namespace Razor.Analysers.Blazor.Example.Pages
{
    public partial class Counter
    {
        private int currentCount = 0;
        private int Model = 42;
        private void IncrementCount()
        {
            FormattableString.Invariant($"Value: {Model}");
            currentCount++;
            this.Model = 21;
        }
    }
}