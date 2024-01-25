using Aosta.Common.Extensions;

namespace Aosta.Jikan.Query;

internal class EnumQueryParameter<T> : JikanQueryParameter<T> where T : struct, Enum
{
    public override string ToString() => $"{Name}={Value.StringValue()}";
}
