// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;

using Aosta.Ava.ViewModels;

using Avalonia.Controls;
using Avalonia.Interactivity;

using Splat;

namespace Aosta.Ava.Pages;

public partial class OnlineAnimeDetailsPage : ReactivePageBase<OnlineAnimeDetailsViewModel>, IEnableLogger
{
    public OnlineAnimeDetailsPage()
    {
        InitializeComponent();
    }

    private void onEpisodesPullRefresh(object? sender, RefreshRequestedEventArgs e)
    {
        var deferral = e.GetDeferral();

        this.Log().Debug<OnlineAnimeDetailsPage>("Triggered episodes list refresh event");
        ViewModel?.UpdateEpisodesList();

        deferral.Complete();
    }

    private void OnButtonLoaded(object? sender, RoutedEventArgs e)
    {
        var button = sender as Button ?? throw new ArgumentNullException(nameof(sender), "NULL Button");

        button.IsEnabled = ViewModel.CanBeAdded();
        button.Tapped += (_, _) =>
        {
            _ = ViewModel.AddToRealm();
            button.IsEnabled = false;
        };
    }
}

