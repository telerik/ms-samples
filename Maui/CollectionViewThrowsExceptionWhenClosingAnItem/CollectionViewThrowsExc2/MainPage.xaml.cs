using System.Collections.ObjectModel;

namespace CollectionViewThrowsExc2;

public partial class MainPage : ContentPage
{
	private ObservableCollection<string> items = new();

	public MainPage()
	{
		InitializeComponent();

		this.items.Add("item " + this.items.Count);
		this.cv1.ItemsSource = this.items;
	}

	private void ButtonClose_Clicked(object sender, EventArgs e)
	{
		Button button = (Button)sender;
		string item = (string)button.BindingContext;
		this.items.Remove(item);
	}
}

