using System.Diagnostics;
using Aosta.Core.API;
using Aosta.Core.Data.Models;
using Aosta.Core.Extensions;
using JikanDotNet;
using Realms;
using Realms.Exceptions;

namespace Aosta.Core;

/// <summary>
/// Facade class that exposes core functionality to consumer applications
/// </summary>
public class AostaDotNet
{
    /// <summary> Jikan.net client </summary>
    internal IJikan Jikan { get; } = new Jikan();

    /// <summary> Semaphore that controls how many tasks per unit of time can be run </summary>
    internal ITaskLimiter Limiter { get; }

    /// <summary> Realm configuration </summary>
    internal RealmConfigurationBase RealmConfig { get; }

    public AostaDotNet() : this(new RealmConfiguration("aosta.realm"), TaskLimiterConfiguration.DefaultConfiguration) { }

    public AostaDotNet(RealmConfigurationBase realmConfig) : this(realmConfig, TaskLimiterConfiguration.DefaultConfiguration) { }

    public AostaDotNet(RealmConfigurationBase realmConfig, IEnumerable<TaskLimiterConfiguration> limiterConfigurations)
    {
        RealmConfig = realmConfig;

        Limiter = new CompositeTaskLimiter(limiterConfigurations);

        foreach (var taskLimiterConfiguration in limiterConfigurations)
        {
            Console.WriteLine(taskLimiterConfiguration);
        }
    }

    #region Jikan tasks

    public Task<Guid> WriteAnimeAndEpisodesAsync(int malId)
    {
        throw new NotImplementedException();
/*
        var animeId = await WriteContentAsync(malId);
        await Task.Delay(500);
        await WriteEpisodesAsync(malId, animeId);
        return animeId;
*/
    }

    private Task WriteEpisodesAsync(int malId, Guid animeId)
    {
        throw new NotImplementedException();

        /*
        ICollection<AnimeEpisode> retrievedEpisodes = Jikan.GetAnimeEpisodesAsync(malId).Result.Data;

        using var realm = GetInstance();
        await realm.WriteAsync(() =>
        {
            IList<EpisodeObject> mappedEps =
                Mapper.Map<ICollection<AnimeEpisode>, IList<EpisodeObject>>(retrievedEpisodes);

            var anime = realm.Find<ContentObject>(animeId);

            foreach (var ep in mappedEps)
            {
                ep.Content = anime;
                realm.Add(ep);
            }
        });
        */
    }

    /// <inheritdoc cref="UpdateJikanContentAsync(long,bool,System.Threading.CancellationToken)" />
    public Task UpdateJikanContentAsync(long malId, CancellationToken ct = default)
    {
        return UpdateJikanContentAsync(malId, false, ct);
    }

    /// <summary>
    /// Retrieve new data about a content from MyAnimeList and save it to Realm
    /// </summary>
    /// <param name="malId">The MyAnimeList ID of the content </param>
    /// <param name="overrideLocal">If set to <c>true</c>, <see cref="ContentObject">local user data</see> will be overridden with new data from MyAnimeList</param>
    /// <param name="ct">Cancellation token</param>
    /// <exception cref="ArgumentException">The specified <paramref name="malId"/> is not present in the Realm.</exception>
    public async Task UpdateJikanContentAsync(long malId, bool overrideLocal, CancellationToken ct = default)
    {
        // Throw if task was cancelled already
        ct.ThrowIfCancellationRequested();

        // Throw if the ID is not present in Realm
        if (!JikanDataExists(malId))
        {
            throw new ArgumentException("The specified MyAnimeList ID is not present in Realm. " +
                                        $"Can't update something that does not exist! MalID: {malId}", nameof(malId));
        }

        // Get response from Jikan REST API
        // Always await responses, never use .Result
        var response = await Limiter.LimitAsync(() => Jikan.GetAnimeAsync(malId, ct));

        Debug.WriteLine($"Got anime: {response.Data.Titles.First()} ({response.Data.MalId})");

        // Update the entities with retrieved data
        await UpdateJikanContentAsync(response.Data, overrideLocal, ct);
    }

    /// <inheritdoc cref="UpdateJikanContentAsync(JikanDotNet.Anime,bool,System.Threading.CancellationToken)"/>
    public Task UpdateJikanContentAsync(Anime jikanAnime, CancellationToken ct = default)
    {
        return UpdateJikanContentAsync(jikanAnime, false, ct);
    }

    /// <summary>
    /// Save new Jikan API content data to Realm
    /// </summary>
    /// <param name="jikanAnime">Response data from Jikan API</param>
    /// <param name="overrideLocal">If set to <c>true</c>, <see cref="ContentObject">local user data</see> will be overridden with data from <paramref name="jikanAnime"/></param>
    /// <param name="ct">Cancellation token</param>
    public async Task UpdateJikanContentAsync(Anime jikanAnime, bool overrideLocal, CancellationToken ct = default)
    {
        //Throw if task was cancelled already
        ct.ThrowIfCancellationRequested();

        Guid id = Guid.Empty;
        var jikanObject = jikanAnime.ToRealmObject();

        //Get a disposable realm instance
        using var realm = GetInstance();

        //Update the data to realm
        await realm.WriteAsync(() =>
        {
            // Realm requires explicit consent for row upsertion.
            // Every time the content data is retrieved from Jikan, its row must be updated.
            realm.Add(jikanObject, true);

            if (!overrideLocal) return;

            // If local data override is requested, also update the local data object
            foreach (var entity in realm.All<ContentObject>().Where(o => o.JikanResponseData == jikanObject))
            {
                entity.UpdateFromJikan(jikanObject);
            }
        }, ct);
    }

    public Task<Guid> CreateJikanContentAsync(long malId, CancellationToken ct = default)
    {
        return CreateJikanContentAsync(malId, true, ct);
    }

    public async Task<Guid> CreateJikanContentAsync(long malId, bool update, CancellationToken ct = default)
    {
        //Throw if task was cancelled already
        ct.ThrowIfCancellationRequested();

        //Get response from Jikan REST API
        //Always await responses, never use .Result
        var response = await Limiter.LimitAsync(() => Jikan.GetAnimeAsync(malId, ct));

        Debug.WriteLine($"Got anime: {response.Data.Titles.First().Title} ({response.Data.MalId})");

        //Write the data to local realm and return the primary key
        return await CreateJikanContentAsync(response.Data, update, ct);
    }

    public Task<Guid> CreateJikanContentAsync(Anime jikanAnime, CancellationToken ct = default)
    {
        return CreateJikanContentAsync(jikanAnime, true, ct);
    }

    public async Task<Guid> CreateJikanContentAsync(Anime jikanAnime, bool update, CancellationToken ct = default)
    {
        //Throw if task was cancelled already
        ct.ThrowIfCancellationRequested();

        Guid id = Guid.Empty;
        var jikanObject = jikanAnime.ToRealmObject();

        //Get a disposable realm instance
        using var realm = GetInstance();

        //Add the data to realm
        await realm.WriteAsync(() =>
        {
            // Realm requires explicit consent for row upsertion.
            // Every time the content data is retrieved from Jikan, its row must be updated.
            realm.Add(jikanObject, update);
            id = realm.Add(new ContentObject(jikanObject)).Id;
        }, ct);

        //Return the primary key
        return id;
    }

    public Task<Guid> CreateLocalContentAsync(CancellationToken ct = default)
    {
        return CreateLocalContentAsync(new ContentObject(), false, ct);
    }

    public Task<Guid> CreateLocalContentAsync(ContentObject content, CancellationToken ct = default)
    {
        return CreateLocalContentAsync(content, false, ct);
    }

    public async Task<Guid> CreateLocalContentAsync(ContentObject content, bool update, CancellationToken ct = default)
    {
        //Throw if task was cancelled already
        ct.ThrowIfCancellationRequested();

        var id = Guid.Empty;

        //Get a disposable realm instance
        using var realm = GetInstance();

        //Add the data to realm
        await realm.WriteAsync(() =>
        {
            id = realm.Add(content, update).Id;
        }, ct);

        //Return the primary key
        return id;
    }

    public Task WriteSingleEpisodeAsync(int animeId, int episodeId)
    {
        throw new NotImplementedException();

        /*
        AnimeEpisode retrievedEpisode = Jikan.GetAnimeEpisodeAsync(animeId, episodeId).Result.Data;

        using var realm = GetInstance();
        await realm.WriteAsync(() =>
        {
            var mappedEpisode = Mapper.Map<EpisodeObject>(retrievedEpisode);
            realm.Add(mappedEpisode);
        });

        */
    }

    #endregion

    /// <summary>
    /// Check if Realm contains an object with a specific MAL ID
    /// </summary>
    /// <param name="malId">The MyAnimeList ID of the content</param>
    /// <returns><c>true</c> if an object with that key is present, <c>false</c> otherwise</returns>
    public bool JikanDataExists(long malId)
    {
        using var realm = GetInstance();
        return realm.Find<JikanContentObject>(malId) != null;
    }

    public bool TryDeleteRealm()
    {
        try
        {
            DeleteRealm();
        }
        catch (RealmInUseException ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }

        return true; 
    }

    public void DeleteRealm()
    {
        Realm.DeleteRealm(RealmConfig);
    }

    public Realm GetInstance()
    {
        return Realm.GetInstance(RealmConfig);
    }
}