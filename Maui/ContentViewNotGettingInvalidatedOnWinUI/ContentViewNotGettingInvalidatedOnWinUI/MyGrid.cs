namespace ContentViewNotGettingInvalidatedOnWinUI;

public class MyGrid : Grid
{
    public string DebugName { get; set; } = "MyGrid";

    public override SizeRequest Measure(double widthConstraint, double heightConstraint, MeasureFlags flags = MeasureFlags.None)
    {
        this.Writeline("will Measure: wC:{0}, hC:{1}", widthConstraint, heightConstraint);
        SizeRequest size = base.Measure(widthConstraint, heightConstraint, flags);
        return size;
    }

    protected override Size MeasureOverride(double widthConstraint, double heightConstraint)
    {
        this.Writeline("will MeasureOverride: wC:{0}, hC:{1}", widthConstraint, heightConstraint);
        Size size = base.MeasureOverride(widthConstraint, heightConstraint);
        return size;
    }

    protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
    {
        this.Writeline("will OnMeasure: wC:{0}, hC:{1}", widthConstraint, heightConstraint);
        SizeRequest size = base.OnMeasure(widthConstraint, heightConstraint);
        return size;
    }

    protected override Size ArrangeOverride(Rect bounds)
    {
        this.Writeline("will ArrangeOverride: bounds:{0}", bounds);
        Size size = base.ArrangeOverride(bounds);
        return size;
    }

    private void Writeline(string format, params object[] args)
    {
        string message = string.Format(format, args);
        string line = string.Format("::: {0}: {1}", this.DebugName, message);
        System.Diagnostics.Debug.WriteLine(line);
        System.Console.WriteLine(line);
    }
}