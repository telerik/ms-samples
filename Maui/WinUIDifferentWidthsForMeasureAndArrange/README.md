**Description of the issue**
Maui: WinUI reports different widths for Measure and Arrange. 
We are using a custom Layout, and a custom ILayoutManager. During the lifecycle of the layout - we get different widths for Measure and Arrange.
For example:

::: Measure widthConstraint: 1201.77783203125
::: ArrangeChildren bounds.Width: 1203.5555419921875

::: Measure widthConstraint: 1089.77783203125
::: ArrangeChildren bounds.Width: 1091.5555419921875

This causes layout issues in our applications, namely in the RadWrapLayout.

**Steps to reproduce:**
1. Run the sample application.
2. Check the output window for the printed widths or debug the CustomLayoutLayoutManager.

**Expected Behavior**
We expect to get the same width for Measure and Arrange.

**Actual Behavior**
The widths are different.

**Link to issue**
https://github.com/dotnet/maui/issues/31293
