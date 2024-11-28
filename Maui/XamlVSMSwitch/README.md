## Clear implicit style glitch
 - Open the demo in macOS
 - Click the top "Styled Button", it works as expected
 - Click the "Local Style1", it applies local style to "Styled Button" reddish, it works as expected
 - Click the "Local Style2", it applies local style ot "Styled Button" bluish, it works as expected
 - Click the "Clear", it clears the local style and it works as expected

### The point
 - Click "Implicit Style1", it applies the "Local Style1" as implicit by merging it with the button type string, all buttons go red
 - Click "Clear", it clears the local style for "Styled Button" and clears the implicit style

### Issue
***Expected***: after removing the implicit style from the resource dictionary, is to revert all buttons to blue
***Actual***: after removing the implicit style from the resource dictionary all buttons remain red or green, until mouse-overed, when mouse-overed the VisualStateManager somehow figures out it should revert some values to the default style from App.xaml

link: https://github.com/dotnet/maui/issues/26208
