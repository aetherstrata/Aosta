using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using ReactiveUI;

namespace Aosta.Ava.ViewModels;

public class MainWindowViewModel : ReactiveObject
{
    public void QuitProgram()
    {
        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.Shutdown();
        }
    }
}