using System.Text.Json;
using Aosta.Common.Consts;
using Aosta.Common.Limiter;
using Aosta.Jikan.Enums;
using Aosta.Jikan.Models;
using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;
using Aosta.Jikan.Models.Search;
using Aosta.Jikan.Query;
using Aosta.Jikan.Query.Enums;
using Serilog;

namespace Aosta.Jikan;

/// <summary>
/// Implementation of Jikan wrapper for .Net platform.
/// </summary>
public sealed class JikanClient : IJikan, IDisposable
{
    private QueryExecutor Executor { get; }

    /// <summary>
    /// Http client class to call REST request and receive REST response.
    /// </summary>
    private readonly HttpClient _httpClient;

    /// <summary>
    /// API call limiter
    /// </summary>
    private readonly ITaskLimiter _limiter;

    /// <summary>
    /// Serilog logger
    /// </summary>
    private readonly ILogger? _logger;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="http"> Static http client instance.</param>
    /// <param name="taskLimiter"> API limiter </param>
    /// <param name="logger"> Serilog logger </param>
    internal JikanClient(HttpClient http, ITaskLimiter taskLimiter, ILogger? logger)
    {
        Executor = new QueryExecutor(http, taskLimiter, logger);
        _httpClient = http;
        _limiter = taskLimiter;
        _logger = logger;
    }

    public void Dispose()
    {
        _httpClient.Dispose();
    }

    #region Private Methods

    /// <summary>
    /// Basic method for handling requests and responses from endpoint.
    /// </summary>
    /// <typeparam name="T">Class type deserialized from GET request.</typeparam>
    /// <param name="requestEndpoint">The query endpoint.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>Requested object if successful.</returns>
    /// <exception cref="JikanRequestException">The request endpoint must be a valid API endpoint</exception>
    private async Task<T> ExecuteGetRequestAsync<T>(string requestEndpoint, CancellationToken ct = default) where T : class
    {
        string fullUrl = _httpClient.BaseAddress + requestEndpoint;
        try
        {
            _logger?.Debug("Performing GET request: \"{Request}\"", fullUrl);
            using var response = await _limiter.LimitAsync(() => _httpClient.GetAsync(requestEndpoint, ct));

            string json = await response.Content.ReadAsStringAsync(ct);
            _logger?.Verbose("Retrieved JSON string: {Json}", json);

            if (response.IsSuccessStatusCode)
            {
                _logger?.Debug("Got HTTP response for \"{Request}\" successfully", fullUrl);
                return JsonSerializer.Deserialize<T>(json) ?? throw new JikanRequestException(
                    ErrorMessages.SerializationNullResult + Environment.NewLine
                    + "Raw JSON string:" + Environment.NewLine + json);
            }

            _logger?.Error("Failed to get HTTP resource for \"{Resource}\", Status Code: {Status}", requestEndpoint, response.StatusCode);
            var errorData = JsonSerializer.Deserialize<JikanApiError>(json);
            throw new JikanRequestException(string.Format(ErrorMessages.FailedRequest, response.StatusCode, response.Content), errorData);
        }
        catch (JsonException ex)
        {
            throw new JikanRequestException(
                ErrorMessages.SerializationFailed + Environment.NewLine + "Inner exception message: " + ex.Message, ex);
        }
    }

    private Task<T> ExecuteGetRequestAsync<T>(IEnumerable<string> routeSections, CancellationToken ct = default) where T : class
    {
        return ExecuteGetRequestAsync<T>(string.Join("/", routeSections), ct);
    }

    private Task<T> ExecuteGetRequestAsync<T>(IQuery<T> query, CancellationToken ct = default) where T : class
    {
        return Executor.GetRequest(query, ct);
    }

    #endregion Private Methods

    #region Public Methods

    #region Anime methods

    /// <inheritdoc />
    public Task<BaseJikanResponse<AnimeResponse>> GetAnimeAsync(long id, CancellationToken ct = default)
    {
        return Executor.GetRequest(AnimeQuery.Create(id), ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<AnimeCharacterResponse>>> GetAnimeCharactersAsync(long id, CancellationToken ct = default)
    {
        return Executor.GetRequest(AnimeCharactersQuery.Create(id), ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<AnimeStaffPositionResponse>>> GetAnimeStaffAsync(long id, CancellationToken ct = default)
    {
        return Executor.GetRequest(AnimeStaffQuery.Create(id), ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<AnimeEpisodeResponse>>> GetAnimeEpisodesAsync(long id, CancellationToken ct = default)
    {
        return Executor.GetRequest(AnimeEpisodesQuery.Create(id), ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<AnimeEpisodeResponse>>> GetAnimeEpisodesAsync(long id, int page, CancellationToken ct = default)
    {
        return Executor.GetRequest(AnimeEpisodesQuery.Create(id, page), ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<AnimeEpisodeResponse>> GetAnimeEpisodeAsync(long animeId, int episodeId, CancellationToken ct = default)
    {
        return Executor.GetRequest(AnimeEpisodeQuery.Create(animeId, episodeId), ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<NewsResponse>>> GetAnimeNewsAsync(long id, CancellationToken ct = default)
    {
        return Executor.GetRequest(AnimeNewsQuery.Create(id), ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<NewsResponse>>> GetAnimeNewsAsync(long id, int page, CancellationToken ct = default)
    {
        return Executor.GetRequest(AnimeNewsQuery.Create(id, page), ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<ForumTopicResponse>>> GetAnimeForumTopicsAsync(long id, CancellationToken ct = default)
    {
        return Executor.GetRequest(AnimeForumTopicsQuery.Create(id), ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<ForumTopicResponse>>> GetAnimeForumTopicsAsync(long id, ForumTopicType type, CancellationToken ct = default)
    {
        return Executor.GetRequest(AnimeForumTopicsQuery.Create(id, type), ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<AnimeVideosResponse>> GetAnimeVideosAsync(long id, CancellationToken ct = default)
    {
        return Executor.GetRequest(AnimeVideosQuery.Create(id), ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<ImagesSetResponse>>> GetAnimePicturesAsync(long id, CancellationToken ct = default)
    {
        return Executor.GetRequest(AnimePicturesQuery.Create(id), ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<AnimeStatisticsResponse>> GetAnimeStatisticsAsync(long id, CancellationToken ct = default)
    {
        return Executor.GetRequest(AnimeStatisticsQuery.Create(id), ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<MoreInfoResponse>> GetAnimeMoreInfoAsync(long id, CancellationToken ct = default)
    {
        return Executor.GetRequest(AnimeMoreInfoQuery.Create(id), ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<RecommendationResponse>>> GetAnimeRecommendationsAsync(long id, CancellationToken ct = default)
    {
        return Executor.GetRequest(AnimeRecommendationsQuery.Create(id), ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<AnimeUserUpdateResponse>>> GetAnimeUserUpdatesAsync(long id, CancellationToken ct = default)
    {
        return Executor.GetRequest(AnimeUserUpdatesQuery.Create(id), ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<AnimeUserUpdateResponse>>> GetAnimeUserUpdatesAsync(long id, int page, CancellationToken ct = default)
    {
        return Executor.GetRequest(AnimeUserUpdatesQuery.Create(id, page), ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<ReviewResponse>>> GetAnimeReviewsAsync(long id, CancellationToken ct = default)
    {
        return Executor.GetRequest(AnimeReviewsQuery.Create(id), ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<ReviewResponse>>> GetAnimeReviewsAsync(long id, int page, CancellationToken ct = default)
    {
        return Executor.GetRequest(AnimeReviewsQuery.Create(id, page), ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<RelatedEntryResponse>>> GetAnimeRelationsAsync(long id, CancellationToken ct = default)
    {
        return Executor.GetRequest(AnimeRelationsQuery.Create(id), ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<AnimeThemesResponse>> GetAnimeThemesAsync(long id, CancellationToken ct = default)
    {
        return Executor.GetRequest(AnimeThemesQuery.Create(id), ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<ExternalLinkResponse>>> GetAnimeExternalLinksAsync(long id, CancellationToken ct = default)
    {
        return Executor.GetRequest(AnimeExternalLinksQuery.Create(id), ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<ExternalLinkResponse>>> GetAnimeStreamingLinksAsync(long id, CancellationToken ct = default)
    {
        return Executor.GetRequest(AnimeStreamingLinksQuery.Create(id), ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<AnimeResponseFull>> GetAnimeFullDataAsync(long id, CancellationToken ct = default)
    {
        return Executor.GetRequest(AnimeFullDataQuery.Create(id), ct);
    }

    #endregion Anime methods

    #region Character methods

    /// <inheritdoc />
    public Task<BaseJikanResponse<CharacterResponse>> GetCharacterAsync(long id, CancellationToken ct = default)
    {
        return Executor.GetRequest(CharacterQuery.Create(id), ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<CharacterAnimeographyEntryResponse>>> GetCharacterAnimeAsync(long id, CancellationToken ct = default)
    {
        return Executor.GetRequest(CharacterAnimeQuery.Create(id), ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<CharacterMangaographyEntryResponse>>> GetCharacterMangaAsync(long id, CancellationToken ct = default)
    {
        return Executor.GetRequest(CharacterMangaQuery.Create(id), ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<VoiceActorEntryResponse>>> GetCharacterVoiceActorsAsync(long id, CancellationToken ct = default)
    {
        return Executor.GetRequest(CharacterVoiceActorsQuery.Create(id), ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<ImagesSetResponse>>> GetCharacterPicturesAsync(long id, CancellationToken ct = default)
    {
        return Executor.GetRequest(CharacterPicturesQuery.Create(id), ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<CharacterResponseFull>> GetCharacterFullDataAsync(long id, CancellationToken ct = default)
    {
        return Executor.GetRequest(CharacterFullDataQuery.Create(id), ct);
    }

    #endregion Character methods

    #region Manga methods

    /// <inheritdoc />
    public Task<BaseJikanResponse<MangaResponse>> GetMangaAsync(long id, CancellationToken ct = default)
    {
        return Executor.GetRequest(MangaQuery.Create(id), ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<MangaCharacterResponse>>> GetMangaCharactersAsync(long id, CancellationToken ct = default)
    {
        return Executor.GetRequest(MangaCharactersQuery.Create(id), ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<NewsResponse>>> GetMangaNewsAsync(long id, CancellationToken ct = default)
    {
        return Executor.GetRequest(MangaNewsQuery.Create(id), ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<NewsResponse>>> GetMangaNewsAsync(long id, int page, CancellationToken ct = default)
    {
        return Executor.GetRequest(MangaNewsQuery.Create(id, page), ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<ForumTopicResponse>>> GetMangaForumTopicsAsync(long id, CancellationToken ct = default)
    {
        return Executor.GetRequest(MangaForumTopicsQuery.Create(id), ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<ImagesSetResponse>>> GetMangaPicturesAsync(long id, CancellationToken ct = default)
    {
        return Executor.GetRequest(MangaPicturesQuery.Create(id), ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<MangaStatisticsResponse>> GetMangaStatisticsAsync(long id, CancellationToken ct = default)
    {
        return Executor.GetRequest(MangaStatisticsQuery.Create(id), ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<MoreInfoResponse>> GetMangaMoreInfoAsync(long id, CancellationToken ct = default)
    {
        return Executor.GetRequest(MangaMoreInfoQuery.Create(id), ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<MangaUserUpdateResponse>>> GetMangaUserUpdatesAsync(long id, CancellationToken ct = default)
    {
        return Executor.GetRequest(MangaUserUpdatesQuery.Create(id), ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<MangaUserUpdateResponse>>> GetMangaUserUpdatesAsync(long id, int page, CancellationToken ct = default)
    {
        return Executor.GetRequest(MangaUserUpdatesQuery.Create(id, page), ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<RecommendationResponse>>> GetMangaRecommendationsAsync(long id, CancellationToken ct = default)
    {
        return Executor.GetRequest(MangaRecommendationsQuery.Create(id), ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<ReviewResponse>>> GetMangaReviewsAsync(long id, CancellationToken ct = default)
    {
        return Executor.GetRequest(MangaReviewsQuery.Create(id), ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<ReviewResponse>>> GetMangaReviewsAsync(long id, int page, CancellationToken ct = default)
    {
        return Executor.GetRequest(MangaReviewsQuery.Create(id, page), ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<RelatedEntryResponse>>> GetMangaRelationsAsync(long id, CancellationToken ct = default)
    {
        return Executor.GetRequest(MangaRelationsQuery.Create(id), ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<ExternalLinkResponse>>> GetMangaExternalLinksAsync(long id, CancellationToken ct = default)
    {
        return Executor.GetRequest(MangaExternalLinksQuery.Create(id), ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<MangaResponseFull>> GetMangaFullDataAsync(long id, CancellationToken ct = default)
    {
        return Executor.GetRequest(MangaFullDataQuery.Create(id), ct);
    }

    #endregion Manga methods

    #region Person methods

    /// <inheritdoc />
    public Task<BaseJikanResponse<PersonResponse>> GetPersonAsync(long id, CancellationToken ct = default)
    {
        return Executor.GetRequest(PersonQuery.Create(id), ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<PersonAnimeographyEntryResponse>>> GetPersonAnimeAsync(long id, CancellationToken ct = default)
    {
        return Executor.GetRequest(PersonAnimeQuery.Create(id), ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<PersonMangaographyEntryResponse>>> GetPersonMangaAsync(long id, CancellationToken ct = default)
    {
        return Executor.GetRequest(PersonMangaQuery.Create(id), ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<VoiceActingRoleResponse>>> GetPersonVoiceActingRolesAsync(long id, CancellationToken ct = default)
    {
        return Executor.GetRequest(PersonVoiceActingRolesQuery.Create(id), ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<ImagesSetResponse>>> GetPersonPicturesAsync(long id, CancellationToken ct = default)
    {
        return Executor.GetRequest(PersonPicturesQuery.Create(id), ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<PersonResponseFull>> GetPersonFullDataAsync(long id, CancellationToken ct = default)
    {
        return Executor.GetRequest(PersonFullDataQuery.Create(id), ct);
    }

    #endregion Person methods

    #region Season methods

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetSeasonAsync(int year, Season season, CancellationToken ct = default)
    {
        return Executor.GetRequest(SeasonQuery.Create(year, season), ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetSeasonAsync(int year, Season season, int page, CancellationToken ct = default)
    {
        return Executor.GetRequest(SeasonQuery.Create(year, season, page), ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetSeasonAsync(int year, Season season, SeasonQueryParameters parameters, CancellationToken ct = default)
    {
        return Executor.GetRequest(SeasonQuery.Create(year, season, parameters), ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<SeasonArchiveResponse>>> GetSeasonArchiveAsync(CancellationToken ct = default)
    {
        return Executor.GetRequest(SeasonArchiveQuery.Create(), ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetCurrentSeasonAsync(CancellationToken ct = default)
    {
        return Executor.GetRequest(CurrentSeasonQuery.Create(), ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetCurrentSeasonAsync(int page, CancellationToken ct = default)
    {
        return Executor.GetRequest(CurrentSeasonQuery.Create(page), ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetCurrentSeasonAsync(SeasonQueryParameters parameters, CancellationToken ct = default)
    {
        return Executor.GetRequest(CurrentSeasonQuery.Create(parameters), ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetUpcomingSeasonAsync(CancellationToken ct = default)
    {
        return Executor.GetRequest(UpcomingSeasonQuery.Create(), ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetUpcomingSeasonAsync(int page, CancellationToken ct = default)
    {
        return Executor.GetRequest(UpcomingSeasonQuery.Create(page), ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetUpcomingSeasonAsync(SeasonQueryParameters parameters, CancellationToken ct = default)
    {
        return Executor.GetRequest(UpcomingSeasonQuery.Create(parameters), ct);
    }

    #endregion Season methods

    #region Schedule methods

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetScheduleAsync(CancellationToken ct = default)
    {
        return Executor.GetRequest(ScheduleQuery.Create(), ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetScheduleAsync(int page, CancellationToken ct = default)
    {
        return Executor.GetRequest(ScheduleQuery.Create(page), ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetScheduleAsync(ScheduledDay scheduledDay, CancellationToken ct = default)
    {
        return Executor.GetRequest(ScheduleQuery.Create(scheduledDay), ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetScheduleAsync(ScheduleQueryParameters parameters, CancellationToken ct = default)
    {
        return Executor.GetRequest(ScheduleQuery.Create(parameters), ct);
    }

    #endregion Schedule methods

    #region Top methods

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetTopAnimeAsync(CancellationToken ct = default)
    {
        return Executor.GetRequest(TopAnimeQuery.Create(), ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetTopAnimeAsync(int page, CancellationToken ct = default)
    {
        return Executor.GetRequest(TopAnimeQuery.Create(page), ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetTopAnimeAsync(TopAnimeFilter filter, CancellationToken ct = default)
    {
        return Executor.GetRequest(TopAnimeQuery.Create(filter), ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetTopAnimeAsync(TopAnimeFilter filter, int page, CancellationToken ct = default)
    {
        return Executor.GetRequest(TopAnimeQuery.Create(page, filter), ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetTopAnimeAsync(TopAnimeQueryParameters parameters, CancellationToken ct = default)
    {
        return Executor.GetRequest(TopAnimeQuery.Create(parameters), ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<MangaResponse>>> GetTopMangaAsync(CancellationToken ct = default)
    {
        return Executor.GetRequest(TopMangaQuery.Create(), ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<MangaResponse>>> GetTopMangaAsync(int page, CancellationToken ct = default)
    {
        return Executor.GetRequest(TopMangaQuery.Create(page), ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<MangaResponse>>> GetTopMangaAsync(TopMangaFilter filter, CancellationToken ct = default)
    {
        return Executor.GetRequest(TopMangaQuery.Create(filter), ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<MangaResponse>>> GetTopMangaAsync(TopMangaFilter filter, int page, CancellationToken ct = default)
    {
        return Executor.GetRequest(TopMangaQuery.Create(page, filter), ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<MangaResponse>>> GetTopMangaAsync(TopMangaQueryParameters parameters, CancellationToken ct = default)
    {
        return Executor.GetRequest(TopMangaQuery.Create(parameters), ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<PersonResponse>>> GetTopPeopleAsync(CancellationToken ct = default)
    {
        return Executor.GetRequest(TopPeopleQuery.Create(), ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<PersonResponse>>> GetTopPeopleAsync(int page, CancellationToken ct = default)
    {
        return Executor.GetRequest(TopPeopleQuery.Create(page), ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<PersonResponse>>> GetTopPeopleAsync(int page, int limit, CancellationToken ct = default)
    {
        return Executor.GetRequest(TopPeopleQuery.Create(page, limit), ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<CharacterResponse>>> GetTopCharactersAsync(CancellationToken ct = default)
    {
        return Executor.GetRequest(TopCharacterQuery.Create(), ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<CharacterResponse>>> GetTopCharactersAsync(int page, CancellationToken ct = default)
    {
        return Executor.GetRequest(TopCharacterQuery.Create(page), ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<CharacterResponse>>> GetTopCharactersAsync(int page, int limit, CancellationToken ct = default)
    {
        return Executor.GetRequest(TopCharacterQuery.Create(page, limit), ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<ReviewResponse>>> GetTopReviewsAsync(CancellationToken ct = default)
    {
        return Executor.GetRequest(TopReviewsQuery.Create(), ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<ReviewResponse>>> GetTopReviewsAsync(int page, CancellationToken ct = default)
    {
        return Executor.GetRequest(TopReviewsQuery.Create(page), ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<ReviewResponse>>> GetTopReviewsAsync(TopReviewsTypeFilter type, CancellationToken ct = default)
    {
        return Executor.GetRequest(TopReviewsQuery.Create(type), ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<ReviewResponse>>> GetTopReviewsAsync(int page, TopReviewsTypeFilter type, CancellationToken ct = default)
    {
        return Executor.GetRequest(TopReviewsQuery.Create(page, type), ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<ReviewResponse>>> GetTopReviewsAsync(TopReviewsQueryParameters parameters, CancellationToken ct = default)
    {
        return Executor.GetRequest(TopReviewsQuery.Create(parameters), ct);
    }

    #endregion Top methods

    #region Genre methods

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<GenreResponse>>> GetAnimeGenresAsync(CancellationToken ct = default)
    {
        return Executor.GetRequest(AnimeGenresQuery.Create(), ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<GenreResponse>>> GetAnimeGenresAsync(GenresFilter filter, CancellationToken ct = default)
    {
        return Executor.GetRequest(AnimeGenresQuery.Create(filter), ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<GenreResponse>>> GetMangaGenresAsync(CancellationToken ct = default)
    {
        return Executor.GetRequest(MangaGenresQuery.Create(), ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<GenreResponse>>> GetMangaGenresAsync(GenresFilter filter, CancellationToken ct = default)
    {
        return Executor.GetRequest(MangaGenresQuery.Create(filter), ct);
    }

    #endregion Genre methods

    #region Producer methods

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<ProducerResponse>>> GetProducersAsync(CancellationToken ct = default)
    {
        return Executor.GetRequest(ProducersQuery.Create(), ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<ProducerResponse>>> GetProducersAsync(int page, CancellationToken ct = default)
    {
        return Executor.GetRequest(ProducersQuery.Create(page), ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<ProducerResponse>>> GetProducersAsync(ProducersQueryParameters parameters, CancellationToken ct = default)
    {
        return Executor.GetRequest(ProducersQuery.Create(parameters), ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ProducerResponse>> GetProducerAsync(long id, CancellationToken ct = default)
    {
        return Executor.GetRequest(ProducerQuery.Create(id), ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<ExternalLinkResponse>>> GetProducerExternalLinksAsync(long id, CancellationToken ct = default)
    {
        return Executor.GetRequest(ProducerExternalLinksQuery.Create(id), ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ProducerResponseFull>> GetProducerFullDataAsync(long id, CancellationToken ct = default)
    {
        return Executor.GetRequest(ProducerFullDataQuery.Create(id), ct);
    }

    #endregion Producer methods

    #region Magazine methods

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<MagazineResponse>>> GetMagazinesAsync(CancellationToken ct = default)
    {
        return Executor.GetRequest(MagazinesQuery.Create(), ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<MagazineResponse>>> GetMagazinesAsync(int page, CancellationToken ct = default)
    {
        return Executor.GetRequest(MagazinesQuery.Create(page), ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<MagazineResponse>>> GetMagazinesAsync(string query, CancellationToken ct = default)
    {
        return Executor.GetRequest(MagazinesQuery.Create(query), ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<MagazineResponse>>> GetMagazinesAsync(string query, int page, CancellationToken ct = default)
    {
        return Executor.GetRequest(MagazinesQuery.Create(query, page), ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<MagazineResponse>>> GetMagazinesAsync(MagazinesQueryParameters parameters, CancellationToken ct = default)
    {
        return Executor.GetRequest(MagazinesQuery.Create(parameters), ct);
    }

    #endregion Magazine methods

    #region Club methods

    /// <inheritdoc />
    public Task<BaseJikanResponse<ClubResponse>> GetClubAsync(long id, CancellationToken ct = default)
    {
        return Executor.GetRequest(ClubQuery.Create(id), ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<ClubMemberResponse>>> GetClubMembersAsync(long id, CancellationToken ct = default)
    {
        return Executor.GetRequest(ClubMembersQuery.Create(id), ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<ClubMemberResponse>>> GetClubMembersAsync(long id, int page, CancellationToken ct = default)
    {
        return Executor.GetRequest(ClubMembersQuery.Create(id, page), ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<ClubStaffResponse>>> GetClubStaffAsync(long id, CancellationToken ct = default)
    {
        return Executor.GetRequest(ClubStaffQuery.Crete(id), ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ClubRelationsResponse>> GetClubRelationsAsync(long id, CancellationToken ct = default)
    {
        return Executor.GetRequest(ClubRelationsQuery.Create(id), ct);
    }

    #endregion Club methods

    #region User methods

    /// <inheritdoc />
    public Task<BaseJikanResponse<UserProfileResponse>> GetUserProfileAsync(string username, CancellationToken ct = default)
    {
        return Executor.GetRequest(UserProfileQuery.Create(username), ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<UserStatisticsResponse>> GetUserStatisticsAsync(string username, CancellationToken ct = default)
    {
        return Executor.GetRequest(UserStatisticsQuery.Create(username), ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<UserFavoritesResponse>> GetUserFavoritesAsync(string username, CancellationToken ct = default)
    {
        return Executor.GetRequest(UserFavoritesQuery.Create(username), ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<UserUpdatesResponse>> GetUserUpdatesAsync(string username, CancellationToken ct = default)
    {
        return Executor.GetRequest(UserUpdatesQuery.Create(username), ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<UserAboutResponse>> GetUserAboutAsync(string username, CancellationToken ct = default)
    {
        return Executor.GetRequest(UserAboutQuery.Create(username), ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<HistoryEntryResponse>>> GetUserHistoryAsync(string username, CancellationToken ct = default)
    {
        return Executor.GetRequest(UserHistoryQuery.Create(username), ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<HistoryEntryResponse>>> GetUserHistoryAsync(string username, UserHistoryTypeFilter type, CancellationToken ct = default)
    {
        return Executor.GetRequest(UserHistoryQuery.Create(username, type), ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<AnimeListEntryResponse>>> GetUserAnimeListAsync(string username, CancellationToken ct = default)
    {
        Guard.IsDefaultEndpoint(_httpClient.BaseAddress?.ToString(), nameof(GetUserAnimeListAsync));
        return Executor.GetRequest(UserAnimeListQuery.Create(username), ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<AnimeListEntryResponse>>> GetUserAnimeListAsync(string username, UserAnimeStatusFilter filter, CancellationToken ct = default)
    {
        Guard.IsDefaultEndpoint(_httpClient.BaseAddress?.ToString(), nameof(GetUserAnimeListAsync));
        return Executor.GetRequest(UserAnimeListQuery.Create(username, filter), ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<MangaListEntryResponse>>> GetUserMangaListAsync(string username, CancellationToken ct = default)
    {
        Guard.IsDefaultEndpoint(_httpClient.BaseAddress?.ToString(), nameof(GetUserMangaListAsync));
        return Executor.GetRequest(UserMangaListQuery.Create(username), ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<MangaListEntryResponse>>> GetUserMangaListAsync(string username, UserMangaStatusFilter filter, CancellationToken ct = default)
    {
        Guard.IsDefaultEndpoint(_httpClient.BaseAddress?.ToString(), nameof(GetUserMangaListAsync));
        return Executor.GetRequest(UserMangaListQuery.Create(username, filter), ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<FriendResponse>>> GetUserFriendsAsync(string username, CancellationToken ct = default)
    {
        return Executor.GetRequest(UserFriendsQuery.Create(username), ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<FriendResponse>>> GetUserFriendsAsync(string username, int page, CancellationToken ct = default)
    {
        return Executor.GetRequest(UserFriendsQuery.Create(username, page), ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<ReviewResponse>>> GetUserReviewsAsync(string username, CancellationToken ct = default)
    {
        return Executor.GetRequest(UserReviewsQuery.Create(username), ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<ReviewResponse>>> GetUserReviewsAsync(string username, int page, CancellationToken ct = default)
    {
        return Executor.GetRequest(UserReviewsQuery.Create(username, page), ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<UserRecommendationResponse>>> GetUserRecommendationsAsync(string username, CancellationToken ct = default)
    {
        return Executor.GetRequest(UserRecommendationsQuery.Create(username), ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<UserRecommendationResponse>>> GetUserRecommendationsAsync(string username, int page, CancellationToken ct = default)
    {
        return Executor.GetRequest(UserRecommendationsQuery.Create(username, page), ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<MalUrlResponse>>> GetUserClubsAsync(string username, CancellationToken ct = default)
    {
        return Executor.GetRequest(UserClubsQuery.Create(username), ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<MalUrlResponse>>> GetUserClubsAsync(string username, int page, CancellationToken ct = default)
    {
        return Executor.GetRequest(UserClubsQuery.Create(username, page), ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<ExternalLinkResponse>>> GetUserExternalLinksAsync(string username, CancellationToken ct = default)
    {
        return Executor.GetRequest(UserExternalLinksQuery.Create(username), ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<UserResponseFull>> GetUserFullDataAsync(string username, CancellationToken ct = default)
    {
        return Executor.GetRequest(UserFullDataQuery.Create(username), ct);
    }

    #endregion User methods

    #region Random methods

    /// <inheritdoc/>
    public Task<BaseJikanResponse<AnimeResponse>> GetRandomAnimeAsync(CancellationToken ct = default)
    {
        return Executor.GetRequest(RandomAnimeQuery.Create(), ct);
    }

    /// <inheritdoc/>
    public Task<BaseJikanResponse<MangaResponse>> GetRandomMangaAsync(CancellationToken ct = default)
    {
        return Executor.GetRequest(RandomMangaQuery.Create(), ct);
    }

    /// <inheritdoc/>
    public Task<BaseJikanResponse<CharacterResponse>> GetRandomCharacterAsync(CancellationToken ct = default)
    {
        return Executor.GetRequest(RandomCharacterQuery.Create(), ct);
    }

    /// <inheritdoc/>
    public Task<BaseJikanResponse<PersonResponse>> GetRandomPersonAsync(CancellationToken ct = default)
    {
        return Executor.GetRequest(RandomPersonQuery.Create(), ct);
    }

    /// <inheritdoc/>
    public Task<BaseJikanResponse<UserProfileResponse>> GetRandomUserAsync(CancellationToken ct = default)
    {
        return Executor.GetRequest(RandomUserQuery.Create(), ct);
    }

    #endregion Random methods

    #region Watch methods

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<WatchEpisodeResponse>>> GetWatchRecentEpisodesAsync(CancellationToken ct = default)
    {
        return Executor.GetRequest(WatchRecentEpisodesQuery.Create(), ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<WatchEpisodeResponse>>> GetWatchPopularEpisodesAsync(CancellationToken ct = default)
    {
        return Executor.GetRequest(WatchPopularEpisodesQuery.Create(), ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<WatchPromoVideoResponse>>> GetWatchRecentPromosAsync(CancellationToken ct = default)
    {
        return Executor.GetRequest(WatchRecentPromosQuery.Create(), ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<WatchPromoVideoResponse>>> GetWatchPopularPromosAsync(CancellationToken ct = default)
    {
        return Executor.GetRequest(WatchPopularPromosQuery.Create(), ct);
    }

    #endregion

    #region Reviews methods

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<ReviewResponse>>> GetRecentAnimeReviewsAsync(CancellationToken ct = default)
    {
        return Executor.GetRequest(RecentAnimeReviewsQuery.Create(), ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<ReviewResponse>>> GetRecentAnimeReviewsAsync(int page, CancellationToken ct = default)
    {
        return Executor.GetRequest(RecentAnimeReviewsQuery.Create(page), ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<ReviewResponse>>> GetRecentMangaReviewsAsync(CancellationToken ct = default)
    {
        return Executor.GetRequest(RecentMangaReviewsQuery.Create(), ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<ReviewResponse>>> GetRecentMangaReviewsAsync(int page, CancellationToken ct = default)
    {
        return Executor.GetRequest(RecentMangaReviewsQuery.Create(page), ct);
    }

    #endregion

    #region Recommendations methods

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<UserRecommendationResponse>>> GetRecentAnimeRecommendationsAsync(CancellationToken ct = default)
    {
        return Executor.GetRequest(RecentAnimeRecommendationsQuery.Create(), ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<UserRecommendationResponse>>> GetRecentAnimeRecommendationsAsync(int page, CancellationToken ct = default)
    {
        return Executor.GetRequest(RecentAnimeRecommendationsQuery.Create(page), ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<UserRecommendationResponse>>> GetRecentMangaRecommendationsAsync(CancellationToken ct = default)
    {
        return Executor.GetRequest(RecentMangaRecommendationsQuery.Create(), ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<UserRecommendationResponse>>> GetRecentMangaRecommendationsAsync(int page, CancellationToken ct = default)
    {
        return Executor.GetRequest(RecentMangaRecommendationsQuery.Create(page), ct);
    }

    #endregion

    #region Search methods

    //TODO: Refactor

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> SearchAnimeAsync(string query, CancellationToken ct = default)
    {
        return SearchAnimeAsync(new AnimeSearchConfig { Query = query }, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> SearchAnimeAsync(AnimeSearchConfig searchConfig, CancellationToken ct = default)
    {
        Guard.IsNotNull(searchConfig, nameof(searchConfig));

        string[] endpointParts =
        {
            JikanEndpointConsts.Anime + searchConfig.ConfigToString()
        };
        return Executor.GetRequest(endpointParts, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<MangaResponse>>> SearchMangaAsync(string query, CancellationToken ct = default)
    {
        return SearchMangaAsync(new MangaSearchConfig { Query = query }, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<MangaResponse>>> SearchMangaAsync(MangaSearchConfig searchConfig, CancellationToken ct = default)
    {
        Guard.IsNotNull(searchConfig, nameof(searchConfig));

        string[] endpointParts =
        {
            JikanEndpointConsts.Manga + searchConfig.ConfigToString()
        };
        return Executor.GetRequest(endpointParts, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<PersonResponse>>> SearchPersonAsync(string query, CancellationToken ct = default)
    {
        return SearchPersonAsync(new PersonSearchConfig { Query = query }, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<PersonResponse>>> SearchPersonAsync(PersonSearchConfig searchConfig, CancellationToken ct = default)
    {
        Guard.IsNotNull(searchConfig, nameof(searchConfig));

        string[] endpointParts =
        {
            JikanEndpointConsts.People + searchConfig.ConfigToString()
        };
        return Executor.GetRequest(endpointParts, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<CharacterResponse>>> SearchCharacterAsync(string query, CancellationToken ct = default)
    {
        return SearchCharacterAsync(new CharacterSearchConfig { Query = query }, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<CharacterResponse>>> SearchCharacterAsync(CharacterSearchConfig searchConfig, CancellationToken ct = default)
    {
        Guard.IsNotNull(searchConfig, nameof(searchConfig));

        string[] endpointParts =
        {
            JikanEndpointConsts.Characters + searchConfig.ConfigToString()
        };
        return Executor.GetRequest(endpointParts, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<UserMetadataResponse>>> SearchUserAsync(string query, CancellationToken ct = default)
    {
        return SearchUserAsync(new UserSearchConfig { Query = query }, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<UserMetadataResponse>>> SearchUserAsync(UserSearchConfig searchConfig, CancellationToken ct = default)
    {
        Guard.IsNotNull(searchConfig, nameof(searchConfig));

        string[] endpointParts =
        {
            JikanEndpointConsts.Users + searchConfig.ConfigToString()
        };
        return Executor.GetRequest(endpointParts, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<ClubResponse>>> SearchClubAsync(string query, CancellationToken ct = default)
    {
        return SearchClubAsync(new ClubSearchConfig { Query = query }, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<ClubResponse>>> SearchClubAsync(ClubSearchConfig searchConfig, CancellationToken ct = default)
    {
        Guard.IsNotNull(searchConfig, nameof(searchConfig));

        string[] endpointParts =
        {
            JikanEndpointConsts.Users + searchConfig.ConfigToString()
        };
        return Executor.GetRequest(endpointParts, ct);
    }

    #endregion Search methods

    #endregion Public Methods
}