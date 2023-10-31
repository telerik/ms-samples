namespace VSLIssue;

public partial class MainPage : ContentPage
{
	readonly string text="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
	string[] tokens;
	Random random;

	public MainPage()
	{
		InitializeComponent();
		tokens = text.Split(" ");
		random = new Random();
	}

	private void OnAddClicked(object sender, EventArgs e)
	{
		var token = this.GetRandomToken();
		this.Label1.Text = this.Label1.Text + " " + token;
		this.Label2.Text = this.Label2.Text + " " + token;
	}

	private string GetRandomToken()
	{
		var index = this.random.Next(0, tokens.Length - 1);
		var token = tokens[index];
		return token;
	}
}

