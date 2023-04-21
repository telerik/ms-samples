namespace MeasureIssueApp;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
        this.border.HeightRequest += 50;
    }
}

