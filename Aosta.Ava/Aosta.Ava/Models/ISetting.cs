// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace Aosta.Ava.Models;

public interface ISetting<out T>
{
    abstract static T? Load();

    void Save();
}
