using Realms;

namespace Aosta.Core.Database.Models;

public partial class User : IRealmObject, IHasPrimaryKey<Guid>
{
    [PrimaryKey]
    public Guid ID { get; private set; } = Guid.NewGuid();

    public string Username { get; set; }

    [Backlink(nameof(Anime.User))]
    public IQueryable<Anime> AnimeList { get; }

    internal static User Empty() => new()
    {
        ID = Guid.Empty,
        Username = "localuser"
    };
}
