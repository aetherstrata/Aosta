// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using Realms;

namespace Aosta.Data.Models;

public partial class EpisodeNote : IEmbeddedObject
{
    public TimeSpan PointInTime
    {
        get => TimeSpan.FromTicks(pointInTime);
        set => pointInTime = value.Ticks;
    }

    public string Note { get; set; }

    [Indexed]
    [MapTo(nameof(PointInTime))]
    private long pointInTime { get; set; }
}
