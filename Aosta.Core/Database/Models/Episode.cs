using Realms;

namespace Aosta.Core.Database.Models;

//TODO: modellare epidosi

/// <summary>
/// Episode model class
/// </summary>
public partial class Episode : IRealmObject, IHasGuidPrimaryKey
{
    /// <summary>The unique ID of this content</summary>
    [PrimaryKey]
    public Guid Id { get; private set; } = Guid.NewGuid();
}