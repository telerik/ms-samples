using System.Diagnostics;

namespace TestApp;

public class CustomComponent : ContentView
{
    public static readonly BindableProperty FirstColorProperty = BindableProperty.Create(nameof(FirstColor),
        typeof(Color), typeof(CustomComponent), propertyChanged: OnFirstColorChanged);

    public static readonly BindableProperty SecondColorProperty = BindableProperty.Create(nameof(SecondColor),
        typeof(Color), typeof(CustomComponent), propertyChanged: OnSecondColorChanged);

    public CustomComponent()
    {
        Debug.WriteLine("CustomComponent constructor is called.");
    }

    public Color FirstColor
    {
        get => (Color)this.GetValue(FirstColorProperty);
        set => this.SetValue(FirstColorProperty, value); 
    }

    public Color SecondColor
    {
        get => (Color)this.GetValue(SecondColorProperty);
        set => this.SetValue(SecondColorProperty, value);
    }

    private static void OnFirstColorChanged(BindableObject bindable, object oldValue, object newValue)
    {
        Debug.WriteLine("FirstColor property is set.");
    }

    private static void OnSecondColorChanged(BindableObject bindable, object oldValue, object newValue)
    {
        Debug.WriteLine("SecondColor property is set.");
    }
}
