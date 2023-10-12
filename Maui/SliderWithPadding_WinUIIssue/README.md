**Description of the issue**
Maui: When placing `Slider` inside a `Grid` with Padding, dragging the slider thumb near the end causes layout cycle and the app crashes.

**Steps to reproduce:**
1. Run the project 
2. Drag the `Slider` thumb to the left.

**Expected Behavior**
The `Slider` thumb should move and the `Slider` value should change.

**Actual Behavior**
Layout cycle occurs and the app crashes.

**Link to issue**
