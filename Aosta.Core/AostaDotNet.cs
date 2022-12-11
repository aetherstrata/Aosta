using Aosta.Core.Data;
using Aosta.Core.Realm;
using AutoMapper;
using JikanDotNet;

namespace Aosta.Core;

public class AostaDotNet
{
    private static MapperConfiguration _mapperConfiguration = new(cfg =>
    {
        cfg.AddProfile<AnimeMapperProfile>();
        cfg.AddProfile<EpisodeMapperProfile>();
    });

    internal static IJikan Jikan { get; } = new Jikan();

    internal static IMapper Mapper { get; } = new Mapper(_mapperConfiguration);

    public required DatabaseConfiguration DatabaseConfiguration { get; init; }

    public async Task<Guid> WriteOnlineAnimeAsync(int malId)
    {
        Guid id = Guid.Empty;
        int epNumber = 1;

        Anime retrievedAnime = Jikan.GetAnimeAsync(malId).Result.Data;
        await Task.Delay(500);
        ICollection<AnimeEpisode> retrievedEpisodes = Jikan.GetAnimeEpisodesAsync(malId).Result.Data;

        using (var realm = RealmAccess.Singleton.GetInstance(DatabaseConfiguration))
        {
            await realm.WriteAsync(() =>
            {
                ContentDTO mappedAnime = Mapper.Map<ContentDTO>(retrievedAnime);
                IList<EpisodeDTO> mappedEps =
                    Mapper.Map<ICollection<AnimeEpisode>, IList<EpisodeDTO>>(retrievedEpisodes);

                realm.Add(mappedAnime);
                foreach (var ep in mappedEps)
                {
                    ep.Content = mappedAnime;
                    ep.Number = epNumber++;
                    realm.Add(ep);
                }

                id = mappedAnime.Id;
            });
        }

        return id;
    }

    public Realms.Realm GetInstance()
    {
        return RealmAccess.Singleton.GetInstance(DatabaseConfiguration);
    }
}