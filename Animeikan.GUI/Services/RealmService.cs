using Realms;
using Realms.Exceptions;

using System.Diagnostics;

namespace Animeikan.GUI.Services;

public class RealmService
{
  private static Realm db = RealmInstance.Singleton.Db;
}
