using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Realms;

namespace Aosta.GUI.Features;
public abstract class RealmViewModel
{
    /// <summary> Realm instance of this UI page </summary>
    protected Realm Realm { get; private set; }

    protected RealmViewModel() 
    {
        Realm = App.Core.GetInstance();
    }

    protected Realm NewInstance()
    {
        Realm.Dispose();
        Realm = App.Core.GetInstance();
        return Realm;
    }
}
