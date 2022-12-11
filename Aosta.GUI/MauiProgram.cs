using Aosta.GUI.Extensions;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace Aosta.GUI;

public static class MauiProgram
{
  public static MauiApp CreateMauiApp()
  {
    //TODO: crea mapper

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
