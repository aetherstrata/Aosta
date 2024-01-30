// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using Aosta.Data.Database.Models;

namespace Aosta.Data.Tests.Realm;

[TestFixture]
public class AccessorTests
{
    [Test]
    public void differentAccessors_haveDifferentRealms()
    {
        var a1 = RealmSetup.NewInstance();
        var a2 = RealmSetup.NewInstance();

        a1.AddAnime();

        a1.Run(r => r.All<Anime>().Count()).Should().Be(1);
        a2.Run(r => r.All<Anime>().Count()).Should().Be(0);
    }
}
