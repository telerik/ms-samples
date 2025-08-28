using Microsoft.Maui.Layouts;

namespace GridRowSpacingIssue;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }
}

public class MyLayout : Layout
{
    protected override ILayoutManager CreateLayoutManager()
    {
        return new MyLayoutManager(this);
    }
}

public class MyLayoutManager : LayoutManager
{
    public MyLayoutManager(MyLayout layout)
        : base(layout)
    {
    }

    public override Size Measure(double widthConstraint, double heightConstraint)
    {
        System.Diagnostics.Debug.WriteLine($"Measure: {widthConstraint}x{heightConstraint}");
        var children = this.Layout;
        foreach (IView child in children)
        {
            child.Measure(widthConstraint, heightConstraint);
        }

        return new Size(widthConstraint, heightConstraint);
    }

    public override Size ArrangeChildren(Rect bounds)
    {
        System.Diagnostics.Debug.WriteLine($"Arrange: {bounds}");
        var children = this.Layout;
        foreach (IView child in children)
        {
            child.Arrange(new Rect(bounds.X, bounds.Y, child.DesiredSize.Width, child.DesiredSize.Height));
        }

        return new Size(bounds.Width, bounds.Height);
    }
}