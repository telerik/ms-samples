namespace ContentViewNewContentIssue;

public class MyContentView : ContentView
{
    /// <summary>
    /// Identifies the <see cref="Content"/> property.
    /// </summary>
    public new static readonly BindableProperty ContentProperty =
        BindableProperty.Create(nameof(Content), typeof(object), typeof(MyContentView), null);

    public new object Content
    {
        get => this.GetValue(ContentProperty);
        set => this.SetValue(ContentProperty, value);
    }
}
