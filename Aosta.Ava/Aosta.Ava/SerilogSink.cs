// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Diagnostics;

using Avalonia.Logging;

using Serilog;

namespace Aosta.Ava;

public class SerilogSink(ILogger log) : ILogSink
{
    public bool IsEnabled(LogEventLevel level, string area)
    {
        return level > LogEventLevel.Verbose            // Do not log verbose events
               && area != "Layout"                      // Do not log layout passes
               && log.IsEnabled(toSerilog(level));
    }

    public void Log(LogEventLevel level, string area, object? source, string messageTemplate)
    {
        log.Write(toSerilog(level), messageTemplate);
    }

    public void Log(LogEventLevel level, string area, object? source, string messageTemplate, params object?[] propertyValues)
    {
        log.Write(toSerilog(level), messageTemplate, propertyValues);
    }

    private static Serilog.Events.LogEventLevel toSerilog(LogEventLevel level) => level switch
    {
        LogEventLevel.Verbose => Serilog.Events.LogEventLevel.Verbose,
        LogEventLevel.Debug => Serilog.Events.LogEventLevel.Debug,
        LogEventLevel.Information => Serilog.Events.LogEventLevel.Information,
        LogEventLevel.Warning => Serilog.Events.LogEventLevel.Warning,
        LogEventLevel.Error => Serilog.Events.LogEventLevel.Error,
        LogEventLevel.Fatal => Serilog.Events.LogEventLevel.Fatal,
        _ => throw new UnreachableException()
    };
}
