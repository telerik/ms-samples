**Description of the issue**
We’ve updated to SR4 and enabled SourceGen. The XAML compiled as expected with SourceGen enabled. However, a NullReferenceException is thrown at runtime. It appears to be caused by the OnPlatform XAML logic we have. When I add the following XAML:

```xaml
<OnPlatform x:TypeArguments="View">
    <On Platform="WinUI">
        <Label Text="This is a label only visible on WinUI" />
    </On>
</OnPlatform>
```

the following exception is thrown:

System.NullReferenceException: Object reference not set to an instance of an object. at SourceGenRuntimeException.MainPage.InitializeComponent() in /Users/apopatan/Desktop/SourceGenRuntimeException/obj/Generated/Microsoft.Maui.Controls.SourceGen/Microsoft.Maui.Controls.SourceGen.XamlGenerator/MainPage.xaml.xsg.cs:line 93
at SourceGenRuntimeException.MainPage..ctor() in /Users/apopatan/Desktop/SourceGenRuntimeException/MainPage.xaml.cs:line 9
at SourceGenRuntimeException.App.CreateWindow(IActivationState activationState) in /Users/apopatan/Desktop/SourceGenRuntimeException/App.xaml.cs:line 14
at Microsoft.Maui.Controls.Application.Microsoft.Maui.IApplication.CreateWindow(IActivationState activationState)

**Steps to reproduce:**
1. Run the attached sample project.
2. Compile the project and run it on a platform different from WinUI, as the OnPlatform example above targets WinUI. For instance, if the code is intended for Android, the issue will occur on all platforms except Android.

**Expected Behavior**
The application should run as expected.

**Actual Behavior**
A NullReferenceException is thrown.

**Link to issue**
https://github.com/dotnet/maui/issues/34074
