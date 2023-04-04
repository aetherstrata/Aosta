using Aosta.Core;
using Realms;

namespace Aosta.GUI.Features;
public abstract class RealmViewModel
{
    /// <summary> Realm instance of this UI page </summary>
    protected Realm Realm { get; private set; }

    private readonly AostaDotNet _aosta;

    protected RealmViewModel(AostaDotNet aosta)
    {
        _aosta = aosta;
        Realm = _aosta.GetInstance();
    }

    protected Realm NewInstance()
    {
        Realm.Dispose();
        Realm = _aosta.GetInstance();
        return Realm;
    }
}
