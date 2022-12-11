namespace Aosta.Core.Realm;

using Realms;

public sealed class RealmAccess
{
    private static readonly Lazy<RealmAccess> Lazy = new(() => new RealmAccess());

    public static RealmAccess Singleton => Lazy.Value;

    private RealmAccess()
    {

    }

    public Realm GetInstance(DatabaseConfiguration databaseConfiguration) =>
        Realm.GetInstance(databaseConfiguration.Configuration);
}