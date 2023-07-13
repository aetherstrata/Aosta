using Aosta.Common.Extensions;
using Aosta.Jikan.Query.Enums;

namespace Aosta.Jikan.Query;

public class TopReviewsQueryParameters : JikanQueryParameterSet
{
    public TopReviewsQueryParameters SetPage(int page)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        base.Add(QueryParameter.Page, page);
        return this;
    }

    public TopReviewsQueryParameters SetType(TopReviewsTypeFilter type)
    {
        Guard.IsValidEnum(type, nameof(type));
        base.Add(QueryParameter.Type, type.StringValue());
        return this;
    }

    public TopReviewsQueryParameters SetPreliminary(bool preliminary)
    {
        base.Add(QueryParameter.Preliminary, preliminary.ToStringLower());
        return this;
    }

    public TopReviewsQueryParameters SetSpoilers(bool spoiler)
    {
        base.Add(QueryParameter.Spoilers, spoiler.ToStringLower());
        return this;
    }
}