﻿using AutoMapper;
using JikanDotNet;

namespace Aosta.Core.Data;

public class AnimeMapperProfile : Profile
{
    public AnimeMapperProfile()
    {
        //TODO: think about global scores
        //for now I'll ignore it and store only the personal score
        CreateMap<Anime, ContentDTO>()
            .ForMember(dest => dest.Episodes, opt => opt.Ignore())
            .ForMember(dest => dest.EpisodeCount, opt => opt.MapFrom(src => src.Episodes))
            .ForMember(dest => dest.Score, opt => opt.Ignore());
    }
}