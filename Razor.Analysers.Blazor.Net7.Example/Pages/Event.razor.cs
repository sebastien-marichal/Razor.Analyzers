namespace Razor.Analysers.Blazor.Net7.Example.Pages
{
    public partial class Event
    {
        private string currentHeading = "Initial heading";
        private string? newHeading;
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
}