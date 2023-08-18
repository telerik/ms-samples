**Description of the issue**
Maui: TwoWay Binding does not work when value is changed from the source ViewModel

**Steps to reproduce:**
1. Run the project.
2. Click the Button that sets the Text of the Entry to empty in the ViewModel.
3. Notice how the text in the Entry itself is not changed.

**Expected Behavior**
The Entry should update its UI when the text is set to empty from the ViewModel.

**Actual Behavior**
The Entry doesn't change its Text.

**Link to issue**
https://github.com/dotnet/maui/issues/16849
