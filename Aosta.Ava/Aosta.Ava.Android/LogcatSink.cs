using System;
using System.IO;

using Serilog;
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Formatting.Display;

namespace Aosta.Ava.Android;

internal class LogcatSink(
    string tag,
    ITextFormatter formatter,
    LogEventLevel minimumLevel,
    LoggingLevelSwitch? levelSwitch)
    : ILogEventSink
{
    private readonly string _tag = tag ?? throw new ArgumentNullException(nameof(tag));

    public void Emit(LogEvent logEvent)
    {
        if (logEvent.Level < minimumLevel) return;

        using var writer = new StringWriter();

        formatter.Format(logEvent, writer);

        string msg = writer.ToString();

        switch (levelSwitch?.MinimumLevel ?? logEvent.Level)
        {
            case LogEventLevel.Verbose:
                global::Android.Util.Log.Verbose(_tag, msg);
                break;
            case LogEventLevel.Debug:
                global::Android.Util.Log.Debug(_tag, msg);
                break;
            case LogEventLevel.Information:
                global::Android.Util.Log.Info(_tag, msg);
                break;
            case LogEventLevel.Warning:
                global::Android.Util.Log.Warn(_tag, msg);
                break;
            case LogEventLevel.Fatal:
            case LogEventLevel.Error:
                global::Android.Util.Log.Error(_tag, msg);
                break;
        }
    }
}

/// <summary>
/// Adds the WriteTo.Logcat() extension method to <see cref="LoggerConfiguration"/>.
/// </summary>
public static class LogcatConfigurationExtensions
{
    private const string default_console_output_template = "{Message:lj}{NewLine}{Exception}";

    /// <summary>
    /// Writes log events to <see cref="System.Console"/>.
    /// </summary>
    /// <param name="sinkConfiguration">Logger sink configuration.</param>
    /// <param name="restrictedToMinimumLevel">The minimum level for
    /// events passed through the sink. Ignored when <paramref name="levelSwitch"/> is specified.</param>
    /// <param name="outputTemplate">A message template describing the format used to write to the sink.
    /// The default is <code>"[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"</code>.</param>
    /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
    /// <param name="levelSwitch">A switch allowing the pass-through minimum level
    /// to be changed at runtime.</param>
    /// <exception cref="ArgumentNullException">When <paramref name="sinkConfiguration"/> is <code>null</code></exception>
    /// <exception cref="ArgumentNullException">When <paramref name="outputTemplate"/> is <code>null</code></exception>
    public static LoggerConfiguration Logcat(
        this LoggerSinkConfiguration sinkConfiguration,
        string tag,
        string outputTemplate = default_console_output_template,
        LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
        LoggingLevelSwitch? levelSwitch = null,
        IFormatProvider? formatProvider = null)
    {
        if (sinkConfiguration is null) throw new ArgumentNullException(nameof(sinkConfiguration));
        if (outputTemplate is null) throw new ArgumentNullException(nameof(outputTemplate));

        var formatter = new MessageTemplateTextFormatter(outputTemplate, formatProvider);

        return sinkConfiguration.Sink(new LogcatSink(tag, formatter, restrictedToMinimumLevel, levelSwitch));
    }

    /// <summary>
    /// Writes log events to <see cref="System.Console"/>.
    /// </summary>
    /// <param name="sinkConfiguration">Logger sink configuration.</param>
    /// <param name="formatter">Controls the rendering of log events into text, for example to log JSON. To
    /// control plain text formatting, use the overload that accepts an output template.</param>
    /// <param name="restrictedToMinimumLevel">The minimum level for
    /// events passed through the sink. Ignored when <paramref name="levelSwitch"/> is specified.</param>
    /// <param name="levelSwitch">A switch allowing the pass-through minimum level
    /// to be changed at runtime.</param>
    /// <returns>Configuration object allowing method chaining.</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="sinkConfiguration"/> is <code>null</code></exception>
    /// <exception cref="ArgumentNullException">When <paramref name="formatter"/> is <code>null</code></exception>
    public static LoggerConfiguration Logcat(
        this LoggerSinkConfiguration sinkConfiguration,
        string tag,
        ITextFormatter formatter,
        LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
        LoggingLevelSwitch? levelSwitch = null)
    {
        if (sinkConfiguration is null) throw new ArgumentNullException(nameof(sinkConfiguration));
        if (formatter is null) throw new ArgumentNullException(nameof(formatter));

        return sinkConfiguration.Sink(new LogcatSink(tag, formatter, restrictedToMinimumLevel, levelSwitch));
    }
}
