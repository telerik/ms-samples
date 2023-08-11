**Description of the issue**
Maui: The Unloaded event of an inner element of a custom layout is raised incorrectly during navigation.

**Steps to reproduce:**
1. Run the app in Android.
2. Click the "navigate" button.
3. Navigate back to the previous page.

**Expected Behavior**
The Unloaded event of the innerGrid element should have not been raised, as it was not raised for the CustomLayout.

**Actual Behavior**
The Unloaded event of the innerGrid element is raised, but the Loaded is never raised after that, leaving custom logic done in Loaded/Unloaded to malfunction.

**Link to issue**
to be added
