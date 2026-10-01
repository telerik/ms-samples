**Description of the issue**

The sample uses:

- a base button style (`BaseButtonStyle`) with shared visual states
- a derived style (`TransparentButtonStyle`) using `BasedOn`
- only one override in the derived style:
  `BackgroundColor="Transparent"`

Hover color is configured as:

```xml
<Color x:Key="ButtonHoverBackgroundColor">#B2F9F9F9</Color>
```

and applied through `DynamicResource` in `PointerOver`.

When hovering the transparent button, the background transition flickers.
The base (solid-background) button does not show this behavior.

**Steps to reproduce**

1. Run the Windows target.
2. Move the pointer over `Base Button`.
3. Move the pointer over `Transparent Button`.
4. Repeat enter/leave on `Transparent Button` and compare behavior.

**Expected behavior**

Both buttons should transition cleanly to `PointerOver` using the configured
hover color, without flicker.

**Actual behavior**

`Transparent Button` flickers while pointer enter/leave transitions occur.

**Link to issue**
https://github.com/dotnet/maui/issues/39017
