namespace PerformanceMetricsSample;

public partial class MainPage : ContentPage
{
    private FrameMetricsReporter frameMetricsReporter;

    public MainPage()
    {
        InitializeComponent();

        this.frameMetricsReporter = new FrameMetricsReporter();
        this.frameMetricsReporter.Start();

        this.frameMetricsReporter.FrameMetricsUpdated += (s, e) =>
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                this.FrameStatsLabel.Text = $"FPS: {this.frameMetricsReporter.LastFps:F1}, Dropped: {this.frameMetricsReporter.LastDroppedFrames}";
            });
        };
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        var lines = this.sv.profiler.ToString().Split("\n");
        foreach (var line in lines)
        {
            System.Diagnostics.Debug.WriteLine(line);
        }
    }
}