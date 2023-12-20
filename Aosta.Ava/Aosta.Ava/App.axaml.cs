using System.Reflection;

using Aosta.Ava.Extensions;
using Aosta.Ava.Localization;
using Aosta.Ava.ViewModels;
using Aosta.Ava.Views;
using Aosta.Core;
using Aosta.Jikan;

using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

using ReactiveUI;

using Splat;

using ILogger = Serilog.ILogger;

namespace Aosta.Ava;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        Localizer.Instance.Language = InterfaceLanguage.English;

        Locator.CurrentMutable
            .RegisterAnd(() => new JikanConfiguration()
                .Use.Logger(Locator.Current.GetSafely<ILogger>())
                .Build())
            .RegisterAnd(() => Locator.Current.GetSafely<AostaDotNet>().Realm)
            .RegisterViewsForViewModels(Assembly.GetExecutingAssembly());

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainViewModel()
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = new MainViewModel()
            };
        }

        Locator.Current.GetSafely<ILogger>().Debug("Framework initialization completed");

        base.OnFrameworkInitializationCompleted();
    }
}
