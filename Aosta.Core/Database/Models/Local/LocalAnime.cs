// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using Aosta.Core.Database.Enums;

using Realms;

namespace Aosta.Core.Database.Models.Local;

public partial class LocalAnime : IRealmObject
{
    [Indexed]
    internal int? _type { get; set; }

    /// The type of this content. (Eg. Movie, OVA)
    public ContentType? Type
    {
        get => (ContentType?)_type;
        set => _type = (int?)value;
    }

    [Indexed]
    internal int? _airingStatus { get; private set; }

    /// The airing status (e. g. "Currently Airing").
    [Ignored]
    public AiringStatus? AiringStatus
    {
        get => (AiringStatus?)_airingStatus;
        set => _airingStatus = (int?)value;
    }

    internal int? _season { get; private set; } = 0;

    /// Seasons of the year the anime premiered.
    [Ignored]
    public Season? Season
    {
        get => (Season?)_season;
        set => _season = (int?)value;
    }

    /// The title of this content
    [Indexed]
    public string? DefaultTitle { get; set; }

    /// The description of this content
    public string? Synopsis { get; set; }

    /// Content source (e .g. "Manga" or "Original").
    public string? Source { get; set; }

    /// Year the anime premiered.
    public int? Year { get; set; }
}
