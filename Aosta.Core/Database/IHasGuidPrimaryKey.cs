using System;
using Aosta.Core.Database.Models;
using Realms;

namespace Aosta.Core.Database;

public interface IHasGuidPrimaryKey
{
    public Guid ID { get; }
}