using System;

namespace Razor.Generator;

public partial class Event
{
    private string currentHeading = "Initial heading";
    private string? newHeading;
    private string? unused; // Noncompliant
    private string checkedMessage = "Not changed yet";

    private void UpdateHeading()
    {
        currentHeading = $"{newHeading}!!!";
    }

    private void CheckChanged()
    {
        checkedMessage = $"Last changed at {DateTime.Now}";
    }
}
