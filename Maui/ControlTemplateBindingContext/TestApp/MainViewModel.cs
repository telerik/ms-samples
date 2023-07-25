namespace TestApp;

public class MainViewModel : ViewModelBase
{
    private string text;

    public MainViewModel()
    {
        this.text = "Hello World !";
    }

    public string Text
    {
        get => this.text;
        set
        {
            if (this.text != value)
            {
                this.text = value;
                this.OnPropertyChanged();
            }
        }
    }
}
