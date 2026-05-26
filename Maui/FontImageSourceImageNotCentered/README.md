# [Windows] Image with FontImageSource is not centered and gets clipped when WidthRequest/HeightRequest equals FontImageSource Size

**Description of the issue**

After a .NET MAUI update (10.0.60), `Image` controls that use a `FontImageSource` and have `Aspect="Center"` with `WidthRequest`/`HeightRequest` set to the same value as `FontImageSource.Size` are no longer perfectly centered and are sometimes clipped/cut off.

**Steps to reproduce**

1. Run the attached sample project on Windows.
2. Notice that the font images are not centered.

**Root cause**

When `WidthRequest`/`HeightRequest` on the `Image` is set to the exact same value as `FontImageSource.Size`, the layout engine leaves no extra space for `Aspect="Center"` to correctly position the rendered glyph within its bounding box. This causes the glyph to appear off-center or clipped.

The issue was introduced with the following PR: https://github.com/dotnet/maui/pull/30068

**Link to issue**
https://github.com/dotnet/maui/issues/35618
