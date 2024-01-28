// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;

using Aosta.Ava.ViewModels;

using Avalonia.Controls;
using Avalonia.ReactiveUI;

using Splat;

namespace Aosta.Ava.Pages;

public partial class JikanAnimeDetailsPage : ReactiveUserControl<JikanAnimeDetailsViewModel>, IEnableLogger
{
    public JikanAnimeDetailsPage()
    {
        InitializeComponent();
    }

    private void onEpisodesRefreshRequested(object? sender, RefreshRequestedEventArgs e)
    {
        this.Log().Debug<JikanAnimeDetailsPage>("Triggered episodes list refresh event");
        ViewModel?.UpdateEpisodesList.Execute().Subscribe();
    }
}

