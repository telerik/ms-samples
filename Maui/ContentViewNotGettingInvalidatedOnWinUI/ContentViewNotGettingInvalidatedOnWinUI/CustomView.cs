namespace ContentViewNotGettingInvalidatedOnWinUI;

public class CustomView : ContentView
{
    public override SizeRequest Measure(double widthConstraint, double heightConstraint, MeasureFlags flags = MeasureFlags.None)
    {
        System.Diagnostics.Debug.WriteLine("::: CustomView will Measure: ");
        SizeRequest sr = base.Measure(widthConstraint, heightConstraint, flags);
        return sr;
    }

    protected override Size MeasureOverride(double widthConstraint, double heightConstraint)
    {
        System.Diagnostics.Debug.WriteLine("::: CustomView will MeasureOverride: ");
        Size s = base.MeasureOverride(widthConstraint, heightConstraint);
        return s;
    }

    protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
    {
        System.Diagnostics.Debug.WriteLine("::: CustomView will OnMeasure: ");
        SizeRequest sr = base.OnMeasure(widthConstraint, heightConstraint);
        return sr;
    }

    protected override Size ArrangeOverride(Rect bounds)
    {
        System.Diagnostics.Debug.WriteLine("::: CustomView will ArrangeOverride: ");
        Size s = base.ArrangeOverride(bounds);
        return s;
    }

    protected override void LayoutChildren(double x, double y, double width, double height)
    {
        System.Diagnostics.Debug.WriteLine("::: CustomView will LayoutChildren: ");
        base.LayoutChildren(x, y, width, height);
    }

    internal void InvalidateMeasureInternal()
    {
        this.InvalidateMeasure();
        //this.InvalidateLayout();
        //(this as IView).InvalidateMeasure();
        //(this as IView).InvalidateArrange();            
    }
}
