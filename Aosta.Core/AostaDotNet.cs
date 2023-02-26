using Aosta.Core.Extensions;
using AutoMapper;
using JikanDotNet;
using Realms;
using Aosta.Core.Data.Models;
using ContentObject = Aosta.Core.Data.Models.ContentObject;

namespace Aosta.Core;

public class AostaDotNet
{
    internal static IJikan Jikan { get; } = new Jikan();

    internal RealmConfigurationBase RealmConfig { get; }

    public AostaDotNet() : this(new RealmConfiguration()) { }

    public AostaDotNet(RealmConfigurationBase config)
    {
        RealmConfig = config;
    }

    public async Task<Guid> WriteAnimeAndEpisodesAsync(int malId)
    {
        throw new NotImplementedException();

        Guid animeId = await WriteAnimeAsync(malId);
        await Task.Delay(500);
        await WriteEpisodesAsync(malId, animeId);
        return animeId;
    }

    private async Task WriteEpisodesAsync(int malId, Guid animeId)
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

    public async Task UpdateMyAnimeListData(long malId)
    {
        using var realm = GetInstance();
        await realm.WriteAsync(() =>
        {
            realm.Add(Jikan.GetAnimeAsync(malId).Result.Data.ToRealmObject());
        });
    }

    public async Task<Guid> WriteAnimeAsync(int malId)
    {
        return await WriteAnimeAsync(Jikan.GetAnimeAsync(malId).Result.Data);
    }

    public async Task<Guid> WriteAnimeAsync(Anime jikanAnime)
    {
        throw new NotImplementedException();

        /*
        Guid id = Guid.Empty;

        using var realm = GetInstance();
        await realm.WriteAsync(() =>
        {
            var anime = jikanAnime.ToAnimeObject();
            realm.Add(anime);
            id = anime.Id;
        });

        return id;
        */
    }

    public async Task<Guid> WriteContentAsync(ContentObject content)
    {
        Guid id = Guid.Empty;

        using var realm = GetInstance();
        await realm.WriteAsync(() =>
        {
            id = realm.Add(content).Id;
        });

        return id;
    }

    public async Task WriteSingleEpisodeAsync(int animeId, int episodeId)
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

    public Realm GetInstance()
    {
        return Realm.GetInstance(RealmConfig);
    }

    public static void Hello()
    {
        Console.WriteLine("Pippa");
    }
}