// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using Aosta.Ava.ViewModels;

using Avalonia.Interactivity;

namespace Aosta.Ava.Pages;

public partial class LocalEpisodeDetailsPage : ReactivePageBase<LocalEpisodeDetailsViewModel>
{
    public LocalEpisodeDetailsPage()
    {
        InitializeComponent();
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);

        MarkWatchedButton.IsVisible = !ViewModel!.Episode.Watched.HasValue;

        MarkWatchedButton.Tapped += (_, _) =>
        {
            ViewModel.MarkAsWatched();
            MarkWatchedButton.IsVisible = false;
        };
    }
}
