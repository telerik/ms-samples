namespace SelectedStateIssue;

/// <summary>
/// Minimal custom VisualElement that:
///   1. Exposes an <see cref="IsSelected"/> bindable property.
///   2. Overrides <see cref="ChangeVisualState"/> to go to "Selected" when selected,
///      or delegates to base otherwise.
///
/// The bug: after transitioning to "Selected" and then setting IsSelected = false,
/// base.ChangeVisualState() re-applies "Selected" because
/// VisualElement.IsElementInSelectedState() reads the current VSM group state,
/// which still reports "Selected" at the time of the call.
/// </summary>
public class SelectableBox : ContentView
{
    public static readonly BindableProperty IsSelectedProperty =
        BindableProperty.Create(
            nameof(IsSelected),
            typeof(bool),
            typeof(SelectableBox),
            defaultValue: false,
            propertyChanged: (b, _, _) => ((SelectableBox)b).ChangeVisualState());

    public bool IsSelected
    {
        get => (bool)GetValue(IsSelectedProperty);
        set => SetValue(IsSelectedProperty, value);
    }

    public SelectableBox()
    {
        VisualStateManager.SetVisualStateGroups(this, new VisualStateGroupList
        {
            new VisualStateGroup
            {
                Name = "CommonStates",
                States =
                {
                    new VisualState
                    {
                        Name = VisualStateManager.CommonStates.Normal,
                        Setters = { new Setter { Property = ContentView.BackgroundProperty, Value = Colors.LightGray } }
                    },
                    new VisualState
                    {
                        Name = VisualStateManager.CommonStates.Selected,
                        Setters = { new Setter { Property = ContentView.BackgroundProperty, Value = Colors.MediumSeaGreen } }
                    },
                    new VisualState
                    {
                        Name = VisualStateManager.CommonStates.Disabled,
                        Setters = { new Setter { Property = ContentView.BackgroundProperty, Value = Colors.DarkGray } }
                    }
                }
            }
        });
    }

    /// <inheritdoc/>
    protected override void ChangeVisualState()
    {
        if (IsSelected && IsEnabled)
        {
            VisualStateManager.GoToState(this, VisualStateManager.CommonStates.Selected);
        }
        else
        {
            // BUG: on .NET 10, base.ChangeVisualState() calls IsElementInSelectedState()
            // which reads the current VSM group state. Since the group is still "Selected"
            // at this point, the base re-applies "Selected" even though IsSelected is false.
            base.ChangeVisualState();
        }
    }
}
