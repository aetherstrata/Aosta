namespace Aosta.Jikan.Query;

internal class BoolQueryParameter : JikanQueryParameter<bool>
{
    public override string ToString() => Value ? Name : string.Empty;
}
