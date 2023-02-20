using CommunityToolkit.Mvvm.ComponentModel;

using Realms;

using System.Windows.Input;
using Aosta.Core.Data;
using Aosta.Core.Data.Realm;

namespace Aosta.GUI.ViewModels;

[ObservableObject]
public partial class AnimeManualAddViewModel
{
  private Realm _realm;

  [ObservableProperty]
  private string _animeTitle = string.Empty;

  [ObservableProperty]
  private string _animeScore = string.Empty;

  [ObservableProperty]
  private bool _isScoreValid;

  [ObservableProperty]
  private string _animeTitleBack = string.Empty;

  public AnimeManualAddViewModel()
  {
    this._realm = App.Core.GetInstance();
  }

  public ICommand AddToRealm => new Command(async () =>
  {
    if (IsScoreValid)
    {
      var anime = new AnimeObject
      {
        Title = AnimeTitle,
        Score = string.IsNullOrWhiteSpace(AnimeScore) ? -1 :int.Parse(AnimeScore),
        Type = ContentType.TV
      };

      Guid guid = anime.Id;


        await _realm.WriteAsync(() =>
        {
          guid = anime.Id;

          _realm.Add(anime);
        });

        AnimeTitleBack = _realm.Find<AnimeObject>(guid).Title;

    }
  });
}
