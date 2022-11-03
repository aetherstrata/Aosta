using Animeikan.GUI.Extensions;

using CommunityToolkit.Maui;

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
        .RegisterFonts()
        .RegisterHandlers()
        .RegisterServices();

#if DEBUG
    builder.Logging.AddDebug();
#endif

    return builder.Build();
  }
}
