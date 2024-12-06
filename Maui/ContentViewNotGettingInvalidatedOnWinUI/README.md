**Description of the issue**
Maui: In WinUI .Net9, the ContentView is not getting properly invalidated when invoking the InvalidateMeasure() method.

**Steps to reproduce:**
1. Run the app.
2. Place a breakpoint in CustomView in MeasureOverride()
2. Click the button.

**Expected Behavior**
The breakpoint should get hit. If debugging isn't available to you, it is expected that in the Debug Output a message is displayed saying that the ContentView was measured and arranged.

**Actual Behavior**
The ContentView is not getting invalidated, i.e. the breakpoint is not getting hit, and there is no information in the Debug Output that the MeasureOverride() and ArrangeOverride() methods were called.

**Rationale**
If one puts custom logic in the MeasureOverride() or ArrangeOverride() methods, that this custom logic will not get executed when the ContentView is invalidated, which obstructs many custom scenarios.

**Link to issue**
TBD
