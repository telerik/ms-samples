using System.Reflection;

namespace HiddenBindableProperty;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();

		// Issue can also be reproduced by invoking the following method
		// this.checkBox.GetType().GetRuntimeProperty("IsChecked");
	}
}
