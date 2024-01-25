// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using Aosta.Ava.Extensions;
using Aosta.Ava.Localization;
using Aosta.Core.Database;

using Splat;

namespace Aosta.Ava.Models;

public interface ISetting<out T>
{
    abstract static T? Load();

    void Save();
}
