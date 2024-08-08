# MauiWinUIWindow Leak
.NET MAUI. WinUI only. MauiWinUIWindow seems to leak.

## How did we get here
We are building on top of the DeviceTests from the dotnet maui repo.
After getting about 100 - 150 tests using CreateHandlerAndAddToWindow out app started crashing with win32 exceptions.
WinDbg was showing random places mostly when creating a new window.
Adding some stats logging we found out our test pages are never reclaimed by GC.
We drilled down to a point where a MauiWinUIWindow is created and doesn't seem to be reclaimed.
The GC memory doesn't seem to grow so rapidly on our side, but the overall app memory grows by a lot,
until AccessViolation or some sort of "not enough memory" terminates the testing app.

## How to reproduce
Here in this example if you keep clicking the "Create and close one..." button,
you can see how the weak ref list is added a weak reference to a MauiWinUIWindow that is opened and then closed.
The memory grows over time and the weak refs are alwasy alived.

## Expected behavior
The expected behavior would be for the MauiWinUIWindow after time to be reclaimed, some memory freed, and weak refs to be un-alived.