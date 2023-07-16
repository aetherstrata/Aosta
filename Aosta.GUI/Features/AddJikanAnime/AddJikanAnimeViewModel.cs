using Aosta.Core;
using Realms;

namespace Aosta.GUI.Features.AddJikanAnime;

internal class AddJikanAnimeViewModel
{
    private readonly Realm _realm;

    public AddJikanAnimeViewModel(AostaDotNet aosta)
    {
        _realm = aosta.GetInstance();
    }
}