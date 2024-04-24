**Description of the issue**
Maui: XamlParseException is thrown changing the return type of a BindableProperty using the new modifier and setting it in Style. 
On Net7 even setting the property directly on the View in Xaml (outside the style) also throws the same exception.

**Steps to reproduce:**
1. Run the app.

**Expected Behavior**
The application should run.

**Actual Behavior**
XamlParseException is thrown. Multiple properties with name 'ContentViewNewContentIssue.MyContentView.Content' found.'

**Link to issue**
https://github.com/dotnet/maui/issues/22024
