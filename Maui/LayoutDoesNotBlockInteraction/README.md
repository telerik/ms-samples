**Description of the issue**
Consider the following layout:

```
<Grid>
    <Button Text="Click" Clicked="Button_Clicked" />
    <Grid>
        <TemplatedView />
    </Grid>
</Grid>
```

The button can be clicked on Android and WinUI even with the default `InputTransparent` property being `false`.

For comparison, the button's Clicked event is not fired on iOS and MacCatalyst.

The issue may be related to the following two issues:
* [[Bug] Click on an element of one view behind another view](https://github.com/dotnet/maui/issues/9153)
* [Overlaying a StackLayout on another StackLayout does not block interaction](https://github.com/dotnet/maui/issues/10252)

**Steps to reproduce:**
1. Run the app on Android or WinUI.
2. Click on the button.

**Expected Behavior**
The button does not react to the interaction.

**Actual Behavior**
The button accepts the click.