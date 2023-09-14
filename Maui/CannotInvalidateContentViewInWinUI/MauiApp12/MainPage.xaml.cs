namespace MauiApp12;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        this.InitializeComponent();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        this.customContentView1.Max++;
    }
}

