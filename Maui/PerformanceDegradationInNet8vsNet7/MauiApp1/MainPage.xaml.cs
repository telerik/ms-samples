namespace MauiApp1;

public partial class MainPage : ContentPage
{
    private static readonly List<Color> colors = new List<Color> { Colors.Gray, Colors.Yellow, Colors.HotPink, Colors.Black, Colors.DarkBlue, Colors.DarkGoldenrod, };
    private static int generatedColors;

    public MainPage()
    {
        this.InitializeComponent();

        this.ReloadItems();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        this.ReloadItems();
    }

    private static Color GetColor()
    {
        generatedColors++;

        int half  = colors.Count / 2;
        int index = generatedColors % half;

        if ((int)(generatedColors / 400) % 2 == 1)
        {
            index += half;
        }

        return colors[index];
    }

    private void ReloadItems()
    {
        this.innerGrid.Children.Clear();

        for (int r = 0; r < this.innerGrid.RowDefinitions.Count; r++)
        {
            for (int c = 0; c < this.innerGrid.ColumnDefinitions.Count; c++)
            {
                View view = new Border();
                view.BackgroundColor = GetColor();
                Grid.SetRow(view, r);
                Grid.SetColumn(view, c);
                this.innerGrid.Children.Add(view);
            }
        }
    }
}

