using Aosta.Core.Data.Realm;
using AutoMapper;
using JikanDotNet;

namespace Aosta.Core.Data;

public class EpisodeMapperProfile : Profile
{
    public EpisodeMapperProfile()
    {
        CreateMap<AnimeEpisode, EpisodeObject>()
            .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.MalId))
            .ForMember(dest => dest.IsFiller, opt => opt.MapFrom(src => src.Filler))
            .ForMember(dest => dest.IsRecap, opt => opt.MapFrom(src => src.Recap));
    }
}