﻿using System.Text.Json;
using Aosta.Core.Utils;
using Aosta.Core.Utils.Consts;
using Aosta.Core.Utils.Exceptions;
using Aosta.Core.Utils.Limiter;
using Aosta.Jikan.Consts;
using Aosta.Jikan.Enums;
using Aosta.Jikan.Models;
using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;
using Aosta.Jikan.Models.Search;
using FastEnumUtility;
using Serilog;

namespace Aosta.Jikan;

/// <summary>
/// Implementation of Jikan wrapper for .Net platform.
/// </summary>
public class JikanClient : IJikan, IDisposable
{
    #region Fields

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

    #endregion Fields

    #region Constructor

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

    #endregion Constructor

    #region Finalizer

    ~JikanClient()
    {
        Dispose(false);
    }

    private void Dispose(bool disposing)
    {
        if (disposing)
        {
            _httpClient.Dispose();
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    #endregion Finalizer

    #region Private Methods

    /// <summary>
    /// Basic method for handling requests and responses from endpoint.
    /// </summary>
    /// <typeparam name="T">Class type received from GET requests.</typeparam>
    /// <param name="routeSections">Arguments building endpoint.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>Requested object if successful, null otherwise.</returns>
    private async Task<T> ExecuteGetRequestAsync<T>(IEnumerable<string> routeSections, CancellationToken ct = default) where T : class
    {
        string requestEndpoint = string.Join("/", routeSections);
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
                var deserialized =  JsonSerializer.Deserialize<T>(json) ?? throw new JikanRequestException(
                    ErrorMessages.SerializationNullResult + Environment.NewLine + "Raw JSON string:" +
                    Environment.NewLine + json);
                //deserialized.QueryEndpoint = fullUrl;
                _logger?.Verbose("Deserialized resources from \"{Request}\" into:\n{@Data}", requestEndpoint, deserialized);
                return deserialized;
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

    private void ThrowIfDefaultEndpoint(string methodName)
    {
        if (_httpClient.BaseAddress.ToString().Equals(JikanConfiguration.DefaultEndpoint))
        {
            throw new NotSupportedException($"Operation {methodName} is not supported on the default endpoint.");
        }
    }

    #endregion Private Methods

    #region Public Methods

    #region Anime methods

    /// <inheritdoc />
    public async Task<BaseJikanResponse<AnimeResponse>> GetAnimeAsync(long id, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(id, nameof(id));

        string[] endpointParts =
        {
            JikanEndpointConsts.Anime,
            id.ToString()
        };
        return await ExecuteGetRequestAsync<BaseJikanResponse<AnimeResponse>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<BaseJikanResponse<ICollection<AnimeCharacterResponse>>> GetAnimeCharactersAsync(long id, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(id, nameof(id));

        string[] endpointParts =
        {
            JikanEndpointConsts.Anime,
            id.ToString(),
            JikanEndpointConsts.Characters
        };
        return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<AnimeCharacterResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<BaseJikanResponse<ICollection<AnimeStaffPositionResponse>>> GetAnimeStaffAsync(long id, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(id, nameof(id));

        string[] endpointParts =
        {
            JikanEndpointConsts.Anime,
            id.ToString(),
            JikanEndpointConsts.Staff
        };
        return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<AnimeStaffPositionResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<PaginatedJikanResponse<ICollection<AnimeEpisodeResponse>>> GetAnimeEpisodesAsync(long id, int page, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        Guard.IsGreaterThanZero(page, nameof(page));

        string[] endpointParts =
        {
            JikanEndpointConsts.Anime,
            id.ToString(),
            JikanEndpointConsts.Episodes + $"?page={page}"
        };
        return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<AnimeEpisodeResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<PaginatedJikanResponse<ICollection<AnimeEpisodeResponse>>> GetAnimeEpisodesAsync(long id, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(id, nameof(id));

        string[] endpointParts =
        {
            JikanEndpointConsts.Anime,
            id.ToString(),
            JikanEndpointConsts.Episodes
        };
        return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<AnimeEpisodeResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<BaseJikanResponse<AnimeEpisodeResponse>> GetAnimeEpisodeAsync(long animeId, int episodeId, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(animeId, nameof(animeId));
        Guard.IsGreaterThanZero(episodeId, nameof(episodeId));

        string[] endpointParts =
        {
            JikanEndpointConsts.Anime,
            animeId.ToString(),
            JikanEndpointConsts.Episodes,
            episodeId.ToString()
        };
        return await ExecuteGetRequestAsync<BaseJikanResponse<AnimeEpisodeResponse>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<PaginatedJikanResponse<ICollection<NewsResponse>>> GetAnimeNewsAsync(long id, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(id, nameof(id));

        string[] endpointParts =
        {
            JikanEndpointConsts.Anime,
            id.ToString(),
            JikanEndpointConsts.News
        };
        return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<NewsResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<PaginatedJikanResponse<ICollection<NewsResponse>>> GetAnimeNewsAsync(long id, int page, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        Guard.IsGreaterThanZero(page, nameof(page));

        string[] endpointParts =
        {
            JikanEndpointConsts.Anime,
            id.ToString(),
            JikanEndpointConsts.News + $"?page={page}"
        };
        return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<NewsResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<BaseJikanResponse<ICollection<ForumTopicResponse>>> GetAnimeForumTopicsAsync(long id, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(id, nameof(id));

        string[] endpointParts =
        {
            JikanEndpointConsts.Anime,
            id.ToString(),
            JikanEndpointConsts.Forum
        };
        return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<ForumTopicResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<BaseJikanResponse<ICollection<ForumTopicResponse>>> GetAnimeForumTopicsAsync(long id, ForumTopicType type, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        Guard.IsValidEnum(type, nameof(type));

        var queryParams = $"?filter={type.GetEnumMemberValue()}";
        string[] endpointParts =
        {
            JikanEndpointConsts.Anime,
            id.ToString(),
            JikanEndpointConsts.Forum + queryParams
        };
        return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<ForumTopicResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<BaseJikanResponse<AnimeVideosResponse>> GetAnimeVideosAsync(long id, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(id, nameof(id));

        string[] endpointParts =
        {
            JikanEndpointConsts.Anime,
            id.ToString(),
            JikanEndpointConsts.Videos
        };
        return await ExecuteGetRequestAsync<BaseJikanResponse<AnimeVideosResponse>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<BaseJikanResponse<ICollection<ImagesSetResponse>>> GetAnimePicturesAsync(long id, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(id, nameof(id));

        string[] endpointParts =
        {
            JikanEndpointConsts.Anime,
            id.ToString(),
            JikanEndpointConsts.Pictures
        };
        return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<ImagesSetResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<BaseJikanResponse<AnimeStatisticsResponse>> GetAnimeStatisticsAsync(long id, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(id, nameof(id));

        string[] endpointParts =
        {
            JikanEndpointConsts.Anime,
            id.ToString(),
            JikanEndpointConsts.Statistics
        };
        return await ExecuteGetRequestAsync<BaseJikanResponse<AnimeStatisticsResponse>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<BaseJikanResponse<MoreInfoResponse>> GetAnimeMoreInfoAsync(long id, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(id, nameof(id));

        string[] endpointParts =
        {
            JikanEndpointConsts.Anime,
            id.ToString(),
            JikanEndpointConsts.MoreInfo
        };
        return await ExecuteGetRequestAsync<BaseJikanResponse<MoreInfoResponse>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<BaseJikanResponse<ICollection<RecommendationResponse>>> GetAnimeRecommendationsAsync(long id, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(id, nameof(id));

        string[] endpointParts =
        {
            JikanEndpointConsts.Anime,
            id.ToString(),
            JikanEndpointConsts.Recommendations
        };
        return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<RecommendationResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<PaginatedJikanResponse<ICollection<AnimeUserUpdateResponse>>> GetAnimeUserUpdatesAsync(long id, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(id, nameof(id));

        string[] endpointParts =
        {
            JikanEndpointConsts.Anime,
            id.ToString(),
            JikanEndpointConsts.UserUpdates
        };
        return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<AnimeUserUpdateResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<PaginatedJikanResponse<ICollection<AnimeUserUpdateResponse>>> GetAnimeUserUpdatesAsync(long id, int page, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        Guard.IsGreaterThanZero(page, nameof(page));

        var queryParams = $"?page={page}";
        string[] endpointParts =
        {
            JikanEndpointConsts.Anime,
            id.ToString(),
            JikanEndpointConsts.UserUpdates + queryParams
        };
        return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<AnimeUserUpdateResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<PaginatedJikanResponse<ICollection<ReviewResponse>>> GetAnimeReviewsAsync(long id, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(id, nameof(id));

        string[] endpointParts =
        {
            JikanEndpointConsts.Anime,
            id.ToString(),
            JikanEndpointConsts.Reviews
        };
        return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<ReviewResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<PaginatedJikanResponse<ICollection<ReviewResponse>>> GetAnimeReviewsAsync(long id, int page, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        Guard.IsGreaterThanZero(page, nameof(page));

        var queryParams = $"?page={page}";
        string[] endpointParts =
        {
            JikanEndpointConsts.Anime,
            id.ToString(),
            JikanEndpointConsts.Reviews + queryParams
        };
        return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<ReviewResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<BaseJikanResponse<ICollection<RelatedEntryResponse>>> GetAnimeRelationsAsync(long id, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(id, nameof(id));

        string[] endpointParts =
        {
            JikanEndpointConsts.Anime,
            id.ToString(),
            JikanEndpointConsts.Relations
        };
        return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<RelatedEntryResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<BaseJikanResponse<AnimeThemesResponse>> GetAnimeThemesAsync(long id, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(id, nameof(id));

        string[] endpointParts =
        {
            JikanEndpointConsts.Anime,
            id.ToString(),
            JikanEndpointConsts.Themes
        };
        return await ExecuteGetRequestAsync<BaseJikanResponse<AnimeThemesResponse>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<BaseJikanResponse<ICollection<ExternalLinkResponse>>> GetAnimeExternalLinksAsync(long id, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(id, nameof(id));

        string[] endpointParts =
        {
            JikanEndpointConsts.Anime,
            id.ToString(),
            JikanEndpointConsts.External
        };
        return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<ExternalLinkResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<BaseJikanResponse<ICollection<ExternalLinkResponse>>> GetAnimeStreamingLinksAsync(long id, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(id, nameof(id));

        string[] endpointParts =
        {
            JikanEndpointConsts.Anime,
            id.ToString(),
            JikanEndpointConsts.Streaming
        };
        return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<ExternalLinkResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<BaseJikanResponse<AnimeResponseFull>> GetAnimeFullDataAsync(long id, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(id, nameof(id));

        string[] endpointParts =
        {
            JikanEndpointConsts.Anime,
            id.ToString(),
            JikanEndpointConsts.Full
        };
        return await ExecuteGetRequestAsync<BaseJikanResponse<AnimeResponseFull>>(endpointParts, ct);
    }

    #endregion Anime methods

    #region Character methods

    /// <inheritdoc />
    public async Task<BaseJikanResponse<CharacterResponse>> GetCharacterAsync(long id, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(id, nameof(id));

        string[] endpointParts =
        {
            JikanEndpointConsts.Characters,
            id.ToString()
        };
        return await ExecuteGetRequestAsync<BaseJikanResponse<CharacterResponse>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<BaseJikanResponse<ICollection<CharacterAnimeographyEntryResponse>>> GetCharacterAnimeAsync(long id, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(id, nameof(id));

        string[] endpointParts =
        {
            JikanEndpointConsts.Characters,
            id.ToString(),
            JikanEndpointConsts.Anime
        };
        return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<CharacterAnimeographyEntryResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<BaseJikanResponse<ICollection<CharacterMangaographyEntryResponse>>> GetCharacterMangaAsync(long id, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(id, nameof(id));

        string[] endpointParts =
        {
            JikanEndpointConsts.Characters,
            id.ToString(),
            JikanEndpointConsts.Manga
        };
        return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<CharacterMangaographyEntryResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<BaseJikanResponse<ICollection<VoiceActorEntryResponse>>> GetCharacterVoiceActorsAsync(long id, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(id, nameof(id));

        string[] endpointParts =
        {
            JikanEndpointConsts.Characters,
            id.ToString(),
            JikanEndpointConsts.Voices
        };
        return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<VoiceActorEntryResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<BaseJikanResponse<ICollection<ImagesSetResponse>>> GetCharacterPicturesAsync(long id, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(id, nameof(id));

        string[] endpointParts =
        {
            JikanEndpointConsts.Characters,
            id.ToString(),
            JikanEndpointConsts.Pictures
        };
        return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<ImagesSetResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<BaseJikanResponse<CharacterResponseFull>> GetCharacterFullDataAsync(long id, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(id, nameof(id));

        string[] endpointParts =
        {
            JikanEndpointConsts.Characters,
            id.ToString(),
            JikanEndpointConsts.Full
        };
        return await ExecuteGetRequestAsync<BaseJikanResponse<CharacterResponseFull>>(endpointParts, ct);
    }

    #endregion Character methods

    #region Manga methods

    /// <inheritdoc />
    public async Task<BaseJikanResponse<MangaResponse>> GetMangaAsync(long id, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(id, nameof(id));

        string[] endpointParts =
        {
            JikanEndpointConsts.Manga,
            id.ToString()
        };
        return await ExecuteGetRequestAsync<BaseJikanResponse<MangaResponse>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<BaseJikanResponse<ICollection<MangaCharacterResponse>>> GetMangaCharactersAsync(long id, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(id, nameof(id));

        string[] endpointParts =
        {
            JikanEndpointConsts.Manga,
            id.ToString(),
            JikanEndpointConsts.Characters
        };
        return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<MangaCharacterResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<PaginatedJikanResponse<ICollection<NewsResponse>>> GetMangaNewsAsync(long id, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(id, nameof(id));

        string[] endpointParts =
        {
            JikanEndpointConsts.Manga,
            id.ToString(),
            JikanEndpointConsts.News
        };
        return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<NewsResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<PaginatedJikanResponse<ICollection<NewsResponse>>> GetMangaNewsAsync(long id, int page, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        Guard.IsGreaterThanZero(page, nameof(page));

        var queryParams = $"?page={page}";
        string[] endpointParts =
        {
            JikanEndpointConsts.Manga,
            id.ToString(),
            JikanEndpointConsts.News + queryParams
        };
        return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<NewsResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<BaseJikanResponse<ICollection<ForumTopicResponse>>> GetMangaForumTopicsAsync(long id, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(id, nameof(id));

        string[] endpointParts =
        {
            JikanEndpointConsts.Manga,
            id.ToString(),
            JikanEndpointConsts.Forum
        };
        return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<ForumTopicResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<BaseJikanResponse<ICollection<ImagesSetResponse>>> GetMangaPicturesAsync(long id, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(id, nameof(id));

        string[] endpointParts =
        {
            JikanEndpointConsts.Manga,
            id.ToString(),
            JikanEndpointConsts.Pictures
        };
        return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<ImagesSetResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<BaseJikanResponse<MangaStatisticsResponse>> GetMangaStatisticsAsync(long id, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(id, nameof(id));

        string[] endpointParts =
        {
            JikanEndpointConsts.Manga,
            id.ToString(),
            JikanEndpointConsts.Statistics
        };
        return await ExecuteGetRequestAsync<BaseJikanResponse<MangaStatisticsResponse>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<BaseJikanResponse<MoreInfoResponse>> GetMangaMoreInfoAsync(long id, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(id, nameof(id));

        string[] endpointParts =
        {
            JikanEndpointConsts.Manga,
            id.ToString(),
            JikanEndpointConsts.MoreInfo
        };
        return await ExecuteGetRequestAsync<BaseJikanResponse<MoreInfoResponse>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<PaginatedJikanResponse<ICollection<MangaUserUpdateResponse>>> GetMangaUserUpdatesAsync(long id, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(id, nameof(id));

        string[] endpointParts =
        {
            JikanEndpointConsts.Manga,
            id.ToString(),
            JikanEndpointConsts.UserUpdates
        };
        return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<MangaUserUpdateResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<PaginatedJikanResponse<ICollection<MangaUserUpdateResponse>>> GetMangaUserUpdatesAsync(long id, int page, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        Guard.IsGreaterThanZero(page, nameof(page));

        var queryParams = $"?page={page}";
        string[] endpointParts =
        {
            JikanEndpointConsts.Manga,
            id.ToString(),
            JikanEndpointConsts.UserUpdates + queryParams
        };
        return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<MangaUserUpdateResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<BaseJikanResponse<ICollection<RecommendationResponse>>> GetMangaRecommendationsAsync(long id, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(id, nameof(id));

        string[] endpointParts =
        {
            JikanEndpointConsts.Manga,
            id.ToString(),
            JikanEndpointConsts.Recommendations
        };
        return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<RecommendationResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<PaginatedJikanResponse<ICollection<ReviewResponse>>> GetMangaReviewsAsync(long id, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(id, nameof(id));

        string[] endpointParts =
        {
            JikanEndpointConsts.Manga,
            id.ToString(),
            JikanEndpointConsts.Reviews
        };
        return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<ReviewResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<BaseJikanResponse<ICollection<RelatedEntryResponse>>> GetMangaRelationsAsync(long id, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(id, nameof(id));

        string[] endpointParts =
        {
            JikanEndpointConsts.Manga,
            id.ToString(),
            JikanEndpointConsts.Relations
        };
        return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<RelatedEntryResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<BaseJikanResponse<ICollection<ExternalLinkResponse>>> GetMangaExternalLinksAsync(long id, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(id, nameof(id));

        string[] endpointParts =
        {
            JikanEndpointConsts.Manga,
            id.ToString(),
            JikanEndpointConsts.External
        };
        return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<ExternalLinkResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<BaseJikanResponse<MangaResponseFull>> GetMangaFullDataAsync(long id, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(id, nameof(id));

        string[] endpointParts =
        {
            JikanEndpointConsts.Manga,
            id.ToString(),
            JikanEndpointConsts.Full
        };
        return await ExecuteGetRequestAsync<BaseJikanResponse<MangaResponseFull>>(endpointParts, ct);
    }

    #endregion Manga methods

    #region Person methods

    /// <inheritdoc />
    public async Task<BaseJikanResponse<PersonResponse>> GetPersonAsync(long id, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(id, nameof(id));

        string[] endpointParts =
        {
            JikanEndpointConsts.People,
            id.ToString()
        };
        return await ExecuteGetRequestAsync<BaseJikanResponse<PersonResponse>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<BaseJikanResponse<ICollection<PersonAnimeographyEntryResponse>>> GetPersonAnimeAsync(long id, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(id, nameof(id));

        string[] endpointParts =
        {
            JikanEndpointConsts.People,
            id.ToString(),
            JikanEndpointConsts.Anime
        };
        return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<PersonAnimeographyEntryResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<BaseJikanResponse<ICollection<PersonMangaographyEntryResponse>>> GetPersonMangaAsync(long id, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(id, nameof(id));

        string[] endpointParts =
        {
            JikanEndpointConsts.People,
            id.ToString(),
            JikanEndpointConsts.Manga
        };
        return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<PersonMangaographyEntryResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<BaseJikanResponse<ICollection<VoiceActingRoleResponse>>> GetPersonVoiceActingRolesAsync(long id, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(id, nameof(id));

        string[] endpointParts =
        {
            JikanEndpointConsts.People,
            id.ToString(),
            JikanEndpointConsts.Voices
        };
        return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<VoiceActingRoleResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<BaseJikanResponse<ICollection<ImagesSetResponse>>> GetPersonPicturesAsync(long id, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(id, nameof(id));

        string[] endpointParts =
        {
            JikanEndpointConsts.People,
            id.ToString(),
            JikanEndpointConsts.Pictures
        };
        return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<ImagesSetResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<BaseJikanResponse<PersonResponseFull>> GetPersonFullDataAsync(long id, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(id, nameof(id));

        string[] endpointParts =
        {
            JikanEndpointConsts.People,
            id.ToString(),
            JikanEndpointConsts.Full
        };
        return await ExecuteGetRequestAsync<BaseJikanResponse<PersonResponseFull>>(endpointParts, ct);
    }

    #endregion Person methods

    #region Season methods

    /// <inheritdoc />
    public async Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetSeasonAsync(int year, Season season, CancellationToken ct = default)
    {
        Guard.IsValid(x => x is >= 1000 and < 10000, year, nameof(year));
        Guard.IsValidEnum(season, nameof(season));

        string[] endpointParts =
        {
            JikanEndpointConsts.Seasons,
            year.ToString(),
            season.GetEnumMemberValue() ?? throw InvalidEnumException<Season>.EnumMember(season)
        };
        return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<AnimeResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetSeasonAsync(int year, Season season,
        int page, CancellationToken ct = default)
    {
        Guard.IsValid(x => x is >= 1000 and < 10000, year, nameof(year));
        Guard.IsValidEnum(season, nameof(season));
        Guard.IsGreaterThanZero(page, nameof(page));

        var queryParams = $"?page={page}";
        string[] endpointParts =
        {
            JikanEndpointConsts.Seasons,
            year.ToString(),
            (season.GetEnumMemberValue() ?? throw InvalidEnumException<Season>.EnumMember(season)) + queryParams
        };
        return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<AnimeResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<PaginatedJikanResponse<ICollection<SeasonArchiveResponse>>> GetSeasonArchiveAsync(CancellationToken ct = default)
    {
        string[] endpointParts =
        {
            JikanEndpointConsts.Seasons
        };
        return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<SeasonArchiveResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetCurrentSeasonAsync(CancellationToken ct = default)
    {
        string[] endpointParts =
        {
            JikanEndpointConsts.Seasons,
            JikanEndpointConsts.Now
        };
        return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<AnimeResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetCurrentSeasonAsync(int page, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(page, nameof(page));

        var queryParams = $"?page={page}";
        string[] endpointParts =
        {
            JikanEndpointConsts.Seasons,
            JikanEndpointConsts.Now + queryParams
        };
        return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<AnimeResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetUpcomingSeasonAsync(CancellationToken ct = default)
    {
        string[] endpointParts =
        {
            JikanEndpointConsts.Seasons,
            JikanEndpointConsts.Upcoming
        };
        return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<AnimeResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetUpcomingSeasonAsync(int page, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(page, nameof(page));

        var queryParams = $"?page={page}";
        string[] endpointParts =
        {
            JikanEndpointConsts.Seasons,
            JikanEndpointConsts.Upcoming + queryParams
        };
        return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<AnimeResponse>>>(endpointParts, ct);
    }

    #endregion Season methods

    #region Schedule methods

    /// <inheritdoc />
    public async Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetScheduleAsync(CancellationToken ct = default)
    {
        string[] endpointParts =
        {
            JikanEndpointConsts.Schedules
        };
        return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<AnimeResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetScheduleAsync(int page, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(page, nameof(page));

        var queryParams = $"?page={page}";
        string[] endpointParts =
        {
            JikanEndpointConsts.Schedules + queryParams
        };
        return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<AnimeResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetScheduleAsync(ScheduledDay scheduledDay, CancellationToken ct = default)
    {
        Guard.IsValidEnum(scheduledDay, nameof(scheduledDay));

        var queryParams = $"?filter={scheduledDay.GetEnumMemberValue()}";
        string[] endpointParts =
        {
            JikanEndpointConsts.Schedules + queryParams
        };
        return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<AnimeResponse>>>(endpointParts, ct);
    }

    #endregion Schedule methods

    #region Top methods

    /// <inheritdoc />
    public async Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetTopAnimeAsync(CancellationToken ct = default)
    {
        string[] endpointParts =
        {
            JikanEndpointConsts.TopList,
            JikanEndpointConsts.Anime
        };
        return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<AnimeResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetTopAnimeAsync(int page, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(page, nameof(page));

        var queryParams = $"?page={page}";
        string[] endpointParts =
        {
            JikanEndpointConsts.TopList,
            JikanEndpointConsts.Anime + queryParams
        };
        return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<AnimeResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetTopAnimeAsync(TopAnimeFilter filter, CancellationToken ct = default)
    {
        Guard.IsValidEnum(filter, nameof(filter));

        var queryParams = $"?filter={filter.GetEnumMemberValue() ?? throw InvalidEnumException<TopAnimeFilter>.EnumMember(filter)}";
        string[] endpointParts =
        {
            JikanEndpointConsts.TopList,
            JikanEndpointConsts.Anime + queryParams
        };
        return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<AnimeResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetTopAnimeAsync(TopAnimeFilter filter, int page, CancellationToken ct = default)
    {
        Guard.IsValidEnum(filter, nameof(filter));
        Guard.IsGreaterThanZero(page, nameof(page));

        var queryParams = $"?page={page}&filter={filter.GetEnumMemberValue() ?? throw InvalidEnumException<TopAnimeFilter>.EnumMember(filter)}";
        string[] endpointParts =
        {
            JikanEndpointConsts.TopList,
            JikanEndpointConsts.Anime + queryParams
        };
        return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<AnimeResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<PaginatedJikanResponse<ICollection<MangaResponse>>> GetTopMangaAsync(CancellationToken ct = default)
    {
        string[] endpointParts =
        {
            JikanEndpointConsts.TopList,
            JikanEndpointConsts.Manga
        };
        return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<MangaResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<PaginatedJikanResponse<ICollection<MangaResponse>>> GetTopMangaAsync(int page, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(page, nameof(page));

        var queryParams = $"?page={page}";
        string[] endpointParts =
        {
            JikanEndpointConsts.TopList,
            JikanEndpointConsts.Manga + queryParams
        };
        return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<MangaResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<PaginatedJikanResponse<ICollection<PersonResponse>>> GetTopPeopleAsync(CancellationToken ct = default)
    {
        string[] endpointParts =
        {
            JikanEndpointConsts.TopList,
            JikanEndpointConsts.People
        };
        return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<PersonResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<PaginatedJikanResponse<ICollection<PersonResponse>>> GetTopPeopleAsync(int page, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(page, nameof(page));

        var queryParams = $"?page={page}";
        string[] endpointParts =
        {
            JikanEndpointConsts.TopList,
            JikanEndpointConsts.People + queryParams
        };
        return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<PersonResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<PaginatedJikanResponse<ICollection<CharacterResponse>>> GetTopCharactersAsync(CancellationToken ct = default)
    {
        string[] endpointParts =
        {
            JikanEndpointConsts.TopList,
            JikanEndpointConsts.Characters
        };
        return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<CharacterResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<PaginatedJikanResponse<ICollection<CharacterResponse>>> GetTopCharactersAsync(int page, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(page, nameof(page));

        var queryParams = $"?page={page}";
        string[] endpointParts =
        {
            JikanEndpointConsts.TopList,
            JikanEndpointConsts.Characters + queryParams
        };
        return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<CharacterResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<PaginatedJikanResponse<ICollection<ReviewResponse>>> GetTopReviewsAsync(CancellationToken ct = default)
    {
        string[] endpointParts =
        {
            JikanEndpointConsts.TopList,
            JikanEndpointConsts.Reviews
        };
        return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<ReviewResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<PaginatedJikanResponse<ICollection<ReviewResponse>>> GetTopReviewsAsync(int page, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(page, nameof(page));

        var queryParams = $"?page={page}";
        string[] endpointParts =
        {
            JikanEndpointConsts.TopList,
            JikanEndpointConsts.Reviews + queryParams
        };
        return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<ReviewResponse>>>(endpointParts, ct);
    }

    #endregion Top methods

    #region Genre methods

    /// <inheritdoc />
    public async Task<BaseJikanResponse<ICollection<GenreResponse>>> GetAnimeGenresAsync(CancellationToken ct = default)
    {
        string[] endpointParts =
        {
            JikanEndpointConsts.Genres,
            JikanEndpointConsts.Anime
        };
        return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<GenreResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<BaseJikanResponse<ICollection<GenreResponse>>> GetAnimeGenresAsync(GenresFilter filter, CancellationToken ct = default)
    {
        Guard.IsValidEnum(filter, nameof(filter));

        var queryParams = $"?filter={filter.GetEnumMemberValue() ?? throw InvalidEnumException<GenresFilter>.EnumMember(filter)}";
        string[] endpointParts =
        {
            JikanEndpointConsts.Genres,
            JikanEndpointConsts.Anime + queryParams
        };
        return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<GenreResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<BaseJikanResponse<ICollection<GenreResponse>>> GetMangaGenresAsync(CancellationToken ct = default)
    {
        string[] endpointParts =
        {
            JikanEndpointConsts.Genres,
            JikanEndpointConsts.Manga
        };
        return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<GenreResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<BaseJikanResponse<ICollection<GenreResponse>>> GetMangaGenresAsync(GenresFilter filter, CancellationToken ct = default)
    {
        Guard.IsValidEnum(filter, nameof(filter));

        var queryParams = $"?filter={filter.GetEnumMemberValue() ?? throw InvalidEnumException<GenresFilter>.EnumMember(filter)}";
        string[] endpointParts =
        {
            JikanEndpointConsts.Genres,
            JikanEndpointConsts.Manga + queryParams
        };
        return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<GenreResponse>>>(endpointParts, ct);
    }

    #endregion Genre methods

    #region Producer methods

    /// <inheritdoc />
    public async Task<PaginatedJikanResponse<ICollection<ProducerResponse>>> GetProducersAsync(CancellationToken ct = default)
    {
        string[] endpointParts =
        {
            JikanEndpointConsts.Producers
        };
        return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<ProducerResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<PaginatedJikanResponse<ICollection<ProducerResponse>>> GetProducersAsync(int page, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(page, nameof(page));

        var queryParams = $"?page={page}";
        string[] endpointParts =
        {
            JikanEndpointConsts.Producers + queryParams
        };
        return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<ProducerResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<BaseJikanResponse<ProducerResponse>> GetProducerAsync(long id, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(id, nameof(id));

        string[] endpointParts =
        {
            JikanEndpointConsts.Producers, id.ToString()
        };
        return await ExecuteGetRequestAsync<BaseJikanResponse<ProducerResponse>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<BaseJikanResponse<ICollection<ExternalLinkResponse>>> GetProducerExternalLinksAsync(long id, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(id, nameof(id));

        string[] endpointParts =
        {
            JikanEndpointConsts.Producers, id.ToString(), JikanEndpointConsts.External
        };
        return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<ExternalLinkResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<BaseJikanResponse<ProducerResponseFull>> GetProducerFullDataAsync(long id, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(id, nameof(id));

        string[] endpointParts =
        {
            JikanEndpointConsts.Producers,
            id.ToString(),
            JikanEndpointConsts.Full
        };
        return await ExecuteGetRequestAsync<BaseJikanResponse<ProducerResponseFull>>(endpointParts, ct);
    }

    #endregion Producer methods

    #region Magazine methods

    /// <inheritdoc />
    public async Task<PaginatedJikanResponse<ICollection<MagazineResponse>>> GetMagazinesAsync(CancellationToken ct = default)
    {
        string[] endpointParts =
        {
            JikanEndpointConsts.Magazines
        };
        return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<MagazineResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<PaginatedJikanResponse<ICollection<MagazineResponse>>> GetMagazinesAsync(int page, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(page, nameof(page));

        var queryParams = $"?page={page}";
        string[] endpointParts =
        {
            JikanEndpointConsts.Magazines + queryParams
        };
        return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<MagazineResponse>>>(endpointParts, ct);
    }

    #endregion Magazine methods

    #region Club methods

    /// <inheritdoc />
    public async Task<BaseJikanResponse<ClubResponse>> GetClubAsync(long id, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        string[] endpointParts = { JikanEndpointConsts.Clubs, id.ToString() };
        return await ExecuteGetRequestAsync<BaseJikanResponse<ClubResponse>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<PaginatedJikanResponse<ICollection<ClubMemberResponse>>> GetClubMembersAsync(long id,
        CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        string[] endpointParts = { JikanEndpointConsts.Clubs, id.ToString(), JikanEndpointConsts.Members };
        return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<ClubMemberResponse>>>(endpointParts,
            ct);
    }

    /// <inheritdoc />
    public async Task<PaginatedJikanResponse<ICollection<ClubMemberResponse>>> GetClubMembersAsync(long id,
        int page, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        Guard.IsGreaterThanZero(page, nameof(page));
        var queryParams = $"?page={page}";
        string[] endpointParts =
            { JikanEndpointConsts.Clubs, id.ToString(), JikanEndpointConsts.Members + queryParams };
        return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<ClubMemberResponse>>>(endpointParts,
            ct);
    }

    /// <inheritdoc />
    public async Task<BaseJikanResponse<ICollection<ClubStaffResponse>>> GetClubStaffAsync(long id,
        CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        string[] endpointParts = { JikanEndpointConsts.Clubs, id.ToString(), JikanEndpointConsts.Staff };
        return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<ClubStaffResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<BaseJikanResponse<ClubRelationsResponse>> GetClubRelationsAsync(long id,
        CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        string[] endpointParts = { JikanEndpointConsts.Clubs, id.ToString(), JikanEndpointConsts.Relations };
        return await ExecuteGetRequestAsync<BaseJikanResponse<ClubRelationsResponse>>(endpointParts, ct);
    }

    #endregion Club methods

    #region User methods

    /// <inheritdoc />
    public async Task<BaseJikanResponse<UserProfileResponse>> GetUserProfileAsync(string username, CancellationToken ct = default)
    {
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));

        string[] endpointParts =
        {
            JikanEndpointConsts.Users,
            username
        };
        return await ExecuteGetRequestAsync<BaseJikanResponse<UserProfileResponse>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<BaseJikanResponse<UserStatisticsResponse>> GetUserStatisticsAsync(string username, CancellationToken ct = default)
    {
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));

        string[] endpointParts =
        {
            JikanEndpointConsts.Users,
            username,
            JikanEndpointConsts.Statistics
        };
        return await ExecuteGetRequestAsync<BaseJikanResponse<UserStatisticsResponse>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<BaseJikanResponse<UserFavoritesResponse>> GetUserFavoritesAsync(string username, CancellationToken ct = default)
    {
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));

        string[] endpointParts =
        {
            JikanEndpointConsts.Users,
            username,
            JikanEndpointConsts.Favorites
        };
        return await ExecuteGetRequestAsync<BaseJikanResponse<UserFavoritesResponse>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<BaseJikanResponse<UserUpdatesResponse>> GetUserUpdatesAsync(string username, CancellationToken ct = default)
    {
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));

        string[] endpointParts =
        {
            JikanEndpointConsts.Users,
            username,
            JikanEndpointConsts.UserUpdates
        };
        return await ExecuteGetRequestAsync<BaseJikanResponse<UserUpdatesResponse>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<BaseJikanResponse<UserAboutResponse>> GetUserAboutAsync(string username, CancellationToken ct = default)
    {
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));

        string[] endpointParts =
        {
            JikanEndpointConsts.Users,
            username,
            JikanEndpointConsts.About
        };
        return await ExecuteGetRequestAsync<BaseJikanResponse<UserAboutResponse>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<BaseJikanResponse<ICollection<HistoryEntryResponse>>> GetUserHistoryAsync(string username, CancellationToken ct = default)
    {
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));

        string[] endpointParts =
        {
            JikanEndpointConsts.Users,
            username,
            JikanEndpointConsts.History
        };
        return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<HistoryEntryResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<BaseJikanResponse<ICollection<HistoryEntryResponse>>> GetUserHistoryAsync(string username, UserHistoryExtension userHistory, CancellationToken ct = default)
    {
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));
        Guard.IsValidEnum(userHistory, nameof(userHistory));

        var queryParams = $"?filter={userHistory.GetEnumMemberValue() ?? throw InvalidEnumException<UserHistoryExtension>.EnumMember(userHistory)}";
        string[] endpointParts =
        {
            JikanEndpointConsts.Users,
            username,
            JikanEndpointConsts.History + queryParams
        };
        return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<HistoryEntryResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<BaseJikanResponse<ICollection<AnimeListEntryResponse>>> GetUserAnimeListAsync(string username, CancellationToken ct = default)
    {
        ThrowIfDefaultEndpoint(nameof(GetUserAnimeListAsync));
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));

        string[] endpointParts =
        {
            JikanEndpointConsts.Users,
            username,
            JikanEndpointConsts.AnimeList
        };
        return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<AnimeListEntryResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<BaseJikanResponse<ICollection<AnimeListEntryResponse>>> GetUserAnimeListAsync(string username, int page, CancellationToken ct = default)
    {
        ThrowIfDefaultEndpoint(nameof(GetUserAnimeListAsync));
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));
        Guard.IsGreaterThanZero(page, nameof(page));

        var queryParams = $"?page={page}";
        string[] endpointParts =
        {
            JikanEndpointConsts.Users,
            username,
            JikanEndpointConsts.AnimeList + queryParams
        };
        return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<AnimeListEntryResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<BaseJikanResponse<ICollection<MangaListEntryResponse>>> GetUserMangaListAsync(string username, CancellationToken ct = default)
    {
        ThrowIfDefaultEndpoint(nameof(GetUserMangaListAsync));
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));

        string[] endpointParts =
        {
            JikanEndpointConsts.Users,
            username,
            JikanEndpointConsts.MangaList
        };
        return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<MangaListEntryResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<BaseJikanResponse<ICollection<MangaListEntryResponse>>> GetUserMangaListAsync(string username, int page, CancellationToken ct = default)
    {
        ThrowIfDefaultEndpoint(nameof(GetUserMangaListAsync));
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));
        Guard.IsGreaterThanZero(page, nameof(page));

        var queryParams = $"?page={page}";
        string[] endpointParts =
        {
            JikanEndpointConsts.Users,
            username,
            JikanEndpointConsts.MangaList + queryParams
        };
        return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<MangaListEntryResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<PaginatedJikanResponse<ICollection<FriendResponse>>> GetUserFriendsAsync(string username, CancellationToken ct = default)
    {
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));

        string[] endpointParts =
        {
            JikanEndpointConsts.Users,
            username,
            JikanEndpointConsts.Friends
        };
        return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<FriendResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<PaginatedJikanResponse<ICollection<FriendResponse>>> GetUserFriendsAsync(string username, int page, CancellationToken ct = default)
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
        return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<FriendResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<PaginatedJikanResponse<ICollection<ReviewResponse>>> GetUserReviewsAsync(string username, CancellationToken ct = default)
    {
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));

        string[] endpointParts =
        {
            JikanEndpointConsts.Users,
            username,
            JikanEndpointConsts.Reviews
        };
        return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<ReviewResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<PaginatedJikanResponse<ICollection<ReviewResponse>>> GetUserReviewsAsync(string username, int page, CancellationToken ct = default)
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
        return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<ReviewResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<PaginatedJikanResponse<ICollection<UserRecommendationResponse>>> GetUserRecommendationsAsync(string username, CancellationToken ct = default)
    {
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));

        string[] endpointParts =
        {
            JikanEndpointConsts.Users,
            username,
            JikanEndpointConsts.Recommendations
        };
        return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<UserRecommendationResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<PaginatedJikanResponse<ICollection<UserRecommendationResponse>>> GetUserRecommendationsAsync(string username, int page, CancellationToken ct = default)
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
        return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<UserRecommendationResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<PaginatedJikanResponse<ICollection<MalUrlResponse>>> GetUserClubsAsync(string username, CancellationToken ct = default)
    {
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));

        string[] endpointParts =
        {
            JikanEndpointConsts.Users,
            username,
            JikanEndpointConsts.Clubs
        };
        return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<MalUrlResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<PaginatedJikanResponse<ICollection<MalUrlResponse>>> GetUserClubsAsync(string username, int page, CancellationToken ct = default)
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
        return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<MalUrlResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<BaseJikanResponse<ICollection<ExternalLinkResponse>>> GetUserExternalLinksAsync(string username, CancellationToken ct = default)
    {
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));

        string[] endpointParts =
        {
            JikanEndpointConsts.Users,
            username,
            JikanEndpointConsts.External
        };
        return await ExecuteGetRequestAsync<BaseJikanResponse<ICollection<ExternalLinkResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<BaseJikanResponse<UserResponseFull>> GetUserFullDataAsync(string username, CancellationToken ct = default)
    {
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));

        string[] endpointParts =
        {
            JikanEndpointConsts.Users,
            username,
            JikanEndpointConsts.Full
        };
        return await ExecuteGetRequestAsync<BaseJikanResponse<UserResponseFull>>(endpointParts, ct);
    }

    #endregion User methods

    #region GetRandom methods

    /// <inheritdoc/>
    public async Task<BaseJikanResponse<AnimeResponse>> GetRandomAnimeAsync(CancellationToken ct = default)
    {
        string[] endpointParts =
        {
            JikanEndpointConsts.Random,
            JikanEndpointConsts.Anime
        };
        return await ExecuteGetRequestAsync<BaseJikanResponse<AnimeResponse>>(endpointParts, ct);
    }

    /// <inheritdoc/>
    public async Task<BaseJikanResponse<MangaResponse>> GetRandomMangaAsync(CancellationToken ct = default)
    {
        string[] endpointParts =
        {
            JikanEndpointConsts.Random,
            JikanEndpointConsts.Manga
        };
        return await ExecuteGetRequestAsync<BaseJikanResponse<MangaResponse>>(endpointParts, ct);
    }

    /// <inheritdoc/>
    public async Task<BaseJikanResponse<CharacterResponse>> GetRandomCharacterAsync(CancellationToken ct = default)
    {
        string[] endpointParts =
        {
            JikanEndpointConsts.Random,
            JikanEndpointConsts.Characters
        };
        return await ExecuteGetRequestAsync<BaseJikanResponse<CharacterResponse>>(endpointParts, ct);
    }

    /// <inheritdoc/>
    public async Task<BaseJikanResponse<PersonResponse>> GetRandomPersonAsync(CancellationToken ct = default)
    {
        string[] endpointParts =
        {
            JikanEndpointConsts.Random,
            JikanEndpointConsts.People
        };
        return await ExecuteGetRequestAsync<BaseJikanResponse<PersonResponse>>(endpointParts, ct);
    }

    /// <inheritdoc/>
    public async Task<BaseJikanResponse<UserProfileResponse>> GetRandomUserAsync(CancellationToken ct = default)
    {
        string[] endpointParts =
        {
            JikanEndpointConsts.Random,
            JikanEndpointConsts.Users
        };
        return await ExecuteGetRequestAsync<BaseJikanResponse<UserProfileResponse>>(endpointParts, ct);
    }

    #endregion GetRandom methods

    #region Watch methods

    /// <inheritdoc />
    public async Task<PaginatedJikanResponse<ICollection<WatchEpisodeResponse>>> GetWatchRecentEpisodesAsync(CancellationToken ct = default)
    {
        string[] endpointParts =
        {
            JikanEndpointConsts.Watch,
            JikanEndpointConsts.Episodes
        };
        return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<WatchEpisodeResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<PaginatedJikanResponse<ICollection<WatchEpisodeResponse>>> GetWatchPopularEpisodesAsync(CancellationToken ct = default)
    {
        string[] endpointParts =
        {
            JikanEndpointConsts.Watch,
            JikanEndpointConsts.Episodes,
            JikanEndpointConsts.Popular
        };
        return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<WatchEpisodeResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<PaginatedJikanResponse<ICollection<WatchPromoVideoResponse>>> GetWatchRecentPromosAsync(CancellationToken ct = default)
    {
        string[] endpointParts =
        {
            JikanEndpointConsts.Watch,
            JikanEndpointConsts.Promos
        };
        return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<WatchPromoVideoResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<PaginatedJikanResponse<ICollection<WatchPromoVideoResponse>>> GetWatchPopularPromosAsync(CancellationToken ct = default)
    {
        string[] endpointParts =
        {
            JikanEndpointConsts.Watch,
            JikanEndpointConsts.Promos,
            JikanEndpointConsts.Popular
        };
        return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<WatchPromoVideoResponse>>>(endpointParts, ct);
    }

    #endregion

    #region Reviews methods

    /// <inheritdoc />
    public async Task<PaginatedJikanResponse<ICollection<ReviewResponse>>> GetRecentAnimeReviewsAsync(CancellationToken ct = default)
    {
        string[] endpointParts =
        {
            JikanEndpointConsts.Reviews,
            JikanEndpointConsts.Anime
        };
        return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<ReviewResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<PaginatedJikanResponse<ICollection<ReviewResponse>>> GetRecentAnimeReviewsAsync(int page, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(page, nameof(page));

        var queryParams = $"?page={page}";
        string[] endpointParts =
        {
            JikanEndpointConsts.Reviews,
            JikanEndpointConsts.Anime + queryParams
        };
        return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<ReviewResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<PaginatedJikanResponse<ICollection<ReviewResponse>>> GetRecentMangaReviewsAsync(CancellationToken ct = default)
    {
        string[] endpointParts =
        {
            JikanEndpointConsts.Reviews,
            JikanEndpointConsts.Manga
        };
        return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<ReviewResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<PaginatedJikanResponse<ICollection<ReviewResponse>>> GetRecentMangaReviewsAsync(int page, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(page, nameof(page));

        var queryParams = $"?page={page}";
        string[] endpointParts =
        {
            JikanEndpointConsts.Reviews,
            JikanEndpointConsts.Manga + queryParams
        };
        return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<ReviewResponse>>>(endpointParts, ct);
    }

    #endregion

    #region Recommendations methods

    /// <inheritdoc />
    public async Task<PaginatedJikanResponse<ICollection<UserRecommendationResponse>>> GetRecentAnimeRecommendationsAsync(CancellationToken ct = default)
    {
        string[] endpointParts =
        {
            JikanEndpointConsts.Recommendations,
            JikanEndpointConsts.Anime
        };
        return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<UserRecommendationResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<PaginatedJikanResponse<ICollection<UserRecommendationResponse>>> GetRecentAnimeRecommendationsAsync(int page, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(page, nameof(page));

        var queryParams = $"?page={page}";
        string[] endpointParts =
        {
            JikanEndpointConsts.Recommendations,
            JikanEndpointConsts.Anime + queryParams
        };
        return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<UserRecommendationResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<PaginatedJikanResponse<ICollection<UserRecommendationResponse>>> GetRecentMangaRecommendationsAsync(CancellationToken ct = default)
    {
        string[] endpointParts =
        {
            JikanEndpointConsts.Recommendations,
            JikanEndpointConsts.Manga
        };
        return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<UserRecommendationResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public async Task<PaginatedJikanResponse<ICollection<UserRecommendationResponse>>> GetRecentMangaRecommendationsAsync(int page, CancellationToken ct = default)
    {
        Guard.IsGreaterThanZero(page, nameof(page));

        var queryParams = $"?page={page}";
        string[] endpointParts =
        {
            JikanEndpointConsts.Recommendations,
            JikanEndpointConsts.Manga + queryParams
        };
        return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<UserRecommendationResponse>>>(endpointParts, ct);
    }

    #endregion

    #region Search methods

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> SearchAnimeAsync(string query, CancellationToken ct = default)
    {
        return SearchAnimeAsync(new AnimeSearchConfig { Query = query }, ct);
    }

    /// <inheritdoc />
    public async Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> SearchAnimeAsync(AnimeSearchConfig searchConfig, CancellationToken ct = default)
    {
        Guard.IsNotNull(searchConfig, nameof(searchConfig));

        string[] endpointParts =
        {
            JikanEndpointConsts.Anime + searchConfig.ConfigToString()
        };
        return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<AnimeResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<MangaResponse>>> SearchMangaAsync(string query, CancellationToken ct = default)
    {
        return SearchMangaAsync(new MangaSearchConfig { Query = query }, ct);
    }

    /// <inheritdoc />
    public async Task<PaginatedJikanResponse<ICollection<MangaResponse>>> SearchMangaAsync(MangaSearchConfig searchConfig, CancellationToken ct = default)
    {
        Guard.IsNotNull(searchConfig, nameof(searchConfig));

        string[] endpointParts =
        {
            JikanEndpointConsts.Manga + searchConfig.ConfigToString()
        };
        return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<MangaResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<PersonResponse>>> SearchPersonAsync(string query, CancellationToken ct = default)
    {
        return SearchPersonAsync(new PersonSearchConfig { Query = query }, ct);
    }

    /// <inheritdoc />
    public async Task<PaginatedJikanResponse<ICollection<PersonResponse>>> SearchPersonAsync(PersonSearchConfig searchConfig, CancellationToken ct = default)
    {
        Guard.IsNotNull(searchConfig, nameof(searchConfig));

        string[] endpointParts =
        {
            JikanEndpointConsts.People + searchConfig.ConfigToString()
        };
        return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<PersonResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<CharacterResponse>>> SearchCharacterAsync(string query, CancellationToken ct = default)
    {
        return SearchCharacterAsync(new CharacterSearchConfig { Query = query }, ct);
    }

    /// <inheritdoc />
    public async Task<PaginatedJikanResponse<ICollection<CharacterResponse>>> SearchCharacterAsync(CharacterSearchConfig searchConfig, CancellationToken ct = default)
    {
        Guard.IsNotNull(searchConfig, nameof(searchConfig));

        string[] endpointParts =
        {
            JikanEndpointConsts.Characters + searchConfig.ConfigToString()
        };
        return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<CharacterResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<UserMetadataResponse>>> SearchUserAsync(string query, CancellationToken ct = default)
    {
        return SearchUserAsync(new UserSearchConfig { Query = query }, ct);
    }

    /// <inheritdoc />
    public async Task<PaginatedJikanResponse<ICollection<UserMetadataResponse>>> SearchUserAsync(UserSearchConfig searchConfig, CancellationToken ct = default)
    {
        Guard.IsNotNull(searchConfig, nameof(searchConfig));

        string[] endpointParts =
        {
            JikanEndpointConsts.Users + searchConfig.ConfigToString()
        };
        return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<UserMetadataResponse>>>(endpointParts, ct);
    }

    /// <inheritdoc />
    public Task<PaginatedJikanResponse<ICollection<ClubResponse>>> SearchClubAsync(string query, CancellationToken ct = default)
    {
        return SearchClubAsync(new ClubSearchConfig { Query = query }, ct);
    }

    /// <inheritdoc />
    public async Task<PaginatedJikanResponse<ICollection<ClubResponse>>> SearchClubAsync(ClubSearchConfig searchConfig, CancellationToken ct = default)
    {
        Guard.IsNotNull(searchConfig, nameof(searchConfig));

        string[] endpointParts =
        {
            JikanEndpointConsts.Users + searchConfig.ConfigToString()
        };
        return await ExecuteGetRequestAsync<PaginatedJikanResponse<ICollection<ClubResponse>>>(endpointParts, ct);
    }

    #endregion Search methods

    #endregion Public Methods
}