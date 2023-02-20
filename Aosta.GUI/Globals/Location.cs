namespace Aosta.GUI.Globals;

public static class Location
{
  public static readonly string AppData = FileSystem.Current.AppDataDirectory;
  public static readonly string CacheDir = FileSystem.Current.CacheDirectory;
  public static readonly string Database = Path.Combine(AppData, "Aosta.realm");
}
