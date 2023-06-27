namespace InconsistentInputTransparency;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
		this.DisplayAlert("Button Clicked", "The Button was clicked!", "OK");
    }
}

