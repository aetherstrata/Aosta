﻿using System.Windows.Input;
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

    private readonly CancellationTokenSource _cts = new();

    public AnimeManualAddViewModel()
    {

    }

    public ICommand AddToRealm => new Command(async () =>
    {
        var token = _cts.Token;

        var anime = new ContentObject()
        {
            Title = AnimeTitle,
            Score = float.TryParse(AnimeScore, out float score) ? -1 : (int)Math.Floor(score*10),
            Type = ContentType.TV
        };

        var guid = anime.Id;

        Guid id = await App.Core.WriteContentAsync(anime, token);

        AnimeTitleBack = App.Core.GetInstance().Find<ContentObject>(guid).Title;

        _cts.Dispose();
    });
}
