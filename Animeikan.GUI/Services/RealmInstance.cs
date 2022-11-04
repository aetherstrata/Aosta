using Realms.Exceptions;
using Realms;
using System.Diagnostics;

namespace Animeikan.GUI.Services;

public sealed class RealmInstance
{
  private static readonly Lazy<RealmInstance> manager = new(() => new RealmInstance());
  public static RealmInstance Manager { get { return manager.Value; } }

  private static readonly RealmConfiguration Config = 
    new RealmConfiguration(Globals.Location.Database)
    {
      IsReadOnly = false
    };

  private ThreadLocal<Realm> db = new();

  public Realm Db 
  { 
    get 
    { 
      if (!db.IsValueCreated) Init();
      return db.Value; 
    }
  }

  private void Init()
  {
    try
    {
      db = new ThreadLocal<Realm>(() => 
      {
        return Realm.GetInstance(Config);
      });
    }
    catch (RealmFileAccessErrorException ex)
    {
      Debug.WriteLine($"Erorr creating/opening the realm.\n{ex.Message}");
    }
  }
}
