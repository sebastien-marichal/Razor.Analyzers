﻿using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Razor.Analyzers.Razor.Net7.Example.Pages;

public class PrivacyModel : PageModel
{
    private readonly ILogger<PrivacyModel> _logger;

    public PrivacyModel(ILogger<PrivacyModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }
}

