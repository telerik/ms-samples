using System.Collections.ObjectModel;

namespace CollectionViewThrowsAnException;

public partial class MainPage : ContentPage
{
	private ObservableCollection<string> items = new();
	
	public MainPage()
	{
		InitializeComponent();

		this.items.Add("item: " + this.items.Count);
		this.cv1.ItemsSource = this.items;
	}

	private void ButtonAdd_Clicked(object sender, System.EventArgs e)
	{
		double collectionViewHeight = this.cv1.DesiredSize.Height;

		this.items.Add("item: " + this.items.Count);
	}
}