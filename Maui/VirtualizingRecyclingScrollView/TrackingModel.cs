using System.ComponentModel;

namespace VirtualizingRecyclingScrollView;

public sealed class TrackingModel : INotifyPropertyChanged
{
    public static TrackingModel Instance = new TrackingModel();

    private TrackingModel()
    {
    }

    private uint layoutNodes = 0;

    private uint layoutVirtualNodes = 0;

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
