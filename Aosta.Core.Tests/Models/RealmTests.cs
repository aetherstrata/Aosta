using System.Diagnostics;
using Aosta.Core.Data.Models;
using Realms;

namespace Aosta.Core.Tests.Models;

internal static class RealmSetup
{
    private static readonly string InstancePath = Path.Combine(AppContext.BaseDirectory, "instances");

    private static string NewInstancePath => Path.Combine(InstancePath, $"{Guid.NewGuid()}");

    private static readonly string RealmPath = Path.Combine(AppContext.BaseDirectory, "realms");

    private static string NewRealmPath => Path.Combine(RealmPath, $"{Guid.NewGuid()}.realm");

    internal static AostaDotNet NewInstance()
    {
        string path = NewInstancePath;
        Directory.CreateDirectory(path);
        return new AostaConfiguration(path).Build();
    }

    internal static RealmConfiguration NewConfig()
    {
        return new RealmConfiguration(NewRealmPath);
    }

    internal static Realm CreateNewRealm(InitConfig init = InitConfig.Empty)
    {
        return CreateNewRealm(NewConfig(), init);
    }

    internal static Realm CreateNewRealm(RealmConfigurationBase cfg, InitConfig init = InitConfig.Empty)
    {
        using var realm = Realm.GetInstance(cfg);

        switch (init)
        {
            case InitConfig.Empty:
                break;

            case InitConfig.OneAnime:
                realm.Write(() =>
                {
                    realm.Add(new AnimeObject());
                });
                break;

            default:
                throw new UnreachableException();
        }

        return Realm.GetInstance(cfg);
    }

    [SetUpFixture]
    public class RealmTests
    {
        [OneTimeSetUp]
        public void Initialize()
        {
            if (Directory.Exists(RealmSetup.RealmPath)) Directory.Delete(RealmPath, true);
            Directory.CreateDirectory(RealmPath);
        }
    }
}

public enum InitConfig
{
    Empty,
    OneAnime
}