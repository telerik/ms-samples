namespace TestApp;

public class TextWebViewSource : WebViewSource
{
    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(nameof(Text), typeof(string), typeof(TextWebViewSource));

    public TextWebViewSource()
    {
    }

    public TextWebViewSource(string text)
    {
        this.Text = text;
    }

    public string Text
    {
        get => (string)this.GetValue(TextProperty);
        set => this.SetValue(TextProperty, value);
    }

    public override void Load(IWebViewDelegate webViewDelegate)
    {
        webViewDelegate.LoadHtml(this.Text, null);
    }
}
