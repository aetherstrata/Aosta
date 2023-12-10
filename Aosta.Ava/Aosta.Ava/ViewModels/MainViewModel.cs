﻿using System;
using System.Reactive;
using System.Reactive.Linq;

using Aosta.Core;
using Aosta.Jikan;

using ReactiveUI;

namespace Aosta.Ava.ViewModels;

public class MainViewModel : ReactiveObject, IScreen
{
    private readonly AostaDotNet _aosta = new AostaConfiguration(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)).Build();
    private readonly IJikan _jikan = new JikanConfiguration().Build();

    /// <inheritdoc />
    public RoutingState Router { get; } = new();

    public ReactiveCommand<Unit, IRoutableViewModel> GoHome { get; }
    public ReactiveCommand<Unit, IRoutableViewModel> GoSearch { get; }
    public ReactiveCommand<Unit, IRoutableViewModel> GoBack { get; }

    public string Greeting => "Welcome to Avalonia!";

    public MainViewModel()
    {
        var canGoBack = this
            .WhenAnyValue(vm => vm.Router.NavigationStack.Count)
            .Select(count => count > 0);

        GoHome = ReactiveCommand.CreateFromObservable(
            () => Router.NavigateAndReset.Execute(new HomePageViewModel(this, _jikan, _aosta)),
            isDifferentFrom<HomePageViewModel>());

        GoSearch = ReactiveCommand.CreateFromObservable(
            () => Router.Navigate.Execute(new SearchPageViewModel(this, _aosta)),
            isDifferentFrom<SearchPageViewModel>());

        GoBack = ReactiveCommand.CreateFromObservable(() => Router.NavigateBack.Execute(Unit.Default), canGoBack)!;

        GoHome.Execute();
    }

    private IObservable<bool> isDifferentFrom<T>() where T : IRoutableViewModel
    {
        return this.WhenAnyObservable(vm => vm.Router.CurrentViewModel)
            .Select(x => x is not T);
    }
}
