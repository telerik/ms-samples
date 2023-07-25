namespace TestApp;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        this.InitializeComponent();
        this.InitializeEditors();
        this.InitializeWebView();
    }

    private void InitializeEditors()
    {
        this.htmlEditor.Text = """
            <!DOCTYPE html>
            <html>
                <head>
                </head>
                <body>
                    <script>
                        function test() {
                            return "Test";
                        }
                    </script>
                    <p>
                        Hello, World!!!
                    </p>
                </body>
            </html>
            """;

        this.scriptEditor.Text = "test();";
    }

    private void InitializeWebView()
    {
        this.webView.Source = new TextWebViewSource(this.htmlEditor.Text);
    }

    private void OnLoadClicked(object sender, EventArgs eventArgs)
    {
        this.webView.Source = new TextWebViewSource(this.htmlEditor.Text);
    }

    private async void OnEvaluateClicked(object sender, EventArgs eventArgs)
    {
        this.resultEditor.Text = await this.webView.EvaluateJavaScriptAsync(this.scriptEditor.Text);
    }
}
