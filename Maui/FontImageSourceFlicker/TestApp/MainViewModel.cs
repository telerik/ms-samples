using System;

namespace TestApp;

public class MainViewModel : ViewModelBase
{
    private readonly IList<string> colors;
    private string selectedColor;

    public MainViewModel()
    {
        this.colors = new[]
        {
            "Black",
            "Red",
            "Green",
            "Blue",
            "Cyan",
            "Magenta",
            "Yellow",
            "Gray"
        };

        this.selectedColor = "Black";
    }

    public IList<string> Colors => this.colors;

    public string SelectedColor
    {
        get => this.selectedColor;
        set
        {
            if (this.selectedColor != value)
            {
                this.selectedColor = value;
                this.OnPropertyChanged();
            }
        }
    }
}

