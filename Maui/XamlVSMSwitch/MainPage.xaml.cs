namespace XamlVSMSwitch;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

    private void SetStyle1(object sender, EventArgs e)
    {
		if (this.Resources.TryGetValue("Style1", out var res) && res is Style style)
		{
			this.Styleable.Style = style;
		}
    }

    private void SetStyle2(object sender, EventArgs e)
    {
		if (this.Resources.TryGetValue("Style2", out var res) && res is Style style)
		{
			this.Styleable.Style = style;
		}
    }

	private void SetClearStyle(object sender, EventArgs e)
    {
		this.Styleable.Style = null;
		this.Resources.Remove(typeof(Button).FullName);
    }

	private void SetStyle1Implicit(object sender, EventArgs e)
    {
		this.Resources.Remove(typeof(Button).FullName);
		if (this.Resources.TryGetValue("Style1", out var res) && res is Style style)
		{
			this.Resources.Add(typeof(Button).FullName, style);
		}
    }

	private void SetStyle2Implicit(object sender, EventArgs e)
    {
		this.Resources.Remove(typeof(Button).FullName);
		if (this.Resources.TryGetValue("Style2", out var res) && res is Style style)
		{
			this.Resources.Add(typeof(Button).FullName, style);
		}
    }
}

