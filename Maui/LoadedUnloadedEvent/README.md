**Description of the issue**
Maui: Loaded and Unloaded events are not fired correctly.

**Steps to reproduce:**
1. Run the sample application from the provided repository.
2. Observe the output on the screen showing a single Loaded event.
3. Click the First Template button to set the first custom ControlTemplate to the ContentView.
4. Observe the output on the screen showing an Unloaded event without a corresponding Loaded event.
5. Click the Second Template button to set the second custom ControlTemplate to the ContentView.
6. Observe the output on the screen showing a Loaded event without a corresponding Unloaded event.
7. Click the First Template button again to set the first custom ControlTemplate to the ContentView.
8. Observe the output on the screen showing both an Unloaded event paired with a corresponding Loaded event.

**Expected Behavior**
The Unloaded and Loaded should both be fired when reloading the content.

**Actual Behavior**
Sometimes the Unloaded event is fired without a corresponding Loaded event and vice-versa.

**Link to issue**
https://github.com/dotnet/maui/issues/13245.