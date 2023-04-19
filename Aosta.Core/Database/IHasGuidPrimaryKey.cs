using Realms;

namespace Aosta.Core.Database;

public interface IHasGuidPrimaryKey
{
    [PrimaryKey]
    public Guid Id { get; }
}