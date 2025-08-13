using Microsoft.Maui.Layouts;
using System.Diagnostics;

namespace LayoutMeasuredWrong;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        CustomLayout layout = new CustomLayout();
        layout.WidthRequest = 215;
        layout.Children.Add(new Button() { Text = "Click Me", WidthRequest = 66 });

        this.Content = layout;

        InitializeComponent();
    }
}

public class CustomLayout : Layout
{
    protected override ILayoutManager CreateLayoutManager()
    {
        return new CustomLayoutManager(this);
    }
}

public class CustomLayoutManager : ILayoutManager
{
    public CustomLayoutManager(CustomLayout layout)
    {
        this.MyCustomLayout = layout;
    }

    public CustomLayout MyCustomLayout { get; }

    public Size ArrangeChildren(Rect bounds)
    {
        Debug.WriteLine($"::Arrange called for CustomLayout: bounds.Width {bounds.Width}, bounds.Height {bounds.Height}");

        return bounds.Size;
    }

    public Size Measure(double widthConstraint, double heightConstraint)
    {
        Debug.WriteLine($"::Measure called for CustomLayout: WidthRequest {this.MyCustomLayout.WidthRequest}, widthConstraint {widthConstraint}, heightConstraint {heightConstraint}");

        var child = this.MyCustomLayout.Children.FirstOrDefault();
        var childMeasure = child?.Measure(widthConstraint, heightConstraint);

        Debug.WriteLine($"::Measure child: WidthRequest {((View)child).WidthRequest}, widthConstraint {childMeasure?.Width}");

        return new Size(widthConstraint, heightConstraint);
    }
}