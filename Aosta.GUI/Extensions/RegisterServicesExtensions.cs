using Aosta.Core;
using Aosta.GUI.Features.AddJikanAnime;
using Aosta.GUI.Features.AddManualAnime;
using Aosta.GUI.Features.AnimeSummaryPage;
using Aosta.GUI.Features.MainPage;
using Aosta.GUI.Features.OnboardingPage;
using Aosta.GUI.Features.ProfileMainPage;
using Aosta.GUI.Features.SettingsPage;
using Aosta.GUI.Services;
using Aosta.Jikan;
using CommunityToolkit.Maui.Storage;
using Serilog;

namespace Aosta.GUI.Extensions;

internal static partial class MauiAppBuilderExtensions
{
    public static MauiAppBuilder RegisterServices(this MauiAppBuilder builder)
    {
        #region Services

        var jikan = new JikanConfiguration().Use.Logger(Log.Logger).Build();

        builder.Services.AddSingleton<IFileSaver>(FileSaver.Default);
        builder.Services.AddSingleton<ISecureStorageService, SecureStorageService>();
        builder.Services.AddSingleton<ISettingsService, SettingsService>();
        builder.Services.AddSingleton<IJikan >(jikan);
        builder.Services.AddSingleton<AostaDotNet>(new AostaConfiguration(FileSystem.Current.AppDataDirectory)
                .With.CacheDirectory(FileSystem.Current.CacheDirectory)
                .Source.FromJikan(jikan)
                .Log.With(Log.Logger)
                .Build());

        #endregion

        #region ViewModels

        // Singleton ViewModels
        builder.Services.AddSingleton<OnboardingScreenViewModel>();
        builder.Services.AddSingleton<SettingsViewModel>();
        builder.Services.AddSingleton<MainPageViewModel>();

        //Transient ViewModels
        builder.Services.AddTransient<AnimeSummaryViewModel>();
        builder.Services.AddTransient<AddManualAnimeViewModel>();
        builder.Services.AddTransient<AddJikanAnimeViewModel>();

        #endregion

        #region Views

        //Singleton Views
        builder.Services.AddSingleton<OnboardingPage>();
        builder.Services.AddSingleton<SettingsPage>();
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<ProfilePage>();

        //Transient Views
        builder.Services.AddTransient<AnimeSummaryPage>();
        builder.Services.AddTransient<AddManualAnimePage>();
        builder.Services.AddTransient<AddJikanAnimePage>();

        #endregion

        // Default method
        //builder.Services.Add();
        // Scoped objects are the same within a request, but different across different requests.
        //builder.Services.AddScoped();     
        // Singleton objects are created as a single instance throughout the application. It creates the instance for the first time and reuses the same object in the all calls.
        //builder.Services.AddSingleton();  
        // Transient objects lifetime services are created each time they are requested. This lifetime works best for lightweight, stateless services.
        //builder.Services.AddTransient();  

        return builder;
    }
}