**Description of the issue**
Consider the following custom control which hides a BindableProperty:

```
public class CustomCheckBox : CheckBox
{
    public static new readonly BindableProperty IsCheckedProperty =
        BindableProperty.Create(nameof(IsChecked), typeof(bool?), typeof(CustomCheckBox), false, BindingMode.TwoWay);

    public new bool? IsChecked
    {
        get { return (bool?)this.GetValue(IsCheckedProperty); }
        set { this.SetValue(IsCheckedProperty, value); }
    }
}
```

Notice that the type of the new property (bool?) differs from that of the default property (bool).

When trying to set the new property in XAML, an AmbiguousMatchException is thrown with the following message:
"Multiple properties with name 'CustomCheckBox.IsChecked' found."

Setting the property in code-behind works as expected.

The exception seems to stem from the TrySetValue method of the ApplyPropertiesVisitor which uses the GetRuntimeProperty method. Actually, calling the method in the following manner is also sufficient to reproduce the exception:

```
this.checkBox.GetType().GetRuntimeProperty("IsChecked");
```

Possible fix would be to use the GetRuntimeProperties method instead and then check the DeclaringType property to retrieve only a single property.

```
var properties = this.checkBox.GetType().GetRuntimeProperties();
var property = properties.FirstOrDefault(x => x.Name == "IsChecked" && x.DeclaringType == this.checkBox.GetType());
```

Note: Removing the "new" keyword does not resolve the exception.

**Steps to reproduce:**
1. Run the app on any platform.

**Expected Behavior**
The project runs successfully and the CheckBox is displayed.

**Actual Behavior**
The application crashes with an AmbiguousMatchException.

**Link to issue**
https://github.com/dotnet/maui/issues/13962
