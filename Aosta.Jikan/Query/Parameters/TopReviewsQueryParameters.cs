using Aosta.Common;
using Aosta.Common.Extensions;
using Aosta.Jikan.Query.Enums;

namespace Aosta.Jikan.Query.Parameters;

public class TopReviewsQueryParameters : JikanQueryParameterSet, IFactory<TopReviewsQueryParameters>
{
    private TopReviewsQueryParameters(){}

    public static TopReviewsQueryParameters Create()
    {
        return new TopReviewsQueryParameters();
    }

    public TopReviewsQueryParameters Page(int page)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        Add(QueryParameter.PAGE, page);
        return this;
    }

    public TopReviewsQueryParameters Type(TopReviewsTypeFilter type)
    {
        Guard.IsValidEnum(type, nameof(type));
        Add(QueryParameter.TYPE, type);
        return this;
    }

    // These parameters act like booleans but have to be set like strings (eg. ?name=true)
    public TopReviewsQueryParameters Preliminary(bool preliminary)
    {
        Add(QueryParameter.PRELIMINARY, preliminary.ToStringLower());
        return this;
    }

    public TopReviewsQueryParameters Spoilers(bool spoiler)
    {
        Add(QueryParameter.SPOILERS, spoiler.ToStringLower());
        return this;
    }
}
