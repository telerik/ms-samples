#if ANDROID
using Android.Views;

namespace PerformanceMetricsSample;

public class FrameMetricsReporter
{
    private int frameCount;
    private int droppedFrames;
    private double lastTimestamp;
    private int lastFrameCount;
    private int lastDroppedFrames;
    private double lastFps;
    private double refreshRate;

    private bool isRunning;

    private FrameCallback? androidFrameCallback;

    public double LastFps => this.lastFps;
    public int LastFrameCount => this.lastFrameCount;
    public int LastDroppedFrames => this.lastDroppedFrames;

    public FrameMetricsReporter()
    {
        this.refreshRate = Platform.CurrentActivity?.Display.RefreshRate ?? 60.0;
    }

    public EventHandler? FrameMetricsUpdated;

    public void Start()
    {
        if (this.isRunning)
        {
            return;
        }

        this.isRunning = true;
        this.frameCount = 0;
        this.droppedFrames = 0;
        this.lastTimestamp = 0;
        this.androidFrameCallback = new FrameCallback(this);
        Android.Views.Choreographer.Instance!.PostFrameCallback(this.androidFrameCallback);
    }

    public void Stop()
    {
        this.isRunning = false;
        Android.Views.Choreographer.Instance!.RemoveFrameCallback(this.androidFrameCallback);
    }

    internal void OnFrame(double timestamp)
    {
        if (!this.isRunning)
        {
            return;
        }

        if (this.lastTimestamp == 0)
        {
            this.lastTimestamp = timestamp;
        }
        else
        {
            this.frameCount++;
            double elapsed = timestamp - this.lastTimestamp;
            if (elapsed >= 1.0)
            {
                double fps = this.frameCount / elapsed;
                int expectedFrames = (int)(elapsed * this.refreshRate);
                this.droppedFrames = expectedFrames - this.frameCount;
                this.lastFps = fps;
                this.lastFrameCount = this.frameCount;
                this.lastDroppedFrames = this.droppedFrames;
                this.frameCount = 0;
                this.droppedFrames = 0;
                this.lastTimestamp = timestamp;

                this.FrameMetricsUpdated?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    class FrameCallback : Java.Lang.Object, Android.Views.Choreographer.IFrameCallback
    {
        private readonly FrameMetricsReporter reporter;

        public FrameCallback(FrameMetricsReporter reporter)
        {
            this.reporter = reporter;
        }

        public void DoFrame(long frameTimeNanos)
        {
            this.reporter.OnFrame(frameTimeNanos / 1000000000.0);
            Android.Views.Choreographer.Instance!.PostFrameCallback(this);
        }
    }
}
#endif