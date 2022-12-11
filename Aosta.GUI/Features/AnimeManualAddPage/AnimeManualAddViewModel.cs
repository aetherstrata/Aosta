using CommunityToolkit.Mvvm.ComponentModel;

using Realms;

using System.Windows.Input;
using Aosta.Core.Data;
using Aosta.Core.Extensions;
using Aosta.Core.Realm;

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
      var anime = new ContentDTO
      {
        Title = AnimeTitle,
        Score = int.Parse(AnimeScore),
        Type = ContentType.TV.ToStringCached()
      };

      Guid guid = anime.Id;


        await _realm.WriteAsync(() =>
        {
          guid = anime.Id;

          _realm.Add(anime);
        });

        AnimeTitleBack = _realm.Find<ContentDTO>(guid).Title;

    }
  });
}
