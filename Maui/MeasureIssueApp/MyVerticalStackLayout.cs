using Microsoft.Maui.Controls;

namespace MeasureIssueApp
{
    internal class MyVerticalStackLayout : VerticalStackLayout
    {
        // Add a breakpoint inside NotifyMeasureInvalidated and the other 2 methods and then click the button in the MainPage.
        // The breakpoint is not hit aka the MeasureInvalided is not being Invoked.
        public MyVerticalStackLayout()
        {
            this.MeasureInvalidated += NotifyMeasureInvalidated;
        }
        
        protected override void InvalidateMeasure()
        {
            base.InvalidateMeasure();
        }

        protected override void InvalidateMeasureOverride()
        {
            base.InvalidateMeasureOverride();
        }

        private void NotifyMeasureInvalidated(object sender, EventArgs args)
        {
        }
    }
}
