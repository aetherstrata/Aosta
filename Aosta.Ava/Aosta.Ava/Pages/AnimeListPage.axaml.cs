// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using Aosta.Ava.ViewModels;

using Avalonia.Interactivity;
using Avalonia.ReactiveUI;


namespace Aosta.Ava.Pages;

public partial class AnimeListPage : ReactiveUserControl<AnimeListPageViewModel>
{
    public AnimeListPage()
    {
        InitializeComponent();
    }

    /// <inheritdoc />
    protected override void OnUnloaded(RoutedEventArgs e)
    {
        base.OnUnloaded(e);
        ViewModel?.Dispose();
    }
}
