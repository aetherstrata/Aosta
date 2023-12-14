using System.Reactive.Disposables;

using Aosta.Ava.Extensions;
using Aosta.Ava.ViewModels;

using Avalonia.Controls;
using Avalonia.Interactivity;
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

    /// <inheritdoc />
    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);
        if (TopLevel.GetTopLevel(this) is { } topLevel)
        {
            topLevel.BackRequested += onBackRequested;
        }
    }

    /// <inheritdoc />
    protected override void OnUnloaded(RoutedEventArgs e)
    {
        base.OnUnloaded(e);
        if (TopLevel.GetTopLevel(this) is { } topLevel)
        {
            topLevel.BackRequested -= onBackRequested;
        }
    }

    private void onBackRequested(object? sender, RoutedEventArgs args)
    {
        if (ViewModel is null || !ViewModel.CanGoBack())
            return;

        ViewModel.GoBack.Execute();
        args.Handled = true;
    }
}
