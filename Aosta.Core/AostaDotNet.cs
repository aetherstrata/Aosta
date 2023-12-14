using Aosta.Core.Data;
using Aosta.Core.Database;
using Aosta.Core.Database.Models;

using Realms;
using Serilog;

namespace Aosta.Core;

/// <summary>
/// Facade class that exposes core functionality to consumer applications
/// </summary>
public class AostaDotNet
{
    /// <summary> Serilog logger instance </summary>
    public ILogger Log { get; }

    /// <summary> Realm access manager </summary>
    public RealmAccess Realm { get; }

    /// <summary> Explicit parameterless constructor internal to forbid direct initialization </summary>
    internal AostaDotNet(ILogger log, RealmConfigurationBase realmConfig)
    {
        Log = log;
        Realm = new RealmAccess(log, realmConfig);
    }
}
