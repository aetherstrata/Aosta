using System.Windows.Input;
using Aosta.Core.Data.Enums;
using Aosta.Core.Data.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Aosta.GUI.Features.AnimeManualAddPage;

public partial class AnimeManualAddViewModel : ObservableObject
{
    [ObservableProperty]
    private string _animeScore = string.Empty;

    [ObservableProperty]
    private string _animeTitle = string.Empty;

    [ObservableProperty]
    private string _animeTitleBack = string.Empty;

    [ObservableProperty]
    private bool _isScoreValid;

    private readonly CancellationTokenSource _cts = new();

    public AnimeManualAddViewModel()
    {

    }

    public ICommand AddToRealm => new Command(async () =>
    {
        var token = _cts.Token;

        if (IsScoreValid)
        {
            var anime = new ContentObject()
            {
                Title = AnimeTitle,
                Score = string.IsNullOrWhiteSpace(AnimeScore) ? -1 : int.Parse(AnimeScore),
                Type = ContentType.TV
            };

            var guid = anime.Id;


            Guid id = await App.Core.WriteContentAsync(anime, token);

            var palle = App.Core.GetInstance().All<ContentObject>();

            _cts.Dispose();

            AnimeTitleBack = App.Core.GetInstance().Find<ContentObject>(guid).Title;
        }
    });
}