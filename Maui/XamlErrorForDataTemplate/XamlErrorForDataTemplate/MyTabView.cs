using System.Collections;

namespace XamlErrorForDataTemplate;

public class MyTabView : View
{
    public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create("ItemsSource", typeof(IEnumerable), typeof(MyTabView));
    public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create("ItemTemplate", typeof(DataTemplate), typeof(MyTabView));

    public IEnumerable ItemsSource { get { return (IEnumerable)this.GetValue(ItemsSourceProperty); } set { this.SetValue(ItemsSourceProperty, value); } }
    public DataTemplate ItemTemplate { get { return (DataTemplate)this.GetValue(ItemTemplateProperty); } set { this.SetValue(ItemTemplateProperty, value); } }
}
