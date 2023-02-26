﻿using Aosta.Core.Data.Enums;
using JikanDotNet;
using Realms;

namespace Aosta.Core.Data.Models.Jikan;

/// <summary>
/// Model class for broadcasting details (e.g. day and time of broadcasting)
/// </summary>
public partial class BroadcastObject : IEmbeddedObject
{
    private int _Day { get; set; }

    /// <summary> The days the content is broadcasted on (e.g. mondays)</summary>
    [Ignored]
    public DaysOfWeek Day
    {
        get => (DaysOfWeek)_Day;
        set => _Day = (int)value;
    }

    private long? _Ticks { get; set; }

    [Ignored]
    public TimeOnly? LocalTime
    {
        get => _Ticks.HasValue ? new TimeOnly(_Ticks.Value) : null;
        set => _Ticks = value?.Ticks;
    }

    private string? _TimezoneId { get; set; }

    [Ignored]
    public TimeZoneInfo? Timezone
    {
        get => _TimezoneId is null ? null : TimeZoneInfo.FindSystemTimeZoneById(_TimezoneId);
        set => _TimezoneId = value?.Id;
    }

    [Ignored] public TimeOnly? UtcTime => Timezone is null ? LocalTime : LocalTime?.Add(-Timezone.BaseUtcOffset);

    public string String { get; set; } = string.Empty;

    /// <remarks>The parameterless constructor is made private because this model should only be created from a Jikan response.</remarks>
    private BroadcastObject() { }

    internal BroadcastObject(AnimeBroadcast broadcast)
    {
        Day = ParseDayOfWeek(broadcast.Day);
        LocalTime = broadcast.Time is null ? null : TimeOnly.Parse(broadcast.Time);
        Timezone = broadcast.Timezone is null ? null : TimeZoneInfo.FindSystemTimeZoneById(broadcast.Timezone);
        String = broadcast.String ?? string.Empty;
    }

    private static DaysOfWeek ParseDayOfWeek(string s) => s switch
    {
        "Mondays" => DaysOfWeek.Monday,
        "Tuesdays" => DaysOfWeek.Tuesday,
        "Wednesdays" => DaysOfWeek.Wednesday,
        "Thursdays" => DaysOfWeek.Thursday,
        "Fridays" => DaysOfWeek.Friday,
        "Saturdays" => DaysOfWeek.Saturday,
        "Sundays" => DaysOfWeek.Sunday,
        _ => DaysOfWeek.None
    };
}