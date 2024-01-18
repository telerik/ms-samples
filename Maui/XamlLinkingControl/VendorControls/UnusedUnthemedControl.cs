namespace VendorControls;

public class UnusedUnthemedControl : ContentView
{
	public UnusedUnthemedControl()
	{
		Content = new VerticalStackLayout
		{
			Children = {
				new Label { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, Text = "Welcome to .NET MAUI!"
				}
			}
		};
	}
}