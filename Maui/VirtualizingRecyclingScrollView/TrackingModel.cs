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

#if IOS || MACCATALYST
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

#if IOS || MACCATALYST
        this.displayLink = CoreAnimation.CADisplayLink.Create(OnIosFrame);
        this.displayLink.AddToRunLoop(Foundation.NSRunLoop.Main, Foundation.NSRunLoopMode.Common);
#elif ANDROID
        this.androidFrameCallback = new FrameCallback(this);
        Android.Views.Choreographer.Instance!.PostFrameCallback(androidFrameCallback);
#endif
    }

    double last = 0;

    double estimatedFrameLength = 1.05 / MeasurementFPS;

#if IOS || MACCATALYST
    private void OnIosFrame()
    {
        var elapsed = CoreAnimation.CAAnimation.CurrentMediaTime();
        this.Frame(elapsed);
    }
#elif ANDROID

    private FrameCallback androidFrameCallback;

    class FrameCallback : Java.Lang.Object, Android.Views.Choreographer.IFrameCallback
    {
        private TrackingModel trackingModel;

        public FrameCallback(TrackingModel trackingModel)
        {
            this.trackingModel = trackingModel;
        }

        public void DoFrame(long frameTimeNanos)
        {
            this.trackingModel.Frame(frameTimeNanos / 1000000000.0);
            Android.Views.Choreographer.Instance!.PostFrameCallback(this);
        }
    }
#endif

    private void Frame(double elapsedSeconds)
    {
        if (last == 0)
        {
            last = elapsedSeconds;
            return;
        }
        var duration = elapsedSeconds - last;
        
        // Console.WriteLine("Lastframe " + duration + " estimated " + estimatedFrameLength);

        // For macOS the simulator runs in 60 fps
        if (duration > estimatedFrameLength)
        {
            var droppedFrames = (uint)Math.Floor(duration / estimatedFrameLength);
            this.DroppedFrames += droppedFrames;
            Console.WriteLine($"Dropped {this.DroppedFrames} frames! Next frame duration: {duration}");
        }
        this.last = elapsedSeconds;
    }

    internal void Clear()
    {
        this.droppedFrames = 0;
        this.layoutNodes = 0;
        this.layoutVirtualNodes = 0;
        this.recycledItems = 0;
        this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
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
