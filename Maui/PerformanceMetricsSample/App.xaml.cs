namespace PerformanceMetricsSample;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new NavigationPage(new StartupPage()));
    }
}

public class StartupPage : ContentPage
{
    public StartupPage()
    {
        var button = new Button
        {
            Text = "Load MainPage",
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            Command = new Command(() =>
            {
                this.Navigation.PushAsync(new MainPage());
            })
        };

        this.Content = button;
    }
}