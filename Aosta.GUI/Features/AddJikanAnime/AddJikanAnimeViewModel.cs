using System.Collections.ObjectModel;
using Aosta.Core;
using Aosta.Jikan;
using Aosta.Jikan.Models.Response;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Realms;

namespace Aosta.GUI.Features.AddJikanAnime;

public partial class AddJikanAnimeViewModel : ObservableObject
{
    private readonly IJikan _jikan;

    [ObservableProperty]
    private string _searchQuery;

    [ObservableProperty]
    private List<AnimeResponse>? _animeList;

    public AddJikanAnimeViewModel(IJikan jikan)
    {
        _jikan = jikan;
    }

    [RelayCommand]
    private void UpdateList()
    {
        Task.Run(async () =>
        {
            var response = await _jikan.SearchAnimeAsync(SearchQuery);
            AnimeList = response.Data.ToList();
        });
    }
}