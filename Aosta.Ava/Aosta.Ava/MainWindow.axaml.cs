using Aosta.Ava.ViewModels;

using Avalonia;
using Avalonia.ReactiveUI;

namespace Aosta.Ava;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    public MainWindow()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
    }
}
