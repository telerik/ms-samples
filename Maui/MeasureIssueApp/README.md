**Description of the issue**

MeasureInvalidated event of a Parent is not being invoked when its child's MeasureInvalidated event is invoked

**Steps to reproduce**

1. Run the app
2. After the MainPage has loaded, add breakpoints to the methods defined in MyVerticalStackLayout.cs
3. Click the 'Expand' Button in the MainPage

**Expected Result**

We hit the breakpoint in NotifyMeasureInvalidated since the MeasureInvalidated event is invoked

**Actual Result**

We don't hit any breakpoints 

**Link to issue**

// TBA