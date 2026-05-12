namespace SelectedStateIssue;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        UpdateStatus();
    }

    private void OnSelectClicked(object sender, EventArgs e)
    {
        TheBox.IsSelected = true;
        UpdateStatus();
    }

    private void OnDeselectClicked(object sender, EventArgs e)
    {
        TheBox.IsSelected = false;
        UpdateStatus();
    }

    private void UpdateStatus()
    {
        StatusLabel.Text = $"IsSelected = {TheBox.IsSelected}";
    }
}
