using System.ComponentModel;

namespace VirtualizingRecyclingScrollView;

public sealed class TrackingModel : INotifyPropertyChanged
{
    public static TrackingModel Instance = new TrackingModel();

    private uint layoutNodes = 0;

    private uint layoutVirtualNodes = 0;

    private uint printedLayoutNodes = 0;

    private uint printedLayoutVirtualNodes = 0;

    private uint recycledItems = 0;

    private uint droppedFrames = 0;

    private const float MeasurementFPS = 60;

#if IOS
    private CoreAnimation.CADisplayLink displayLink;
#endif

    public TrackingModel()
    {
        var timer = Application.Current.Dispatcher.CreateTimer();
        timer.Interval = TimeSpan.FromMicroseconds(100);
        timer.Tick += (s,e) =>
        {
            if (layoutNodes != printedLayoutNodes || layoutVirtualNodes != printedLayoutVirtualNodes)
            {
                printedLayoutNodes = layoutNodes;
                printedLayoutVirtualNodes = layoutVirtualNodes;
                Console.WriteLine($"  Shell: {layoutNodes}, ScrollView: {layoutVirtualNodes}");
            }
        };
        timer.Start();

#if IOS
        this.displayLink = CoreAnimation.CADisplayLink.Create(Frame);
        this.displayLink.PreferredFrameRateRange = new CoreAnimation.CAFrameRateRange()
        {
            Minimum = MeasurementFPS,
            Maximum = MeasurementFPS,
            Preferred = MeasurementFPS
        };
        this.displayLink.AddToRunLoop(Foundation.NSRunLoop.Main, Foundation.NSRunLoopMode.Common);
#endif
    }

    double last = 0;

    double estimatedFrameLength = 1.05 / MeasurementFPS;

    private void Frame()
    {
#if IOS
        var elapsed = CoreAnimation.CAAnimation.CurrentMediaTime();
        if (last == 0)
        {
            last = elapsed;
            return;
        }
        var duration = elapsed - last;

        // For macOS the simulator runs in 60 fps
        if (duration > estimatedFrameLength)
        {
            var droppedFrames = (uint)Math.Floor(duration / estimatedFrameLength);
            this.DroppedFrames += droppedFrames;
            Console.WriteLine($"Dropped {this.DroppedFrames} frames! Next frame duration: {duration}");
        }
        this.last = elapsed;
#endif
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public uint DroppedFrames
    {
        get => this.droppedFrames;
        set
        {
            this.droppedFrames = value;
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DroppedFrames)));
        }
    }

    public uint LayoutNodes
    {
        get => this.layoutNodes;
        set
        {
            this.layoutNodes = value;
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LayoutNodes)));
        }
    }

    public uint LayoutVirtualNodes
    {
        get => this.layoutVirtualNodes;
        set
        {
            this.layoutVirtualNodes = value;
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LayoutVirtualNodes)));
        }
    }

    public uint RecycledItems
    {
        get => this.recycledItems;
        set
        {
            this.recycledItems = value;
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RecycledItems)));
        }
    }
}
