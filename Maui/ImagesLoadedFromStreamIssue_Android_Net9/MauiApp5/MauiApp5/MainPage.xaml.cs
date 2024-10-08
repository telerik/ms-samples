using System.Collections.ObjectModel;
using System.Reflection;

namespace MauiApp5;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        this.ImageSources = new ObservableCollection<ImageSource>();

        this.InitializeComponent();
        this.InitImages();

        this.BindingContext = this;
    }

    public ObservableCollection<ImageSource> ImageSources { get; set; }

    private void InitImages()
    {
        var assembly = typeof(MainPage).GetTypeInfo().Assembly;
        var resourceNames = assembly.GetManifestResourceNames();

        foreach (var resourceName in resourceNames)
        {
            if (resourceName.Contains("sample") && resourceName.EndsWith("jpg"))
            {
                this.ImageSources.Add(ImageSource.FromStream(() => assembly.GetManifestResourceStream(resourceName)));
            }
        }
    }
}
