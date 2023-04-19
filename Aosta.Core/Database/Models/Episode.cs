using Realms;

namespace Aosta.Core.Database.Models;

public partial class Episode : IRealmObject, IHasGuidPrimaryKey
{
    /// <summary>The unique ID of this content</summary>
    [PrimaryKey]
    public Guid Id { get; private set; } = Guid.NewGuid();
}