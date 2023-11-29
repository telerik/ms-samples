using Microsoft.Maui.Platform;
using Microsoft.UI.Xaml;

namespace NativeEmbedding;

public sealed partial class MainWindow : Window
{
    public MainWindow()
    {
        this.InitializeComponent();
    }

    private void myButton_Click(object sender, RoutedEventArgs e)
    {
        // prepare the maui view as usual
        var mauiView = new Microsoft.Maui.Controls.Button { Text = "maui button here" };
        var tap = new Microsoft.Maui.Controls.TapGestureRecognizer();
        tap.Tapped += (s, e) => { mauiView.Text = "maui button was clicked"; };
        mauiView.GestureRecognizers.Add(tap);

        // work-around things like tap & loaded/unloaded not working
        var window = new CustomWindow();
        window.ToHandler(App.MauiContext);
        window.AddLogicalChild(mauiView);

        // use ToPlatform() as usual
        this.Content = mauiView.ToPlatform(App.MauiContext);
    }
}
