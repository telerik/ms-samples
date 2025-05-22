using System.Diagnostics;

namespace PerformanceMetricsSample;

public static class LoadTimeTracker
{
    public static void Track(VisualElement control, string name = null)
    {
        var stopwatch = Stopwatch.StartNew();

        EventHandler handler = null;
        handler = (s, e) =>
        {
            stopwatch.Stop();
            System.Diagnostics.Debug.WriteLine($"{name ?? control.GetType().Name} loaded in {stopwatch.ElapsedMilliseconds} ms");
            control.Loaded -= handler;
        };

        control.Loaded += handler;
    }
}