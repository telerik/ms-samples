#if IOS || MACCATALYST
using System;
using CoreAnimation;
using CoreFoundation;
using Foundation;
using UIKit;

namespace PerformanceMetricsSample;

public class FrameMetricsReporter : NSObject
{
    private static DispatchQueue queue = null!;

    private CADisplayLink? displayLink;
    private int frameCount;
    private int droppedFrames;
    private double lastTimestamp;
    private int lastFrameCount;
    private int lastDroppedFrames;
    private double lastFps;
    private int refreshRate;

    public double LastFps => this.lastFps;
    public int LastFrameCount => this.lastFrameCount;
    public int LastDroppedFrames => this.lastDroppedFrames;

    public EventHandler? FrameMetricsUpdated;

    public void Start()
    {
        if (this.displayLink != null)
        {
            return;
        }

        this.refreshRate = (int)UIScreen.MainScreen.MaximumFramesPerSecond;

        this.displayLink = CADisplayLink.Create(this.OnDisplayLink);
        this.displayLink.PreferredFramesPerSecond = this.refreshRate;
        this.displayLink.Paused = true;
        this.displayLink.AddToRunLoop(NSRunLoop.Main, NSRunLoopMode.Common);
        this.displayLink.Paused = false;

        this.lastTimestamp = 0;
        this.frameCount = 0;
        this.droppedFrames = 0;

        queue = DispatchQueue.GetGlobalQueue(DispatchQueuePriority.High);
    }

    public void Stop()
    {
        this.displayLink?.Invalidate();
        this.displayLink = null;
    }

    private void OnDisplayLink()
    {
        queue.DispatchAsync(() =>
        {
            if (this.lastTimestamp == 0)
            {
                this.lastTimestamp = this.displayLink!.Timestamp;
                return;
            }

            this.frameCount++;
            double elapsed = this.displayLink!.Timestamp - this.lastTimestamp;
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
                this.lastTimestamp = this.displayLink.Timestamp;

                this.FrameMetricsUpdated?.Invoke(this, EventArgs.Empty);
            }
        });
    }
}
#endif
