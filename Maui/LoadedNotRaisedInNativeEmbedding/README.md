**Update** (13.08.2026)
Updated to MAUI 10.0.90 and re-tested. Loaded and Unloaded events are triggered as expected.

**Description of the issue**
The Loaded and Unloaded events are not raised for Maui elements when in Native Embedding

**Steps to reproduce**
1. Run the app

**Expected Result**
We hit the two breakpoints for the Loaded event of the Maui Grid and Button and the code in the event handlers should get executed.

**Actual Result**
We don't hit any breakpoints 

**Link to issue**
https://github.com/dotnet/maui/issues/18714