**Description of the issue**
Maui: Implicit styles in App.xaml are applied too early, before the class's constructor.

**Steps to reproduce:**
1. Run the sample project from the provided link and observe the debug output.

**Expected Behavior**
The setters should be called after the constructor is invoked.

**Actual Behavior**
The setter from the implicit style is called before the constructor is invoked.

**Link to issue**
https://github.com/dotnet/maui/issues/10162