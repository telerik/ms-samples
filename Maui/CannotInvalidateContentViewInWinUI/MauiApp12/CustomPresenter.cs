using Microsoft.Maui.Layouts;

namespace MauiApp12;

public class CustomPresenter : Layout
{
    protected override ILayoutManager CreateLayoutManager()
    {
        return new CustomPresenterLM(this);
    }
}

public class CustomPresenterLM : ILayoutManager
{
    private readonly CustomPresenter presenter;

    public CustomPresenterLM(CustomPresenter presenter)
    {
        this.presenter = presenter;
    }

    Size ILayoutManager.Measure(double widthConstraint, double heightConstraint)
    {
        System.Diagnostics.Debug.WriteLine("::: CustomPresenterLM Measure");

        Size desiredSize = new Size(10, 10);

        foreach (IView child in this.presenter)
        {
            Size size = child.Measure(widthConstraint, heightConstraint);
            desiredSize.Width = Math.Max(desiredSize.Width, size.Width);
            desiredSize.Height = Math.Max(desiredSize.Height, size.Height);
        }

        // Include some custom logic.

        return desiredSize;
    }

    Size ILayoutManager.ArrangeChildren(Rect bounds)
    {
        System.Diagnostics.Debug.WriteLine("::: CustomPresenterLM ArrangeChildren");

        // Include some custom logic.

        foreach (IView child in this.presenter)
        {
            child.Arrange(bounds);
        }

        return bounds.Size;
    }
}
