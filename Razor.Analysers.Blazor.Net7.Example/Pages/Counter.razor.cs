namespace Razor.Analysers.Blazor.Net7.Example.Pages
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