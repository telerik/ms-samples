using Microsoft.Maui.Layouts;

namespace MauiApp5;

public class CustomLayout : Layout
{
    protected override ILayoutManager CreateLayoutManager() => new CustomLayoutLayoutManager();
}

public class CustomLayoutLayoutManager : ILayoutManager
{
    public Size Measure(double widthConstraint, double heightConstraint)
    {
        System.Diagnostics.Debug.WriteLine($"::: Measure widthConstraint: {widthConstraint}");

        return new Size(100, 100);
    }

    public Size ArrangeChildren(Rect bounds)
    {
        System.Diagnostics.Debug.WriteLine($"::: ArrangeChildren bounds.Width: {bounds.Width}");

        return bounds.Size;
    }
}
