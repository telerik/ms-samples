**Description of the issue**
Maui: Binding ContentPresenter.Content in xaml does not remove the internally set ContentPresenter.Content binding.
This results in a defaut Label being displayed (populated from the ContentConverter), instead of the bound View.

**Steps to reproduce:**
1. Run the provided sample project
2. Click the button to change the Content of a custom TemplatedView.

**Expected Behavior**
The MyView View should be displayed in the ContentPresenter.

**Actual Behavior**
Instead of the MyView being displayed, a Label is displayed.

**Link to issue**
https://github.com/dotnet/maui/issues/22027