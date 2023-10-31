**Description of the issue**
Maui: VerticalStackLayout with WidthRequest set is not invalidated when children sizes change on iOS and MacCatalyst.

**Steps to reproduce:**
1. Run the app in iOS and MacCatalyst.
2. Observe that not the full text is displayed in the Label inside the VerticalStackLayout(left), while the full text is displayed in the Label inside the Grid (rigth).
3. Click either one of the "Add Text" Buttons.
4. Observe that the Label below the button inside the VerticalStackLayout does update correctly when new text is added, while the Label inside the Grid updates correctly.

**Expected Behavior**
The Label inside the VerticalStackLayout should be displayed correctly and when new Text is added the UI should update accordingly.

**Actual Behavior**
The Label inside the VerticalStackLayout is not displayed correctly and when new Text is added the UI is not updated.

**Link to issue**
