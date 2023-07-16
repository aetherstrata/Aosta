using Realms;

namespace Aosta.Core.Database.Models;

public partial class User : IRealmObject, IHasGuidPrimaryKey
{
    [PrimaryKey]
    public Guid ID { get; private set; }

    public string Username { get; set; }

    [Backlink(nameof(Anime.User))]
    public IQueryable<Anime> AnimeList { get; }
}