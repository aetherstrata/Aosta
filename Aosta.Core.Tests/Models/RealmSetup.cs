using Aosta.Core.Database.Models;
using Realms;

namespace Aosta.Core.Tests.Models;

internal static class RealmSetup
{
    private static readonly string s_InstancePath = Path.Combine(AppContext.BaseDirectory, "instances");
    private static string newInstancePath => Path.Combine(s_InstancePath, $"{Guid.NewGuid()}");

    private static readonly string s_RealmPath = Path.Combine(AppContext.BaseDirectory, "realms");
    private static string newRealmPath => Path.Combine(s_RealmPath, $"{Guid.NewGuid()}.realm");

    internal static AostaDotNet NewInstance()
    {
        string path = newInstancePath;
        Directory.CreateDirectory(path);
        return new AostaConfiguration(path).Build();
    }

    internal static Realm NewRealm()
    {
        return Realm.GetInstance(newConfig());
    }

    internal static AostaDotNet AddAnime(this AostaDotNet aosta, Anime? anime = null)
    {
        aosta.Write(r => r.Add(anime ?? new Anime()));

        return aosta;
    }

    [SetUpFixture]
    public class RealmTests
    {
        [OneTimeSetUp]
        public void Initialize()
        {
            if (Directory.Exists(s_InstancePath)) Directory.Delete(s_InstancePath, true);
            Directory.CreateDirectory(s_InstancePath);

            if (Directory.Exists(s_RealmPath)) Directory.Delete(s_RealmPath, true);
            Directory.CreateDirectory(s_RealmPath);
        }

    }

    private static RealmConfiguration newConfig()
    {
        return new RealmConfiguration(newRealmPath);
    }
}
