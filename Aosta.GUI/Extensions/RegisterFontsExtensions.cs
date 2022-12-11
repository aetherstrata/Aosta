namespace Aosta.GUI.Extensions;

internal static partial class MauiAppBuilderExtensions
{
    public static MauiAppBuilder RegisterFonts(this MauiAppBuilder builder)
    {
        return builder.ConfigureFonts(fonts =>
        {
            fonts.AddFont("Comfortaa-Regular.ttf", "ComfortaaRegular");
            fonts.AddFont("Comfortaa-Light.ttf", "ComfortaaLight");
            fonts.AddFont("Comfortaa-Bold.ttf", "ComfortaaBold");
            fonts.AddFont("FontAwesome-Free-Solid.otf", "FontAwesome");
        });
    }
}
