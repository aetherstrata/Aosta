// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;

using Aosta.Ava.Extensions;
using Aosta.Ava.ViewModels;
using Aosta.Data;
using Aosta.Data.Database.Models;
using Aosta.Data.Extensions;

using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.ReactiveUI;

using Splat;

namespace Aosta.Ava.Pages;

public partial class JikanAnimeDetailsPage : ReactiveUserControl<JikanAnimeDetailsViewModel>, IEnableLogger
{
    public JikanAnimeDetailsPage()
    {
        InitializeComponent();
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);

        if (ViewModel != null) AddButton.IsEnabled = ViewModel.CanBeAdded();
    }

    private void onEpisodesPullRefresh(object? sender, RefreshRequestedEventArgs e)
    {
        var deferral = e.GetDeferral();

        this.Log().Debug<JikanAnimeDetailsPage>("Triggered episodes list refresh event");
        ViewModel?.UpdateEpisodesList();

        deferral.Complete();
    }
}

