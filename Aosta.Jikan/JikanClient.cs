using System.Text.Json;
using Aosta.Common.Consts;
using Aosta.Common.Exceptions;
using Aosta.Common.Limiter;
using Aosta.Jikan.Enums;
using Aosta.Jikan.Models;
using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;
using Aosta.Jikan.Models.Search;
using Aosta.Jikan.Query;
using Aosta.Jikan.Query.Enums;
using FastEnumUtility;
using Serilog;

namespace Aosta.Jikan;

/// <summary>
/// Implementation of Jikan wrapper for .Net platform.
/// </summary>
public sealed class JikanClient : IJikan, IDisposable
{
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
    /// <param name="client"> Static http client instance.</param>
    /// <param name="taskLimiter"> API limiter </param>
    /// <param name="logger"> Serilog logger </param>
    internal JikanClient(HttpClient client, ITaskLimiter taskLimiter, ILogger? logger)
    {
        _httpClient = client;
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

    private Task<T> ExecuteGetRequestAsync<T>(IQuery query, CancellationToken ct = default) where T : class
    {
        return ExecuteGetRequestAsync<T>(query.GetQuery(), ct);
    }

    #endregion Private Methods

    #region Public Methods

    #region Anime methods

    /// <inheritdoc />
    public Task<BaseJikanResponse<AnimeResponse>> GetAnimeAsync(long id, CancellationToken ct = default)
    {
        var query = AnimeQuery.Create(id);
        return ExecuteGetRequestAsync<BaseJikanResponse<AnimeResponse>>(query, ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<AnimeCharacterResponse>>> GetAnimeCharactersAsync(long id, CancellationToken ct = default)
    {
        var query = AnimeCharactersQuery.Create(id);
        return ExecuteGetRequestAsync<BaseJikanResponse<ICollection<AnimeCharacterResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<AnimeStaffPositionResponse>>> GetAnimeStaffAsync(long id, CancellationToken ct = default)
    {
        var query = AnimeStaffQuery.Create(id);
        return ExecuteGetRequestAsync<BaseJikanResponse<ICollection<AnimeStaffPositionResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<AnimeEpisodeResponse>>> GetAnimeEpisodesAsync(long id, CancellationToken ct = default)
    {
        var query = AnimeEpisodesQuery.Create(id);
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<AnimeEpisodeResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<AnimeEpisodeResponse>>> GetAnimeEpisodesAsync(long id, int page, CancellationToken ct = default)
    {
        var query = AnimeEpisodesQuery.Create(id, page);
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<AnimeEpisodeResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<AnimeEpisodeResponse>> GetAnimeEpisodeAsync(long animeId, int episodeId, CancellationToken ct = default)
    {
        var query = AnimeEpisodeQuery.Create(animeId, episodeId);
        return ExecuteGetRequestAsync<BaseJikanResponse<AnimeEpisodeResponse>>(query, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<NewsResponse>>> GetAnimeNewsAsync(long id, CancellationToken ct = default)
    {
        var query = AnimeNewsQuery.Create(id);
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<NewsResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<NewsResponse>>> GetAnimeNewsAsync(long id, int page, CancellationToken ct = default)
    {
        var query = AnimeNewsQuery.Create(id, page);
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<NewsResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<ForumTopicResponse>>> GetAnimeForumTopicsAsync(long id, CancellationToken ct = default)
    {
        var query = AnimeForumTopicsQuery.Create(id);
        return ExecuteGetRequestAsync<BaseJikanResponse<ICollection<ForumTopicResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<ForumTopicResponse>>> GetAnimeForumTopicsAsync(long id, ForumTopicType type, CancellationToken ct = default)
    {
        var query = AnimeForumTopicsQuery.Create(id, type);
        return ExecuteGetRequestAsync<BaseJikanResponse<ICollection<ForumTopicResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<AnimeVideosResponse>> GetAnimeVideosAsync(long id, CancellationToken ct = default)
    {
        var query = AnimeVideosQuery.Create(id);
        return ExecuteGetRequestAsync<BaseJikanResponse<AnimeVideosResponse>>(query, ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<ImagesSetResponse>>> GetAnimePicturesAsync(long id, CancellationToken ct = default)
    {
        var query = AnimePicturesQuery.Create(id);
        return ExecuteGetRequestAsync<BaseJikanResponse<ICollection<ImagesSetResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<AnimeStatisticsResponse>> GetAnimeStatisticsAsync(long id, CancellationToken ct = default)
    {
        var query = AnimeStatisticsQuery.Create(id);
        return ExecuteGetRequestAsync<BaseJikanResponse<AnimeStatisticsResponse>>(query, ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<MoreInfoResponse>> GetAnimeMoreInfoAsync(long id, CancellationToken ct = default)
    {
        var query = AnimeMoreInfoQuery.Create(id);
        return ExecuteGetRequestAsync<BaseJikanResponse<MoreInfoResponse>>(query, ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<RecommendationResponse>>> GetAnimeRecommendationsAsync(long id, CancellationToken ct = default)
    {
        var query = AnimeRecommendationsQuery.Create(id);
        return ExecuteGetRequestAsync<BaseJikanResponse<ICollection<RecommendationResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<AnimeUserUpdateResponse>>> GetAnimeUserUpdatesAsync(long id, CancellationToken ct = default)
    {
        var query = AnimeUserUpdatesQuery.Create(id);
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<AnimeUserUpdateResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<AnimeUserUpdateResponse>>> GetAnimeUserUpdatesAsync(long id, int page, CancellationToken ct = default)
    {
        var query = AnimeUserUpdatesQuery.Create(id, page);
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<AnimeUserUpdateResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<ReviewResponse>>> GetAnimeReviewsAsync(long id, CancellationToken ct = default)
    {
        var query = AnimeReviewsQuery.Create(id);
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<ReviewResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<ReviewResponse>>> GetAnimeReviewsAsync(long id, int page, CancellationToken ct = default)
    {
        var query = AnimeReviewsQuery.Create(id, page);
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<ReviewResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<RelatedEntryResponse>>> GetAnimeRelationsAsync(long id, CancellationToken ct = default)
    {
        var query = AnimeRelationsQuery.Create(id);
        return ExecuteGetRequestAsync<BaseJikanResponse<ICollection<RelatedEntryResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<AnimeThemesResponse>> GetAnimeThemesAsync(long id, CancellationToken ct = default)
    {
        var query = AnimeThemesQuery.Create(id);
        return ExecuteGetRequestAsync<BaseJikanResponse<AnimeThemesResponse>>(query, ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<ExternalLinkResponse>>> GetAnimeExternalLinksAsync(long id, CancellationToken ct = default)
    {
        var query = AnimeExternalLinksQuery.Create(id);
        return ExecuteGetRequestAsync<BaseJikanResponse<ICollection<ExternalLinkResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<ExternalLinkResponse>>> GetAnimeStreamingLinksAsync(long id, CancellationToken ct = default)
    {
        var query = AnimeStreamingLinksQuery.Create(id);
        return ExecuteGetRequestAsync<BaseJikanResponse<ICollection<ExternalLinkResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<AnimeResponseFull>> GetAnimeFullDataAsync(long id, CancellationToken ct = default)
    {
        var query = AnimeFullDataQuery.Create(id);
        return ExecuteGetRequestAsync<BaseJikanResponse<AnimeResponseFull>>(query, ct);
    }

    #endregion Anime methods

    #region Character methods

    /// <inheritdoc />
    public Task<BaseJikanResponse<CharacterResponse>> GetCharacterAsync(long id, CancellationToken ct = default)
    {
        var query = CharacterQuery.Create(id);
        return ExecuteGetRequestAsync<BaseJikanResponse<CharacterResponse>>(query, ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<CharacterAnimeographyEntryResponse>>> GetCharacterAnimeAsync(long id, CancellationToken ct = default)
    {
        var query = CharacterAnimeQuery.Create(id);
        return ExecuteGetRequestAsync<BaseJikanResponse<ICollection<CharacterAnimeographyEntryResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<CharacterMangaographyEntryResponse>>> GetCharacterMangaAsync(long id, CancellationToken ct = default)
    {
        var query = CharacterMangaQuery.Create(id);
        return ExecuteGetRequestAsync<BaseJikanResponse<ICollection<CharacterMangaographyEntryResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<VoiceActorEntryResponse>>> GetCharacterVoiceActorsAsync(long id, CancellationToken ct = default)
    {
        var query = CharacterVoiceActorsQuery.Create(id);
        return ExecuteGetRequestAsync<BaseJikanResponse<ICollection<VoiceActorEntryResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<ImagesSetResponse>>> GetCharacterPicturesAsync(long id, CancellationToken ct = default)
    {
        var query = CharacterPicturesQuery.Create(id);
        return ExecuteGetRequestAsync<BaseJikanResponse<ICollection<ImagesSetResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<CharacterResponseFull>> GetCharacterFullDataAsync(long id, CancellationToken ct = default)
    {
        var query = CharacterFullDataQuery.Create(id);
        return ExecuteGetRequestAsync<BaseJikanResponse<CharacterResponseFull>>(query, ct);
    }

    #endregion Character methods

    #region Manga methods

    /// <inheritdoc />
    public Task<BaseJikanResponse<MangaResponse>> GetMangaAsync(long id, CancellationToken ct = default)
    {
        var query = MangaQuery.Create(id);
        return ExecuteGetRequestAsync<BaseJikanResponse<MangaResponse>>(query, ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<MangaCharacterResponse>>> GetMangaCharactersAsync(long id, CancellationToken ct = default)
    {
        var query = MangaCharactersQuery.Create(id);
        return ExecuteGetRequestAsync<BaseJikanResponse<ICollection<MangaCharacterResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<NewsResponse>>> GetMangaNewsAsync(long id, CancellationToken ct = default)
    {
        var query = MangaNewsQuery.Create(id);
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<NewsResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<NewsResponse>>> GetMangaNewsAsync(long id, int page, CancellationToken ct = default)
    {
        var query = MangaNewsQuery.Create(id, page);
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<NewsResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<ForumTopicResponse>>> GetMangaForumTopicsAsync(long id, CancellationToken ct = default)
    {
        var query = MangaForumTopicsQuery.Create(id);
        return ExecuteGetRequestAsync<BaseJikanResponse<ICollection<ForumTopicResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<ImagesSetResponse>>> GetMangaPicturesAsync(long id, CancellationToken ct = default)
    {
        var query = MangaPicturesQuery.Create(id);
        return ExecuteGetRequestAsync<BaseJikanResponse<ICollection<ImagesSetResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<MangaStatisticsResponse>> GetMangaStatisticsAsync(long id, CancellationToken ct = default)
    {
        var query = MangaStatisticsQuery.Create(id);
        return ExecuteGetRequestAsync<BaseJikanResponse<MangaStatisticsResponse>>(query, ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<MoreInfoResponse>> GetMangaMoreInfoAsync(long id, CancellationToken ct = default)
    {
        var query = MangaMoreInfoQuery.Create(id);
        return ExecuteGetRequestAsync<BaseJikanResponse<MoreInfoResponse>>(query, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<MangaUserUpdateResponse>>> GetMangaUserUpdatesAsync(long id, CancellationToken ct = default)
    {
        var query = MangaUserUpdatesQuery.Create(id);
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<MangaUserUpdateResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<MangaUserUpdateResponse>>> GetMangaUserUpdatesAsync(long id, int page, CancellationToken ct = default)
    {
        var query = MangaUserUpdatesQuery.Create(id, page);
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<MangaUserUpdateResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<RecommendationResponse>>> GetMangaRecommendationsAsync(long id, CancellationToken ct = default)
    {
        var query = MangaRecommendationsQuery.Create(id);
        return ExecuteGetRequestAsync<BaseJikanResponse<ICollection<RecommendationResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<ReviewResponse>>> GetMangaReviewsAsync(long id, CancellationToken ct = default)
    {
        var query = MangaReviewsQuery.Create(id);
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<ReviewResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<ReviewResponse>>> GetMangaReviewsAsync(long id, int page, CancellationToken ct = default)
    {
        var query = MangaReviewsQuery.Create(id, page);
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<ReviewResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<RelatedEntryResponse>>> GetMangaRelationsAsync(long id, CancellationToken ct = default)
    {
        var query = MangaRelationsQuery.Create(id);
        return ExecuteGetRequestAsync<BaseJikanResponse<ICollection<RelatedEntryResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<ExternalLinkResponse>>> GetMangaExternalLinksAsync(long id, CancellationToken ct = default)
    {
        var query = MangaExternalLinksQuery.Create(id);
        return ExecuteGetRequestAsync<BaseJikanResponse<ICollection<ExternalLinkResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<MangaResponseFull>> GetMangaFullDataAsync(long id, CancellationToken ct = default)
    {
        var query = MangaFullDataQuery.Create(id);
        return ExecuteGetRequestAsync<BaseJikanResponse<MangaResponseFull>>(query, ct);
    }

    #endregion Manga methods

    #region Person methods

    /// <inheritdoc />
    public Task<BaseJikanResponse<PersonResponse>> GetPersonAsync(long id, CancellationToken ct = default)
    {
        var query = PersonQuery.Create(id);
        return ExecuteGetRequestAsync<BaseJikanResponse<PersonResponse>>(query, ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<PersonAnimeographyEntryResponse>>> GetPersonAnimeAsync(long id, CancellationToken ct = default)
    {
        var query = PersonAnimeQuery.Create(id);
        return ExecuteGetRequestAsync<BaseJikanResponse<ICollection<PersonAnimeographyEntryResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<PersonMangaographyEntryResponse>>> GetPersonMangaAsync(long id, CancellationToken ct = default)
    {
        var query = PersonMangaQuery.Create(id);
        return ExecuteGetRequestAsync<BaseJikanResponse<ICollection<PersonMangaographyEntryResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<VoiceActingRoleResponse>>> GetPersonVoiceActingRolesAsync(long id, CancellationToken ct = default)
    {
        var query = PersonVoiceActingRolesQuery.Create(id);
        return ExecuteGetRequestAsync<BaseJikanResponse<ICollection<VoiceActingRoleResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<ImagesSetResponse>>> GetPersonPicturesAsync(long id, CancellationToken ct = default)
    {
        var query = PersonPicturesQuery.Create(id);
        return ExecuteGetRequestAsync<BaseJikanResponse<ICollection<ImagesSetResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<PersonResponseFull>> GetPersonFullDataAsync(long id, CancellationToken ct = default)
    {
        var query = PersonFullDataQuery.Create(id);
        return ExecuteGetRequestAsync<BaseJikanResponse<PersonResponseFull>>(query, ct);
    }

    #endregion Person methods

    #region Season methods

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetSeasonAsync(int year, Season season, CancellationToken ct = default)
    {
        var query = SeasonQuery.Create(year, season);
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<AnimeResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetSeasonAsync(int year, Season season, int page, CancellationToken ct = default)
    {
        var query = SeasonQuery.Create(year, season, page);
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<AnimeResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetSeasonAsync(int year, Season season, SeasonQueryParameters parameters, CancellationToken ct = default)
    {
        var query = SeasonQuery.Create(year, season, parameters);
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<AnimeResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<SeasonArchiveResponse>>> GetSeasonArchiveAsync(CancellationToken ct = default)
    {
        var query = SeasonArchiveQuery.Create();
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<SeasonArchiveResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetCurrentSeasonAsync(CancellationToken ct = default)
    {
        var query = CurrentSeasonQuery.Create();
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<AnimeResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetCurrentSeasonAsync(int page, CancellationToken ct = default)
    {
        var query = CurrentSeasonQuery.Create(page);
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<AnimeResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetCurrentSeasonAsync(SeasonQueryParameters parameters, CancellationToken ct = default)
    {
        var query = CurrentSeasonQuery.Create(parameters);
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<AnimeResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetUpcomingSeasonAsync(CancellationToken ct = default)
    {
        var query = UpcomingSeasonQuery.Create();
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<AnimeResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetUpcomingSeasonAsync(int page, CancellationToken ct = default)
    {
        var query = UpcomingSeasonQuery.Create(page);
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<AnimeResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetUpcomingSeasonAsync(SeasonQueryParameters parameters, CancellationToken ct = default)
    {
        var query = UpcomingSeasonQuery.Create(parameters);
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<AnimeResponse>>>(query, ct);
    }

    #endregion Season methods

    #region Schedule methods

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetScheduleAsync(CancellationToken ct = default)
    {
        var query = ScheduleQuery.Create();
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<AnimeResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetScheduleAsync(int page, CancellationToken ct = default)
    {
        var query = ScheduleQuery.Create(page);
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<AnimeResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetScheduleAsync(ScheduledDay scheduledDay, CancellationToken ct = default)
    {
        var query = ScheduleQuery.Create(scheduledDay);
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<AnimeResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetScheduleAsync(ScheduleQueryParameters parameters, CancellationToken ct = default)
    {
        var query = ScheduleQuery.Create(parameters);
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<AnimeResponse>>>(query, ct);
    }

    #endregion Schedule methods

    #region Top methods

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetTopAnimeAsync(CancellationToken ct = default)
    {
        var query = TopAnimeQuery.Create();
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<AnimeResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetTopAnimeAsync(int page, CancellationToken ct = default)
    {
        var query = TopAnimeQuery.Create(page);
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<AnimeResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetTopAnimeAsync(TopAnimeFilter filter, CancellationToken ct = default)
    {
        var query = TopAnimeQuery.Create(filter);
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<AnimeResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetTopAnimeAsync(TopAnimeFilter filter, int page, CancellationToken ct = default)
    {
        var query = TopAnimeQuery.Create(page, filter);
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<AnimeResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetTopAnimeAsync(TopAnimeQueryParameters parameters, CancellationToken ct = default)
    {
        var query = TopAnimeQuery.Create(parameters);
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<AnimeResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<MangaResponse>>> GetTopMangaAsync(CancellationToken ct = default)
    {
        var query = TopMangaQuery.Create();
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<MangaResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<MangaResponse>>> GetTopMangaAsync(int page, CancellationToken ct = default)
    {
        var query = TopMangaQuery.Create(page);
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<MangaResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<MangaResponse>>> GetTopMangaAsync(TopMangaFilter filter, CancellationToken ct = default)
    {
        var query = TopMangaQuery.Create(filter);
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<MangaResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<MangaResponse>>> GetTopMangaAsync(TopMangaFilter filter, int page, CancellationToken ct = default)
    {
        var query = TopMangaQuery.Create(page, filter);
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<MangaResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<MangaResponse>>> GetTopMangaAsync(TopMangaQueryParameters parameters, CancellationToken ct = default)
    {
        var query = TopMangaQuery.Create(parameters);
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<MangaResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<PersonResponse>>> GetTopPeopleAsync(CancellationToken ct = default)
    {
        var query = TopPeopleQuery.Create();
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<PersonResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<PersonResponse>>> GetTopPeopleAsync(int page, CancellationToken ct = default)
    {
        var query = TopPeopleQuery.Create(page);
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<PersonResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<PersonResponse>>> GetTopPeopleAsync(int page, int limit, CancellationToken ct = default)
    {
        var query = TopPeopleQuery.Create(page, limit);
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<PersonResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<CharacterResponse>>> GetTopCharactersAsync(CancellationToken ct = default)
    {
        var query = TopCharacterQuery.Create();
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<CharacterResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<CharacterResponse>>> GetTopCharactersAsync(int page, CancellationToken ct = default)
    {
        var query = TopCharacterQuery.Create(page);
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<CharacterResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<CharacterResponse>>> GetTopCharactersAsync(int page, int limit, CancellationToken ct = default)
    {
        var query = TopCharacterQuery.Create(page, limit);
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<CharacterResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<ReviewResponse>>> GetTopReviewsAsync(CancellationToken ct = default)
    {
        var query = TopReviewsQuery.Create();
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<ReviewResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<ReviewResponse>>> GetTopReviewsAsync(int page, CancellationToken ct = default)
    {
        var query = TopReviewsQuery.Create(page);
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<ReviewResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<ReviewResponse>>> GetTopReviewsAsync(TopReviewsType type, CancellationToken ct = default)
    {
        var query = TopReviewsQuery.Create(type);
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<ReviewResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<ReviewResponse>>> GetTopReviewsAsync(int page, TopReviewsType type, CancellationToken ct = default)
    {
        var query = TopReviewsQuery.Create(page, type);
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<ReviewResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<ReviewResponse>>> GetTopReviewsAsync(TopReviewsQueryParameters parameters, CancellationToken ct = default)
    {
        var query = TopReviewsQuery.Create(parameters);
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<ReviewResponse>>>(query, ct);
    }

    #endregion Top methods

    #region Genre methods

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<GenreResponse>>> GetAnimeGenresAsync(CancellationToken ct = default)
    {
        var query = AnimeGenresQuery.Create();
        return ExecuteGetRequestAsync<BaseJikanResponse<ICollection<GenreResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<GenreResponse>>> GetAnimeGenresAsync(GenresFilter filter, CancellationToken ct = default)
    {
        var query = AnimeGenresQuery.Create(filter);
        return ExecuteGetRequestAsync<BaseJikanResponse<ICollection<GenreResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<GenreResponse>>> GetMangaGenresAsync(CancellationToken ct = default)
    {
        var query = MangaGenresQuery.Create();
        return ExecuteGetRequestAsync<BaseJikanResponse<ICollection<GenreResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<GenreResponse>>> GetMangaGenresAsync(GenresFilter filter, CancellationToken ct = default)
    {
        var query = MangaGenresQuery.Create(filter);
        return ExecuteGetRequestAsync<BaseJikanResponse<ICollection<GenreResponse>>>(query, ct);
    }

    #endregion Genre methods

    #region Producer methods

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<ProducerResponse>>> GetProducersAsync(CancellationToken ct = default)
    {
        string[] endpointParts =
        {
            JikanEndpointConsts.Producers
        };
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<ProducerResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<ProducerResponse>>> GetProducersAsync(int page, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(page, nameof(page));

        var queryParams = $"?page={page}";
        string[] endpointParts =
        {
            JikanEndpointConsts.Producers + queryParams
        };
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<ProducerResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ProducerResponse>> GetProducerAsync(long id, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(id, nameof(id));

        string[] endpointParts =
        {
            JikanEndpointConsts.Producers, id.ToString()
        };
        return ExecuteGetRequestAsync<BaseJikanResponse<ProducerResponse>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<ExternalLinkResponse>>> GetProducerExternalLinksAsync(long id, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(id, nameof(id));

        string[] endpointParts =
        {
            JikanEndpointConsts.Producers, id.ToString(), JikanEndpointConsts.External
        };
        return ExecuteGetRequestAsync<BaseJikanResponse<ICollection<ExternalLinkResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ProducerResponseFull>> GetProducerFullDataAsync(long id, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(id, nameof(id));

        string[] endpointParts =
        {
            JikanEndpointConsts.Producers,
            id.ToString(),
            JikanEndpointConsts.Full
        };
        return ExecuteGetRequestAsync<BaseJikanResponse<ProducerResponseFull>>(endpointParts, ct);
    }

    #endregion Producer methods

    #region Magazine methods

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<MagazineResponse>>> GetMagazinesAsync(CancellationToken ct = default)
    {
        var query = MagazinesQuery.Create();
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<MagazineResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<MagazineResponse>>> GetMagazinesAsync(int page, CancellationToken ct = default)
    {
        var query = MagazinesQuery.Create(page);
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<MagazineResponse>>>(query, ct);
    }

    #endregion Magazine methods

    #region Club methods

    /// <inheritdoc />
    public Task<BaseJikanResponse<ClubResponse>> GetClubAsync(long id, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        string[] endpointParts = { JikanEndpointConsts.Clubs, id.ToString() };
        return ExecuteGetRequestAsync<BaseJikanResponse<ClubResponse>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<ClubMemberResponse>>> GetClubMembersAsync(long id,
        CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        string[] endpointParts = { JikanEndpointConsts.Clubs, id.ToString(), JikanEndpointConsts.Members };
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<ClubMemberResponse>>>(endpointParts,
            ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<ClubMemberResponse>>> GetClubMembersAsync(long id,
        int page, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        Guard.IsGreaterThanZero(page, nameof(page));
        var queryParams = $"?page={page}";
        string[] endpointParts =
            { JikanEndpointConsts.Clubs, id.ToString(), JikanEndpointConsts.Members + queryParams };
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<ClubMemberResponse>>>(endpointParts,
            ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<ClubStaffResponse>>> GetClubStaffAsync(long id,
        CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        string[] endpointParts = { JikanEndpointConsts.Clubs, id.ToString(), JikanEndpointConsts.Staff };
        return ExecuteGetRequestAsync<BaseJikanResponse<ICollection<ClubStaffResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ClubRelationsResponse>> GetClubRelationsAsync(long id,
        CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        string[] endpointParts = { JikanEndpointConsts.Clubs, id.ToString(), JikanEndpointConsts.Relations };
        return ExecuteGetRequestAsync<BaseJikanResponse<ClubRelationsResponse>>(endpointParts, ct);
    }

    #endregion Club methods

    #region User methods

    /// <inheritdoc />
    public Task<BaseJikanResponse<UserProfileResponse>> GetUserProfileAsync(string username, CancellationToken ct = default)
    {
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));

        string[] endpointParts =
        {
            JikanEndpointConsts.Users,
            username
        };
        return ExecuteGetRequestAsync<BaseJikanResponse<UserProfileResponse>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<UserStatisticsResponse>> GetUserStatisticsAsync(string username, CancellationToken ct = default)
    {
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));

        string[] endpointParts =
        {
            JikanEndpointConsts.Users,
            username,
            JikanEndpointConsts.Statistics
        };
        return ExecuteGetRequestAsync<BaseJikanResponse<UserStatisticsResponse>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<UserFavoritesResponse>> GetUserFavoritesAsync(string username, CancellationToken ct = default)
    {
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));

        string[] endpointParts =
        {
            JikanEndpointConsts.Users,
            username,
            JikanEndpointConsts.Favorites
        };
        return ExecuteGetRequestAsync<BaseJikanResponse<UserFavoritesResponse>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<UserUpdatesResponse>> GetUserUpdatesAsync(string username, CancellationToken ct = default)
    {
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));

        string[] endpointParts =
        {
            JikanEndpointConsts.Users,
            username,
            JikanEndpointConsts.UserUpdates
        };
        return ExecuteGetRequestAsync<BaseJikanResponse<UserUpdatesResponse>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<UserAboutResponse>> GetUserAboutAsync(string username, CancellationToken ct = default)
    {
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));

        string[] endpointParts =
        {
            JikanEndpointConsts.Users,
            username,
            JikanEndpointConsts.About
        };
        return ExecuteGetRequestAsync<BaseJikanResponse<UserAboutResponse>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<HistoryEntryResponse>>> GetUserHistoryAsync(string username, CancellationToken ct = default)
    {
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));

        string[] endpointParts =
        {
            JikanEndpointConsts.Users,
            username,
            JikanEndpointConsts.History
        };
        return ExecuteGetRequestAsync<BaseJikanResponse<ICollection<HistoryEntryResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<HistoryEntryResponse>>> GetUserHistoryAsync(string username, UserHistoryExtension userHistory, CancellationToken ct = default)
    {
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));
        Guard.IsValidEnum(userHistory, nameof(userHistory));

        var queryParams = $"?filter={userHistory.GetEnumMemberValue() ?? throw InvalidEnumException<UserHistoryExtension>.EnumMember(userHistory, nameof(userHistory))}";
        string[] endpointParts =
        {
            JikanEndpointConsts.Users,
            username,
            JikanEndpointConsts.History + queryParams
        };
        return ExecuteGetRequestAsync<BaseJikanResponse<ICollection<HistoryEntryResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<AnimeListEntryResponse>>> GetUserAnimeListAsync(string username, CancellationToken ct = default)
    {
        Guard.IsDefaultEndpoint(_httpClient.BaseAddress.ToString(), nameof(GetUserAnimeListAsync));
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));

        string[] endpointParts =
        {
            JikanEndpointConsts.Users,
            username,
            JikanEndpointConsts.AnimeList
        };
        return ExecuteGetRequestAsync<BaseJikanResponse<ICollection<AnimeListEntryResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<AnimeListEntryResponse>>> GetUserAnimeListAsync(string username, int page, CancellationToken ct = default)
    {
        Guard.IsDefaultEndpoint(_httpClient.BaseAddress.ToString(), nameof(GetUserAnimeListAsync));
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));
        Guard.IsGreaterThanZero(page, nameof(page));

        var queryParams = $"?page={page}";
        string[] endpointParts =
        {
            JikanEndpointConsts.Users,
            username,
            JikanEndpointConsts.AnimeList + queryParams
        };
        return ExecuteGetRequestAsync<BaseJikanResponse<ICollection<AnimeListEntryResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<MangaListEntryResponse>>> GetUserMangaListAsync(string username, CancellationToken ct = default)
    {
        Guard.IsDefaultEndpoint(_httpClient.BaseAddress.ToString(), nameof(GetUserMangaListAsync));
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));

        string[] endpointParts =
        {
            JikanEndpointConsts.Users,
            username,
            JikanEndpointConsts.MangaList
        };
        return ExecuteGetRequestAsync<BaseJikanResponse<ICollection<MangaListEntryResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<MangaListEntryResponse>>> GetUserMangaListAsync(string username, int page, CancellationToken ct = default)
    {
        Guard.IsDefaultEndpoint(_httpClient.BaseAddress.ToString(), nameof(GetUserMangaListAsync));
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));
        Guard.IsGreaterThanZero(page, nameof(page));

        var queryParams = $"?page={page}";
        string[] endpointParts =
        {
            JikanEndpointConsts.Users,
            username,
            JikanEndpointConsts.MangaList + queryParams
        };
        return ExecuteGetRequestAsync<BaseJikanResponse<ICollection<MangaListEntryResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<FriendResponse>>> GetUserFriendsAsync(string username, CancellationToken ct = default)
    {
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));

        string[] endpointParts =
        {
            JikanEndpointConsts.Users,
            username,
            JikanEndpointConsts.Friends
        };
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<FriendResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<FriendResponse>>> GetUserFriendsAsync(string username, int page, CancellationToken ct = default)
    {
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));
        Guard.IsGreaterThanZero(page, nameof(page));

        var queryParams = $"?page={page}";
        string[] endpointParts =
        {
            JikanEndpointConsts.Users,
            username,
            JikanEndpointConsts.Friends + queryParams
        };
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<FriendResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<ReviewResponse>>> GetUserReviewsAsync(string username, CancellationToken ct = default)
    {
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));

        string[] endpointParts =
        {
            JikanEndpointConsts.Users,
            username,
            JikanEndpointConsts.Reviews
        };
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<ReviewResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<ReviewResponse>>> GetUserReviewsAsync(string username, int page, CancellationToken ct = default)
    {
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));
        Guard.IsGreaterThanZero(page, nameof(page));

        var queryParams = $"?page={page}";
        string[] endpointParts =
        {
            JikanEndpointConsts.Users,
            username,
            JikanEndpointConsts.Reviews + queryParams
        };
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<ReviewResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<UserRecommendationResponse>>> GetUserRecommendationsAsync(string username, CancellationToken ct = default)
    {
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));

        string[] endpointParts =
        {
            JikanEndpointConsts.Users,
            username,
            JikanEndpointConsts.Recommendations
        };
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<UserRecommendationResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<UserRecommendationResponse>>> GetUserRecommendationsAsync(string username, int page, CancellationToken ct = default)
    {
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));
        Guard.IsGreaterThanZero(page, nameof(page));

        var queryParams = $"?page={page}";
        string[] endpointParts =
        {
            JikanEndpointConsts.Users,
            username,
            JikanEndpointConsts.Recommendations + queryParams
        };
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<UserRecommendationResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<MalUrlResponse>>> GetUserClubsAsync(string username, CancellationToken ct = default)
    {
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));

        string[] endpointParts =
        {
            JikanEndpointConsts.Users,
            username,
            JikanEndpointConsts.Clubs
        };
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<MalUrlResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<MalUrlResponse>>> GetUserClubsAsync(string username, int page, CancellationToken ct = default)
    {
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));
        Guard.IsGreaterThanZero(page, nameof(page));

        var queryParams = $"?page={page}";
        string[] endpointParts =
        {
            JikanEndpointConsts.Users,
            username,
            JikanEndpointConsts.Clubs + queryParams
        };
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<MalUrlResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<ICollection<ExternalLinkResponse>>> GetUserExternalLinksAsync(string username, CancellationToken ct = default)
    {
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));

        string[] endpointParts =
        {
            JikanEndpointConsts.Users,
            username,
            JikanEndpointConsts.External
        };
        return ExecuteGetRequestAsync<BaseJikanResponse<ICollection<ExternalLinkResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public Task<BaseJikanResponse<UserResponseFull>> GetUserFullDataAsync(string username, CancellationToken ct = default)
    {
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));

        string[] endpointParts =
        {
            JikanEndpointConsts.Users,
            username,
            JikanEndpointConsts.Full
        };
        return ExecuteGetRequestAsync<BaseJikanResponse<UserResponseFull>>(endpointParts, ct);
    }

    #endregion User methods

    #region Random methods

    /// <inheritdoc/>
    public Task<BaseJikanResponse<AnimeResponse>> GetRandomAnimeAsync(CancellationToken ct = default)
    {
        var query = RandomAnimeQuery.Create();
        return ExecuteGetRequestAsync<BaseJikanResponse<AnimeResponse>>(query, ct);
    }

    /// <inheritdoc/>
    public Task<BaseJikanResponse<MangaResponse>> GetRandomMangaAsync(CancellationToken ct = default)
    {
        var query = RandomMangaQuery.Create();
        return ExecuteGetRequestAsync<BaseJikanResponse<MangaResponse>>(query, ct);
    }

    /// <inheritdoc/>
    public Task<BaseJikanResponse<CharacterResponse>> GetRandomCharacterAsync(CancellationToken ct = default)
    {
        var query = RandomCharacterQuery.Create();
        return ExecuteGetRequestAsync<BaseJikanResponse<CharacterResponse>>(query, ct);
    }

    /// <inheritdoc/>
    public Task<BaseJikanResponse<PersonResponse>> GetRandomPersonAsync(CancellationToken ct = default)
    {
        var query = RandomPersonQuery.Create();
        return ExecuteGetRequestAsync<BaseJikanResponse<PersonResponse>>(query, ct);
    }

    /// <inheritdoc/>
    public Task<BaseJikanResponse<UserProfileResponse>> GetRandomUserAsync(CancellationToken ct = default)
    {
        var query = RandomUserQuery.Create();
        return ExecuteGetRequestAsync<BaseJikanResponse<UserProfileResponse>>(query, ct);
    }

    #endregion Random methods

    #region Watch methods

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<WatchEpisodeResponse>>> GetWatchRecentEpisodesAsync(CancellationToken ct = default)
    {
        var query = WatchRecentEpisodesQuery.Create();
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<WatchEpisodeResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<WatchEpisodeResponse>>> GetWatchPopularEpisodesAsync(CancellationToken ct = default)
    {
        var query = WatchPopularEpisodesQuery.Create();
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<WatchEpisodeResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<WatchPromoVideoResponse>>> GetWatchRecentPromosAsync(CancellationToken ct = default)
    {
        var query = WatchRecentPromosQuery.Create();
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<WatchPromoVideoResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<WatchPromoVideoResponse>>> GetWatchPopularPromosAsync(CancellationToken ct = default)
    {
        var query = WatchPopularPromosQuery.Create();
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<WatchPromoVideoResponse>>>(query, ct);
    }

    #endregion

    #region Reviews methods

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<ReviewResponse>>> GetRecentAnimeReviewsAsync(CancellationToken ct = default)
    {
        var query = RecentAnimeReviewsQuery.Create();
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<ReviewResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<ReviewResponse>>> GetRecentAnimeReviewsAsync(int page, CancellationToken ct = default)
    {
        var query = RecentAnimeReviewsQuery.Create(page);
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<ReviewResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<ReviewResponse>>> GetRecentMangaReviewsAsync(CancellationToken ct = default)
    {
        var query = RecentMangaReviewsQuery.Create();
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<ReviewResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<ReviewResponse>>> GetRecentMangaReviewsAsync(int page, CancellationToken ct = default)
    {
        var query = RecentMangaReviewsQuery.Create(page);
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<ReviewResponse>>>(query, ct);
    }

    #endregion

    #region Recommendations methods

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<UserRecommendationResponse>>> GetRecentAnimeRecommendationsAsync(CancellationToken ct = default)
    {
        var query = RecentAnimeRecommendationsQuery.Create();
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<UserRecommendationResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<UserRecommendationResponse>>> GetRecentAnimeRecommendationsAsync(int page, CancellationToken ct = default)
    {
        var query = RecentAnimeRecommendationsQuery.Create(page);
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<UserRecommendationResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<UserRecommendationResponse>>> GetRecentMangaRecommendationsAsync(CancellationToken ct = default)
    {
        var query = RecentMangaRecommendationsQuery.Create();
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<UserRecommendationResponse>>>(query, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<UserRecommendationResponse>>> GetRecentMangaRecommendationsAsync(int page, CancellationToken ct = default)
    {
        var query = RecentMangaRecommendationsQuery.Create(page);
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<UserRecommendationResponse>>>(query, ct);
    }

    #endregion

    #region Search methods

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
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<AnimeResponse>>>(endpointParts, ct);
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
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<MangaResponse>>>(endpointParts, ct);
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
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<PersonResponse>>>(endpointParts, ct);
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
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<CharacterResponse>>>(endpointParts, ct);
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
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<UserMetadataResponse>>>(endpointParts, ct);
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
        return ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<ClubResponse>>>(endpointParts, ct);
    }

    #endregion Search methods

    #endregion Public Methods
}