using Aosta.Core;
using Realms;

namespace Aosta.GUI.Features;

public abstract class RealmViewModel
{
    /// <summary> Realm instance of this UI page </summary>
    protected Realm Realm { get; private set; }

    protected RealmViewModel(AostaDotNet aosta)
    {
         Realm = aosta.GetInstance();
    }
}
