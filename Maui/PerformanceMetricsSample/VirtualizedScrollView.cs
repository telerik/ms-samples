using Microsoft.Maui.Layouts;

namespace PerformanceMetricsSample;

public class VirtualizedScrollView : ScrollView
{
    // Grid settings
    internal static int RowHeight = 60;
    internal static int ColWidth = 60;
    internal static int TotalRows = 10000; // Simulate infinite
    internal static int TotalCols = 1000;  // Simulate infinite
    internal static int BufferRows = 2;    // Extra rows for smooth recycling
    internal static int BufferCols = 2;    // Extra cols for smooth recycling

    private readonly VirtualizedLayout layout;
    private readonly Queue<View> recycledItems = new();
    private readonly Dictionary<(int row, int col), View> displayedItems = new();

    internal readonly VisualElementProfiler profiler = new();

    public static readonly BindableProperty ItemTemplateProperty =
        BindableProperty.Create(nameof(ItemTemplate), typeof(DataTemplate), typeof(VirtualizedScrollView), default(DataTemplate));

    private bool loaded = false;
    private double scrollX;
    private double scrollY;

    public VirtualizedScrollView()
    {
        LoadTimeTracker.Track(this, nameof(VirtualizedScrollView));
        this.Orientation = ScrollOrientation.Both;

        this.layout = new VirtualizedLayout();
        this.layout.WidthRequest = TotalCols * ColWidth;
        this.layout.HeightRequest = TotalRows * RowHeight;
        this.Content = this.layout;

        this.profiler.Attach(this.layout);

        this.Scrolled += this.OnScroll;
    }

    public DataTemplate? ItemTemplate
    {
        get => (DataTemplate?)GetValue(ItemTemplateProperty);
        set => SetValue(ItemTemplateProperty, value);
    }

    private void OnScroll(object? sender, ScrolledEventArgs e)
    {
#if ANDROID
        // NOTE: Ugly workaround for Android bug.
        if (e.ScrollX != 0)
        {
            this.scrollX = e.ScrollX;
        }

        if (e.ScrollY != 0)
        {
            this.scrollY = e.ScrollY;
        }
#else
        this.scrollX = e.ScrollX;
        this.scrollY = e.ScrollY;
#endif
        this.UpdateVisibleItems();
    }

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);

        if (this.loaded || width < 0 || height < 0)
        {
            return;
        }

        this.loaded = true;
        this.UpdateVisibleItems();
    }

    private void UpdateVisibleItems()
    {
        var scrollX = this.scrollX;
        var scrollY = this.scrollY;
        var viewportWidth = this.Width;
        var viewportHeight = this.Height;

        int firstVisibleRow = Math.Max(0, (int)(scrollY / RowHeight));
        int firstVisibleCol = Math.Max(0, (int)(scrollX / ColWidth));
        int visibleRows = Math.Min(TotalRows - firstVisibleRow, (int)(viewportHeight / RowHeight) + BufferRows);
        int visibleCols = Math.Min(TotalCols - firstVisibleCol, (int)(viewportWidth / ColWidth) + BufferCols);

        var needed = new HashSet<(int row, int col)>(visibleRows * visibleCols);
        for (int r = firstVisibleRow; r < firstVisibleRow + visibleRows; r++)
        {
            for (int c = firstVisibleCol; c < firstVisibleCol + visibleCols; c++)
            {
                needed.Add((r, c));
            }
        }

        // Remove items out of view
        foreach (var key in new List<(int, int)>(this.displayedItems.Keys))
        {
            if (!needed.Contains(key))
            {
                var view = this.displayedItems[key];
                view.IsVisible = false;
                this.recycledItems.Enqueue(view);
                this.displayedItems.Remove(key);
            }
        }

        // Add new items
        foreach (var key in needed)
        {
            if (!this.displayedItems.ContainsKey(key))
            {
                var view = this.GetOrCreateItem(key.row, key.col);
                this.displayedItems[key] = view;
            }
        }
    }

    private View GetOrCreateItem(int row, int col)
    {
        View view;
        if (this.recycledItems.Count > 0)
        {
            view = this.recycledItems.Dequeue();
            view.IsVisible = true;
        }
        else
        {
            if (this.ItemTemplate != null)
            {
                view = (View)this.ItemTemplate.CreateContent();
            }
            else
            {
                view = new ContentView();
            }

            this.layout.Children.Add(view);
        }

        var color = GetColor(row, col);
        view.BackgroundColor = color;
        view.BindingContext = new GridCellContext(row, col);

        return view;
    }

    private static Color GetColor(int row, int col)
    {
        Color[] colors = new[]
        {
            Color.FromArgb("#FF0000"), Color.FromArgb("#00FF00"), Color.FromArgb("#0000FF"),
            Color.FromArgb("#FFA500"), Color.FromArgb("#800080"), Color.FromArgb("#008080"),
            Color.FromArgb("#FFFF00"), Color.FromArgb("#A52A2A")
        };

        return colors[(row + col) % colors.Length];
    }
}

public class GridCellContext
{
    public GridCellContext(int row, int col)
    {
        this.Row = row;
        this.Col = col;
    }

    public int Row { get; set; }
    public int Col { get; set; }

    public override string ToString()
        => $"Row: {this.Row}, Col: {this.Col}";
}

public class VirtualizedLayout : CustomLayout
{
    protected override ILayoutManager CreateLayoutManager()
        => new VirtualizedLayoutManager(this);
}

public class VirtualizedLayoutManager : LayoutManager
{
    private readonly VirtualizedLayout layout;
    public VirtualizedLayoutManager(VirtualizedLayout layout) : base(layout)
    {
        this.layout = layout;
    }

    public override Size ArrangeChildren(Rect bounds)
    {
        this.layout.TriggerLayoutPassEvent(LayoutPassEventTypes.ArrangeStart);
        foreach (var child in this.layout.Children)
        {
            if (child.Visibility != Visibility.Visible)
            {
                continue;
            }

            if (((View)child).BindingContext is not GridCellContext ctx)
            {
                continue;
            }

            double x = ctx.Col * VirtualizedScrollView.ColWidth;
            double y = ctx.Row * VirtualizedScrollView.RowHeight;
            double w = VirtualizedScrollView.ColWidth - 4;
            double h = VirtualizedScrollView.RowHeight - 4;
            child.Arrange(new Rect(x, y, w, h));
        }

        this.layout.TriggerLayoutPassEvent(LayoutPassEventTypes.ArrangeEnd);

        return bounds.Size;
    }

    public override Size Measure(double widthConstraint, double heightConstraint)
    {
        this.layout.TriggerLayoutPassEvent(LayoutPassEventTypes.MeasureStart);
        foreach (var child in this.layout.Children)
        {
            if (child.Visibility != Visibility.Visible)
            {
                continue;
            }

            child.Measure(VirtualizedScrollView.ColWidth - 4, VirtualizedScrollView.RowHeight - 4);
        }

        this.layout.TriggerLayoutPassEvent(LayoutPassEventTypes.MeasureEnd);

        return new Size(VirtualizedScrollView.TotalCols * VirtualizedScrollView.ColWidth, VirtualizedScrollView.TotalRows * VirtualizedScrollView.RowHeight);
    }
}

public abstract class CustomLayout : Layout
{
    internal event EventHandler<LayoutPassEventTypes> LayoutPassEvent;

    internal void TriggerLayoutPassEvent(LayoutPassEventTypes layoutPassEvent)
    {
        this.LayoutPassEvent?.Invoke(this, layoutPassEvent);
    }
}

internal enum LayoutPassEventTypes
{
    MeasureStart,
    MeasureEnd,
    ArrangeStart,
    ArrangeEnd,
}