using Animeikan.GUI.Extensions;
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Markup;
using Microsoft.Extensions.Logging;

namespace Animeikan.GUI;

public static class MauiProgram
{
	static readonly string AppDataPath = FileSystem.Current.AppDataDirectory;
	static readonly string CacheDirPath = FileSystem.Current.CacheDirectory;

	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.UseMauiCommunityToolkitMarkup()
			.RegisterFonts()
			.RegisterHandlers()
			.RegisterServices();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
