using System.Diagnostics;
using Aosta.Core.Data.Models;
using Aosta.Core.Extensions;
using Aosta.Core.Jikan;
using Aosta.Core.Jikan.Models.Response;
using Realms;
using Realms.Exceptions;
using Serilog;

namespace Aosta.Core;

/// <summary>
/// Facade class that exposes core functionality to consumer applications
/// </summary>
public class AostaDotNet
{
    internal AostaDotNet() : this(new AostaConfiguration())
    {
    }

    public AostaDotNet(AostaConfiguration configuration)
    {
        Configuration = configuration;
        Logger = Configuration.LoggerConfig.CreateLogger();
        Jikan = new JikanClient(Logger);
    }

    public AostaConfiguration Configuration { get; set; }

    /// <summary> Jikan.net client </summary>
    public IJikan Jikan { get; private set; }

    /// <summary> Serilog logger instance </summary>
    public ILogger Logger { get; private set; }

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

            var anime = realm.Find<AnimeObject>(animeId);

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
    /// Retrieve new data about a anime from MyAnimeList and save it to Realm
    /// </summary>
    /// <param name="malId">The MyAnimeList ID of the anime </param>
    /// <param name="overrideLocal">If set to <c>true</c>, <see cref="AnimeObject">local user data</see> will be overridden with new data from MyAnimeList</param>
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
        var response = await Jikan.GetAnimeAsync(malId, ct);

        Logger.Information("Got anime: {0} ({1})", response.Data.Titles.First().Title, response.Data.MalId);

        // Update the entities with retrieved data
        await UpdateJikanContentAsync(response.Data, overrideLocal, ct);
    }

    /// <inheritdoc cref="UpdateJikanContentAsync(AnimeResponse,bool,System.Threading.CancellationToken)"/>
    public Task UpdateJikanContentAsync(AnimeResponse animeResponse, CancellationToken ct = default)
    {
        return UpdateJikanContentAsync(animeResponse, false, ct);
    }

    /// <summary>
    /// Save new Jikan API anime data to Realm
    /// </summary>
    /// <param name="animeResponse">Response data from Jikan API</param>
    /// <param name="overrideLocal">If set to <c>true</c>, <see cref="AnimeObject">local user data</see> will be overridden with data from <paramref name="animeResponse"/></param>
    /// <param name="ct">Cancellation token</param>
    public async Task UpdateJikanContentAsync(AnimeResponse animeResponse, bool overrideLocal, CancellationToken ct = default)
    {
        //Throw if task was cancelled already
        ct.ThrowIfCancellationRequested();

        var jikanObject = animeResponse.ToRealmObject();

        //Get a disposable realm instance
        using var realm = GetInstance();

        //Update the data to realm
        await realm.WriteAsync(() =>
        {
            // Realm requires explicit consent for row upsertion.
            // Every time the anime data is retrieved from Jikan, its row must be updated.
            realm.Add(jikanObject, true);

            if (!overrideLocal) return;

            // If local data override is requested, also update the local data object
            foreach (var entity in realm.All<AnimeObject>().Where(o => o.JikanResponseData == jikanObject))
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
        var response = await Jikan.GetAnimeAsync(malId, ct);

        Logger.Information("Got anime: {Title} ({Id})", response.Data.Titles?.First().Title, response.Data.MalId);

        //Write the data to local realm and return the primary key
        return await CreateJikanContentAsync(response.Data, update, ct);
    }

    public Task<Guid> CreateJikanContentAsync(AnimeResponse animeResponse, CancellationToken ct = default)
    {
        return CreateJikanContentAsync(animeResponse, true, ct);
    }

    public async Task<Guid> CreateJikanContentAsync(AnimeResponse animeResponse, bool update, CancellationToken ct = default)
    {
        //Throw if task was cancelled already
        ct.ThrowIfCancellationRequested();

        Guid id = Guid.Empty;
        var jikanObject = animeResponse.ToRealmObject();

        //Get a disposable realm instance
        using var realm = GetInstance();

        //Add the data to realm
        await realm.WriteAsync(() =>
        {
            // Realm requires explicit consent for row upsertion.
            // Every time the anime data is retrieved from Jikan, its row must be updated.
            realm.Add(jikanObject, update);
            id = realm.Add(new AnimeObject(jikanObject)).Id;
        }, ct);

        //Return the primary key
        return id;
    }

    public Task<Guid> CreateLocalContentAsync(CancellationToken ct = default)
    {
        return CreateLocalContentAsync(new AnimeObject(), false, ct);
    }

    public Task<Guid> CreateLocalContentAsync(AnimeObject anime, CancellationToken ct = default)
    {
        return CreateLocalContentAsync(anime, false, ct);
    }

    public async Task<Guid> CreateLocalContentAsync(AnimeObject anime, bool update, CancellationToken ct = default)
    {
        //Throw if task was cancelled already
        ct.ThrowIfCancellationRequested();

        var id = Guid.Empty;

        //Get a disposable realm instance
        using var realm = GetInstance();

        //Add the data to realm
        await realm.WriteAsync(() =>
        {
            id = realm.Add(anime, update).Id;
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
    /// <param name="malId">The MyAnimeList ID of the anime</param>
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
        Realm.DeleteRealm(Configuration.RealmConfig);
    }

    public Realm GetInstance()
    {
        return Realm.GetInstance(Configuration.RealmConfig);
    }
}