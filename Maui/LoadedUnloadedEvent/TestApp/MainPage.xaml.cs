namespace TestApp;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        this.InitializeComponent();
    }

    private void OnFirstClicked(object sender, EventArgs eventArgs)
    {
        this.contentView.ControlTemplate = (ControlTemplate)this.Resources["firstTemplate"];
    }

    private void OnSecondClicked(object sender, EventArgs eventArgs)
    {
        this.contentView.ControlTemplate = (ControlTemplate)this.Resources["secondTemplate"];
    }

    private void OnClearClicked(object sender, EventArgs eventArgs)
    {
        this.outputLog.Text = null;
    }

    private void OnContentLoaded(object sender, EventArgs eventArgs)
    {
        this.outputLog.Text += "Content Loaded\n";
    }

    private void OnContentUnloaded(object sender, EventArgs eventArgs)
    {
        this.outputLog.Text += "Content Unloaded\n";
    }
}
