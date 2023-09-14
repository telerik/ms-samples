namespace MauiApp12;

public class CustomContentView : ContentView
{
    public static readonly BindableProperty MaxProperty = BindableProperty.Create(
        nameof(Max), typeof(double), typeof(CustomContentView), 0.0, propertyChanged: (b, o, n) => ((CustomContentView)b).OnMaxChanged());

    public CustomContentView()
    {
        this.ControlTemplate = (ControlTemplate)App.Current.Resources["CustomContentView_ControlTemplate"];
    }

    public double Max
    {
        get {  return (double)this.GetValue(MaxProperty); }
        set { this.SetValue(MaxProperty, value); }
    }

    protected override Size MeasureOverride(double widthConstraint, double heightConstraint)
    {
        System.Diagnostics.Debug.WriteLine("::: CustomContentView MeasureOverride start");
        Size size = base.MeasureOverride(widthConstraint, heightConstraint);
        System.Diagnostics.Debug.WriteLine("::: CustomContentView MeasureOverride end");
        return size;
    }

    public override SizeRequest Measure(double widthConstraint, double heightConstraint, MeasureFlags flags = MeasureFlags.None)
    {
        System.Diagnostics.Debug.WriteLine("::: CustomContentView Measure start");
        SizeRequest sizeRequest = base.Measure(widthConstraint, heightConstraint, flags);
        System.Diagnostics.Debug.WriteLine("::: CustomContentView Measure end");
        return sizeRequest;
    }

    protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
    {
        System.Diagnostics.Debug.WriteLine("::: CustomContentView OnMeasure start");
        SizeRequest sizeRequest = base.OnMeasure(widthConstraint, heightConstraint);
        System.Diagnostics.Debug.WriteLine("::: CustomContentView OnMeasure end");
        return sizeRequest;
    }

    protected override Size ArrangeOverride(Rect bounds)
    {
        System.Diagnostics.Debug.WriteLine("::: CustomContentView ArrangeOverride start");
        Size size = base.ArrangeOverride(bounds);
        System.Diagnostics.Debug.WriteLine("::: CustomContentView ArrangeOverride end");
        return size;
    }

    private void OnMaxChanged()
    {
        System.Diagnostics.Debug.WriteLine("::: CustomContentView OnMaxChanged");

        this.InvalidateMeasure();
        this.InvalidateLayout();
        //((IView)this).InvalidateMeasure(); // not helping
        //((IView)this).InvalidateArrange(); // not helping
    }
}
