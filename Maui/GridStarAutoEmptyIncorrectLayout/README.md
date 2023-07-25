**Description of the issue**
Maui: When using a `Grid` with a `*` and `Auto` columns and the `Auto` column has no content, the `Grid` is rendered with an incorrect layout.

**Steps to reproduce:**
1. Run the project

**Expected Behavior**
Both entries should be visible below the labels.

**Actual Behavior**
The entries are not visible unless the window is resized or the phone/tabled is rotated.

**Link to issue**
https://github.com/dotnet/maui/issues/16334