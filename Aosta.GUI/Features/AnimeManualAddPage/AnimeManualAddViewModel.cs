using System.Windows.Input;
using Aosta.Core;
using Aosta.Core.Database.Models.Jikan;
using Aosta.Core.Database.Enums;
using Aosta.Core.Database.Models;
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

    private CancellationTokenSource _cts = null!;

    private readonly AostaDotNet _aosta;

    public AnimeManualAddViewModel(AostaDotNet aosta)
    {
        _aosta = aosta;
    }

    public ICommand AddToRealm => new Command(async () =>
    {
        _cts = new();

        var token = _cts.Token;

        var anime = new Anime()
        {
            Title = AnimeTitle,
            Score = float.TryParse(AnimeScore, out float score) ? -1 : (int)Math.Floor(score*10),
            Type = ContentType.TV
        };

        var guid = anime.Id;

        Guid id = await _aosta.CreateLocalContentAsync(anime, token);

        AnimeTitleBack = _aosta.GetInstance().Find<Anime>(guid).Title;

        _cts.Dispose();
    });
}
