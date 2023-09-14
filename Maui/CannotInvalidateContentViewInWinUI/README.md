**Description of the issue**
Maui: [WinUI] Cannot invalidate measure and arrange for a ContentView

**Steps to reproduce:**
1. Run the app in WinUI.
2. Clear the Output window.
3. Click the "change max" button.

**Expected Behavior**
The measure and arrange of the custom content view should be invalidated and the MeasureOverride and ArrangeOverride methods should get invoked, so we should see information about this in the Output window.

**Actual Behavior**
Nothing happens, MeasureOverride and ArrangeOverride are not invoked. The only thing in the output is the notification for the Max property change:
"::: CustomContentView OnMaxChanged"

**Link to issue**
soon
