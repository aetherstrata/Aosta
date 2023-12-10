// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using Aosta.Core.Database.Enums;
using Aosta.Jikan.Models.Response;

using Realms;

namespace Aosta.Core.Database.Models.Local;

public partial class LocalAnime : IRealmObject
{
    [Indexed]
    internal int? _Type { get; set; }

    /// The type of this content. (Eg. Movie, OVA)
    public ContentType? Type
    {
        get => (ContentType?)_Type;
        set => _Type = (int?)value;
    }

    [Indexed]
    internal int? _AiringStatus { get; private set; }

    /// The airing status (e. g. "Currently Airing").
    [Ignored]
    public AiringStatus? AiringStatus
    {
        get => (AiringStatus?)_AiringStatus;
        set => _AiringStatus = (int?)value;
    }

    internal int? _Season { get; private set; } = 0;

    /// Seasons of the year the anime premiered.
    [Ignored]
    public Season? Season
    {
        get => (Season?)_Season;
        set => _Season = (int?)value;
    }

    /// The title of this content
    [Indexed]
    public string? Title { get; set; }

    /// The description of this content
    public string? Synopsis { get; set; }

    /// Content source (e .g. "Manga" or "Original").
    public string? Source { get; set; }

    /// Year the anime premiered.
    public int? Year { get; set; }
}
