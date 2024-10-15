**Description of the issue**
This project demonstrates an issue in a .NET MAUI application where images loaded from embedded resources using streams (`ImageSource.FromStream`) are not displayed when the app is run in **Release mode** on Android using **.NET 9.0.100-rc.1.24452.12** version. In **Debug mode** the images are displayed as expected.


**Code explanation**
The images are loaded by retrieving the names of the embedded resources from the assembly, and then using a stream to read them. Here is the relevant method that loads the images:

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

The method fetches the image resources and loads them as streams, converting them into `ImageSource` objects, which are then added to `ImageSources` collection.

**MainPage.xaml**
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="MauiApp5.MainPage">
    
    <CollectionView ItemsSource="{Binding ImageSources}">
        <CollectionView.ItemTemplate>
            <DataTemplate>
                <Image Source="{Binding}" HeightRequest="100" WidthRequest="100" />
            </DataTemplate>
        </CollectionView.ItemTemplate>
    </CollectionView>
    
</ContentPage>

A CollectionView is responsible for displaying the images, which are dynamically loaded from the `ImageSources` collection.


**Steps to reproduce** 
1.	Run the sample application in **Release mode** on Android device or simulator.
2.	Observe the `MainPage`.

**Expected:** Four images should be displayed in the `MainPage`.
**Actual:** `The MainPage` is blank.

**Environment Details**
- .NET version: 9.0.100-rc.1.24452.12
- Target platform: Android (tested on physical device and Pixel 7 - API 35 simulator).
- Development IDE: Visual Studio 2022 Preview.
