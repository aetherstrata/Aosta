using AutoMapper;
using JikanDotNet;

namespace Aosta.Core.Data;

public class EpisodeMapperProfile : Profile
{
    public EpisodeMapperProfile()
    {
        CreateMap<AnimeEpisode, EpisodeDTO>()
            .ForMember(dest => dest.IsFiller, opt => opt.MapFrom(src => src.Filler))
            .ForMember(dest => dest.IsRecap, opt => opt.MapFrom(src => src.Recap));
    }
}