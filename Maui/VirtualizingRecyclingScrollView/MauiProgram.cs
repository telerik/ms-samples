using Microsoft.Extensions.Logging;
using Microsoft.Maui.Handlers;

namespace VirtualizingRecyclingScrollView;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureMauiHandlers(handlers =>
			{
				ViewHandler.ViewCommandMapper.ModifyMapping<IView, IViewHandler>(nameof(IView.InvalidateMeasure), (layout, handler, args, current) =>
				{
					// Comment this out to stop layout invalidation...
					current?.Invoke(layout, handler, args);
				});
			})
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
