**Description of the issue**
I have a CollectionView with an ItemTemplate that contains a Button. The button's Command property is set using relative binding. Initially, the command is invoked correctly. However, when the CollectionView reuses a container and re-applies the BindingContext, the relative binding is not re-evaluated, resulting in a null command.

**Steps to reproduce:**
1. Run the provided project.
2. Clear the item using the Clear button, which is part of the item's template.
3. Add a new item.
4. Attempt to clear the item again by pressing the Clear button.

**Expected Behavior**
The clear command should be invoked as expected.

**Actual Behavior**
The clear command is not invoked once the container is reused.

**Link to issue**
https://github.com/dotnet/maui/issues/31042
