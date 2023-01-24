**Description of the issue**
Maui: On MacCatalyst the SKCanvasView captures the mouse and does not release it if EnableTouchEvents=true and the Touch is handled.

**Steps to reproduce:**
1. Run the app on MacCatalyst (Maui)
2. Mouse down, then mouse move, then mouse up inside the SKCanvasView.
3. Click the Button.

**Expected Behavior**
The button is clicked (Clicked event handler is called; Command is executed).

**Actual Behavior**
The Button is not clicked (Clicked is not called; Command is not executed).

**Link to issue**
https://github.com/mono/SkiaSharp/issues/2367
