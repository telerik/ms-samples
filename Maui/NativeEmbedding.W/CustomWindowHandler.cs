using Microsoft.UI.Xaml;

namespace NativeEmbedding;

public class CustomWindowHandler : Microsoft.Maui.Handlers.WindowHandler
{
    protected override Window CreatePlatformElement()
    {
        return ((NativeEmbedding.App)Microsoft.UI.Xaml.Application.Current).m_window;
    }
}
