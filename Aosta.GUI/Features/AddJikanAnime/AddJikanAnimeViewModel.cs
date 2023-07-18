using System.Collections.ObjectModel;
using System.Linq;
using Aosta.Common.Limiter;
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
    private readonly Throttler _throttler = new Throttler(1000);

    [ObservableProperty]
    private string _searchQuery;

    [ObservableProperty]
    private ObservableCollection<JikanResult> _animeList = new();

    public AddJikanAnimeViewModel(IJikan jikan)
    {
        _jikan = jikan;
    }

    [RelayCommand]
    private async Task UpdateList()
    {
        if (string.IsNullOrWhiteSpace(SearchQuery))
        {
            AnimeList = new ObservableCollection<JikanResult>();
        }
        else await _throttler.ThrottleAsync(async () =>
        {
            var response = await _jikan.SearchAnimeAsync(SearchQuery);
            AnimeList = new ObservableCollection<JikanResult>(response.Data
                .Select(result => new JikanResult
                {
                    MalID = result.MalId,
                    Title = result.Titles?.FirstOrDefault(entry => entry.Type.Equals("Default"), TitleEntryResponse.Empty).Title ?? "N/A",
                    ImageUrl = result.Images?.JPG?.ImageUrl ?? string.Empty
                }));
        });
    }

    public sealed class JikanResult
    {
        public required long MalID { get; init; }
        public required string Title { get; init; }
        public required string ImageUrl { get; init; }
    }
}