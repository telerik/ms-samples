# Control Vendor XAML Stripping
As a control vendor... We ship an assembly with huge amount of MAUI controls to our clients. Our clients use a subset of these controls and use the MAUI Linker to trim down the control assemblies. Our clients have different needs - some want a platform specific UI respecting user system settings (dark mode, font size etc.) others want a build-once-ship-everywhere appearance, etc. To address them we plan to add "themes" in the form of XAML files, our clients can include in their apps.

## XAML Stripping?
If we add several ***.xaml*** files in our assembly, with different "themes" and color variations, and tell our clients to include some of them in their apps, we would need the linker to somehow strip some of the ***.xaml*** files in our assembly.

Currently it seems all ***.xaml*** is considered root by the linker and is included in the app, recursively including the referenced controls.

## Demo App & Controls
Take a look at the example files.
 - ***VendorControls/VendorControls.csproj***  
   A mini-demo of our controls assembly.
   
   It has the `<IsTrimmable>true</IsTrimmable>` attribute set.

   It has 3 controls
    - `UnusedControl.cs` with `UnusedThemeFiles.xaml`, that is not referenced anywhere in the ***UserApp***
    - `UsedControl.cs` with `UsedThemeFiles.xaml`, where the xaml is merged into the ***UserApp***'s ***App.xaml*** and the UsedControl is used in the app's main page
    - `UnusedUnthemedControl.cs` that is not referenced in `.xaml` anywhere
 - ***UserApp/UserApp.csproj***  
   A mini-demo of a client app.

   It has `<IsTrimmable>true</IsTrimmable><PublishTrimmed>true</PublishTrimmed><TrimMode>full</TrimMode>` set.

   It references the ***VendorControls*** project, and has its `UsedThemeFiles.xaml` files merged in ***App.xaml*** and the `UsedControl.cs` displayed in its main page.

Target iOS, iPhone 15 Pro Max (mostly to collect console logs, MacOS won't show in debug console)

Output when build and run in Debug:
```
Assembly UserApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
 - __XamlGeneratedCode__.__TypeB128BCFF9987178E
 - __XamlGeneratedCode__.__Type869116078F19A310
 - UserApp.App
 - UserApp.AppShell
 - UserApp.MainPage
 - UserApp.MauiProgram
 - UserApp.AppDelegate
 - UserApp.Program
 - UserApp.MauiProgram+<>c
Assembly VendorControls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
 - VendorControls.UnusedControl
 - VendorControls.UnusedThemeFiles
 - VendorControls.UnusedUnthemedControl
 - VendorControls.UsedControl
 - VendorControls.UsedThemeFiles
 - VendorControls.VendorControl
```

During build in release, in the build log:
```
Optimizing assemblies for size. This process might take a while.
```

Output when build and run in Release:
```
Assembly UserApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
 - __XamlGeneratedCode__.__TypeB128BCFF9987178E
 - __XamlGeneratedCode__.__Type869116078F19A310
 - UserApp.App
 - UserApp.AppShell
 - UserApp.MainPage
 - UserApp.MauiProgram
 - UserApp.MauiProgram+<>c
 - UserApp.AppDelegate
 - UserApp.Program
Assembly VendorControls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
 - VendorControls.UnusedControl
 - VendorControls.UnusedThemeFiles
 - VendorControls.UsedControl
 - VendorControls.UsedThemeFiles
 - VendorControls.VendorControl
```

The `UnusedUnthemedControl` is now trimmed. What we would like is to have `UnusedControl` and `UnusedThemeFiles` trimmed as well.

The Linker would correctly strip the `UnusedUnthemedControl`, but will treat all `.xaml` files from the `VendorControls` as ***roots***. Adding `.xaml` file to an assembly places a `XamlResourceIdAttribute` and keeps the XAML with all referenced classes in its styles.

Is there a way to trim `.xaml` files from the `VendorControls` when some of them are not referenced in a client app?

So far, some of the solutions we have figured, out is to distribute multiple .xaml files or assemblies and have the clients cherry-pick some of them in their app.