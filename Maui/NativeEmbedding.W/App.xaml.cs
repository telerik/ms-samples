using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui;
using Microsoft.Maui.Embedding;
using Microsoft.Maui.Hosting;
using Microsoft.Maui.Platform;
using Microsoft.UI.Xaml;

namespace NativeEmbedding;

public partial class App : Application
{
    private static MauiContext _mauiContext;

    internal Window m_window;

    public App()
    {
        this.InitializeComponent();
    }

    public static MauiContext MauiContext { get { return _mauiContext; } }

    public static void InitMaui(Window window)
    {
        MauiAppBuilder builder = MauiApp.CreateBuilder();
        builder.UseMauiEmbedding<Microsoft.Maui.Controls.Application>();
        builder.ConfigureMauiHandlers(handlers => { handlers.AddHandler(typeof(CustomWindow), typeof(CustomWindowHandler)); });
        //builder.Services.AddSingleton(new NavigationRootManager(window));
        MauiApp mauiApp = builder.Build();
        _mauiContext = new MauiContext(mauiApp.Services);
    }

    protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
    {
        m_window = new MainWindow();
        m_window.Activate();

        InitMaui(m_window);
    }
}
