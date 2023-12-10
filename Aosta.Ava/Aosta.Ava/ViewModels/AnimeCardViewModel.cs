// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using Aosta.Jikan.Models.Response;

using ReactiveUI;

namespace Aosta.Ava.ViewModels;

public class AnimeCardViewModel : ReactiveObject
{
    public AnimeCardViewModel(AnimeResponse response)
    {
        _response = response;

        Banner = response.Images?.JPG?.ImageUrl;
    }

    private readonly AnimeResponse _response;

    public string? Banner { get; }
}
