using Aosta.Common;
using Aosta.Jikan.Query.Enums;

namespace Aosta.Jikan.Query.Parameters;

public class TopAnimeQueryParameters : JikanQueryParameterSet, IFactory<TopAnimeQueryParameters>
{
    private TopAnimeQueryParameters(){}

    public static TopAnimeQueryParameters Create()
    {
        return new TopAnimeQueryParameters();
    }

    public TopAnimeQueryParameters Page(int page)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        Add(QueryParameter.PAGE, page);
        return this;
    }

    public TopAnimeQueryParameters Limit(int limit)
    {
        Guard.IsGreaterThanZero(limit, nameof(limit));
        Guard.IsLessOrEqualThan(limit, JikanParameterConsts.MAXIMUM_PAGE_SIZE, nameof(limit));
        Add(QueryParameter.LIMIT, limit);
        return this;
    }

    public TopAnimeQueryParameters Type(AnimeTypeFilter filter)
    {
        Guard.IsValidEnum(filter, nameof(filter));
        Add(QueryParameter.TYPE, filter);
        return this;
    }

    public TopAnimeQueryParameters Filter(TopAnimeFilter filter)
    {
        Guard.IsValidEnum(filter, nameof(filter));
        Add(QueryParameter.FILTER, filter);
        return this;
    }

    public TopAnimeQueryParameters Rating(AnimeAgeRatingFilter rating)
    {
        Guard.IsValidEnum(rating, nameof(rating));
        Add(QueryParameter.RATING, rating);
        return this;
    }

    public TopAnimeQueryParameters SafeForWork(bool sfw)
    {
        Add(QueryParameter.SAFE_FOR_WORK, sfw);
        return this;
    }
}
