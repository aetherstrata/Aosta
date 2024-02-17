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

    public string Title { get; set; }

    public string Note { get; set; }

    [Indexed]
    [MapTo(nameof(PointInTime))]
    private long pointInTime { get; set; }

    public static IComparer<EpisodeNote> PointInTimeComparer { get; } = new TimeComparer();

    private sealed class TimeComparer : IComparer<EpisodeNote>
    {
        public int Compare(EpisodeNote? x, EpisodeNote? y)
        {
            if (ReferenceEquals(x, y)) return 0;
            if (ReferenceEquals(null, y)) return 1;
            if (ReferenceEquals(null, x)) return -1;
            return x.pointInTime.CompareTo(y.pointInTime);
        }
    }
}
