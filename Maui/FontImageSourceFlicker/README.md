**Description of the issue**
Maui: [Android] Changing the ImageSource of an Image causes a noticeable flicker.

**Steps to reproduce:**
1. Run the sample application on Android.
2. Pick a color from the list to change both the color of the text and the icon.
3. Observe how the layout jumps and the image flickers.

**Expected Behavior**
The image should nor flicker and the layout should remain stable.

**Actual Behavior**
The layout jumps and the image flickers.

**Link to issue**
https://github.com/dotnet/maui/issues/15501
