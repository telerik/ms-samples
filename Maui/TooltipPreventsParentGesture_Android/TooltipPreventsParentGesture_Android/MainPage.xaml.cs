using System.Windows.Input;

namespace TooltipPreventsParentGesture_Android
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
            this.BindingContext = this;
        }

        public ICommand CountCommand => new Command(
        () =>
        {
            count++;

            if (count == 1)
            {
                this.logLabel.Text = $"Beige Layout Tapped: {count} time.";
            }
            else
            {
                this.logLabel.Text = $"Beige Layout Tapped: {count} times.";
            }
        });
    }
}
