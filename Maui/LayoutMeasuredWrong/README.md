**Description of the issue**
There is a mismatch between WidthRequest/HeightRequest and Measure results for layouts and for simple views.

**Steps to reproduce:**

1. Run the app.
2. Observe the results from Measure in the Output Window.

**Expected Behavior**
WidthConstraint/HeightConstraint args in the Measure method of the LayoutManager will match WidthRequest/HeightRequest of the Layout and the result of Measure applied to a child of the Layout will match the WidthRequest/HeightRequest set to the child.

**Actual Behavior**
WinUI:

* WidthConstraint/HeightConstraint in Measure of the LayoutManager differ from WidthRequest/HeightRequest with a very small amount (sometimes up, sometimes down) probably due to rounding when converting to physical pixels and back to dip.
* Results from child.Measure also deviate from WidthRequest/HeightRequest with a small amount.

Android:

* WidthConstraint/HeightConstraint in Measure of the LayoutManager differ from WidthRequest/HeightRequest with a very small amount - with the tendency to be a bit higher every time.
* Results from child.Measure also deviate from WidthRequest/HeightRequest with a small amount - with the tendency to be a bit higher every time.

iOS \& MacCatalyst:

* WidthConstraint/HeightConstraint in Measure of the LayoutManager do not correspond to the requested width/height for the Layout like on the other two platforms, their values correspond to the screen size.
* Results from child.Measure are identical to WidthRequest/HeightRequest set to the child.

**Link to issue**
https://github.com/dotnet/maui/issues/31150

