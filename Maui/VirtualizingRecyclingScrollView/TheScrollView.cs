using System.Diagnostics.CodeAnalysis;
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

    private Container container;

    public TheScrollView()
    {
        this.container = new Container(this);
        this.Content = container;
        this.Scrolled += OnScrolled;
        this.Orientation = ScrollOrientation.Both;
    }

    private void OnScrolled(object? sender, ScrolledEventArgs args)
    {
        DateTime start = DateTime.Now;
        this.scroll = new Point(this.ScrollX, this.ScrollY);
        this.rect = new Rect(this.scroll.X, this.scroll.Y, this.size.Width, this.size.Height);
        var change = this.container.layoutManager.RealizeViewport();
        if (change != 0)
        {
            Console.WriteLine($" = OnScrolled {DateTime.Now - start}");
        }
    }

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();
#if IOS
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

    private class Container : Layout
    {
        public TheScrollView scrollview;
        public LayoutManager layoutManager;

        public Container(TheScrollView scrollview)
        {
            this.scrollview = scrollview;
            this.layoutManager = new LayoutManager(this);
        }

        protected override ILayoutManager CreateLayoutManager()
        {
            return this.layoutManager;
        }
    }

    private class LayoutManager : ILayoutManager, IEqualityComparer<Key>
    {
        private Container container;

        private Dictionary<Key, View> elements = new Dictionary<Key, View>();

        private Stack<View> trashbin = new Stack<View>();

        private Stack<View> disappearingViews = new Stack<View>();

        private int currentLeft = -1;
        private int currentRight = -1;
        private int currentTop = -1;
        private int currentBottom = -1;

        public LayoutManager(Container container)
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
                var cellmodel = (content as View).BindingContext as CellModel;
                content.Arrange(new Rect(cellmodel.key.x * ColumnWidth, cellmodel.key.y * RowHeight, ColumnWidth, RowHeight));
            }
#endif

            return new Size(10000, 10000);
        }

        public Size Measure(double widthConstraint, double heightConstraint)
        {
            
#if NET8_0
            // net8 8.0.82 and 8.0.92 doesn't seem to work without measuring all children...
            // in net9, we dont measure
            foreach(var content in this.container.Children)
            {
                content.Measure(double.PositiveInfinity, double.PositiveInfinity);
            }
#endif

            return new Size(10000, 10000);
        }

        public int RealizeViewport()
        {
            int left = Math.Max(0, (int)Math.Floor(this.container.scrollview.rect.X / ColumnWidth));
            int right = (int)Math.Ceiling((this.container.scrollview.rect.X + this.container.scrollview.rect.Width) / ColumnWidth);
            int top = Math.Max(0, (int)Math.Floor(this.container.scrollview.rect.Y / RowHeight));
            int bottom = (int)Math.Ceiling((this.container.scrollview.rect.Y + this.container.scrollview.Height) / RowHeight);

            if (left == currentLeft && top != currentTop && right != currentRight && bottom != currentBottom)
            {
                return 0;
            }

#if NET9_0_OR_GREATER
            MauiProgram.IsInVirtualizationScope++;
#endif
#if IOS
            bool animations = UIKit.UIView.AnimationsEnabled;
            UIKit.UIView.AnimationsEnabled = false;
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
                        cellmodel.Color = new Color(200 + code % 56, 200 + (code >> 4) % 56, 200 + (byte)(code >> 8) % 56);
                        cellmodel.Text = $"Cell {x} x {y}";
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

            if (removed != 0 || added != 0)
            {
                Console.WriteLine($"      +{added}/-{removed}");
            }

#if NET9_0_OR_GREATER
            MauiProgram.IsInVirtualizationScope--;
#endif
#if IOS
            UIKit.UIView.AnimationsEnabled = animations;
#endif

            return added + removed;
        }
    }
}