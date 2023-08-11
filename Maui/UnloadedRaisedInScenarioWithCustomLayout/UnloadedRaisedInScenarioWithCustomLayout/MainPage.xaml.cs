namespace UnloadedRaisedInScenarioWithCustomLayout;

using Microsoft.Maui.Controls;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

	private void Button_Clicked(object sender, EventArgs e)
	{
		this.Navigation.PushAsync(new ContentPage());
	}
}
