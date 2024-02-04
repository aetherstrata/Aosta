using Aosta.Jikan.Exceptions;
using Aosta.Jikan.Query;
using Aosta.Jikan.Query.Enums;

namespace Aosta.Jikan.Tests;

[TestFixture]
internal class QueryTests
{
    private static readonly string[] s_Endpoint =
    [
        "api",
        "v2",
        "items"
    ];

    private JikanQuery _query;

    [Test]
    public void GetQuery_shouldReturnEndpoint_whenNoParam()
    {
        _query = JikanQuery.Create(s_Endpoint);

        _query.GetQuery().Should().Be("api/v2/items");
    }

    [Test]
    public void GetQuery_shouldAddParamName_whenTrueBool()
    {
        _query = JikanQuery.Create(s_Endpoint)
            .With(QueryParameter.KIDS, true);

        _query.GetQuery().Should().Be("api/v2/items?kids");
    }

    [Test]
    public void GetQuery_shouldNotAddParamName_whenFalseBool()
    {
        _query = JikanQuery.Create(s_Endpoint)
            .With(QueryParameter.KIDS, false);

        _query.GetQuery().Should().Be("api/v2/items");
    }

    [Test]
    public void GetQuery_shouldAddParam_whenInteger()
    {
        _query = JikanQuery.Create(s_Endpoint)
            .With(QueryParameter.KIDS, 14);

        _query.GetQuery().Should().Be("api/v2/items?kids=14");
    }

    [Test]
    public void GetQuery_shouldAddParam_whenString()
    {
        _query = JikanQuery.Create(s_Endpoint)
            .With(QueryParameter.KIDS, "other");

        _query.GetQuery().Should().Be("api/v2/items?kids=other");
    }

    [Test]
    public void GetQuery_shouldAddParam_whenEnum()
    {
        _query = JikanQuery.Create(s_Endpoint)
            .With(QueryParameter.STATUS, AiringStatusFilter.Airing);

        _query.GetQuery().Should().Be("api/v2/items?status=airing");
    }

    [Test]
    public void GetQuery_shouldConcatParams_whenMultiple()
    {
        _query = JikanQuery.Create(s_Endpoint)
            .With(QueryParameter.SAFE_FOR_WORK, true)
            .With(QueryParameter.UNAPPROVED, false)
            .With(QueryParameter.PAGE, 4)
            .With(QueryParameter.QUERY, "Naruto")
            .With(QueryParameter.STATUS, AiringStatusFilter.Airing);

        _query.GetQuery().Should().Be("api/v2/items?sfw&page=4&q=Naruto&airing=airing");
    }

    [Test]
    public void GetQuery_shouldConcatParams_whenSet()
    {
        var set = new JikanQueryParameterSet();

        set.Add(QueryParameter.SAFE_FOR_WORK, true);
        set.Add(QueryParameter.UNAPPROVED, false);
        set.Add(QueryParameter.PAGE, 4);
        set.Add(QueryParameter.QUERY, "Naruto");
        set.Add(QueryParameter.STATUS, AiringStatusFilter.Airing);

        _query = JikanQuery.Create(s_Endpoint).AddRange(set);

        _query.GetQuery().Should().Be("api/v2/items?sfw&page=4&q=Naruto&status=airing");
    }

    [Test]
    public void QueryParameterSet_shouldThrow_onDuplicateParamName()
    {
        var set = new JikanQueryParameterSet();
        set.Add("name", 1);

        var act = () => set.Add("name", "test");

        act.Should().ThrowExactly<JikanDuplicateParameterException>();
    }
}
