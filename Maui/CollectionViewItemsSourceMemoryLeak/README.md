**Description of the issue**
Maui: CollectionView leaks memory when ItemsSource has changed.

**Steps to reproduce:**
1. Build and run the sample project in Release build configuration.
2. Click on the "Refresh Memory Info" button to show the initial memory statistics.
3. Click on the "Refresh Items Source" button several times to recreate the collection of items.
4. Click on the "Refresh Memory Info" button to update the memory statistics.
5. Repeat the steps 3 and 4 several times to observe how both total tracked objects and alive objects constantly grow.
6. Click the Recreate Control button to recreate the CollectionView in the visual tree.
7. Click on the "Refresh Memory Info" button to update the memory statistics.
8. Observe how the alive objects are decreased after the CollectionView is recreated.

**Expected Behavior**
The number of alive objects should not grow when changing the ItemsSource of the CollectionView.

**Actual Behavior**
The number of alive objects grows when changing the ItemsSource of the CollectionView.

**Link to issue**
https://github.com/dotnet/maui/issues/13393.