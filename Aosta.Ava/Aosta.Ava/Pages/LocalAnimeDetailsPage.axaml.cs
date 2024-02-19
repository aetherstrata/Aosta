// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using Aosta.Ava.ViewModels;

using Avalonia.Controls;
using Avalonia.Input;

namespace Aosta.Ava.Pages;

public partial class LocalAnimeDetailsPage : ReactivePageBase<LocalAnimeDetailsViewModel>
{
    public LocalAnimeDetailsPage()
    {
        InitializeComponent();
    }

    private void SelectingItemsControl_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        StatusPopup.IsOpen = false;
    }

    private void InputElement_OnTapped(object? sender, TappedEventArgs e)
    {
        ScorePopup.Close();
    }
}

