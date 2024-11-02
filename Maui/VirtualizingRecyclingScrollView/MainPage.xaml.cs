namespace VirtualizingRecyclingScrollView;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		this.BindingContext = TrackingModel.Instance;
		InitializeComponent();
	}
}
