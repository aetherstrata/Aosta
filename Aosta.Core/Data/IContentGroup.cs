namespace Aosta.Core.Data;

public interface IContentGroup
{
    public Guid Id { get; init; }

    public string Title { get; set; }

    public string Synopsis { get; set; }

    public IList<IContent> Contents { get; set; }
}