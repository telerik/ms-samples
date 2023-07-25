**Description of the issue**
Maui: Grid layout calls Measure on its children way too many times.

**Steps to reproduce:**
1. Start the sample application from the provided repository.
2. Observe the debug output generated from the application.

**Expected Behavior**
The MeasureOverride method should be called some reasonable number of times.

**Actual Behavior**
The MeasureOverride method is called way too many times.

**Link to issue**
https://github.com/dotnet/maui/issues/11140.
