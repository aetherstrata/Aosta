using Aosta.Core;
using Aosta.GUI.Features.AnimeManualAddPage;
using Aosta.GUI.Features.AnimeSummaryPage;
using Aosta.GUI.Features.MainPage;
using Aosta.GUI.Features.OnboardingPage;
using Aosta.GUI.Features.ProfileMainPage;
using Aosta.GUI.Features.SettingsPage;
using Aosta.GUI.Services;
using CommunityToolkit.Maui;
using JikanDotNet;
using Serilog;

namespace Aosta.GUI.Extensions;

internal static partial class MauiAppBuilderExtensions
{
    public static MauiAppBuilder RegisterServices(this MauiAppBuilder builder)
    {
        #region Services

        builder.Services.AddSingleton<ISecureStorageService, SecureStorageService>();
        builder.Services.AddSingleton<ISettingsService, SettingsService>();
        builder.Services.AddSingleton<IJikan, Jikan>();

        #endregion

        #region ViewModels

        // Singleton VIewModels
        builder.Services.AddSingleton<OnboardingScreenViewModel>();
        builder.Services.AddSingleton<SettingsViewModel>();
        builder.Services.AddSingleton<MainPageViewModel>();

        //Transient ViewModels
        builder.Services.AddTransient<AnimeSummaryViewModel>();
        builder.Services.AddTransient<AnimeManualAddViewModel>();

        #endregion

        #region Views

        //Singleton Views
        builder.Services.AddSingleton<OnboardingPage>();
        builder.Services.AddSingleton<SettingsPage>();
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<ProfilePage>();

        //Transient Views
        builder.Services.AddTransient<AnimeSummaryPage>();
        builder.Services.AddTransient<AddAnimePage>();

        #endregion

        #region Logging

        builder.Services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(App.Core.Logger, dispose: true));

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