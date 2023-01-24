using SkiaSharp.Views.Maui;
using SkiaSharp.Views.Maui.Controls;

namespace SKCanvasViewTouchCaptured_MacCatalyst;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);
	}

    void SKCanvasView_Touch(System.Object sender, SkiaSharp.Views.Maui.SKTouchEventArgs e)
    {
        e.Handled = true;
    }
}

