namespace BrushesMemoryLeak;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

	private void OnClicked(object sender, EventArgs e)
	{
		this.Navigation.PushAsync(new GrumpyPage());
	}
}

