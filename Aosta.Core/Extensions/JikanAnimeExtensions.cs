using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aosta.Core.Data;
using Aosta.Core.Data.Realm;
using JikanDotNet;
using AiringStatus = Aosta.Core.Data.AiringStatus;

namespace Aosta.Core.Extensions;
internal static class JikanAnimeExtensions
{
    internal static AnimeObject ToAnimeObject(this Anime anime)
    {
        return new AnimeObject()
        {
            Title = anime.Titles.First(e => e.Type.Equals("Default")).Title,
            Type = Enum.Parse<ContentType>(anime.Type.Trim()),
            AiringStatus = Enum.Parse<AiringStatus>(anime.Status.Trim()),
            Synopsis = anime.Synopsis,
            EpisodeCount = anime.Episodes,
            Source = anime.Source,
            ImageJPG = new()
            {
                ImageUrl = anime.Images.JPG.ImageUrl,
                SmallImageUrl = anime.Images.JPG.SmallImageUrl,
                MediumImageUrl = anime.Images.JPG.MediumImageUrl,
                LargeImageUrl = anime.Images.JPG.LargeImageUrl,
                MaximumImageUrl = anime.Images.JPG.MaximumImageUrl
            },
            ImageWebP = new()
            {
                ImageUrl = anime.Images.WebP.ImageUrl,
                SmallImageUrl = anime.Images.WebP.SmallImageUrl,
                MediumImageUrl = anime.Images.WebP.MediumImageUrl,
                LargeImageUrl = anime.Images.WebP.LargeImageUrl,
                MaximumImageUrl = anime.Images.WebP.MaximumImageUrl
            }
        };
    }
}
