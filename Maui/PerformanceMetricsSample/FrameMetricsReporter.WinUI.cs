#if WINDOWS
using Microsoft.UI.Xaml.Media;

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

    public double LastFps => this.lastFps;
    public int LastFrameCount => this.lastFrameCount;
    public int LastDroppedFrames => this.lastDroppedFrames;

    public EventHandler? FrameMetricsUpdated;

    public FrameMetricsReporter()
    {
        // Assuming a default refresh rate of 60Hz for most displays
        this.refreshRate = 60.0;
    }

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

        CompositionTarget.Rendering += OnRendering;
    }

    public void Stop()
    {
        if (!this.isRunning)
        {
            return;
        }

        this.isRunning = false;
        CompositionTarget.Rendering -= OnRendering;
    }

    private void OnRendering(object? sender, object e)
    {
        if (!this.isRunning)
        {
            return;
        }

        var renderingEventArgs = e as RenderingEventArgs;
        double currentTimestamp = renderingEventArgs?.RenderingTime.TotalSeconds ?? 0;

        if (this.lastTimestamp == 0)
        {
            this.lastTimestamp = currentTimestamp;
            return;
        }

        this.frameCount++;
        double elapsed = currentTimestamp - this.lastTimestamp;
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
            this.lastTimestamp = currentTimestamp;

            this.FrameMetricsUpdated?.Invoke(this, EventArgs.Empty);
        }
    }
}
#endif