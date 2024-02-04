// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace Aosta.Ava.ViewModels.Contracts;

public interface IEpisodeDetails
{
    string Title { get; }

    string? Synopsis { get; }
}
