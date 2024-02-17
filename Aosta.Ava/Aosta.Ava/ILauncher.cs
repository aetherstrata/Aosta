// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Threading.Tasks;

namespace Aosta.Ava;

public interface ILauncher
{
    Task<bool> LaunchUriAsync(Uri uri);
}
