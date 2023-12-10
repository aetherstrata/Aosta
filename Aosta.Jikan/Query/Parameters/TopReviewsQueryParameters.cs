using Aosta.Common.Extensions;
using Aosta.Jikan.Query.Enums;

namespace Aosta.Jikan.Query.Parameters;

public class TopReviewsQueryParameters : JikanQueryParameterSet
{
    public TopReviewsQueryParameters SetPage(int page)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        Add(QueryParameter.PAGE, page);
        return this;
    }

    public TopReviewsQueryParameters SetType(TopReviewsTypeFilter type)
    {
        Guard.IsValidEnum(type, nameof(type));
        Add(QueryParameter.TYPE, type);
        return this;
    }

    // These parameters act like bools but have to be set like strings (eg. ?name=true)
    public TopReviewsQueryParameters SetPreliminary(bool preliminary)
    {
        Add(QueryParameter.PRELIMINARY, preliminary.ToStringLower());
        return this;
    }

    public TopReviewsQueryParameters SetSpoilers(bool spoiler)
    {
        Add(QueryParameter.SPOILERS, spoiler.ToStringLower());
        return this;
    }
}
