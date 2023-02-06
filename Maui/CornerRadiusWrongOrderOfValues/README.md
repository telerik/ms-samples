**Description of the issue**
Maui: The values of the CornerRadius are in an incorrect order. The expected order is TopLeft, TopRight, BottomRight, BottomLeft, the actual order is TopLeft, TopRight, Bottom**Left**, Bottom**Right**.

**Steps to reproduce:**
1. Run the app on any platform.

**Expected Behavior**
The BoxView should have rounder corners on the right side.

**Actual Behavior**
The BoxView has the bottom left corner rounded instead of the bottom right.

**Link to issue**
https://github.com/dotnet/maui/issues/13136
