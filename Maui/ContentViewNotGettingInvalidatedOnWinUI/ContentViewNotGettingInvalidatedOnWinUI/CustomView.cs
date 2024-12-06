namespace ContentViewNotGettingInvalidatedOnWinUI;

public class CustomView : ContentView
{
    public override SizeRequest Measure(double widthConstraint, double heightConstraint, MeasureFlags flags = MeasureFlags.None)
    {
        this.Writeline("will Measure");
        SizeRequest sr = base.Measure(widthConstraint, heightConstraint, flags);
        return sr;
    }

    protected override Size MeasureOverride(double widthConstraint, double heightConstraint)
    {
        this.Writeline("will MeasureOverride");
        Size s = base.MeasureOverride(widthConstraint, heightConstraint);
        return s;
    }

    protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
    {
        this.Writeline("will OnMeasure");
        SizeRequest sr = base.OnMeasure(widthConstraint, heightConstraint);
        return sr;
    }

    protected override Size ArrangeOverride(Rect bounds)
    {
        this.Writeline("will ArrangeOverride");
        Size s = base.ArrangeOverride(bounds);
        return s;
    }

    protected override void LayoutChildren(double x, double y, double width, double height)
    {
        this.Writeline("will LayoutChildren");
        base.LayoutChildren(x, y, width, height);
    }

    internal void InvalidateMeasureInternal()
    {
        this.InvalidateMeasure();
        //this.InvalidateLayout();
        //(this as IView).InvalidateMeasure();
        //(this as IView).InvalidateArrange();            
    }

    private void Writeline(string format, params object[] args)
    {
        string message = string.Format(format, args);
        string line = string.Format("::: {0}: {1}", "CustomView", message);
        System.Diagnostics.Debug.WriteLine(line);
        System.Console.WriteLine(line);
    }
}
