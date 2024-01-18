using System.Reflection;
using VendorControls;

namespace UserApp;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();

		// List what gets in
		Assembly appAssembly = typeof(App).Assembly;
		Console.WriteLine($"Assembly {appAssembly}");
		foreach(var type in appAssembly.GetTypes())
		{
			Console.WriteLine($" - {type}");
		}

		Assembly controlsAssembly = typeof(VendorControl).Assembly;
		Console.WriteLine($"Assembly {controlsAssembly}");
		foreach(var type in controlsAssembly.GetTypes())
		{
			Console.WriteLine($" - {type}");
		}
	}
}
