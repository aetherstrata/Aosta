// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using Aosta.Ava.ViewModels;

using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Aosta.Ava.Pages;

public partial class LocalEpisodeDetailsPage : ReactivePageBase<LocalEpisodeDetailsViewModel>
{
    public LocalEpisodeDetailsPage()
    {
        InitializeComponent();
    }

    private void Control_OnLoaded(object? sender, RoutedEventArgs e)
    {
        var button = (Button)sender!;

        button.IsVisible = !ViewModel!.Episode.Watched.HasValue;

        button.Tapped += (_, _) =>
        {
            ViewModel.MarkAsWatched();
            button.IsVisible = false;
        };
    }
}
