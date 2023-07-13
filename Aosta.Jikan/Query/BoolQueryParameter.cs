namespace Aosta.Jikan.Query;

internal class BoolQueryParameter : JikanQueryParameter<bool>
{
    public override string ToString() => Value switch
    {
        true => Name,
        false => string.Empty
    };
}