using System.Diagnostics;
using Aosta.Core.Data.Realm;
using Realms;

namespace Aosta.Core.Tests.Fixture;

internal static class RealmFixture
{
    internal static RealmConfiguration CreateNewConfig() =>
        new(Path.Combine(AppContext.BaseDirectory, "realms", Guid.NewGuid().ToString(), "test.realm"))
        {
            IsReadOnly = false,
            ShouldDeleteIfMigrationNeeded = true
        };

    internal static Realm CreateNewRealm(InitConfig init = InitConfig.Empty) =>
        CreateNewRealm(CreateNewConfig(), init);

    internal static Realm CreateNewRealm(RealmConfiguration cfg, InitConfig init = InitConfig.Empty)
    {
        string path = cfg.DatabasePath;
        Directory.CreateDirectory(path.Remove(path.Length - 10));

        switch (init)
        {
            case InitConfig.Empty: break;

            case InitConfig.OneAnime:
                using (var realm = Realm.GetInstance(cfg))
                {
                    realm.Write(() => { realm.Add(new AnimeObject()); });
                }
                break;

            default: throw new UnreachableException();
        }

        return Realm.GetInstance(cfg);
    }

    internal static void Clean()
    {
        Directory.CreateDirectory(Path.Combine(AppContext.BaseDirectory, "realms"));
        foreach (var dir in Directory.EnumerateDirectories(Path.Combine(AppContext.BaseDirectory, "realms")))
        {
            Directory.Delete(dir, true);
        }
    }

    internal static T First<T>(this Realm realm) where T : RealmObject
    {
        return realm.All<T>().First();
    }
}