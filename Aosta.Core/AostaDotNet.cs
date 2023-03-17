using System.Diagnostics;
using Aosta.Core.Data.Models;
using Aosta.Core.Extensions;
using JikanDotNet;
using Realms;

namespace Aosta.Core;

/// <summary>
/// Facade class that exposes core functionality to consumer applications
/// </summary>
public class AostaDotNet
{
    public AostaDotNet() : this(new RealmConfiguration("aosta.realm")) { }

    public AostaDotNet(RealmConfigurationBase config)
    {
        RealmConfig = config;
    }

    /// <summary> Jikan.net client </summary>
    internal IJikan Jikan { get; } = new Jikan();

    /// <summary> Realm configuration </summary>
    internal RealmConfigurationBase RealmConfig { get; }

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

    public Task UpdateJikanContentAsync(long malId, CancellationToken ct = default)
    {
        return UpdateJikanContentAsync(malId, false, ct);
    }

    public async Task UpdateJikanContentAsync(long malId, bool overrideLocal, CancellationToken ct = default)
    {
        // Throw if task was cancelled already
        ct.ThrowIfCancellationRequested();

        // Throw if the ID is not present in Realm
        if (!JikanDataExists(malId))
            throw new ArgumentException("The specified MyAnimeList id is not present in Realm. " +
                                        $"Can't update something that does not exist! MalID: {malId}", nameof(malId));

        // Get response from Jikan REST API
        // Always await responses, never use .Result
        var response = await Jikan.GetAnimeAsync(malId, ct);

        Debug.WriteLine($"Got anime: {response.Data.Titles.First()} ({response.Data.MalId})");

        // Update the entities with retrieved data
        await UpdateJikanContentAsync(response.Data, overrideLocal, ct);
    }
    
    
    public Task UpdateJikanContentAsync(Anime jikanAnime, CancellationToken ct = default)
    {
        return UpdateJikanContentAsync(jikanAnime, false, ct);
    }

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

            // If local data override is requested, also update the local data object
            if (overrideLocal)
            {
                // For some reason
                foreach (var entity in realm.All<ContentObject>().Where(o => o.JikanResponseData == jikanObject))
                {
                    entity.UpdateFromJikan(jikanObject);
                }
            }
        }, ct);
    }

    public Task<Guid> WriteJikanContentAsync(long malId, CancellationToken ct = default)
    {
        return WriteJikanContentAsync(malId, true, ct);
    }

    public async Task<Guid> WriteJikanContentAsync(long malId, bool update, CancellationToken ct = default)
    {
        //Throw if task was cancelled already
        ct.ThrowIfCancellationRequested();

        //Get response from Jikan REST API
        //Always await responses, never use .Result
        var response = await Jikan.GetAnimeAsync(malId, ct);

        Debug.WriteLine($"Got anime: {response.Data.Titles.First()} ({response.Data.MalId})");

        //Write the data to local realm and return the primary key
        return await WriteJikanContentAsync(response.Data, update, ct);
    }

    public Task<Guid> WriteJikanContentAsync(Anime jikanAnime, CancellationToken ct = default)
    {
        return WriteJikanContentAsync(jikanAnime, true, ct);
    }

    public async Task<Guid> WriteJikanContentAsync(Anime jikanAnime, bool update, CancellationToken ct = default)
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

    public Task<Guid> WriteContentAsync(ContentObject content, CancellationToken ct = default)
    {
        return WriteContentAsync(content, false, ct);
    }

    public async Task<Guid> WriteContentAsync(ContentObject content, bool update, CancellationToken ct = default)
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
        //TODO: da finire

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

    public bool JikanDataExists(long malId)
    {
        using var realm = GetInstance();
        return realm.Find<JikanContentObject>(malId) != null;
    }

    public Realm GetInstance()
    {
        return Realm.GetInstance(RealmConfig);
    }
}