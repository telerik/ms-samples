using System.ComponentModel;

namespace VirtualizingRecyclingScrollView;

public sealed class TrackingModel : INotifyPropertyChanged
{
    public static TrackingModel Instance = new TrackingModel();

    private uint layoutNodes = 0;

    private uint layoutVirtualNodes = 0;

    private uint printedLayoutNodes = 0;
    private uint printedLayoutVirtualNodes = 0;

    private IDispatcherTimer timer;

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
    }

    public event PropertyChangedEventHandler? PropertyChanged;

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
}
