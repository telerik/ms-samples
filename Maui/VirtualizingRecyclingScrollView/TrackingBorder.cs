
namespace VirtualizingRecyclingScrollView;

public class TrackingBorder : Border
{
    public bool Virtualized { get; set; } = false;

    protected override Size ArrangeOverride(Rect bounds)
    {
        if (this.Virtualized)
        {
            TrackingModel.Instance.LayoutVirtualNodes++;
        }
        else
        {
            TrackingModel.Instance.LayoutNodes++;
        }

        return base.ArrangeOverride(bounds);
    }

    protected override Size MeasureOverride(double widthConstraint, double heightConstraint)
    {
        if (this.Virtualized)
        {
            TrackingModel.Instance.LayoutVirtualNodes++;
        }
        else
        {
            TrackingModel.Instance.LayoutNodes++;
        }

        return base.MeasureOverride(widthConstraint, heightConstraint);
    }
}