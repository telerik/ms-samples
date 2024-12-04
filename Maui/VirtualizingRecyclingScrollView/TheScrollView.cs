using System.Diagnostics.CodeAnalysis;
using System.Security.AccessControl;
using Microsoft.Maui.Layouts;

namespace VirtualizingRecyclingScrollView;

public class TheScrollView : ScrollView
{
    public DataTemplate Template { get; set; }

#if NET8_0
    const double RowHeight = 100;
    const double ColumnWidth = 200;
#elif NET9_0
    const double RowHeight = 50;
    const double ColumnWidth = 140;
#endif

    private Point scroll;
    private Size size;
    private Rect rect;

    private GraphicsView selectionOutline;
    private SelectionDrawable selectionDrawable;

    private VirtualizingLayout virtualizingLayout;

    public TheScrollView()
    {
        this.virtualizingLayout = new VirtualizingLayout(this);

        this.selectionDrawable = new SelectionDrawable(this);
        this.selectionOutline = new GraphicsView();
        this.selectionOutline.Drawable = this.selectionDrawable;
        this.selectionOutline.ZIndex = 1;
        this.virtualizingLayout.Add(this.selectionOutline);

        this.Content = virtualizingLayout;

        this.Scrolled += OnScrolled;
        this.Orientation = ScrollOrientation.Both;
    }

    private void OnScrolled(object? sender, ScrolledEventArgs args)
    {
        DateTime start = DateTime.Now;
        this.scroll = new Point(this.ScrollX, this.ScrollY);
        this.rect = new Rect(this.scroll.X, this.scroll.Y, this.size.Width, this.size.Height);
        var change = this.virtualizingLayout.layoutManager.RealizeViewport();
        if (change != 0)
        {
            Console.WriteLine($" = OnScrolled {DateTime.Now - start}");
        }
    }

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();
#if IOS || MACCATALYST
        var scrollview = (UIKit.UIScrollView)this.Handler.PlatformView;
        scrollview.ContentInset = new UIKit.UIEdgeInsets(60, 0, 50, 0);
        scrollview.VerticalScrollIndicatorInsets = new UIKit.UIEdgeInsets(50, 0, 20, 0);
        scrollview.HorizontalScrollIndicatorInsets = new UIKit.UIEdgeInsets(0, 0, 20, 0);
#endif
    }

    protected override Size ArrangeOverride(Rect bounds)
    {
        Console.WriteLine("   - ScrollView Arrange");
        DateTime start = DateTime.Now;
        this.size = base.ArrangeOverride(bounds);
        this.rect = new Rect(this.scroll.X, this.scroll.Y, this.size.Width, this.size.Height);
        Console.WriteLine($"     ScrollView Arranged {DateTime.Now - start}");
        return this.size;
    }

    protected override Size MeasureOverride(double widthConstraint, double heightConstraint)
    {
        Console.WriteLine("   - ScrollView Measure");
        DateTime start = DateTime.Now;
        this.size = base.MeasureOverride(widthConstraint, heightConstraint);
        this.rect = new Rect(this.scroll.X, this.scroll.Y, this.size.Width, this.size.Height);
        Console.WriteLine($"     ScrollView Measured {DateTime.Now - start}");
        return size;
    }

    private class VirtualizingLayout : Layout
    {
        public TheScrollView scrollview;
        public VirtualizingLayoutManager layoutManager;

        public VirtualizingLayout(TheScrollView scrollview)
        {
            this.scrollview = scrollview;
            this.layoutManager = new VirtualizingLayoutManager(this);
        }

        protected override ILayoutManager CreateLayoutManager()
        {
            return this.layoutManager;
        }
    }

    private class VirtualizingLayoutManager : ILayoutManager, IEqualityComparer<Key>
    {
        private VirtualizingLayout container;

        private Dictionary<Key, View> elements = new Dictionary<Key, View>();

        private Stack<View> trashbin = new Stack<View>();

        private Stack<View> disappearingViews = new Stack<View>();

        private int currentLeft = -1;
        private int currentRight = -1;
        private int currentTop = -1;
        private int currentBottom = -1;

        public VirtualizingLayoutManager(VirtualizingLayout container)
        {
            this.container = container;
        }

        public bool Equals(Key lhs, Key rhs) => lhs.x == rhs.x && lhs.y == rhs.y;

        public int GetHashCode([DisallowNull] Key obj) => obj.x * 67033 + obj.y * 67043;

        public Size ArrangeChildren(Rect bounds)
        {
#if NET8_0
            // net8 8.0.82 and 8.0.92 doesn't seem to work without arranging all children...
            // in net9, we Arrange once during Recycling
            foreach(var content in this.container.Children)
            {
                if (content == this.container.scrollview.selectionOutline)
                {
                    // BUG: The selectionOutline won't show up
                    this.container.scrollview.selectionOutline.Arrange(
                        new Rect(
                            this.container.scrollview.selectionDrawable.Left * ColumnWidth,
                            this.container.scrollview.selectionDrawable.Top * RowHeight,
                            this.container.scrollview.selectionDrawable.Width * ColumnWidth,
                            this.container.scrollview.selectionDrawable.Height * RowHeight
                        )
                    );
                }
                else
                {
                    var cellmodel = (content as View).BindingContext as CellModel;
                    content.Arrange(new Rect(cellmodel.key.x * ColumnWidth, cellmodel.key.y * RowHeight, ColumnWidth, RowHeight));
                }
            }
#endif

            return new Size(10000, 10000);
        }

        public Size Measure(double widthConstraint, double heightConstraint)
        {
            
#if NET8_0
            // net8 8.0.82 and 8.0.92 doesn't seem to work without measuring all children...
            // in net9, we don't measure
            foreach(var content in this.container.Children)
            {
                if (content == this.container.scrollview.selectionOutline)
                {
                    this.container.scrollview.selectionOutline.Measure(
                        this.container.scrollview.selectionDrawable.Width * ColumnWidth,
                        this.container.scrollview.selectionDrawable.Height * RowHeight);
                }
                else
                {
                    content.Measure(double.PositiveInfinity, double.PositiveInfinity);
                }
            }
#endif

            return new Size(10000, 10000);
        }

        public int RealizeViewport()
        {
            int left = Math.Max(0, (int)Math.Floor(this.container.scrollview.rect.X / ColumnWidth));
            int right = (int)Math.Ceiling((this.container.scrollview.rect.X + this.container.scrollview.rect.Width) / ColumnWidth) - 1;
            int top = Math.Max(0, (int)Math.Floor(this.container.scrollview.rect.Y / RowHeight));
            int bottom = (int)Math.Ceiling((this.container.scrollview.rect.Y + this.container.scrollview.Height) / RowHeight) - 1;

            if (left == currentLeft && top != currentTop && right != currentRight && bottom != currentBottom)
            {
                return 0;
            }

            var outline = this.container.scrollview.selectionOutline;

            // We need proper API to prevent propagation UP
#if NET9_0_OR_GREATER
            MauiProgram.IsInVirtualizationScope++;
#endif

            this.currentLeft = left;
            this.currentRight = right;
            this.currentTop = top;
            this.currentBottom = bottom;

            int removed = 0;
            int added = 0;

            foreach(var kvp in this.elements)
            {
                if (kvp.Key.x < left || kvp.Key.x > right || kvp.Key.y < top || kvp.Key.y > bottom)
                {
                    this.disappearingViews.Push(kvp.Value);
                    this.elements.Remove(kvp.Key);
                    removed++;
                }
            }

            for (int x = left; x <= right; x++)
            {
                for (int y = top; y <= bottom; y++)
                {
                    var key = new Key(x, y);
                    if (!elements.ContainsKey(key))
                    {
                        View? content = null;
                        if (this.disappearingViews.Count > 0)
                        {
                            content = this.disappearingViews.Pop();
                        }
                        else if (this.trashbin.Count > 0)
                        {
                            content = this.trashbin.Pop();
                        }
                        else
                        {
                            content = this.container.scrollview.Template.CreateContent() as View;
                            content.BindingContext = new CellModel();
                            this.container.Add(content);
                        }

                        content.IsVisible = true;
                        var cellmodel = (CellModel)content.BindingContext;
                        cellmodel.key = key;
                        var code = this.container.layoutManager.GetHashCode(key);

                        var color = this.container.scrollview.IsSelected(x, y) ?
                            new Color(150 + code % 56, 150 + (code >> 4) % 56, 150 + (byte)(code >> 8) % 56) :
                            new Color(200 + code % 56, 200 + (code >> 4) % 56, 200 + (byte)(code >> 8) % 56);

                        cellmodel.Update(
                            text: $"Cell {x} x {y}",
                            color: color
                        );

                        elements[key] = content;


#if NET9_0_OR_GREATER
                        // Measure Ad-Hoc... seems to only work with 9, in net8 seems the children are layed out by native and this is overridden.
                        // content.Measure(double.PositiveInfinity, double.PositiveInfinity);
                        content.Arrange(new Rect(cellmodel.key.x * ColumnWidth, cellmodel.key.y * RowHeight, ColumnWidth, RowHeight));
#endif

                        added++;
                    }
                }
            }

            while(this.disappearingViews.Count > 0)
            {
                var popped = this.disappearingViews.Pop();
                popped.IsVisible = false;
                this.trashbin.Push(popped);
            }

            // Range for selection is expanded a little, so edge decorations would work well by the screen edge
            
            // Don't shrink the selection view. Sometimes there will be a frame where a line disappears on top and shortly after another appears at bottom... this avoids resizing.
            var selectionRight = Math.Max(right + 1, left + this.container.scrollview.selectionDrawable.Width - 2);
            var selectionBottom = Math.Max(bottom + 1, top + this.container.scrollview.selectionDrawable.Height - 2);

            this.container.scrollview.selectionDrawable.SetVisibleRange(left - 1, top - 1, selectionRight, selectionBottom);

            var outlineRect = new Rect((left - 1) * ColumnWidth, (top - 1) * RowHeight, (selectionRight - left + 2) * ColumnWidth, (selectionBottom - top + 2) * RowHeight);
#if (IOS || MACCATALYST) && NET8_0
            //BUG: Arrange in net8 doesn't work here, but also the arrange in the layout manager won't show up the selection outline.
#else
            this.container.scrollview.selectionOutline.Arrange(outlineRect);
#endif
            this.container.scrollview.selectionOutline.Invalidate();

            if (removed != 0 || added != 0)
            {
                Console.WriteLine($"      +{added}/-{removed}");
            }

#if NET9_0_OR_GREATER
            MauiProgram.IsInVirtualizationScope--;
#endif

            TrackingModel.Instance.RecycledItems += (uint)removed;

            return added + removed;
        }
    }

    private class SelectionDrawable : IDrawable
    {
        private TheScrollView scrollview;

        private int left = -1000;
        private int top = -1000;
        private int right = -1000;
        private int bottom = -1000;

        public SelectionDrawable(TheScrollView scrollview)
        {
            this.scrollview = scrollview;
        }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            canvas.StrokeColor = new Color(0x00, 0x22, 0x99, 0xFF);
            canvas.StrokeSize = 3;

            for (var x = left; x <= right; x++)
            {
                for (var y = top; y <= bottom; y++)
                {
                    if (this.scrollview.IsSelected(x, y))
                    {
                        Rect rect = new Rect(
                            x: (float)((x - left) * ColumnWidth),
                            y: (float)((y - top) * RowHeight),
                            width: (float)ColumnWidth,
                            height: (float)RowHeight
                        );
                        canvas.DrawRectangle(rect);
                    }
                }
            }
        }

        public int Width => this.right - this.left + 1;

        public int Left => this.left;

        public int Top => this.top;

        public int Height => this.bottom - this.top + 1;

        public void SetVisibleRange(int left, int top, int right, int bottom)
        {
            this.left = left;
            this.top = top;
            this.right = right;
            this.bottom = bottom;
        }
    }

    public bool IsSelected(int x, int y) => (x * 7883 + y * 7901) % 21 <= 3;
}