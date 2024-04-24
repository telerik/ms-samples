namespace ContentPresenterConverterIssue;

public partial class MainPage : ContentPage
{
    int count = 1;

    public MainPage()
    {
        InitializeComponent();
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        count++;

        this.myTemplatedView.Content = "Content " + count;
    }
}