**Description of the issue**
Maui: Setters are not unapplied when using custom VisualState on a Button on Android, iOS and MacCatalyst. 

**Steps to reproduce:**
1. Create a custom Button with custom VisualState.
2. Override the ChangeVisualState method and go to the Custom state when clicking on the Button every other time, otherwise call base.ChangeVisualState().
3. Add VisualStateGroups for the Button.
4. Click on the Button - go to Custom VS.
5. Click on the Button again - go to Normal VS.

**Expected Behavior**
When the Button is clicked the second time it sould go to Normal state.

**Actual Behavior**
When the Button is clicked the second time it stays in Custom state.

**Link to issue**

