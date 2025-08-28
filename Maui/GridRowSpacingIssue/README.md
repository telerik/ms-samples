**Description of the issue**
When using a Grid layout with multiple columns and a defined ColumnSpacing, placing a View on a star-sized row (RowDefinition Height="*") and setting its ColumnSpan to span all columns results in unexpected layout behavior. Specifically, the View appears to have a gap on its right side equal to the grid’s ColumnSpacing.

This issue occurs only when the star row is not the first row in the grid. If the grid has row definitions like RowDefinitions="Auto, *" and the View is placed on Grid.Row="1", the layout behaves incorrectly. In contrast, if the star row is the first row, the layout works as expected.

**Steps to reproduce:**
1. Run the attached project.
2. Observe the gap on the right side of the view that spans all columns in the grid.

**Expected Behavior**
The view should take the full width of the Grid layout.

**Actual Behavior**
There is a gap on the right side of the view which is the ColumnSpacing of the Grid layout.

**Link to issue**
https://github.com/dotnet/maui/issues/24354