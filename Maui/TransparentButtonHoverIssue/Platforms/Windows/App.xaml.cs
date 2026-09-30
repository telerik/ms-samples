using Microsoft.Maui;
using Microsoft.UI.Xaml;

namespace TransparentHoverRepro.WinUI;

public partial class App : MauiWinUIApplication
{
    public App()
    {
        InitializeComponent();
    }

    protected override MauiApp CreateMauiApp() => global::TransparentHoverRepro.MauiProgram.CreateMauiApp();
}
