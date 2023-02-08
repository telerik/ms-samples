**Description of the issue**
Maui: On WinUI CollectionView has default PointerOver state for its items, which cannot be removed. 

**Steps to reproduce:**
1. Add CollectionView with SelectionMode="Single".
2. Add ItemsSource to the CollectionView.
3. Add ItemTemplate containing a Label.
4. Set VisualStateManager.VisualStateGroups to the Label and in the PointerOver state change the BackgroundColor of the Label to some Color (with or without with alpha).
5. Run the app in WinUI

**Expected Behavior**
When hovering over the Labels inside the items if the CollectionView only the set in point 4 color should be seen.

**Actual Behavior**
When hovering over the Labels inside the items if the CollectionView the default native ListViewItem pointerover background color is displayed, as well as the set color in step 4. If the color from point 4 has alpha the grey color of the item is seen through due to the transparency.

**Link to issue**
https://github.com/dotnet/maui/issues/13197
