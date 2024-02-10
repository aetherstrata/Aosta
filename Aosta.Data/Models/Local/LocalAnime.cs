// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using Aosta.Data.Enums;

using Realms;

namespace Aosta.Data.Models.Local;

public partial class LocalAnime : IEmbeddedObject
{
    [Indexed]
    [MapTo(nameof(Type))]
    private int? type { get; set; }

    /// The type of this content. (e.g. Movie, OVA)
    public ContentType? Type
    {
        get => (ContentType?)type;
        set => type = (int?)value;
    }

    [Indexed]
    [MapTo(nameof(AiringStatus))]
    private int? airingStatus { get; set; }

    /// The airing status (e.g. "Currently Airing").
    [Ignored]
    public AiringStatus? AiringStatus
    {
        get => (AiringStatus?)airingStatus;
        set => airingStatus = (int?)value;
    }

    [MapTo(nameof(Season))]
    private int? season { get; set; } = 0;

    /// Seasons of the year the anime aired.
    [Ignored]
    public Season? Season
    {
        get => (Season?)season;
        set => season = (int?)value;
    }

    /// The local title of this content
    [Indexed]
    public string? Title { get; set; }

    /// The description of this content
    public string? Synopsis { get; set; }

    /// Content source (e .g. "Manga" or "Original").
    public string? Source { get; set; }

    /// Year the anime premiered.
    public int? Year { get; set; }
}
