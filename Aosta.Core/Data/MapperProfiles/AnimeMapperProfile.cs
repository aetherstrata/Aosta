using Aosta.Core.Data.Realm;
using AutoMapper;
using JikanDotNet;

namespace Aosta.Core.Data.MapperProfiles;

public class AnimeMapperProfile : Profile
{
    public AnimeMapperProfile()
    {
        //TODO: think about global scores
        //for now I'll ignore it and store only the personal score
        CreateMap<Anime, AnimeObject>()
            .ForMember(dest => dest.Episodes, opt => opt.Ignore())
            .ForMember(dest => dest.EpisodeCount, opt => opt.MapFrom(src => src.Episodes))
            .ForMember(dest => dest.Score, opt => opt.Ignore());
    }
}