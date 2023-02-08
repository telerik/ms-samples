namespace CollectionViewDefaultPointerOver_WinUI;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
        var items = new[] { "Item 1", "Item 2", "Item 3" };
        this.colView.ItemsSource = items;
    }
}

