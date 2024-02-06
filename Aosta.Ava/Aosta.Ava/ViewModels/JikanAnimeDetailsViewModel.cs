// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.ObjectModel;
using System.Reactive;
using System.Threading;
using System.Threading.Tasks;

using Aosta.Ava.Extensions;
using Aosta.Ava.Localization;
using Aosta.Data.Database.Mapper;
using Aosta.Jikan;
using Aosta.Jikan.Models.Response;

using Avalonia.ReactiveUI;

using DynamicData;

using ReactiveUI;
using ReactiveUI.Fody.Helpers;

using Splat;

namespace Aosta.Ava.ViewModels;

public class JikanAnimeDetailsViewModel : ReactiveObject, IRoutableViewModel
{
    /// <inheritdoc />
    public string? UrlPathSegment { get; }

    /// <inheritdoc />
    public IScreen HostScreen { get; }

    private readonly IJikan _jikan = Locator.Current.GetSafely<IJikan>();
    private readonly AnimeResponse _response;

    public JikanAnimeDetailsViewModel(IScreen hostScreen, AnimeResponse response)
    {
        _response = response;

        UrlPathSegment = $"jikan-details-{response.MalId}";
        HostScreen = hostScreen;

        UpdateEpisodesList = ReactiveCommand.CreateFromTask(updateEpisodesList, outputScheduler: AvaloniaScheduler.Instance);

        _ = updateEpisodesList();
    }

    public string? Banner => _response.Images?.JPG?.ImageUrl;

    public string? LargeBanner => _response.Images?.JPG?.LargeImageUrl;

    public string Score => _response.Score?.ToString("0.00") ?? LocalizedString.NA;

    public LocalizedString Season => _response.Season?.Localize() ?? LocalizedString.NOT_AVAILABLE;

    public string Synopsis => _response.Synopsis ?? LocalizedString.NOT_AVAILABLE;

    public string Title => _response.Titles.GetDefault() ?? LocalizedString.NA;

    public LocalizedString Type => _response.Type?.Localize() ?? LocalizedString.NA;

    public string? Year => _response.Year.ToString();

    public ObservableCollection<AnimeEpisodeResponse> Episodes { get; } = [];

    public ReactiveCommand<Unit,Unit> UpdateEpisodesList { get; }

    [Reactive]
    public bool IsLoadEpisodesButtonVisible { get; set; }

    private int _page;
    private async Task updateEpisodesList(CancellationToken ct = default)
    {
        _page++;

        this.Log().Debug<JikanAnimeDetailsViewModel>("Getting episodes page {Page} for anime {Id}", _page, _response.MalId);

        var result = await _jikan.GetAnimeEpisodesAsync(_response.MalId, _page, ct);

        Episodes.AddRange(result.Data);

        if (result.Pagination.HasNextPage) IsLoadEpisodesButtonVisible = true;
    }
}
