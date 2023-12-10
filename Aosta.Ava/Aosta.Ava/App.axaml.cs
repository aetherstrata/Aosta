using System.Reflection;

using Aosta.Ava.Localization;

using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Aosta.Ava.ViewModels;
using Aosta.Ava.Views;
using Aosta.Jikan;

using ReactiveUI;
using Splat;

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

        Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetExecutingAssembly());

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

        base.OnFrameworkInitializationCompleted();
    }
}
