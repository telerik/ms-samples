
using System.ComponentModel;

namespace VirtualizingRecyclingScrollView;

public class CellModel : INotifyPropertyChanged
{
    private static PropertyChangedEventArgs changedAll = new PropertyChangedEventArgs(null);

    public Key key;

    private string text;

    private Color color;

    public event PropertyChangedEventHandler? PropertyChanged;

    public string Text
    {
        get => this.text;
        set
        {
            this.text = value;
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Text)));
        }
    }

    public Color Color
    {
        get => this.color;
        set
        {
            this.color = value;
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Color)));
        }
    }

    internal void Update(string text, Color color)
    {
        this.text = text;
        this.color = color;
        this.PropertyChanged?.Invoke(this, changedAll);
    }
}