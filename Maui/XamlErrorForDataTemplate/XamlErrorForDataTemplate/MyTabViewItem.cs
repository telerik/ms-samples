namespace XamlErrorForDataTemplate;

public class MyTabViewItem : BindableObject
{
    public static readonly BindableProperty HeaderProperty = BindableProperty.Create("Header", typeof(View), typeof(MyTabViewItem));
    public static readonly BindableProperty ContentProperty = BindableProperty.Create("Content", typeof(View), typeof(MyTabViewItem));

    public View Header { get { return (View)this.GetValue(HeaderProperty); } set { this.SetValue(HeaderProperty, value); } }
    public View Content { get { return (View)this.GetValue(ContentProperty); } set { this.SetValue(ContentProperty, value); } }
}
