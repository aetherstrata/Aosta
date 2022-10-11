using Animeikan.GUI.Models;
using Animeikan.GUI.ViewModels;
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Markup;
using Microsoft.Extensions.Logging;

namespace Animeikan.GUI;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.UseMauiCommunityToolkitMarkup()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("Comfortaa-Regular.ttf", "ComfortaaRegular");
				fonts.AddFont("Comfortaa-Light.ttf", "ComfortaaLight");
                fonts.AddFont("Comfortaa-Bold.ttf", "ComfortaaBold");
				fonts.AddFont("FontAwesome-Free-Solid.otf", "FontAwesome");
            });

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
