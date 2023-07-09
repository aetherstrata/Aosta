using Aosta.Core.Database.Models;
using Aosta.Core.Database.Ordering;

namespace Aosta.Core.Extensions
{
    public static class AnimeExtensions
    {
        public static IOrderedQueryable<Anime> OrderBy(this IQueryable<Anime> query, AnimeOrdering ordering){
            return query.OrderBy(i => i.Title);
        }
    }
}