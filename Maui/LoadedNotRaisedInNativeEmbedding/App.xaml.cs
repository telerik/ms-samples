using Microsoft.Maui;
using Microsoft.Maui.Embedding;
using Microsoft.Maui.Hosting;
using Microsoft.UI.Xaml;

namespace NativeEmbedding;

public partial class App : Application
{
    private static MauiContext _mauiContext;

    private Window m_window;

    public App()
    {
        this.InitializeComponent();
    }

    public static MauiContext MauiContext { get { return _mauiContext; } }

    public static void InitMaui()
    {
        MauiAppBuilder builder = MauiApp.CreateBuilder();
        builder.UseMauiEmbedding<Microsoft.Maui.Controls.Application>();
        MauiApp mauiApp = builder.Build();
        _mauiContext = new MauiContext(mauiApp.Services);
    }

    protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
    {
        InitMaui();

        m_window = new MainWindow();
        m_window.Activate();
    }
}
