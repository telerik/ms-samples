using System.Collections.ObjectModel;
using Microsoft.Maui.Layouts;

namespace CollectionViewItemsSourceNet10Issue;

public partial class MainPage : ContentPage
{
    private List<string> allCountries = new();
    public MainPage()
    {
        this.InitializeComponent();
        this.LoadCountries();
        // this.ItemsCollectionView.ItemsSource = this.allCountries;
    }

    private void LoadCountries()
    {
        this.allCountries = new List<string>
        {
            "United States",
            "Canada",
            "United Kingdom",
            "Germany",
            "France",
            "Italy",
            "Spain",
            "Japan",
            "Australia",
            "Brazil",
            "India",
            "China",
            "Russia",
            "Mexico",
            "Argentina",
            "South Africa",
            "Egypt",
            "Turkey",
            "Netherlands",
            "Sweden"
        };
    }

    private void OnSearchTextChanged(object? sender, TextChangedEventArgs e)
    {
        var searchText = e.NewTextValue?.Trim();
        if (string.IsNullOrWhiteSpace(searchText))
        {
            // Show all countries when search is empty
            this.ItemsCollectionView.ItemsSource = this.allCountries;
        }
        else
        {
            // Filter countries that start with the search text (case insensitive)
            var filteredCountries = this.allCountries.OfType<string>()
                .Where(country => country.StartsWith(searchText, StringComparison.OrdinalIgnoreCase));

            this.ItemsCollectionView.ItemsSource = filteredCountries;
        }

    }
}

public class CustomLayout : Layout
{
    protected override ILayoutManager CreateLayoutManager()
    {
        return new CustomLayoutManager(this);
    }
}

public class CustomLayoutManager : ILayoutManager
{
    private readonly CustomLayout layout;

    public CustomLayoutManager(CustomLayout layout)
    {
        this.layout = layout;
    }

    public Size ArrangeChildren(Rect bounds)
    {
        double currentY = bounds.Y;
        
        foreach (var child in this.layout.Children)
        {
            if (child.Visibility == Visibility.Collapsed)
            {
                continue;
            }

            var childBounds = new Rect(bounds.X, currentY, bounds.Width, child.DesiredSize.Height);
            child.Arrange(childBounds);
            currentY += child.DesiredSize.Height;
        }

        return bounds.Size;
    }

    public Size Measure(double widthConstraint, double heightConstraint)
    {
        double totalHeight = 0;
        double maxWidth = 0;
        double remainingHeight = heightConstraint;

        foreach (var child in this.layout.Children)
        {
            if (child.Visibility == Visibility.Collapsed)
            {
                continue;
            }

            var childSize = child.Measure(widthConstraint, remainingHeight);
            totalHeight += childSize.Height;
            maxWidth = Math.Max(maxWidth, childSize.Width);
            
            // Reduce remaining height for next child
            if (!double.IsPositiveInfinity(remainingHeight))
            {
                remainingHeight = Math.Max(0, remainingHeight - childSize.Height);
            }
        }

        var finalWidth = double.IsPositiveInfinity(widthConstraint) ? maxWidth : widthConstraint;
        var finalHeight = double.IsPositiveInfinity(heightConstraint) ? totalHeight : Math.Min(totalHeight, heightConstraint);

        return new Size(finalWidth, finalHeight);
    }
}