using Aosta.Ava.ViewModels;
using Avalonia.ReactiveUI;

namespace Aosta.Ava.Views;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    public MainWindow()
    {
        InitializeComponent();
    }
}