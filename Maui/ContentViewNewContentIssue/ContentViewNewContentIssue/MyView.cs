namespace ContentViewNewContentIssue;

public class MyView: View
{
    /// <summary>Bindable property for <see cref="Margin"/>.</summary>
    public new static readonly BindableProperty MarginProperty =
        BindableProperty.Create(nameof(Margin), typeof(double), typeof(MyView), double.NaN);

    public new double Margin
    {
        get => (double)this.GetValue(MarginProperty);
        set => this.SetValue(MarginProperty, value);
    }
}
