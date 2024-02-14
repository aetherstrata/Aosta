// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace Aosta.Ava.ViewModels.Card;

public interface IOnlineCard
{
    /// <summary>
    /// Url of the image.
    /// </summary>
    public string BannerUrl { get; }

    public const string PORTRAIT_PLACEHOLDER = "avares://Aosta.Ava/Assets/Images/portrait-placeholder.png";
}
