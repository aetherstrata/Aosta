using Aosta.Core.Data;
using Aosta.Core.Data.Realm;
using Aosta.Core.Extensions;
using AutoMapper;
using JikanDotNet;
using Realms;

namespace Aosta.Core;

public class AostaDotNet
{
    private static readonly MapperConfiguration MapperConfiguration = new(cfg =>
    {
        cfg.AddProfile<AnimeMapperProfile>();
        cfg.AddProfile<EpisodeMapperProfile>();
    });

    internal static IJikan Jikan { get; } = new Jikan();

    internal static IMapper Mapper { get; } = new Mapper(MapperConfiguration);

    internal RealmConfiguration RealmConfig { get; }

    public AostaDotNet(RealmConfiguration config)
    {
        RealmConfig = config;
    }

    public async Task<Guid> WriteAnimeAndEpisodesAsync(int malId)
    {
        Guid animeId = await WriteAnimeAsync(malId);
        await Task.Delay(500);
        await WriteEpisodesAsync(malId, animeId);
        return animeId;
    }

    private async Task WriteEpisodesAsync(int malId, Guid animeId)
    {
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
    }

    public async Task<Guid> WriteAnimeAsync(int malId)
    {
        return await WriteAnimeAsync(Jikan.GetAnimeAsync(malId).Result.Data);
    }

    public async Task<Guid> WriteAnimeAsync(Anime jikanAnime)
    {
        Guid id = Guid.Empty;

        using var realm = GetInstance();
        await realm.WriteAsync(() =>
        {
            var anime = jikanAnime.ToAnimeObject();
            realm.Add(anime);
            id = anime.Id;
        });

        return id;
    }

    public async Task<Guid> WriteSingleEpisodeAsync(int animeId, int episodeId)
    {
        //TODO: da finire

        Guid id = Guid.Empty;
        AnimeEpisode retrievedEpisode = Jikan.GetAnimeEpisodeAsync(animeId, episodeId).Result.Data;

        using var realm = GetInstance();
        await realm.WriteAsync(() =>
        {
            var mappedEpisode = Mapper.Map<EpisodeObject>(retrievedEpisode);
            realm.Add(mappedEpisode);
            id = mappedEpisode.Id;
        });
        
        return id;
    }

    public Realms.Realm GetInstance()
    {
        return Realm.GetInstance(RealmConfig);
    }
}