namespace DynamicStyleChangeIssue;

public partial class MainPage : ContentPage
{
    private bool defaultStyleApplied = false;

    public MainPage()
    {
        InitializeComponent();
    }

    private void OnBtnClicked(object sender, EventArgs e)
    {
        if (this.defaultStyleApplied)
        {
            this.lb.Style = (Style)this.Resources["LabelStyle1"];
            this.btn.Style = (Style)this.Resources["ButtonStyle1"];
        }
        else
        {
            this.lb.Style = (Style)this.Resources["LabelStyle2"];
            this.btn.Style = (Style)this.Resources["ButtonStyle2"];
        }

        this.defaultStyleApplied = !this.defaultStyleApplied;
    }
}