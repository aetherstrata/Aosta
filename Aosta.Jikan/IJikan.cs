using Aosta.Jikan.Enums;
using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;
using Aosta.Jikan.Models.Search;
using Aosta.Jikan.Query.Schedule;

namespace Aosta.Jikan;

/// <summary>
/// Interface for Jikan.net client
/// </summary>
public interface IJikan
{
	#region Anime requests

	/// <summary>
	/// Returns anime with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of anime.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Anime with given MAL id.</returns>
	Task<BaseJikanResponse<AnimeResponse>> GetAnimeAsync(long id, CancellationToken ct = default);

	/// <summary>
	/// Returns collections of characters of anime with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of anime.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Collection of characters of anime with given MAL id.</returns>
	Task<BaseJikanResponse<ICollection<AnimeCharacterResponse>>> GetAnimeCharactersAsync(long id, CancellationToken ct = default);

	/// <summary>
	/// Returns collections of staff of anime with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of anime.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Collection of staff of anime with given MAL id.</returns>
	Task<BaseJikanResponse<ICollection<AnimeStaffPositionResponse>>> GetAnimeStaffAsync(long id, CancellationToken ct = default);

	/// <summary>
	/// Returns list of episodes for anime with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of anime.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>List of episodes with details.</returns>
	Task<PaginatedJikanResponse<ICollection<AnimeEpisodeResponse>>> GetAnimeEpisodesAsync(long id, CancellationToken ct = default);

	/// <summary>
	/// Returns list of episodes for anime with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of anime.</param>
	/// <param name="page">Index of page folding 100 records of top ranging (e.g. 1 will return first 100 records, 2 will return record from 101 to 200 etc.)</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>List of episodes with details.</returns>
	Task<PaginatedJikanResponse<ICollection<AnimeEpisodeResponse>>> GetAnimeEpisodesAsync(long id, int page, CancellationToken ct = default);

	/// <summary>
	/// Returns details about specific episode.
	/// </summary>
	/// <param name="animeId">MAL id of anime.</param>
	/// <param name="episodeId">Id of episode.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Details about specific episode.</returns>
	Task<BaseJikanResponse<AnimeEpisodeResponse>> GetAnimeEpisodeAsync(long animeId, int episodeId, CancellationToken ct = default);

	/// <summary>
	/// Returns collections of news related to anime with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of anime.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Collections of news related to anime with given MAL id.</returns>
	Task<PaginatedJikanResponse<ICollection<NewsResponse>>> GetAnimeNewsAsync(long id, CancellationToken ct = default);

	/// <summary>
	/// Returns collections of news related to anime with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of anime.</param>
	/// <param name="page">Index of page folding 100 records of top ranging (e.g. 1 will return first 100 records, 2 will return record from 101 to 200 etc.)</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Collections of news related to anime with given MAL id.</returns>
	Task<PaginatedJikanResponse<ICollection<NewsResponse>>> GetAnimeNewsAsync(long id, int page, CancellationToken ct = default);

	/// <summary>
	/// Returns collections of forum topics related to anime with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of anime.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Collections of forum topics related to anime with given MAL id.</returns>
	Task<BaseJikanResponse<ICollection<ForumTopicResponse>>> GetAnimeForumTopicsAsync(long id, CancellationToken ct = default);

	/// <summary>
	/// Returns collections of forum topics related to anime with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of anime.</param>
	/// <param name="type">ForumTopicType filter</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Collections of forum topics related to anime with given MAL id.</returns>
	Task<BaseJikanResponse<ICollection<ForumTopicResponse>>> GetAnimeForumTopicsAsync(long id, ForumTopicType type, CancellationToken ct = default);

	/// <summary>
	/// Returns collections of videos related to anime with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of anime.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Collections of videos related to anime with given MAL id.</returns>
	Task<BaseJikanResponse<AnimeVideosResponse>> GetAnimeVideosAsync(long id, CancellationToken ct = default);

	/// <summary>
	/// Returns collections of links to pictures related to anime with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of anime.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Collections of links to pictures related to anime with given MAL id.</returns>
	Task<BaseJikanResponse<ICollection<ImagesSetResponse>>> GetAnimePicturesAsync(long id, CancellationToken ct = default);

	/// <summary>
	/// Returns statistics related to anime with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of anime.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Statistics related to anime with given MAL id.</returns>
	Task<BaseJikanResponse<AnimeStatisticsResponse>> GetAnimeStatisticsAsync(long id, CancellationToken ct = default);

	/// <summary>
	/// Returns additional information related to anime with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of anime.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Additional information related to anime with given MAL id.</returns>
	Task<BaseJikanResponse<MoreInfoResponse>> GetAnimeMoreInfoAsync(long id, CancellationToken ct = default);

	/// <summary>
	/// Returns collection of anime recommendation.
	/// </summary>
	/// <param name="id">MAL id of anime.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Collection of anime recommendation.</returns>
	Task<BaseJikanResponse<ICollection<RecommendationResponse>>> GetAnimeRecommendationsAsync(long id, CancellationToken ct = default);

	/// <summary>
	/// Returns collection of anime user updates.
	/// </summary>
	/// <param name="id">MAL id of anime.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Collection of anime user updates.</returns>
	Task<PaginatedJikanResponse<ICollection<AnimeUserUpdateResponse>>> GetAnimeUserUpdatesAsync(long id, CancellationToken ct = default);

	/// <summary>
	/// Returns collection of anime user updates.
	/// </summary>
	/// <param name="id">MAL id of anime.</param>
	/// <param name="page">Index of page folding 75 records of top ranging (e.g. 1 will return first 75 records, 2 will return record from 76 to 150 etc.)</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Collection of anime user updates.</returns>
	Task<PaginatedJikanResponse<ICollection<AnimeUserUpdateResponse>>> GetAnimeUserUpdatesAsync(long id, int page, CancellationToken ct = default);
	/// <summary>
	/// Returns collection of anime reviews.
	/// </summary>
	/// <param name="id">MAL id of anime.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Collection of anime reviews.</returns>
	Task<PaginatedJikanResponse<ICollection<ReviewResponse>>> GetAnimeReviewsAsync(long id, CancellationToken ct = default);

	/// <summary>
	/// Returns collection of anime reviews.
	/// </summary>
	/// <param name="id">MAL id of anime.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <param name="page">Index of page folding 20 records of top ranging (e.g. 1 will return first 20 records, 2 will return record from 21 to 40 etc.)</param>
	/// <returns>Collection of anime reviews.</returns>
	Task<PaginatedJikanResponse<ICollection<ReviewResponse>>> GetAnimeReviewsAsync(long id, int page, CancellationToken ct = default);

	/// <summary>
	/// Returns collection of anime related entries.
	/// </summary>
	/// <param name="id">MAL id of anime.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Collection of anime related entries.</returns>
	Task<BaseJikanResponse<ICollection<RelatedEntryResponse>>> GetAnimeRelationsAsync(long id, CancellationToken ct = default);

	/// <summary>
	/// Returns collection of anime openings and endings.
	/// </summary>
	/// <param name="id">MAL id of anime.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Collection of anime openings and endings.</returns>
	Task<BaseJikanResponse<AnimeThemesResponse>> GetAnimeThemesAsync(long id, CancellationToken ct = default);

	/// <summary>
	/// Returns collection of external services links related to anime.
	/// </summary>
	/// <param name="id">MAL id of anime.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Collection of external services links related to anime.</returns>
	Task<BaseJikanResponse<ICollection<ExternalLinkResponse>>> GetAnimeExternalLinksAsync(long id, CancellationToken ct = default);

	/// <summary>
	/// Returns collection of external streaming services links related to anime.
	/// </summary>
	/// <param name="id">MAL id of anime.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Collection of external services links related to anime.</returns>
	Task<BaseJikanResponse<ICollection<ExternalLinkResponse>>> GetAnimeStreamingLinksAsync(long id, CancellationToken ct = default);

	/// <summary>
	/// Returns anime with additional data.
	/// </summary>
	/// <param name="id">MAL id of anime.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Anime with additional data.</returns>
	Task<BaseJikanResponse<AnimeResponseFull>> GetAnimeFullDataAsync(long id, CancellationToken ct = default);

	#endregion Anime requests

	#region Manga requests

	/// <summary>
	/// Returns manga with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of manga.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Manga with given MAL id.</returns>
	Task<BaseJikanResponse<MangaResponse>> GetMangaAsync(long id, CancellationToken ct = default);

	/// <summary>
	/// Returns collections of characters appearing in manga with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of manga.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Collections of characters appearing in manga with given MAL id.</returns>
	Task<BaseJikanResponse<ICollection<MangaCharacterResponse>>> GetMangaCharactersAsync(long id, CancellationToken ct = default);

	/// <summary>
	/// Returns collections of news related to manga with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of manga.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Collections of news related to manga with given MAL id.</returns>
	Task<PaginatedJikanResponse<ICollection<NewsResponse>>> GetMangaNewsAsync(long id, CancellationToken ct = default);

	/// <summary>
	/// Returns collections of news related to manga with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of manga.</param>
	/// <param name="page">Index of page folding 100 records of top ranging (e.g. 1 will return first 100 records, 2 will return record from 101 to 200 etc.)</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Collections of news related to manga with given MAL id.</returns>
	Task<PaginatedJikanResponse<ICollection<NewsResponse>>> GetMangaNewsAsync(long id, int page, CancellationToken ct = default);

	/// <summary>
	/// Returns collections of forum topics related to manga with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of manga.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Collections of forum topics related to manga with given MAL id.</returns>
	Task<BaseJikanResponse<ICollection<ForumTopicResponse>>> GetMangaForumTopicsAsync(long id, CancellationToken ct = default);

	/// <summary>
	/// Returns collections of links to pictures related to manga with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of manga.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Collections of links to pictures related to manga with given MAL id.</returns>
	Task<BaseJikanResponse<ICollection<ImagesSetResponse>>> GetMangaPicturesAsync(long id, CancellationToken ct = default);

	/// <summary>
	/// Returns statistics related to manga with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of manga.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Statistics related to manga with given MAL id.</returns>
	Task<BaseJikanResponse<MangaStatisticsResponse>> GetMangaStatisticsAsync(long id, CancellationToken ct = default);

	/// <summary>
	/// Returns additional information related to manga with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of manga.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Collections of forum topics related to manga with given MAL id.</returns>
	Task<BaseJikanResponse<MoreInfoResponse>> GetMangaMoreInfoAsync(long id, CancellationToken ct = default);

	/// <summary>
	/// Returns collection of manga user updates.
	/// </summary>
	/// <param name="id">MAL id of manga.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Collection of manga user updates.</returns>
	Task<PaginatedJikanResponse<ICollection<MangaUserUpdateResponse>>> GetMangaUserUpdatesAsync(long id, CancellationToken ct = default);

	/// <summary>
	/// Returns collection of manga user updates.
	/// </summary>
	/// <param name="id">MAL id of manga.</param>
	/// <param name="page">Index of page folding 75 records of top ranging (e.g. 1 will return first 75 records, 2 will return record from 76 to 150 etc.)</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Collection of manga user updates.</returns>
	Task<PaginatedJikanResponse<ICollection<MangaUserUpdateResponse>>> GetMangaUserUpdatesAsync(long id, int page, CancellationToken ct = default);

	/// <summary>
	/// Returns collection of manga recommendation.
	/// </summary>
	/// <param name="id">MAL id of manga.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Collection of manga recomendation.</returns>
	Task<BaseJikanResponse<ICollection<RecommendationResponse>>> GetMangaRecommendationsAsync(long id, CancellationToken ct = default);

	/// <summary>
	/// Returns collection of manga reviews.
	/// </summary>
	/// <param name="id">MAL id of manga.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Collection of manga reviews.</returns>
	Task<PaginatedJikanResponse<ICollection<ReviewResponse>>> GetMangaReviewsAsync(long id, CancellationToken ct = default);

	/// <summary>
	/// Returns collection of manga related entries.
	/// </summary>
	/// <param name="id">MAL id of manga.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Collection of manga related entries.</returns>
	Task<BaseJikanResponse<ICollection<RelatedEntryResponse>>> GetMangaRelationsAsync(long id, CancellationToken ct = default);

	/// <summary>
	/// Returns collection of external services links related to manga.
	/// </summary>
	/// <param name="id">MAL id of manga.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Collection of external services links related to anime.</returns>
	Task<BaseJikanResponse<ICollection<ExternalLinkResponse>>> GetMangaExternalLinksAsync(long id, CancellationToken ct = default);

	/// <summary>
	/// Returns manga with additional data.
	/// </summary>
	/// <param name="id">MAL id of manga.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Manga with additional data.</returns>
	Task<BaseJikanResponse<MangaResponseFull>> GetMangaFullDataAsync(long id, CancellationToken ct = default);

	#endregion Manga requests

	#region Character requests

	/// <summary>
	/// Returns character with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of character.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Character with given MAL id.</returns>
	Task<BaseJikanResponse<CharacterResponse>> GetCharacterAsync(long id, CancellationToken ct = default);

	/// <summary>
	/// Returns animeography of character with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of character.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Collection of anime where character has appeared.</returns>
	Task<BaseJikanResponse<ICollection<CharacterAnimeographyEntryResponse>>> GetCharacterAnimeAsync(long id, CancellationToken ct = default);

	/// <summary>
	/// Returns mangaography of character with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of character.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Collection of manga where character has appeared.</returns>
	Task<BaseJikanResponse<ICollection<CharacterMangaographyEntryResponse>>> GetCharacterMangaAsync(long id, CancellationToken ct = default);

	/// <summary>
	/// Returns voice actors of character with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of character.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Collection of voice actors voicing character.</returns>
	Task<BaseJikanResponse<ICollection<VoiceActorEntryResponse>>> GetCharacterVoiceActorsAsync(long id, CancellationToken ct = default);

	/// <summary>
	/// Returns collections of links to pictures related to character with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of character.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Collections of links to pictures related to character with given MAL id.</returns>
	Task<BaseJikanResponse<ICollection<ImagesSetResponse>>> GetCharacterPicturesAsync(long id, CancellationToken ct = default);

	/// <summary>
	/// Returns character with additional data.
	/// </summary>
	/// <param name="id">MAL id of character.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Character with additional data.</returns>
	Task<BaseJikanResponse<CharacterResponseFull>> GetCharacterFullDataAsync(long id, CancellationToken ct = default);

	#endregion Character requests

	#region Person requests

	/// <summary>
	/// Returns person with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of person.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Person with given MAL id.</returns>
	Task<BaseJikanResponse<PersonResponse>> GetPersonAsync(long id, CancellationToken ct = default);

	/// <summary>
	/// Returns animeography of person with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of person.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Collection of anime the person collaborated on.</returns>
	Task<BaseJikanResponse<ICollection<PersonAnimeographyEntryResponse>>> GetPersonAnimeAsync(long id, CancellationToken ct = default);

	/// <summary>
	/// Returns mangaography of person with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of person.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Collection of manga the person worked on.</returns>
	Task<BaseJikanResponse<ICollection<PersonMangaographyEntryResponse>>> GetPersonMangaAsync(long id, CancellationToken ct = default);

	/// <summary>
	/// Returns voice acting roles of person with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of person.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Collection of voice acting roles of the person.</returns>
	Task<BaseJikanResponse<ICollection<VoiceActingRoleResponse>>> GetPersonVoiceActingRolesAsync(long id, CancellationToken ct = default);

	/// <summary>
	/// Returns collections of links to pictures related to person with given MAL id.
	/// </summary>
	/// <param name="id">MAL id of person.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Collections of links to pictures related to person with given MAL id.</returns>
	Task<BaseJikanResponse<ICollection<ImagesSetResponse>>> GetPersonPicturesAsync(long id, CancellationToken ct = default);

	/// <summary>
	/// Returns person with additional data.
	/// </summary>
	/// <param name="id">MAL id of person.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Person with additional data.</returns>
	Task<BaseJikanResponse<PersonResponseFull>> GetPersonFullDataAsync(long id, CancellationToken ct = default);

	#endregion Person requests

	#region Season requests

	/// <summary>
	/// Returns season preview.
	/// </summary>
	/// <param name="year">Year of selected season.</param>
	/// <param name="season">Selected season.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Season preview.</returns>
	Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetSeasonAsync(int year, Season season, CancellationToken ct = default);

	/// <summary>
	/// Returns season preview.
	/// </summary>
	/// <param name="year">Year of selected season.</param>
	/// <param name="season">Selected season.</param>
	/// <param name="page">Index of page folding 25 records of top ranging (e.g. 1 will return first 25 records, 2 will return record from 26 to 50 etc.)</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Season preview.</returns>
	Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetSeasonAsync(int year, Season season, int page, CancellationToken ct = default);

	/// <summary>
	/// Returns list of available season to query with <see cref="GetSeasonAsync(int, Season, CancellationToken)"/>
	/// </summary>
	/// <param name="ct">Cancellation token.</param>
	/// <returns></returns>
	Task<PaginatedJikanResponse<ICollection<SeasonArchiveResponse>>> GetSeasonArchiveAsync(CancellationToken ct = default);

	/// <summary>
	/// Return season preview for anime in current airing season.
	/// </summary>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Season preview for anime in current airing season.</returns>
	Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetCurrentSeasonAsync(CancellationToken ct = default);

	/// <summary>
	/// Return season preview for anime in current airing season.
	/// </summary>
	/// <param name="page">Index of page folding 25 records of top ranging (e.g. 1 will return first 25 records, 2 will return record from 26 to 50 etc.)</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Season preview for anime in current airing season.</returns>
	Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetCurrentSeasonAsync(int page, CancellationToken ct = default);

	/// <summary>
	/// Return season preview for anime with undefined airing season (marked as "Later" on MAL).
	/// </summary>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Season preview for anime with undefined airing date.</returns>
	Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetUpcomingSeasonAsync(CancellationToken ct = default);

	/// <summary>
	/// Return season preview for anime with undefined airing season (marked as "Later" on MAL).
	/// </summary>
	/// <param name="page">Index of page folding 25 records of top ranging (e.g. 1 will return first 25 records, 2 will return record from 26 to 50 etc.)</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Season preview for anime with undefined airing date.</returns>
	Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetUpcomingSeasonAsync(int page, CancellationToken ct = default);

	#endregion Season requests

	#region Schedule requests

	/// <summary>
	/// Returns current season schedule.
	/// </summary>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Current season schedule.</returns>
	Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetScheduleAsync(CancellationToken ct = default);

	/// <summary>
	/// Returns current season schedule.
	/// </summary>
	/// <param name="page">Index of page folding 25 records of top ranging (e.g. 1 will return first 25 records, 2 will return record from 26 to 50 etc.)</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Current season schedule.</returns>
	Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetScheduleAsync(int page, CancellationToken ct = default);

	/// <summary>
	/// Returns current season schedule.
	/// </summary>
	/// <param name="scheduledDay">Scheduled day to filter by.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Current season schedule.</returns>
	Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetScheduleAsync(ScheduledDay scheduledDay, CancellationToken ct = default);

	/// <summary>
	/// Returns current season schedule.
	/// </summary>
	/// <param name="parameters">Parameter configuration of this query.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Current season schedule.</returns>
	Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetScheduleAsync(ScheduleQueryParameters parameters, CancellationToken ct = default);

	#endregion Schedule requests

	#region Top requests

	/// <summary>
	/// Returns list of top anime.
	/// </summary>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>List of top anime.</returns>
	Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetTopAnimeAsync(CancellationToken ct = default);

	/// <summary>
	/// Returns list of top anime.
	/// </summary>
	/// <param name="page">Index of page folding 25 records of top ranging (e.g. 1 will return first 25 records, 2 will return record from 26 to 50 etc.)</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>List of top anime.</returns>
	Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetTopAnimeAsync(int page, CancellationToken ct = default);

	/// <summary>
	/// Returns list of top anime.
	/// </summary>
	/// <param name="filter">Filter determining result of request (e.g. TopAnimeFilter.Airing will return top airing anime.)</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>List of top anime.</returns>
	Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetTopAnimeAsync(TopAnimeFilter filter, CancellationToken ct = default);

	/// <summary>
	/// Returns list of top anime.
	/// </summary>
	/// <param name="filter">Filter determining result of request (e.g. TopAnimeFilter.Airing will return top airing anime.)</param>
	/// <param name="page">Index of page folding 25 records of top ranging (e.g. 1 will return first 25 records, 2 will return record from 26 to 50 etc.)</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>List of top anime.</returns>
	Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> GetTopAnimeAsync(TopAnimeFilter filter, int page, CancellationToken ct = default);

	/// <summary>
	/// Returns list of top manga.
	/// </summary>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>List of top manga.</returns>
	Task<PaginatedJikanResponse<ICollection<MangaResponse>>> GetTopMangaAsync(CancellationToken ct = default);

	/// <summary>
	/// Returns list of top manga.
	/// </summary>
	/// <param name="page">Index of page folding 25 records of top ranging (e.g. 1 will return first 25 records, 2 will return record from 26 to 50 etc.)</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>List of top manga.</returns>
	Task<PaginatedJikanResponse<ICollection<MangaResponse>>> GetTopMangaAsync(int page, CancellationToken ct = default);

	/// <summary>
	/// Returns list of most popular people.
	/// </summary>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>List of most popular people.</returns>
	Task<PaginatedJikanResponse<ICollection<PersonResponse>>> GetTopPeopleAsync(CancellationToken ct = default);

	/// <summary>
	/// Returns list of most popular people.
	/// </summary>
	/// <param name="page">Index of page folding 25 records of top ranging (e.g. 1 will return first 25 records, 2 will return record from 26 to 50 etc.)</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>List of most popular people.</returns>
	Task<PaginatedJikanResponse<ICollection<PersonResponse>>> GetTopPeopleAsync(int page, CancellationToken ct = default);

	/// <summary>
	/// Returns list of most popular characters.
	/// </summary>
	/// <returns>List of most popular characters.</returns>
	Task<PaginatedJikanResponse<ICollection<CharacterResponse>>> GetTopCharactersAsync(CancellationToken ct = default);

	/// <summary>
	/// Returns list of most popular characters.
	/// </summary>
	/// <param name="page">Index of page folding 25 records of top ranging (e.g. 1 will return first 25 records, 2 will return record from 26 to 50 etc.)</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>List of most popular characters.</returns>
	Task<PaginatedJikanResponse<ICollection<CharacterResponse>>> GetTopCharactersAsync(int page, CancellationToken ct = default);

	/// <summary>
	/// Returns list of most popular reviews.
	/// </summary>
	/// <returns>List of most popular reviews.</returns>
	Task<PaginatedJikanResponse<ICollection<ReviewResponse>>> GetTopReviewsAsync(CancellationToken ct = default);

	/// <summary>
	/// Returns list of most popular reviews.
	/// </summary>
	/// <param name="page">Index of page folding 25 records of top ranging (e.g. 1 will return first 25 records, 2 will return record from 26 to 50 etc.)</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>List of most popular reviews.</returns>
	Task<PaginatedJikanResponse<ICollection<ReviewResponse>>> GetTopReviewsAsync(int page, CancellationToken ct = default);

	#endregion Top requests

	#region Genre requests

	/// <summary>
	/// Returns list of anime genres.
	/// </summary>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>List of anime genres</returns>
	Task<BaseJikanResponse<ICollection<GenreResponse>>> GetAnimeGenresAsync(CancellationToken ct = default);

	/// <summary>
	/// Returns list of anime genres.
	/// </summary>
	/// <param name="filter">Filter for genre types.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>List of anime genres</returns>
	Task<BaseJikanResponse<ICollection<GenreResponse>>> GetAnimeGenresAsync(GenresFilter filter, CancellationToken ct = default);

	/// <summary>
	/// Returns list of manga genres.
	/// </summary>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>List of manga genres</returns>
	Task<BaseJikanResponse<ICollection<GenreResponse>>> GetMangaGenresAsync(CancellationToken ct = default);

	/// <summary>
	/// Returns list of manga genres.
	/// </summary>
	/// <param name="filter">Filter for genre types.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>List of manga genres</returns>
	Task<BaseJikanResponse<ICollection<GenreResponse>>> GetMangaGenresAsync(GenresFilter filter, CancellationToken ct = default);

	#endregion Genre requests

	#region Producer requests

	/// <summary>
	/// Returns information about producers.
	/// </summary>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Basic Information about producers.</returns>
	Task<PaginatedJikanResponse<ICollection<ProducerResponse>>> GetProducersAsync(CancellationToken ct = default);

	/// <summary>
	/// Returns information about producers.
	/// </summary>
	/// <param name="page">Index of page folding 100 records of top ranging (e.g. 1 will return first 100 records, 2 will return record from 101 to 200 etc.)</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Basic Information about producers.</returns>
	Task<PaginatedJikanResponse<ICollection<ProducerResponse>>> GetProducersAsync(int page, CancellationToken ct = default);

	/// <summary>
	/// Returns information about producer.
	/// </summary>
	/// <param name="id">MAL id of the producer.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Basic Information about producer.</returns>
	Task<BaseJikanResponse<ProducerResponse>> GetProducerAsync(long id, CancellationToken ct = default);

	/// <summary>
	/// Returns external links related to producer
	/// </summary>
	/// <param name="id">MAL id of the producer.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>External links related to producer</returns>
	Task<BaseJikanResponse<ICollection<ExternalLinkResponse>>> GetProducerExternalLinksAsync(long id, CancellationToken ct = default);

	/// <summary>
	/// Returns full information about producer.
	/// </summary>
	/// <param name="id">MAL id of the producer.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Full Information about producer.</returns>
	Task<BaseJikanResponse<ProducerResponseFull>> GetProducerFullDataAsync(long id, CancellationToken ct = default);

	#endregion Producer requests

	#region Magazine requests

	/// <summary>
	/// Returns information about magazines.
	/// </summary>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Basic Information about magazines.</returns>
	Task<PaginatedJikanResponse<ICollection<MagazineResponse>>> GetMagazinesAsync(CancellationToken ct = default);

	/// <summary>
	/// Returns information about magazines.
	/// </summary>
	/// <param name="page">Index of page folding 100 records of top ranging (e.g. 1 will return first 100 records, 2 will return record from 101 to 200 etc.)</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Basic Information about magazines.</returns>
	Task<PaginatedJikanResponse<ICollection<MagazineResponse>>> GetMagazinesAsync(int page, CancellationToken ct = default);

	#endregion Magazine requests

	#region Club requests

	/// <summary>
	/// Return club's profile information.
	/// </summary>
	/// <param name="id">MAL id of the club.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Club's profile information.</returns>
	Task<BaseJikanResponse<ClubResponse>> GetClubAsync(long id, CancellationToken ct = default);

	/// <summary>
	/// Return club's member list.
	/// </summary>
	/// <param name="id">MAL id of the club.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Club's member list.</returns>
	Task<PaginatedJikanResponse<ICollection<ClubMemberResponse>>> GetClubMembersAsync(long id, CancellationToken ct = default);

	/// <summary>
	/// Return club's member list.
	/// </summary>
	/// <param name="id">MAL id of the club.</param>
	/// <param name="page">Index of page folding 36 records of top ranging (e.g. 1 will return first 36 records, 2 will return record from 37 to 72 etc.)</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Club's member list.</returns>
	Task<PaginatedJikanResponse<ICollection<ClubMemberResponse>>> GetClubMembersAsync(long id, int page, CancellationToken ct = default);

	/// <summary>
	/// Return club's staff list.
	/// </summary>
	/// <param name="id">MAL id of the club.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Club's staff list.</returns>
	Task<BaseJikanResponse<ICollection<ClubStaffResponse>>> GetClubStaffAsync(long id, CancellationToken ct = default);

	/// <summary>
	/// Return club's related entities.
	/// </summary>
	/// <param name="id">MAL id of the club.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Club's related entities collections..</returns>
	Task<BaseJikanResponse<ClubRelationsResponse>> GetClubRelationsAsync(long id, CancellationToken ct = default);

	#endregion Club requests

	#region User requests

	/// <summary>
	/// Returns information about user's profile with given username.
	/// </summary>
	/// <param name="username">Username.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Information about user's profile with given username.</returns>
	Task<BaseJikanResponse<UserProfileResponse>> GetUserProfileAsync(string username, CancellationToken ct = default);

	/// <summary>
	/// Returns information about user's anime and manga statistics
	/// </summary>
	/// <param name="username">Username.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Information about user's anime and manga statistics.</returns>
	Task<BaseJikanResponse<UserStatisticsResponse>> GetUserStatisticsAsync(string username, CancellationToken ct = default);

	/// <summary>
	/// Returns information about user's favorite section.
	/// </summary>
	/// <param name="username">Username.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Information about user's favorite section..</returns>
	Task<BaseJikanResponse<UserFavoritesResponse>> GetUserFavoritesAsync(string username, CancellationToken ct = default);

	/// <summary>
	/// Returns information about user's updates on anime/manga progress.
	/// </summary>
	/// <param name="username">Username.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Information about user's updates on anime/manga progress.</returns>
	Task<BaseJikanResponse<UserUpdatesResponse>> GetUserUpdatesAsync(string username, CancellationToken ct = default);

	/// <summary>
	/// Returns information about user's description on the profile.
	/// </summary>
	/// <param name="username">Username.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Information about user's description on the profile.</returns>
	Task<BaseJikanResponse<UserAboutResponse>> GetUserAboutAsync(string username, CancellationToken ct = default);

	/// <summary>
	/// Returns information about user's history with given username.
	/// </summary>
	/// <param name="username">Username.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Information about user's profile with given username.</returns>
	Task<BaseJikanResponse<ICollection<HistoryEntryResponse>>> GetUserHistoryAsync(string username, CancellationToken ct = default);

	/// <summary>
	/// Returns information about user's history with given username.
	/// </summary>
	/// <param name="username">Username.</param>
	/// <param name="userHistory">Option to filter history.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Information about user's profile with given username.</returns>
	Task<BaseJikanResponse<ICollection<HistoryEntryResponse>>> GetUserHistoryAsync(string username, UserHistoryExtension userHistory, CancellationToken ct = default);

	/// <summary>
	/// Returns information about user's friends with given username.
	/// </summary>
	/// <param name="username">Username.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Information about user's friends with given username.</returns>
	Task<PaginatedJikanResponse<ICollection<FriendResponse>>> GetUserFriendsAsync(string username, CancellationToken ct = default);

	/// <summary>
	/// Returns information about user's friends with given username.
	/// </summary>
	/// <param name="username">Username.</param>
	/// <param name="page">Index of the page.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Information about user's friends with given username.</returns>
	Task<PaginatedJikanResponse<ICollection<FriendResponse>>> GetUserFriendsAsync(string username, int page, CancellationToken ct = default);

	/// <summary>
	/// Returns entries on user's anime list.
	/// </summary>
	/// <param name="username">Username.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Entries on user's anime list.</returns>
	Task<BaseJikanResponse<ICollection<AnimeListEntryResponse>>> GetUserAnimeListAsync(string username, CancellationToken ct = default);

	/// <summary>
	/// Returns entries on user's anime list.
	/// </summary>
	/// <param name="username">Username.</param>
	/// <param name="page">Index of page folding 300 records of top ranging (e.g. 1 will return first 300 records, 2 will return record from 301 to 600 etc.)</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Entries on user's anime list.</returns>
	Task<BaseJikanResponse<ICollection<AnimeListEntryResponse>>> GetUserAnimeListAsync(string username, int page, CancellationToken ct = default);

	/// <summary>
	/// Returns entries on user's manga list.
	/// </summary>
	/// <param name="username">Username.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Entries on user's manga list.</returns>
	Task<BaseJikanResponse<ICollection<MangaListEntryResponse>>> GetUserMangaListAsync(string username, CancellationToken ct = default);

	/// <summary>
	/// Returns entries on user's manga list.
	/// </summary>
	/// <param name="username">Username.</param>
	/// <param name="page">Index of page folding 300 records of top ranging (e.g. 1 will return first 300 records, 2 will return record from 301 to 600 etc.)</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Entries on user's manga list.</returns>
	Task<BaseJikanResponse<ICollection<MangaListEntryResponse>>> GetUserMangaListAsync(string username, int page, CancellationToken ct = default);

	/// <summary>
	/// Returns user's reviews.
	/// </summary>
	/// <param name="username">Username.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>User's reviews.</returns>
	Task<PaginatedJikanResponse<ICollection<ReviewResponse>>> GetUserReviewsAsync(string username, CancellationToken ct = default);

	/// <summary>
	/// Returns user's reviews.
	/// </summary>
	/// <param name="username">Username.</param>
	/// <param name="page">Index of page folding 10 records of top ranging (e.g. 1 will return first 10 records, 2 will return record from 11 to 21 etc.)</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>User's reviews.</returns>
	Task<PaginatedJikanResponse<ICollection<ReviewResponse>>> GetUserReviewsAsync(string username, int page, CancellationToken ct = default);

	/// <summary>
	/// Returns user's recommendations.
	/// </summary>
	/// <param name="username">Username.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>User's recommendations.</returns>
	Task<PaginatedJikanResponse<ICollection<UserRecommendationResponse>>> GetUserRecommendationsAsync(string username, CancellationToken ct = default);

	/// <summary>
	/// Returns user's recommendations.
	/// </summary>
	/// <param name="username">Username.</param>
	/// <param name="page">Index of page folding 10 records of top ranging (e.g. 1 will return first 10 records, 2 will return record from 11 to 21 etc.)</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>User's recommendations.</returns>
	Task<PaginatedJikanResponse<ICollection<UserRecommendationResponse>>> GetUserRecommendationsAsync(string username, int page, CancellationToken ct = default);

	/// <summary>
	/// Returns user's clubs.
	/// </summary>
	/// <param name="username">Username.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>User's clubs.</returns>
	Task<PaginatedJikanResponse<ICollection<MalUrlResponse>>> GetUserClubsAsync(string username, CancellationToken ct = default);

	/// <summary>
	/// Returns user's clubs.
	/// </summary>
	/// <param name="username">Username.</param>
	/// <param name="page">Index of page folding 10 records of top ranging (e.g. 1 will return first 10 records, 2 will return record from 11 to 21 etc.)</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>User's clubs.</returns>
	Task<PaginatedJikanResponse<ICollection<MalUrlResponse>>> GetUserClubsAsync(string username, int page, CancellationToken ct = default);

	/// <summary>
	/// Returns collection of external services links related to user.
	/// </summary>
	/// <param name="username">Username.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Collection of external services links related to anime.</returns>
	Task<BaseJikanResponse<ICollection<ExternalLinkResponse>>> GetUserExternalLinksAsync(string username, CancellationToken ct = default);

	/// <summary>
	/// Returns user with additional data.
	/// </summary>
	/// <param name="username">Username.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>User profile with additional data.</returns>
	Task<BaseJikanResponse<UserResponseFull>> GetUserFullDataAsync(string username, CancellationToken ct = default);

	#endregion User requests

	#region GetRandom requests

	/// <summary>
	/// Gets random anime.
	/// </summary>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Random anime</returns>
	Task<BaseJikanResponse<AnimeResponse>> GetRandomAnimeAsync(CancellationToken ct = default);

	/// <summary>
	/// Gets random manga.
	/// </summary>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Random manga</returns>
	Task<BaseJikanResponse<MangaResponse>> GetRandomMangaAsync(CancellationToken ct = default);

	/// <summary>
	/// Gets random character.
	/// </summary>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Random character</returns>
	Task<BaseJikanResponse<CharacterResponse>> GetRandomCharacterAsync(CancellationToken ct = default);

	/// <summary>
	/// Gets random person.
	/// </summary>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Random person</returns>
	Task<BaseJikanResponse<PersonResponse>> GetRandomPersonAsync(CancellationToken ct = default);

	/// <summary>
	/// Gets random user.
	/// </summary>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Random character</returns>
	Task<BaseJikanResponse<UserProfileResponse>> GetRandomUserAsync(CancellationToken ct = default);

	#endregion

	#region Recommendations requests

	/// <summary>
	/// Gets collection of recently added anime recommendations.
	/// </summary>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Collection of recently added recommendations.r</returns>
	Task<PaginatedJikanResponse<ICollection<UserRecommendationResponse>>> GetRecentAnimeRecommendationsAsync(CancellationToken ct = default);

	/// <summary>
	/// Gets collection of recently added anime recommendations.
	/// </summary>
	/// <param name="page">Index of page folding 100 records of top ranging (e.g. 1 will return first 100 records, 2 will return record from 101 to 200 etc.)</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Collection of recently added recommendations.r</returns>
	Task<PaginatedJikanResponse<ICollection<UserRecommendationResponse>>> GetRecentAnimeRecommendationsAsync(int page, CancellationToken ct = default);

	/// <summary>
	/// Gets collection of recently added manga recommendations.
	/// </summary>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Collection of recently added recommendations.r</returns>
	Task<PaginatedJikanResponse<ICollection<UserRecommendationResponse>>> GetRecentMangaRecommendationsAsync(CancellationToken ct = default);

	/// <summary>
	/// Gets collection of recently added manga recommendations.
	/// </summary>
	/// <param name="page">Index of page folding 100 records of top ranging (e.g. 1 will return first 100 records, 2 will return record from 101 to 200 etc.)</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Collection of recently added recommendations.r</returns>
	Task<PaginatedJikanResponse<ICollection<UserRecommendationResponse>>> GetRecentMangaRecommendationsAsync(int page, CancellationToken ct = default);

	#endregion

	#region Reviews requests

	/// <summary>
	/// Gets collection of recently added anime reviews.
	/// </summary>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Collection of recently added anime reviews</returns>
	Task<PaginatedJikanResponse<ICollection<ReviewResponse>>> GetRecentAnimeReviewsAsync(CancellationToken ct = default);

	/// <summary>
	/// Gets collection of recently added reviews.
	/// </summary>
	/// <param name="page">Index of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Collection of recently added reviews</returns>
	Task<PaginatedJikanResponse<ICollection<ReviewResponse>>> GetRecentAnimeReviewsAsync(int page, CancellationToken ct = default);

	/// <summary>
	/// Gets collection of recently added manga reviews.
	/// </summary>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Collection of recently added manga reviews</returns>
	Task<PaginatedJikanResponse<ICollection<ReviewResponse>>> GetRecentMangaReviewsAsync(CancellationToken ct = default);

	/// <summary>
	/// Gets collection of recently added manga reviews.
	/// </summary>
	/// <param name="page">Index of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Collection of recently added manga reviews.</returns>
	Task<PaginatedJikanResponse<ICollection<ReviewResponse>>> GetRecentMangaReviewsAsync(int page, CancellationToken ct = default);

	#endregion

	#region Watch requests

	/// <summary>
	/// Return collection of recently released episodes details.
	/// </summary>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Collection of recently released episodes details..</returns>
	Task<PaginatedJikanResponse<ICollection<WatchEpisodeResponse>>> GetWatchRecentEpisodesAsync(CancellationToken ct = default);

	/// <summary>
	/// Return collection of popular episodes details.
	/// </summary>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Collection of popular episodes details.</returns>
	Task<PaginatedJikanResponse<ICollection<WatchEpisodeResponse>>> GetWatchPopularEpisodesAsync(CancellationToken ct = default);

	/// <summary>
	/// Return collection of recently released promos details.
	/// </summary>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Collection of recently released promos details.</returns>
	Task<PaginatedJikanResponse<ICollection<WatchPromoVideoResponse>>> GetWatchRecentPromosAsync(CancellationToken ct = default);

	/// <summary>
	/// Return collection of popular promos details.
	/// </summary>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>Collection of popular promos details.</returns>
	Task<PaginatedJikanResponse<ICollection<WatchPromoVideoResponse>>> GetWatchPopularPromosAsync(CancellationToken ct = default);

	#endregion Watch requests

	#region Search requests

	/// <summary>
	/// Returns list of results related to search.
	/// </summary>
	/// <param name="query">Search query.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>List of result related to search query.</returns>
	Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> SearchAnimeAsync(string query, CancellationToken ct = default);

	/// <summary>
	/// Returns list of results related to search.
	/// </summary>
	/// <param name="searchConfig">Additional configuration for advanced search.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>List of result related to search query.</returns>
	Task<PaginatedJikanResponse<ICollection<AnimeResponse>>> SearchAnimeAsync(AnimeSearchConfig searchConfig, CancellationToken ct = default);

	/// <summary>
	/// Returns list of results related to search.
	/// </summary>
	/// <param name="query">Search query.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>List of result related to search query.</returns>
	Task<PaginatedJikanResponse<ICollection<MangaResponse>>> SearchMangaAsync(string query, CancellationToken ct = default);

	/// <summary>
	/// Returns list of results related to search.
	/// </summary>
	/// <param name="searchConfig">Additional configuration for advanced search.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>List of result related to search query.</returns>
	Task<PaginatedJikanResponse<ICollection<MangaResponse>>> SearchMangaAsync(MangaSearchConfig searchConfig, CancellationToken ct = default);

	/// <summary>
	/// Returns list of results related to search.
	/// </summary>
	/// <param name="query">Search query.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>List of result related to search query.</returns>
	Task<PaginatedJikanResponse<ICollection<PersonResponse>>> SearchPersonAsync(string query, CancellationToken ct = default);

	/// <summary>
	/// Returns list of results related to search.
	/// </summary>
	/// <param name="searchConfig">Additional configuration for advanced search.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>List of result related to search query.</returns>
	Task<PaginatedJikanResponse<ICollection<PersonResponse>>> SearchPersonAsync(PersonSearchConfig searchConfig, CancellationToken ct = default);

	/// <summary>
	/// Returns list of results related to search.
	/// </summary>
	/// <param name="query">Search query.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>List of result related to search query.</returns>
	Task<PaginatedJikanResponse<ICollection<CharacterResponse>>> SearchCharacterAsync(string query, CancellationToken ct = default);

	/// <summary>
	/// Returns list of results related to search.
	/// </summary>
	/// <param name="searchConfig">Additional configuration for advanced search.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>List of result related to search query.</returns>
	Task<PaginatedJikanResponse<ICollection<CharacterResponse>>> SearchCharacterAsync(CharacterSearchConfig searchConfig, CancellationToken ct = default);

	/// <summary>
	/// Returns list of results related to search.
	/// </summary>
	/// <param name="query">Search query.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>List of result related to search query.</returns>
	Task<PaginatedJikanResponse<ICollection<UserMetadataResponse>>> SearchUserAsync(string query, CancellationToken ct = default);

	/// <summary>
	/// Returns list of results related to search.
	/// </summary>
	/// <param name="searchConfig">Additional configuration for advanced search.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>List of result related to search query.</returns>
	Task<PaginatedJikanResponse<ICollection<UserMetadataResponse>>> SearchUserAsync(UserSearchConfig searchConfig, CancellationToken ct = default);

	/// <summary>
	/// Returns list of results related to search.
	/// </summary>
	/// <param name="query">Search query.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>List of result related to search query.</returns>
	Task<PaginatedJikanResponse<ICollection<ClubResponse>>> SearchClubAsync(string query, CancellationToken ct = default);

	/// <summary>
	/// Returns list of results related to search.
	/// </summary>
	/// <param name="searchConfig">Additional configuration for advanced search.</param>
	/// <param name="ct">Cancellation token.</param>
	/// <returns>List of result related to search query.</returns>
	Task<PaginatedJikanResponse<ICollection<ClubResponse>>> SearchClubAsync(ClubSearchConfig searchConfig, CancellationToken ct = default);

	#endregion Search requests
}