using System.Reactive.Disposables;
using Aosta.Ava.ViewModels;
using Avalonia.ReactiveUI;
using ReactiveUI;

namespace Aosta.Ava.Views;

public partial class MainView : ReactiveUserControl<MainViewModel>
{
    public MainView()
    {
        InitializeComponent();
        this.WhenActivated(disposables =>
        {
            this.OneWayBind(ViewModel, vm => vm.GoHome, view => view.HomeButton.Command)
                .DisposeWith(disposables);
            this.OneWayBind(ViewModel, vm => vm.GoSearch, view => view.SearchButton.Command)
                .DisposeWith(disposables);
            this.OneWayBind(ViewModel, vm => vm.Router, view => view.RoutedViewHost.Router)
                .DisposeWith(disposables);
        });
    }
}