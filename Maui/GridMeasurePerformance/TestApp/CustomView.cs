using System.Diagnostics;

namespace TestApp;

public class CustomView : ContentView
{
    protected override Size MeasureOverride(double widthConstraint, double heightConstraint)
    {
        Debug.WriteLine("MeasureOverride");

        return base.MeasureOverride(widthConstraint, heightConstraint);
    }

    protected override Size ArrangeOverride(Rect bounds)
    {
        Debug.WriteLine("ArrangeOverride");

        return base.ArrangeOverride(bounds);
    }
}
