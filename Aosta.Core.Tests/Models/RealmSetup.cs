using System.Diagnostics;
using Aosta.Core.Data.Models;
using Realms;

namespace Aosta.Core.Tests.Models;

[SetUpFixture]
public class RealmSetup
{
    private static readonly string RealmPath = Path.Combine(AppContext.BaseDirectory, "realms");

    [OneTimeSetUp]
    public void Initialize()
    {
        if (Directory.Exists(RealmPath)) Directory.Delete(RealmPath, true);
        Directory.CreateDirectory(RealmPath);
    }

    internal static RealmConfiguration NewConfig()
    {
        return new RealmConfiguration(Path.Combine(AppContext.BaseDirectory, "realms", $"{Guid.NewGuid()}.realm"));
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

            case InitConfig.OneContent:
                realm.Write(() => { realm.Add(new ContentObject()); });
                break;

            default:
                throw new UnreachableException();
        }

        return Realm.GetInstance(cfg);
    }
}

public enum InitConfig
{
    Empty,
    OneContent
}