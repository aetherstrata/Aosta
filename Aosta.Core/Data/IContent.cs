namespace Aosta.Core.Data;

public interface IContent
{
    public Guid Id { get; }

    public ContentType Type { get; set; }

    public string Title { get; set; }

    public string Synopsis { get; set; }

    public string Source { get; set; }
}