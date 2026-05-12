# VisualElement.ChangeVisualState() gets stuck in Selected state

**Description of the issue**

When a custom control overrides `ChangeVisualState()` to apply the `Selected` visual state and then calls `base.ChangeVisualState()` in the `else` branch (i.e. when the element is no longer selected), the element gets permanently stuck in the `Selected` state.

The root cause is `VisualElement.ChangeVisualState()` calling the internal extension method `IsElementInSelectedState()`, introduced in .NET 10, which reads the **current** `VisualStateGroup.CurrentState` to determine whether the element is selected. At the time `base.ChangeVisualState()` is called, the VSM group's `CurrentState` is still `"Selected"` from the previous transition, so the base method re-applies `Selected` even though `IsSelected` is now `false`.

This affects any custom control that follows the common pattern:

```csharp
protected override void ChangeVisualState()
{
    if (IsSelected && IsEnabled)
    {
        VisualStateManager.GoToState(this, VisualStateManager.CommonStates.Selected);
    }
    else
    {
        base.ChangeVisualState(); // ← gets stuck: base re-applies Selected
    }
}
```

**Steps to reproduce**

1. Run the attached sample project on any platform.
2. Tap **Select** — the box turns green (transitions to `Selected` state). ✅
3. Tap **Deselect** — the box should turn grey (`Normal` state) but remains green. ❌

**Expected behavior**

After tapping **Deselect**, `IsSelected` is `false` and `base.ChangeVisualState()` is called. The element should transition to `Normal` (grey box).

**Actual behavior**

The element remains in the `Selected` visual state (green box) after deselection. `base.ChangeVisualState()` re-applies `Selected` because `IsElementInSelectedState()` still finds `"Selected"` in the VSM group's `CurrentState`.

**Root cause**

`VisualElement.ChangeVisualState()` in .NET 10 was updated to call the internal `IsElementInSelectedState()` extension method:

```csharp
// VisualElement.ChangeVisualState() — .NET 10
bool isSelected = this.IsElementInSelectedState();
string targetState = isSelected
    ? VisualStateManager.CommonStates.Selected
    : (IsPointerOver ? VisualStateManager.CommonStates.PointerOver
                     : VisualStateManager.CommonStates.Normal);
```

`IsElementInSelectedState()` is defined in `VisualStateManager.cs`:

```csharp
internal static bool IsElementInSelectedState(this VisualElement element)
{
    var groups = VisualStateManager.GetVisualStateGroups(element);
    foreach (var group in groups)
    {
        if (group.CurrentState?.Name == VisualStateManager.CommonStates.Selected)
            return true;
    }
    return false;
}
```

Because `CurrentState` reflects the **last applied** state (which is still `"Selected"` at the moment `base.ChangeVisualState()` is called), the base re-applies `Selected` unconditionally.

**Suggested fix**

Before calling `base.ChangeVisualState()` in the `else` branch, force the element to `Normal` so that `IsElementInSelectedState()` no longer finds `Selected` in the group:

```csharp
protected override void ChangeVisualState()
{
    if (IsSelected && IsEnabled)
    {
        VisualStateManager.GoToState(this, VisualStateManager.CommonStates.Selected);
    }
    else
    {
        // Workaround for .NET 10+: clear the Selected state first so that
        // VisualElement.IsElementInSelectedState() does not re-apply it.
        if (IsEnabled)
        {
            VisualStateManager.GoToState(this, VisualStateManager.CommonStates.Normal);
        }
        base.ChangeVisualState();
    }
}
```

The `IsEnabled` guard avoids a redundant `Normal → Disabled` double transition when the element is disabled (in that case `base.ChangeVisualState()` takes the `!IsEnabled` branch directly and never reaches `IsElementInSelectedState()`).

**Link to issue**
