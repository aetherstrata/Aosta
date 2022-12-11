using Realms;

namespace Aosta.Core.Realm;

public class DatabaseConfiguration
{
    public string Location { get; init; }

    public RealmConfiguration Configuration { get; init; }
}