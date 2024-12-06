**Description of the issue**
Maui: In WinUI .Net9, the ContentView is not getting properly invalidated when invoking the InvalidateMeasure() method.

**Steps to reproduce:**
1. Run the project.
2. Place a breakpoint in the MeasureOverride() method of the CustomView class
3. Click the button.

**Expected Behavior**
The breakpoint should get hit.
If debugging isn't available to you, it is expected that in the Debug Output a message is displayed saying that the CustomView was measured and arranged.
Expected debug output:
::: CustomView: will MeasureOverride
::: MyGrid: will MeasureOverride
::: CustomView: will ArrangeOverride
::: MyGrid: will ArrangeOverride

**Actual Behavior**
The CustomView is not getting invalidated, i.e. the breakpoint is not getting hit.
There is no information in the Debug Output that the MeasureOverride() and ArrangeOverride() of the CustomView methods were called.
Actual debug output:
::: MyGrid: will MeasureOverride
::: MyGrid: will ArrangeOverride

**Rationale**
If one puts custom logic in the MeasureOverride() or ArrangeOverride() methods, then this custom logic will not get executed when the CustomView is invalidated, which obstructs many custom scenarios.

**Notes**
This works fine on Android and iOS, i.e. the CustomView is getting measured and arranged. Issue is observed only in WinUI.

**Link to issue**
TBD
