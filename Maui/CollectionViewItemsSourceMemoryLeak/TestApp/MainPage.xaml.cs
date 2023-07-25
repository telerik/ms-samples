namespace TestApp;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        this.InitializeComponent();
        this.BindingContext = new MainViewModel(1, 100);
    }

    private void OnRecreateClicked(object sender, EventArgs eventArgs)
    {
        var controlTemplate = this.contentView.ControlTemplate;

        this.contentView.ControlTemplate = null;
        this.contentView.ControlTemplate = controlTemplate;
    }
}
