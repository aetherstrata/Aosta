#region

using Realms;

#endregion

namespace Aosta.Core.Data.Models;

public partial class LocalEpisodeObject : IRealmObject
{
    [PrimaryKey]
     public Guid Id { get; private set; } = Guid.NewGuid();
}