namespace VirtualizingRecyclingScrollView;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		this.BindingContext = TrackingModel.Instance;
		InitializeComponent();

		this.Dispatcher.DispatchDelayed(TimeSpan.FromSeconds(1), () => {
			this.SV.ScrollToAsync(40, 0, false);
			this.Dispatcher.DispatchDelayed(TimeSpan.FromSeconds(1), () => {
				TrackingModel.Instance.Clear();
				this.Dispatcher.DispatchDelayed(TimeSpan.FromSeconds(3), async () => {
					MauiProgram.SuppressWorkarounds = true;
					await this.SV.ScrollToAsync(40, 800, true);
					MauiProgram.SuppressWorkarounds = false;
				});
			});
		});
	}
}
