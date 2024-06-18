**Description of the issue**
The following build error can be observed when the SkiaSharp.Views.Maui.Controls.Compatibility package is added to a project targeting .Net 9 Preview 5 and building in Release configuration.

```
Microsoft.MacCatalyst.Sdk.net9.0_17.2/17.2.9639-net9-p5/targets/Xamarin.Shared.Sdk.targets(1256,3): error : Failed to AOT compile SkiaSharp.Views.Maui.Controls.Compatibility.dll, the AOT compiler exited with code 1.

/Microsoft.MacCatalyst.Sdk.net9.0_17.2/17.2.9639-net9-p5/targets/Xamarin.Shared.Sdk.targets(1256,3): error : Failed to AOT compile aot-instances.dll, the AOT compiler exited with code 1.
```

**Steps to reproduce:**
1. dotnet build -f net9.0-maccatalyst -c Release

**Expected Behavior**
The project is built with no errors

**Actual Behavior**
Build error

**Link to issue**
https://github.com/dotnet/maui/issues/23108
