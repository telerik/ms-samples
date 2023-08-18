using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace MauiBindingIssue;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();

        this.BindingContext = new ViewModel();
	}
}

public class ViewModel : INotifyPropertyChanged
{
    private string myText;

    public ViewModel()
    {
        this.MyText = "Hello World";
        this.ClickCommand = new Command(() => this.MyText = string.Empty);
    }

    public string MyText
    {
        get => myText;
        set
        {
            if (this.myText != value)
            {
                this.myText = value;
                this.OnPropertyChanged();
            }
        }
    }

    public ICommand ClickCommand { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}