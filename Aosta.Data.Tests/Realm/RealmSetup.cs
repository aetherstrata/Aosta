using Aosta.Data.Database.Models;

using Serilog;

using Anime = Aosta.Data.Models.Anime;

namespace Aosta.Data.Tests.Realm;

[SetUpFixture]
internal class RealmSetup
{
    private static readonly string s_RealmsPath = Path.Combine(AppContext.BaseDirectory, "realms");
    private static string newRealmPath() => Path.Combine(s_RealmsPath, $"{Guid.NewGuid()}.realm");

    internal static RealmAccess NewInstance()
    {
        string path = newRealmPath();
        return new RealmAccess(path, Log.Logger);
    }

    [OneTimeSetUp]
    public void Initialize()
    {
        if (Directory.Exists(s_RealmsPath)) Directory.Delete(s_RealmsPath, true);
        Directory.CreateDirectory(s_RealmsPath);
    }
}

internal static class RealmAccessExtensions
{
    internal static RealmAccess AddAnime(this RealmAccess realm, Anime? anime = null)
    {
        realm.Write(r => r.Add(anime ?? new Anime()));

        return realm;
    }
}
