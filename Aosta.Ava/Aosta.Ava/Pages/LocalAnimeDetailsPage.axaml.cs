// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Reactive.Linq;

using Aosta.Ava.ViewModels;

using Avalonia.Input;

namespace Aosta.Ava.Pages;

public partial class LocalAnimeDetailsPage : ReactivePageBase<LocalAnimeDetailsViewModel>
{
    public LocalAnimeDetailsPage()
    {
        InitializeComponent();
    }
}

