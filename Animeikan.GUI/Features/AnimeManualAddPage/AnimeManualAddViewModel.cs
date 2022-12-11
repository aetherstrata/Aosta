using Animeikan.GUI.Models;

using CommunityToolkit.Mvvm.ComponentModel;

using Realms;

using System.Windows.Input;

namespace Animeikan.GUI.ViewModels;

[ObservableObject]
public partial class AnimeManualAddViewModel
{
  [ObservableProperty]
  private string _animeTitle = string.Empty;

  [ObservableProperty]
  private string _animeScore = string.Empty;

  [ObservableProperty]
  private bool _isScoreValid;

  [ObservableProperty]
  private string _animeTitleBack = string.Empty;

  public ICommand AddToRealm => new Command(async () =>
  {
    if (IsScoreValid)
    {
      var anime = new AnimeModel()
      {
        Title = AnimeTitle,
        Score = int.Parse(AnimeScore)
      };

      Guid guid = anime.Id;

      using (var db = Realm.GetInstance(Globals.RealmConfig.Default))
      {
        await db.WriteAsync(() =>
        {
          guid = anime.Id;

          db.Add(anime);
        });

        AnimeTitleBack = db.Find<AnimeModel>(guid).Title;
      }
    }
  });
}
