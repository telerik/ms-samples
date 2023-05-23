**Description of the issue**
Maui: A memory leak is observed, and the whole Page stays alive, when using a Brush that is defined in the static resources of the App.

**Steps to reproduce:**
1. Run the project.
2. Click the "Click me" button in the center of the window.
3. The app navigates to a new GrumpyPage that uses a static Brush (GrumpyBrush). A Label prints the number of alive instances of this type of GrumpyPage.
4. Click the "Back" button in the center of the window.
5. Repeat steps 3-4-5 and see that all instances of the GrumpyPage are alive.

No such issue is observed if the static GrumpyBrush is not used.

**Expected Behavior**
Expected result is for the pages to get released from memory, and so the number of alive instances is expected to be lower than the total created.

**Actual Behavior**
The pages remain in memory, they are not released, and so memory is accumulated over time, and there is a memory leak.

**Link to issue**
https://github.com/dotnet/maui/issues/15231
