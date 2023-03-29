using Aosta.GUI.Extensions;
using CommunityToolkit.Maui;

namespace Aosta.GUI;

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

        return builder.Build();
    }
}