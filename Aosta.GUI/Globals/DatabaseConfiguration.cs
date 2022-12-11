using Aosta.Core.Realm;
using Realms;
using Location = Aosta.GUI.Globals.Location;

namespace Aosta.GUI.Globals;

public class DbConfiguration : DatabaseConfiguration
{
    public static string DbLocation { get; } = Globals.Location.Database;

    public static RealmConfiguration Configuration { get; } = new(DbLocation)
    {
        SchemaVersion = 1,
        IsReadOnly = false,
        ShouldDeleteIfMigrationNeeded = true

    };
}