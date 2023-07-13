using Aosta.Common.Extensions;
using Aosta.Jikan.Enums;
using Aosta.Jikan.Query.Enums;

namespace Aosta.Jikan.Query;

public class TopAnimeQueryParameters : JikanQueryParameterSet
{
    public TopAnimeQueryParameters SetPage(int page)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        base.Add(QueryParameter.Page, page);
        return this;
    }

    public TopAnimeQueryParameters SetLimit(int limit)
    {
        Guard.IsGreaterThanZero(limit, nameof(limit));
        Guard.IsLessOrEqualThan(limit, JikanParameterConsts.MaximumPageSize, nameof(limit));
        base.Add(QueryParameter.Limit, limit);
        return this;
    }

    public TopAnimeQueryParameters SetType(AnimeTypeFilter filter)
    {
        Guard.IsValidEnum(filter, nameof(filter));
        base.Add(QueryParameter.Type, filter.StringValue());
        return this;
    }

    public TopAnimeQueryParameters SetFilter(TopAnimeFilter filter)
    {
        Guard.IsValidEnum(filter, nameof(filter));
        base.Add(QueryParameter.Filter, filter.StringValue());
        return this;
    }

    public TopAnimeQueryParameters SetRating(AnimeAgeRatingFilter rating)
    {
        Guard.IsValidEnum(rating, nameof(rating));
        base.Add(QueryParameter.Rating, rating.StringValue());
        return this;
    }

    public TopAnimeQueryParameters SetSafeForWork(bool sfw)
    {
        base.Add(QueryParameter.SafeForWork, sfw);
        return this;
    }
}