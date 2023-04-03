**Description of the issue**
Maui: [MacCatalyst] [iOS] Label goes out of the bounds of its parent when it has a bigger desired width than the parent constraints

**Steps to reproduce:**
1. Run the project.
2. The parent of the label has Width 150 smaller than the desired width of the Label.

**Expected Behavior**
The Label should be wrapped as the width of the parent is smaller than the desired width.

**Actual Behavior**
The Label does not wrap.

**Link to issue**
https://github.com/dotnet/maui/issues/14368
