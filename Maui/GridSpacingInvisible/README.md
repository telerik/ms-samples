**Description of the issue**
Maui: Grid layout applies ColumnSpacing and RowSpacing to invisible children.

**Steps to reproduce:**
1. Run the sample project from the provided repository.
2. Use the switches to toggle the visibility of the buttons below.
3. Observe the remaining spacing between the visible controls.

**Expected Behavior**
The Grid should not apply spacing to invisible children.

**Actual Behavior**
The Grid applies spacing to invisible children leading to duplicated spacing.

**Link to issue**
https://github.com/dotnet/maui/issues/11222