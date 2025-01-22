**Description of the issue**
Maui: Application freezes when Background property is set to a DynamicResource of type OnPlatform Color

**Steps to reproduce:**
1. Create a ResourceDictionary with OnPlatform resource of type Color.
2. Set the Background property of MainPage to that Color as a DynamicResource.
3. Merge the dictinary to the resources of the MainPage.
4. The application freezes.

**Expected Behavior**
The Background of the MainPage to be changed.

**Actual Behavior**
The application freezes.

**Link to issue**
https://github.com/dotnet/maui/issues/27281
