using Microsoft.Maui.Platform;
using Microsoft.UI.Xaml;

namespace NativeEmbedding;

public sealed partial class MainWindow : Window
{
    Microsoft.Maui.Controls.Grid mauiGridCache;

    public MainWindow()
    {
        this.InitializeComponent();

        Microsoft.Maui.Controls.Button mauiButt = new Microsoft.Maui.Controls.Button();
        mauiButt.Text = "MulPlaAppUI";
        mauiButt.Loaded += this.MauiButt_Loaded;

        Microsoft.Maui.Controls.Grid mauiGrid = new Microsoft.Maui.Controls.Grid();
        mauiGrid.BackgroundColor = Microsoft.Maui.Graphics.Colors.HotPink;
        mauiGrid.Loaded += this.MauiGrid_Loaded;
        mauiGrid.Children.Add(mauiButt);

        this.root.Children.Add(mauiGrid.ToPlatform(App.MauiContext));

        this.mauiGridCache = mauiGrid;
    }

    private void MauiButt_Loaded(object sender, System.EventArgs e)
    {
        ((Microsoft.Maui.Controls.VisualElement)sender).Opacity = 0.5;
    }

    private void MauiGrid_Loaded(object sender, System.EventArgs e)
    {
        ((Microsoft.Maui.Controls.VisualElement)sender).Opacity = 0.5;
    }

    private void myButton_Click(object sender, RoutedEventArgs e)
    {
        myButton.Content = "Clicked";
    }
}
