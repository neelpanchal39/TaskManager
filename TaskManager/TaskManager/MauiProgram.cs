using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Compatibility.Hosting;
using TaskManager.CustomControls;

#if IOS
using TaskManager.Platforms.iOS.Renderer;
#elif ANDROID
using TaskManager.Platforms.Android.Renderer;
#endif

namespace TaskManager;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			}).UseMauiCompatibility()
		   .ConfigureMauiHandlers((handlers) =>
		   {
#if IOS
				handlers.AddCompatibilityRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer));
#elif ANDROID
               handlers.AddCompatibilityRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer));
#endif
           });

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}

