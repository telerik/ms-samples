**Description of the issue**
Maui: A Grid with two columns (Star and Auto) cause an incorrect layout when the element in the Auto column has a HeightRequest. The element in the Star column (an Entry) is not given its desired size and is getting cut off at the bottom.

**Steps to reproduce:**
1. Run the project

**Expected Behavior**
The Entry that is in the first Grid column should be fully seen and should be laid out with its desired size.

**Actual Behavior**
The Entry is getting cut off because it was arranged with size smaller than needed.

**Link to issue**
https://github.com/dotnet/maui/issues/14494
