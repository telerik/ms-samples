**Description of the issue**
Consider the following layout:

```
<StackLayout>
    <Grid ColumnDefinitions="Auto, *">
        <Rectangle />
        <Label Grid.Column="1" Text="Label" />
    </Grid>
</StackLayout>
```

The Label is not displayed when using the unofficial version from the [release/7.0.2xx](https://github.com/dotnet/maui/tree/release/7.0.2xx) branch.

For comparisson, the Label is displayed as expected with version `7.0.59`.

Most probably the issue occurs due to the following change: [#13395](https://github.com/dotnet/maui/pull/13395)

**Steps to reproduce:**
1. Run the app on any platform.

**Expected Behavior**
The Label defined in XAML is displayed as expected.

**Actual Behavior**
The Label is not displayed.