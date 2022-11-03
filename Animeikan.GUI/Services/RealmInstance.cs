using Realms.Exceptions;
using Realms;
using System.Diagnostics;

namespace Animeikan.GUI.Services;

public sealed class RealmInstance
{
  private static readonly Lazy<RealmInstance> singleton = new Lazy<RealmInstance>(() => new RealmInstance());
  public static RealmInstance Singleton { get { return singleton.Value; } }

  private Realm db = null;
  public Realm Db { get { return db; } }

  private RealmInstance()
  {
    var config = new RealmConfiguration(Constants.Location.Database)
    {
      IsReadOnly = false
    };

    try
    {
      db = Realm.GetInstance(config);
    }
    catch (RealmFileAccessErrorException ex)
    {
      Debug.WriteLine($"Erorr creating/opening the realm.\n{ex.Message}");
    }
  }
}
