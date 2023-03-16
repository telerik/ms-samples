namespace HiddenBindableProperty;

public class CustomCheckBox : CheckBox
{
    /// <summary>
    /// Identifies the <see cref="IsChecked"/> property. 
    /// </summary>
    public static new readonly BindableProperty IsCheckedProperty =
        BindableProperty.Create(nameof(IsChecked), typeof(bool?), typeof(CustomCheckBox), false, BindingMode.TwoWay);

    /// <summary>
    /// Gets or sets a <see cref="IsChecked"/>.
    /// The default value is false.
    /// </summary>
    public new bool? IsChecked
    {
        get { return (bool?)this.GetValue(IsCheckedProperty); }
        set { this.SetValue(IsCheckedProperty, value); }
    }
}