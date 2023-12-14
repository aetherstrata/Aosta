// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using Aosta.Ava.ViewModels;

using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;

namespace Aosta.Ava.Pages;

public partial class JikanAnimeDetailsPage : ReactiveUserControl<JikanAnimeDetailsViewModel>
{
    public JikanAnimeDetailsPage()
    {
        InitializeComponent();
    }
}

