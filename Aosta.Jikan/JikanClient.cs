﻿using System.Text.Json;

using Aosta.Common.Consts;
using Aosta.Common.Limiter;
using Aosta.Jikan.Enums;
using Aosta.Jikan.Exceptions;
using Aosta.Jikan.Models;
using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;
using Aosta.Jikan.Query;
using Aosta.Jikan.Query.Builder.Anime;
using Aosta.Jikan.Query.Builder.Character;
using Aosta.Jikan.Query.Builder.Club;
using Aosta.Jikan.Query.Builder.Genre;
using Aosta.Jikan.Query.Builder.Magazine;
using Aosta.Jikan.Query.Builder.Manga;
using Aosta.Jikan.Query.Builder.Person;
using Aosta.Jikan.Query.Builder.Producer;
using Aosta.Jikan.Query.Builder.Random;
using Aosta.Jikan.Query.Builder.Recommendations;
using Aosta.Jikan.Query.Builder.Reviews;
using Aosta.Jikan.Query.Builder.Schedule;
using Aosta.Jikan.Query.Builder.Season;
using Aosta.Jikan.Query.Builder.Top;
using Aosta.Jikan.Query.Builder.User;
using Aosta.Jikan.Query.Builder.Watch;
using Aosta.Jikan.Query.Enums;
using Aosta.Jikan.Query.Parameters;

using Serilog;
using Serilog.Context;

namespace Aosta.Jikan;

/// <summary>
/// Implementation of Jikan wrapper for .Net platform.
/// </summary>
public sealed class JikanClient : IJikan, IDisposable
{
    private readonly string _baseAddress;
    private readonly HttpClient _http;
    private readonly ITaskLimiter _limiter;
    private readonly ILogger? _logger;

    private async Task<T> getRequestAsync<T>(IQuery<T> query, CancellationToken ct = default)
    {
        string queryEndpoint = query.GetQuery();
        string fullUrl = _http.BaseAddress + queryEndpoint;

        using (LogContext.PushProperty("Endpoint", queryEndpoint))
        {
            try
            {
                _logger?.Debug("Performing GET request: \"{Request}\"", fullUrl);
                using var response = await _limiter.LimitAsync(() => _http.GetAsync(queryEndpoint, ct))
                    .ConfigureAwait(false);

                string json = await response.Content.ReadAsStringAsync(ct).ConfigureAwait(false);

                using (LogContext.PushProperty("Json", json))
                {
                    _logger?.Verbose("Content retrieved successfully");
                }

                if (response.IsSuccessStatusCode)
                {
                    _logger?.Debug("Got HTTP response for \"{Request}\" successfully", fullUrl);
                    return JsonSerializer.Deserialize<T>(json) ?? throw new JikanRequestException(
                        ErrorMessages.SERIALIZATION_NULL_RESULT + Environment.NewLine + "Raw JSON string:" +
                        Environment.NewLine + json);
                }

                var errorData = JsonSerializer.Deserialize<JikanApiError>(json);

                using (LogContext.PushProperty("Response", errorData))
                {
                    _logger?.Error("Failed to get HTTP resource for \"{Resource}\", Status Code: {Status}",
                        queryEndpoint, response.StatusCode);
                }

                throw new JikanRequestException(
                    string.Format(ErrorMessages.FAILED_REQUEST, response.StatusCode, response.Content), errorData);
            }
            catch (JsonException ex)
            {
                _logger?.Error(ex, ErrorMessages.SERIALIZATION_FAILED);
                throw;
            }
        }
    }

    /// <summary>
    ///     Constructor.
    /// </summary>
    /// <param name="http"> Static http client instance.</param>
    /// <param name="taskLimiter"> API limiter </param>
    /// <param name="logger"> Serilog logger </param>
    internal JikanClient(HttpClient http, ITaskLimiter taskLimiter, ILogger? logger)
    {
        _baseAddress = http.BaseAddress!.ToString();
        _http = http;
        _limiter = taskLimiter;
        _logger = logger;
    }

    /// <summary>
    ///     Perform disposal of the current client instance.
    /// </summary>
    /// <remarks>
    ///     This operation closes open HTTP connections and cancels any pending API request.
    /// </remarks>
    public void Dispose()
    {
        _http.Dispose();
    }

    #region API Methods

    #region Anime methods

    /// <inheritdoc />
    public Task<BaseJikanResponse<AnimeResponse>> GetAnimeAsync(long id, CancellationToken ct = default) =>
        getRequestAsync(AnimeQuery.Create(id), ct);

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<AnimeCharacterResponse>>> GetAnimeCharactersAsync(long id,
        CancellationToken ct = default) => getRequestAsync(AnimeCharactersQuery.Create(id), ct);

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<AnimeStaffPositionResponse>>> GetAnimeStaffAsync(long id,
        CancellationToken ct = default) => getRequestAsync(AnimeStaffQuery.Create(id), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<AnimeEpisodeResponse>>> GetAnimeEpisodesAsync(long id,
        CancellationToken ct = default) => getRequestAsync(AnimeEpisodesQuery.Create(id), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<AnimeEpisodeResponse>>> GetAnimeEpisodesAsync(long id, int page,
        CancellationToken ct = default) => getRequestAsync(AnimeEpisodesQuery.Create(id, page), ct);

    /// <inheritdoc />
    public Task<BaseJikanResponse<AnimeEpisodeResponse>> GetAnimeEpisodeAsync(long animeId, int episodeId,
        CancellationToken ct = default) => getRequestAsync(AnimeEpisodeQuery.Create(animeId, episodeId), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<NewsResponse>>> GetAnimeNewsAsync(long id,
        CancellationToken ct = default) => getRequestAsync(AnimeNewsQuery.Create(id), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<NewsResponse>>> GetAnimeNewsAsync(long id, int page,
        CancellationToken ct = default) => getRequestAsync(AnimeNewsQuery.Create(id, page), ct);

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<ForumTopicResponse>>> GetAnimeForumTopicsAsync(long id,
        CancellationToken ct = default) => getRequestAsync(AnimeForumTopicsQuery.Create(id), ct);

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<ForumTopicResponse>>> GetAnimeForumTopicsAsync(long id,
        ForumTopicTypeFilter type, CancellationToken ct = default) =>
        getRequestAsync(AnimeForumTopicsQuery.Create(id, type), ct);

    /// <inheritdoc />
    public Task<BaseJikanResponse<AnimeVideosResponse>> GetAnimeVideosAsync(long id, CancellationToken ct = default) =>
        getRequestAsync(AnimeVideosQuery.Create(id), ct);

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<ImagesSetResponse>>> GetAnimePicturesAsync(long id,
        CancellationToken ct = default) => getRequestAsync(AnimePicturesQuery.Create(id), ct);

    /// <inheritdoc />
    public Task<BaseJikanResponse<AnimeStatisticsResponse>> GetAnimeStatisticsAsync(long id,
        CancellationToken ct = default) => getRequestAsync(AnimeStatisticsQuery.Create(id), ct);

    /// <inheritdoc />
    public Task<BaseJikanResponse<MoreInfoResponse>> GetAnimeMoreInfoAsync(long id, CancellationToken ct = default) =>
        getRequestAsync(AnimeMoreInfoQuery.Create(id), ct);

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<RecommendationResponse>>> GetAnimeRecommendationsAsync(long id,
        CancellationToken ct = default) => getRequestAsync(AnimeRecommendationsQuery.Create(id), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<AnimeUserUpdateResponse>>> GetAnimeUserUpdatesAsync(long id,
        CancellationToken ct = default) => getRequestAsync(AnimeUserUpdatesQuery.Create(id), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<AnimeUserUpdateResponse>>> GetAnimeUserUpdatesAsync(long id,
        int page, CancellationToken ct = default) => getRequestAsync(AnimeUserUpdatesQuery.Create(id, page), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<ReviewResponse>>> GetAnimeReviewsAsync(long id,
        CancellationToken ct = default) => getRequestAsync(AnimeReviewsQuery.Create(id), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<ReviewResponse>>> GetAnimeReviewsAsync(long id, int page,
        CancellationToken ct = default) => getRequestAsync(AnimeReviewsQuery.Create(id, page), ct);

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<RelatedEntryResponse>>> GetAnimeRelationsAsync(long id,
        CancellationToken ct = default) => getRequestAsync(AnimeRelationsQuery.Create(id), ct);

    /// <inheritdoc />
    public Task<BaseJikanResponse<AnimeThemesResponse>> GetAnimeThemesAsync(long id, CancellationToken ct = default) =>
        getRequestAsync(AnimeThemesQuery.Create(id), ct);

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<ExternalLinkResponse>>> GetAnimeExternalLinksAsync(long id,
        CancellationToken ct = default) => getRequestAsync(AnimeExternalLinksQuery.Create(id), ct);

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<ExternalLinkResponse>>> GetAnimeStreamingLinksAsync(long id,
        CancellationToken ct = default) => getRequestAsync(AnimeStreamingLinksQuery.Create(id), ct);

    /// <inheritdoc />
    public Task<BaseJikanResponse<AnimeResponseFull>> GetAnimeFullDataAsync(long id, CancellationToken ct = default) =>
        getRequestAsync(AnimeFullDataQuery.Create(id), ct);

    #endregion Anime methods

    #region Character methods

    /// <inheritdoc />
    public Task<BaseJikanResponse<CharacterResponse>> GetCharacterAsync(long id, CancellationToken ct = default) =>
        getRequestAsync(CharacterQuery.Create(id), ct);

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<CharacterAnimeographyEntryResponse>>> GetCharacterAnimeAsync(long id,
        CancellationToken ct = default) => getRequestAsync(CharacterAnimeQuery.Create(id), ct);

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<CharacterMangaographyEntryResponse>>> GetCharacterMangaAsync(long id,
        CancellationToken ct = default) => getRequestAsync(CharacterMangaQuery.Create(id), ct);

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<VoiceActorEntryResponse>>> GetCharacterVoiceActorsAsync(long id,
        CancellationToken ct = default) => getRequestAsync(CharacterVoiceActorsQuery.Create(id), ct);

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<ImagesSetResponse>>> GetCharacterPicturesAsync(long id,
        CancellationToken ct = default) => getRequestAsync(CharacterPicturesQuery.Create(id), ct);

    /// <inheritdoc />
    public Task<BaseJikanResponse<CharacterResponseFull>> GetCharacterFullDataAsync(long id,
        CancellationToken ct = default) => getRequestAsync(CharacterFullDataQuery.Create(id), ct);

    #endregion Character methods

    #region Manga methods

    /// <inheritdoc />
    public Task<BaseJikanResponse<MangaResponse>> GetMangaAsync(long id, CancellationToken ct = default) =>
        getRequestAsync(MangaQuery.Create(id), ct);

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<MangaCharacterResponse>>> GetMangaCharactersAsync(long id,
        CancellationToken ct = default) => getRequestAsync(MangaCharactersQuery.Create(id), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<NewsResponse>>> GetMangaNewsAsync(long id,
        CancellationToken ct = default) => getRequestAsync(MangaNewsQuery.Create(id), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<NewsResponse>>> GetMangaNewsAsync(long id, int page,
        CancellationToken ct = default) => getRequestAsync(MangaNewsQuery.Create(id, page), ct);

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<ForumTopicResponse>>> GetMangaForumTopicsAsync(long id,
        CancellationToken ct = default) => getRequestAsync(MangaForumTopicsQuery.Create(id), ct);

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<ImagesSetResponse>>> GetMangaPicturesAsync(long id,
        CancellationToken ct = default) => getRequestAsync(MangaPicturesQuery.Create(id), ct);

    /// <inheritdoc />
    public Task<BaseJikanResponse<MangaStatisticsResponse>> GetMangaStatisticsAsync(long id,
        CancellationToken ct = default) => getRequestAsync(MangaStatisticsQuery.Create(id), ct);

    /// <inheritdoc />
    public Task<BaseJikanResponse<MoreInfoResponse>> GetMangaMoreInfoAsync(long id, CancellationToken ct = default) =>
        getRequestAsync(MangaMoreInfoQuery.Create(id), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<MangaUserUpdateResponse>>> GetMangaUserUpdatesAsync(long id,
        CancellationToken ct = default) => getRequestAsync(MangaUserUpdatesQuery.Create(id), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<MangaUserUpdateResponse>>> GetMangaUserUpdatesAsync(long id,
        int page, CancellationToken ct = default) => getRequestAsync(MangaUserUpdatesQuery.Create(id, page), ct);

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<RecommendationResponse>>> GetMangaRecommendationsAsync(long id,
        CancellationToken ct = default) => getRequestAsync(MangaRecommendationsQuery.Create(id), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<ReviewResponse>>> GetMangaReviewsAsync(long id,
        CancellationToken ct = default) => getRequestAsync(MangaReviewsQuery.Create(id), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<ReviewResponse>>> GetMangaReviewsAsync(long id, int page,
        CancellationToken ct = default) => getRequestAsync(MangaReviewsQuery.Create(id, page), ct);

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<RelatedEntryResponse>>> GetMangaRelationsAsync(long id,
        CancellationToken ct = default) => getRequestAsync(MangaRelationsQuery.Create(id), ct);

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<ExternalLinkResponse>>> GetMangaExternalLinksAsync(long id,
        CancellationToken ct = default) => getRequestAsync(MangaExternalLinksQuery.Create(id), ct);

    /// <inheritdoc />
    public Task<BaseJikanResponse<MangaResponseFull>> GetMangaFullDataAsync(long id, CancellationToken ct = default) =>
        getRequestAsync(MangaFullDataQuery.Create(id), ct);

    #endregion Manga methods

    #region Person methods

    /// <inheritdoc />
    public Task<BaseJikanResponse<PersonResponse>> GetPersonAsync(long id, CancellationToken ct = default) =>
        getRequestAsync(PersonQuery.Create(id), ct);

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<PersonAnimeographyEntryResponse>>> GetPersonAnimeAsync(long id,
        CancellationToken ct = default) => getRequestAsync(PersonAnimeQuery.Create(id), ct);

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<PersonMangaographyEntryResponse>>> GetPersonMangaAsync(long id,
        CancellationToken ct = default) => getRequestAsync(PersonMangaQuery.Create(id), ct);

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<VoiceActingRoleResponse>>> GetPersonVoiceActingRolesAsync(long id,
        CancellationToken ct = default) => getRequestAsync(PersonVoiceActingRolesQuery.Create(id), ct);

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<ImagesSetResponse>>> GetPersonPicturesAsync(long id,
        CancellationToken ct = default) => getRequestAsync(PersonPicturesQuery.Create(id), ct);

    /// <inheritdoc />
    public Task<BaseJikanResponse<PersonResponseFull>>
        GetPersonFullDataAsync(long id, CancellationToken ct = default) =>
        getRequestAsync(PersonFullDataQuery.Create(id), ct);

    #endregion Person methods

    #region Season methods

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetSeasonAsync(int year, Season season,
        CancellationToken ct = default) => getRequestAsync(SeasonQuery.Create(year, season), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetSeasonAsync(int year, Season season, int page,
        CancellationToken ct = default) => getRequestAsync(SeasonQuery.Create(year, season, page), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetSeasonAsync(int year, Season season,
        SeasonQueryParameters parameters, CancellationToken ct = default) =>
        getRequestAsync(SeasonQuery.Create(year, season, parameters), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<SeasonArchiveResponse>>> GetSeasonArchiveAsync(
        CancellationToken ct = default) => getRequestAsync(SeasonArchiveQuery.Create(), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetCurrentSeasonAsync(
        CancellationToken ct = default) => getRequestAsync(CurrentSeasonQuery.Create(), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetCurrentSeasonAsync(int page,
        CancellationToken ct = default) => getRequestAsync(CurrentSeasonQuery.Create(page), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetCurrentSeasonAsync(
        SeasonQueryParameters parameters, CancellationToken ct = default) =>
        getRequestAsync(CurrentSeasonQuery.Create(parameters), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetUpcomingSeasonAsync(
        CancellationToken ct = default) => getRequestAsync(UpcomingSeasonQuery.Create(), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetUpcomingSeasonAsync(int page,
        CancellationToken ct = default) => getRequestAsync(UpcomingSeasonQuery.Create(page), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetUpcomingSeasonAsync(
        SeasonQueryParameters parameters, CancellationToken ct = default) =>
        getRequestAsync(UpcomingSeasonQuery.Create(parameters), ct);

    #endregion Season methods

    #region Schedule methods

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetScheduleAsync(CancellationToken ct = default) =>
        getRequestAsync(ScheduleQuery.Create(), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetScheduleAsync(int page,
        CancellationToken ct = default) => getRequestAsync(ScheduleQuery.Create(page), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetScheduleAsync(ScheduledDayFilter scheduledDay,
        CancellationToken ct = default) => getRequestAsync(ScheduleQuery.Create(scheduledDay), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetScheduleAsync(ScheduleQueryParameters parameters,
        CancellationToken ct = default) => getRequestAsync(ScheduleQuery.Create(parameters), ct);

    #endregion Schedule methods

    #region Top methods

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetTopAnimeAsync(CancellationToken ct = default) =>
        getRequestAsync(TopAnimeQuery.Create(), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetTopAnimeAsync(int page,
        CancellationToken ct = default) => getRequestAsync(TopAnimeQuery.Create(page), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetTopAnimeAsync(TopAnimeFilter filter,
        CancellationToken ct = default) => getRequestAsync(TopAnimeQuery.Create(filter), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetTopAnimeAsync(TopAnimeFilter filter, int page,
        CancellationToken ct = default) => getRequestAsync(TopAnimeQuery.Create(page, filter), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetTopAnimeAsync(TopAnimeQueryParameters parameters,
        CancellationToken ct = default) => getRequestAsync(TopAnimeQuery.Create(parameters), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<MangaResponse>>> GetTopMangaAsync(CancellationToken ct = default) =>
        getRequestAsync(TopMangaQuery.Create(), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<MangaResponse>>> GetTopMangaAsync(int page,
        CancellationToken ct = default) => getRequestAsync(TopMangaQuery.Create(page), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<MangaResponse>>> GetTopMangaAsync(TopMangaFilter filter,
        CancellationToken ct = default) => getRequestAsync(TopMangaQuery.Create(filter), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<MangaResponse>>> GetTopMangaAsync(TopMangaFilter filter, int page,
        CancellationToken ct = default) => getRequestAsync(TopMangaQuery.Create(page, filter), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<MangaResponse>>> GetTopMangaAsync(TopMangaQueryParameters parameters,
        CancellationToken ct = default) => getRequestAsync(TopMangaQuery.Create(parameters), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<PersonResponse>>>
        GetTopPeopleAsync(CancellationToken ct = default) => getRequestAsync(TopPeopleQuery.Create(), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<PersonResponse>>> GetTopPeopleAsync(int page,
        CancellationToken ct = default) => getRequestAsync(TopPeopleQuery.Create(page), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<PersonResponse>>> GetTopPeopleAsync(int page, int limit,
        CancellationToken ct = default) => getRequestAsync(TopPeopleQuery.Create(page, limit), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<CharacterResponse>>> GetTopCharactersAsync(
        CancellationToken ct = default) => getRequestAsync(TopCharacterQuery.Create(), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<CharacterResponse>>> GetTopCharactersAsync(int page,
        CancellationToken ct = default) => getRequestAsync(TopCharacterQuery.Create(page), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<CharacterResponse>>> GetTopCharactersAsync(int page, int limit,
        CancellationToken ct = default) => getRequestAsync(TopCharacterQuery.Create(page, limit), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<ReviewResponse>>>
        GetTopReviewsAsync(CancellationToken ct = default) => getRequestAsync(TopReviewsQuery.Create(), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<ReviewResponse>>> GetTopReviewsAsync(int page,
        CancellationToken ct = default) => getRequestAsync(TopReviewsQuery.Create(page), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<ReviewResponse>>> GetTopReviewsAsync(TopReviewsTypeFilter type,
        CancellationToken ct = default) => getRequestAsync(TopReviewsQuery.Create(type), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<ReviewResponse>>> GetTopReviewsAsync(int page,
        TopReviewsTypeFilter type, CancellationToken ct = default) =>
        getRequestAsync(TopReviewsQuery.Create(page, type), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<ReviewResponse>>> GetTopReviewsAsync(
        TopReviewsQueryParameters parameters, CancellationToken ct = default) =>
        getRequestAsync(TopReviewsQuery.Create(parameters), ct);

    #endregion Top methods

    #region Genre methods

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<GenreResponse>>> GetAnimeGenresAsync(CancellationToken ct = default) =>
        getRequestAsync(AnimeGenresQuery.Create(), ct);

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<GenreResponse>>> GetAnimeGenresAsync(GenresFilter filter,
        CancellationToken ct = default) => getRequestAsync(AnimeGenresQuery.Create(filter), ct);

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<GenreResponse>>> GetMangaGenresAsync(CancellationToken ct = default) =>
        getRequestAsync(MangaGenresQuery.Create(), ct);

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<GenreResponse>>> GetMangaGenresAsync(GenresFilter filter,
        CancellationToken ct = default) => getRequestAsync(MangaGenresQuery.Create(filter), ct);

    #endregion Genre methods

    #region Producer methods

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<ProducerResponse>>>
        GetProducersAsync(CancellationToken ct = default) => getRequestAsync(ProducersQuery.Create(), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<ProducerResponse>>> GetProducersAsync(int page,
        CancellationToken ct = default) => getRequestAsync(ProducersQuery.Create(page), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<ProducerResponse>>> GetProducersAsync(
        ProducersQueryParameters parameters, CancellationToken ct = default) =>
        getRequestAsync(ProducersQuery.Create(parameters), ct);

    /// <inheritdoc />
    public Task<BaseJikanResponse<ProducerResponse>> GetProducerAsync(long id, CancellationToken ct = default) =>
        getRequestAsync(ProducerQuery.Create(id), ct);

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<ExternalLinkResponse>>> GetProducerExternalLinksAsync(long id,
        CancellationToken ct = default) => getRequestAsync(ProducerExternalLinksQuery.Create(id), ct);

    /// <inheritdoc />
    public Task<BaseJikanResponse<ProducerResponseFull>> GetProducerFullDataAsync(long id,
        CancellationToken ct = default) => getRequestAsync(ProducerFullDataQuery.Create(id), ct);

    #endregion Producer methods

    #region Magazine methods

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<MagazineResponse>>>
        GetMagazinesAsync(CancellationToken ct = default) => getRequestAsync(MagazinesQuery.Create(), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<MagazineResponse>>> GetMagazinesAsync(int page,
        CancellationToken ct = default) => getRequestAsync(MagazinesQuery.Create(page), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<MagazineResponse>>> GetMagazinesAsync(string query,
        CancellationToken ct = default) => getRequestAsync(MagazinesQuery.Create(query), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<MagazineResponse>>> GetMagazinesAsync(string query, int page,
        CancellationToken ct = default) => getRequestAsync(MagazinesQuery.Create(query, page), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<MagazineResponse>>> GetMagazinesAsync(
        MagazinesQueryParameters parameters, CancellationToken ct = default) =>
        getRequestAsync(MagazinesQuery.Create(parameters), ct);

    #endregion Magazine methods

    #region Club methods

    /// <inheritdoc />
    public Task<BaseJikanResponse<ClubResponse>> GetClubAsync(long id, CancellationToken ct = default) =>
        getRequestAsync(ClubQuery.Create(id), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<ClubMemberResponse>>> GetClubMembersAsync(long id,
        CancellationToken ct = default) => getRequestAsync(ClubMembersQuery.Create(id), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<ClubMemberResponse>>> GetClubMembersAsync(long id, int page,
        CancellationToken ct = default) => getRequestAsync(ClubMembersQuery.Create(id, page), ct);

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<ClubStaffResponse>>> GetClubStaffAsync(long id,
        CancellationToken ct = default) => getRequestAsync(ClubStaffQuery.Crete(id), ct);

    /// <inheritdoc />
    public Task<BaseJikanResponse<ClubRelationsResponse>>
        GetClubRelationsAsync(long id, CancellationToken ct = default) =>
        getRequestAsync(ClubRelationsQuery.Create(id), ct);

    #endregion Club methods

    #region User methods

    /// <inheritdoc />
    public Task<BaseJikanResponse<UserProfileResponse>> GetUserProfileAsync(string username,
        CancellationToken ct = default) => getRequestAsync(UserProfileQuery.Create(username), ct);

    /// <inheritdoc />
    public Task<BaseJikanResponse<UserStatisticsResponse>> GetUserStatisticsAsync(string username,
        CancellationToken ct = default) => getRequestAsync(UserStatisticsQuery.Create(username), ct);

    /// <inheritdoc />
    public Task<BaseJikanResponse<UserFavoritesResponse>> GetUserFavoritesAsync(string username,
        CancellationToken ct = default) => getRequestAsync(UserFavoritesQuery.Create(username), ct);

    /// <inheritdoc />
    public Task<BaseJikanResponse<UserUpdatesResponse>> GetUserUpdatesAsync(string username,
        CancellationToken ct = default) => getRequestAsync(UserUpdatesQuery.Create(username), ct);

    /// <inheritdoc />
    public Task<BaseJikanResponse<UserAboutResponse>>
        GetUserAboutAsync(string username, CancellationToken ct = default) =>
        getRequestAsync(UserAboutQuery.Create(username), ct);

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<HistoryEntryResponse>>> GetUserHistoryAsync(string username,
        CancellationToken ct = default) => getRequestAsync(UserHistoryQuery.Create(username), ct);

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<HistoryEntryResponse>>> GetUserHistoryAsync(string username,
        UserHistoryTypeFilter type, CancellationToken ct = default) =>
        getRequestAsync(UserHistoryQuery.Create(username, type), ct);

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<AnimeListEntryResponse>>> GetUserAnimeListAsync(string username,
        CancellationToken ct = default)
    {
        Guard.IsDefaultEndpoint(_baseAddress, nameof(GetUserAnimeListAsync));
        return getRequestAsync(UserAnimeListQuery.Create(username), ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<AnimeListEntryResponse>>> GetUserAnimeListAsync(string username,
        UserAnimeStatusFilter filter, CancellationToken ct = default)
    {
        Guard.IsDefaultEndpoint(_baseAddress, nameof(GetUserAnimeListAsync));
        return getRequestAsync(UserAnimeListQuery.Create(username, filter), ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<MangaListEntryResponse>>> GetUserMangaListAsync(string username,
        CancellationToken ct = default)
    {
        Guard.IsDefaultEndpoint(_baseAddress, nameof(GetUserMangaListAsync));
        return getRequestAsync(UserMangaListQuery.Create(username), ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<MangaListEntryResponse>>> GetUserMangaListAsync(string username,
        UserMangaStatusFilter filter, CancellationToken ct = default)
    {
        Guard.IsDefaultEndpoint(_baseAddress, nameof(GetUserMangaListAsync));
        return getRequestAsync(UserMangaListQuery.Create(username, filter), ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<FriendResponse>>> GetUserFriendsAsync(string username,
        CancellationToken ct = default) => getRequestAsync(UserFriendsQuery.Create(username), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<FriendResponse>>> GetUserFriendsAsync(string username, int page,
        CancellationToken ct = default) => getRequestAsync(UserFriendsQuery.Create(username, page), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<ReviewResponse>>> GetUserReviewsAsync(string username,
        CancellationToken ct = default) => getRequestAsync(UserReviewsQuery.Create(username), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<ReviewResponse>>> GetUserReviewsAsync(string username, int page,
        CancellationToken ct = default) => getRequestAsync(UserReviewsQuery.Create(username, page), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<UserRecommendationResponse>>> GetUserRecommendationsAsync(
        string username, CancellationToken ct = default) =>
        getRequestAsync(UserRecommendationsQuery.Create(username), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<UserRecommendationResponse>>> GetUserRecommendationsAsync(
        string username, int page, CancellationToken ct = default) =>
        getRequestAsync(UserRecommendationsQuery.Create(username, page), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<MalUrlResponse>>> GetUserClubsAsync(string username,
        CancellationToken ct = default) => getRequestAsync(UserClubsQuery.Create(username), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<MalUrlResponse>>> GetUserClubsAsync(string username, int page,
        CancellationToken ct = default) => getRequestAsync(UserClubsQuery.Create(username, page), ct);

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<ExternalLinkResponse>>> GetUserExternalLinksAsync(string username,
        CancellationToken ct = default) => getRequestAsync(UserExternalLinksQuery.Create(username), ct);

    /// <inheritdoc />
    public Task<BaseJikanResponse<UserResponseFull>> GetUserFullDataAsync(string username,
        CancellationToken ct = default) => getRequestAsync(UserFullDataQuery.Create(username), ct);

    #endregion User methods

    #region Random methods

    /// <inheritdoc />
    public Task<BaseJikanResponse<AnimeResponse>> GetRandomAnimeAsync(CancellationToken ct = default) =>
        getRequestAsync(RandomAnimeQuery.Create(), ct);

    /// <inheritdoc />
    public Task<BaseJikanResponse<MangaResponse>> GetRandomMangaAsync(CancellationToken ct = default) =>
        getRequestAsync(RandomMangaQuery.Create(), ct);

    /// <inheritdoc />
    public Task<BaseJikanResponse<CharacterResponse>> GetRandomCharacterAsync(CancellationToken ct = default) =>
        getRequestAsync(RandomCharacterQuery.Create(), ct);

    /// <inheritdoc />
    public Task<BaseJikanResponse<PersonResponse>> GetRandomPersonAsync(CancellationToken ct = default) =>
        getRequestAsync(RandomPersonQuery.Create(), ct);

    /// <inheritdoc />
    public Task<BaseJikanResponse<UserProfileResponse>> GetRandomUserAsync(CancellationToken ct = default) =>
        getRequestAsync(RandomUserQuery.Create(), ct);

    #endregion Random methods

    #region Watch methods

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<WatchEpisodeResponse>>> GetWatchRecentEpisodesAsync(
        CancellationToken ct = default) => getRequestAsync(WatchRecentEpisodesQuery.Create(), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<WatchEpisodeResponse>>> GetWatchPopularEpisodesAsync(
        CancellationToken ct = default) => getRequestAsync(WatchPopularEpisodesQuery.Create(), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<WatchPromoVideoResponse>>> GetWatchRecentPromosAsync(
        CancellationToken ct = default) => getRequestAsync(WatchRecentPromosQuery.Create(), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<WatchPromoVideoResponse>>> GetWatchPopularPromosAsync(
        CancellationToken ct = default) => getRequestAsync(WatchPopularPromosQuery.Create(), ct);

    #endregion

    #region Reviews methods

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<ReviewResponse>>> GetRecentAnimeReviewsAsync(
        CancellationToken ct = default) => getRequestAsync(RecentAnimeReviewsQuery.Create(), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<ReviewResponse>>> GetRecentAnimeReviewsAsync(int page,
        CancellationToken ct = default) => getRequestAsync(RecentAnimeReviewsQuery.Create(page), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<ReviewResponse>>> GetRecentMangaReviewsAsync(
        CancellationToken ct = default) => getRequestAsync(RecentMangaReviewsQuery.Create(), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<ReviewResponse>>> GetRecentMangaReviewsAsync(int page,
        CancellationToken ct = default) => getRequestAsync(RecentMangaReviewsQuery.Create(page), ct);

    #endregion

    #region Recommendations methods

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<UserRecommendationResponse>>> GetRecentAnimeRecommendationsAsync(
        CancellationToken ct = default) => getRequestAsync(RecentAnimeRecommendationsQuery.Create(), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<UserRecommendationResponse>>> GetRecentAnimeRecommendationsAsync(
        int page, CancellationToken ct = default) => getRequestAsync(RecentAnimeRecommendationsQuery.Create(page), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<UserRecommendationResponse>>> GetRecentMangaRecommendationsAsync(
        CancellationToken ct = default) => getRequestAsync(RecentMangaRecommendationsQuery.Create(), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<UserRecommendationResponse>>> GetRecentMangaRecommendationsAsync(
        int page, CancellationToken ct = default) => getRequestAsync(RecentMangaRecommendationsQuery.Create(page), ct);

    #endregion

    #region Search methods

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> SearchAnimeAsync(string query,
        CancellationToken ct = default) => getRequestAsync(AnimeSearchQuery.Create(query), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> SearchAnimeAsync(
        AnimeSearchQueryParameters parameters, CancellationToken ct = default) =>
        getRequestAsync(AnimeSearchQuery.Create(parameters), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<MangaResponse>>> SearchMangaAsync(string query,
        CancellationToken ct = default) => getRequestAsync(MangaSearchQuery.Create(query), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<MangaResponse>>> SearchMangaAsync(
        MangaSearchQueryParameters parameters, CancellationToken ct = default) =>
        getRequestAsync(MangaSearchQuery.Create(parameters), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<PersonResponse>>> SearchPersonAsync(string query,
        CancellationToken ct = default) => getRequestAsync(PersonSearchQuery.Create(query), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<PersonResponse>>> SearchPersonAsync(
        PersonSearchQueryParameters parameters, CancellationToken ct = default) =>
        getRequestAsync(PersonSearchQuery.Create(parameters), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<CharacterResponse>>> SearchCharacterAsync(string query,
        CancellationToken ct = default) => getRequestAsync(CharacterSearchQuery.Create(query), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<CharacterResponse>>> SearchCharacterAsync(
        CharacterSearchQueryParameters parameters, CancellationToken ct = default) =>
        getRequestAsync(CharacterSearchQuery.Create(parameters), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<UserMetadataResponse>>> SearchUserAsync(string query,
        CancellationToken ct = default) => getRequestAsync(UserSearchQuery.Create(query), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<UserMetadataResponse>>> SearchUserAsync(
        UserSearchQueryParameters parameters, CancellationToken ct = default) =>
        getRequestAsync(UserSearchQuery.Create(parameters), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<ClubResponse>>> SearchClubAsync(string query,
        CancellationToken ct = default) => getRequestAsync(ClubSearchQuery.Create(query), ct);

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<ClubResponse>>> SearchClubAsync(ClubSearchQueryParameters parameters,
        CancellationToken ct = default) => getRequestAsync(ClubSearchQuery.Create(parameters), ct);

    #endregion Search methods

    #endregion Public Methods
}
