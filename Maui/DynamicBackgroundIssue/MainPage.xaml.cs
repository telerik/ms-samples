namespace DynamicBackgroundIssue;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{
        this.Resources.MergedDictionaries.Add(new MyColors());
	}
}

