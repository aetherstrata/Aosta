using Animeikan.GUI.Extensions;
using AutoMapper;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace Animeikan.GUI;

public static class MauiProgram
{
  public static MauiApp CreateMauiApp()
  {
    //TODO: crea mapper
    var mapConfig = new MapperConfiguration(cfg => cfg.CreateMap<JikanDotNet.Anime,Data.AnimeData>());

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
