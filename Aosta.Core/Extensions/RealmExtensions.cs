using Realms;

namespace Aosta.Core.Extensions;

public static class RealmExtensions
{
    public static T First<T>(this Realm realm) where T : IRealmObject =>
        realm.All<T>().First();

    public static T? FirstOrDefault<T>(this Realm realm) where T : IRealmObject =>
        realm.All<T>().FirstOrDefault();
}