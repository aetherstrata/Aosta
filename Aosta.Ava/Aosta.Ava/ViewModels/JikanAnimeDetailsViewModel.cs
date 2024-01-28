// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Threading;
using System.Threading.Tasks;

using Aosta.Ava.Extensions;
using Aosta.Ava.Localization;
using Aosta.Core.Database.Mapper;
using Aosta.Core.Database.Models.Jikan;
using Aosta.Core.Extensions;
using Aosta.Jikan;
using Aosta.Jikan.Models.Response;

using Avalonia.ReactiveUI;

using DynamicData;

using ReactiveUI;

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

    private int _page;

    public JikanAnimeDetailsViewModel(IScreen hostScreen, AnimeResponse response)
    {
        _response = response;

        UrlPathSegment = $"jikan-details-{response.MalId}";
        HostScreen = hostScreen;
        GoBack = HostScreen.Router.NavigateBack;

        UpdateEpisodesList = ReactiveCommand.CreateFromTask(updateEpisodesList, outputScheduler: AvaloniaScheduler.Instance);

        _ = updateEpisodesList();
    }

    private async Task updateEpisodesList(CancellationToken ct = default)
    {
        _page++;

        this.Log().Debug<JikanAnimeDetailsViewModel>("Getting page {Page} for anime {Id}", _page, _response.MalId);

        var result = await _jikan.GetAnimeEpisodesAsync(_response.MalId, _page, ct);

        Episodes.AddRange(result.Data);
    }

    public string Title => _response.Titles.GetDefault() ?? "N/A";

    public string Synopsis => _response.Synopsis ?? Localizer.Instance["Label.NotAvailable"];

    public string? Banner => _response.Images?.JPG?.ImageUrl;

    public ObservableCollection<AnimeEpisodeResponse> Episodes { get; } = [];

    public ReactiveCommand<Unit, IRoutableViewModel> GoBack { get; }

    public ReactiveCommand<Unit,Unit> UpdateEpisodesList { get; }
}
