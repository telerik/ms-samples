namespace GridRowsShortSyntax
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            this.grid.RowDefinitions[2].Height = new GridLength(this.grid.RowDefinitions[2].Height.Value + 100, GridUnitType.Absolute);
        }
    }
}
