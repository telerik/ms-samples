namespace ContentPresenterConverterIssue;

public class MyTemplatedView : TemplatedView
{
    private readonly static Dictionary1 resources;
    /// <summary>
    /// Identifies the <see cref="Content"/> property.
    /// </summary>
    public static readonly BindableProperty ContentProperty =
        BindableProperty.Create(nameof(Content), typeof(object), typeof(MyTemplatedView), null, propertyChanged: OnContentPropertyChanged);

    /// <summary>
    /// Identifies the <see cref="MyView"/> property.
    /// </summary>
    public static readonly BindableProperty MyViewProperty =
        BindableProperty.Create(nameof(MyView), typeof(View), typeof(MyTemplatedView), null);

    static MyTemplatedView()
    {
        resources = new Dictionary1();
    }

    public MyTemplatedView()
    {
        this.ControlTemplate = (ControlTemplate)resources["MyTemplatedView_ControlTemplate"];
    }

    public object Content
    {
        get => this.GetValue(ContentProperty);
        set => this.SetValue(ContentProperty, value);
    }

    public View MyView
    {
        get => (View)this.GetValue(MyViewProperty);
        set => this.SetValue(MyViewProperty, value);
    }

    private static void OnContentPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var myTemplView = (MyTemplatedView)bindable;

        if (newValue is string newString && oldValue is not string)
        {
            var myLabel = new Label();
            myLabel.SetBinding(Label.TextProperty, new Binding() { Source = myTemplView, Path = nameof(myTemplView.Content) });
            myLabel.SetBinding(Label.BindingContextProperty, new Binding() { Source = myTemplView, Path = nameof(myTemplView.BindingContext) });
            myLabel.HorizontalTextAlignment = TextAlignment.Center;
            myLabel.FontSize = 20;
            myLabel.TextColor = Colors.Blue;
            myLabel.FontAttributes = FontAttributes.Bold;

            myTemplView.MyView = myLabel;
        }
        else if (newValue is View view)
        {
            myTemplView.MyView = view;
        }
    }
}