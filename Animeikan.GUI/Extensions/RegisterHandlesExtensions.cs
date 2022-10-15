namespace Animeikan.GUI;

internal static partial class MauiAppBuilderExtensions
{
    public static MauiAppBuilder RegisterHandlers(this MauiAppBuilder builder)
    {
        return builder.ConfigureMauiHandlers(handlers =>
        {
            // Your handlers here...
            //handlers.AddHandler(typeof(MyEntry), typeof(MyEntryHandler));
        });
    }
}