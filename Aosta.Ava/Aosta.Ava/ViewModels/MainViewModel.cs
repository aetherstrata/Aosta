using System;
using System.Reactive;
using System.Reactive.Linq;

using Aosta.Ava.Extensions;

using ReactiveUI;

namespace Aosta.Ava.ViewModels;

public class MainViewModel : ReactiveObject, IScreen
{
    /// <inheritdoc />
    public RoutingState Router { get; } = new();

    public ReactiveCommand<Unit, IRoutableViewModel> GoHome { get; }
    public ReactiveCommand<Unit, IRoutableViewModel> GoList { get; }
    public ReactiveCommand<Unit, IRoutableViewModel> GoSearch { get; }
    public ReactiveCommand<Unit, IRoutableViewModel> GoBack { get; }

    public MainViewModel()
    {
        var homePage = new Lazy<HomePageViewModel>(() => new HomePageViewModel(this));
        var listPage = new Lazy<AnimeListPageViewModel>(() => new AnimeListPageViewModel(this));

        var canGoBack = this
            .WhenAnyValue(vm => vm.Router.NavigationStack.Count)
            .Select(_ => this.CanGoBack());

        GoHome = ReactiveCommand.CreateFromObservable(
            () => Router.NavigateAndReset.Execute(homePage.Value),
            isDifferentFrom<HomePageViewModel>());

        GoList = ReactiveCommand.CreateFromObservable(
            () => Router.NavigateAndReset.Execute(listPage.Value),
            isDifferentFrom<AnimeListPageViewModel>());

        GoSearch = ReactiveCommand.CreateFromObservable(
            () => Router.Navigate.Execute(new SearchPageViewModel(this)),
            isDifferentFrom<SearchPageViewModel>());

        GoBack = ReactiveCommand.CreateFromObservable(
            () => Router.NavigateBack.Execute(Unit.Default),
            canGoBack);

        GoHome.Execute();
    }

    private IObservable<bool> isDifferentFrom<T>() where T : IRoutableViewModel
    {
        return this.WhenAnyObservable(vm => vm.Router.CurrentViewModel)
            .Select(x => x is not T);
    }
}
