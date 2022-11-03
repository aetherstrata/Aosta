using Animeikan.GUI.Services;

using CommunityToolkit.Maui;

using JikanDotNet;

using Microsoft.Extensions.DependencyInjection;

namespace Animeikan.GUI.Extensions;

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
    builder.Services.AddSingleton<ViewModels.OnboardingScreenViewModel>();
    builder.Services.AddSingleton<ViewModels.SettingsViewModel>();

    //Transient ViewModels
    builder.Services.AddTransient<ViewModels.AnimeSummaryViewModel>();

    #endregion

    #region Views

    //Singleton Views
    builder.Services.AddSingleton<Views.OnboardingPage>();
    builder.Services.AddSingleton<Views.SettingsPage>();
    builder.Services.AddSingleton<Views.MainPage>();
    builder.Services.AddSingleton<Views.ProfilePage>();

    //Transient Views
    builder.Services.AddTransient<Views.AnimeSummaryPage>();

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
