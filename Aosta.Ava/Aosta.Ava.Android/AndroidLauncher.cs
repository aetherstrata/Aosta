// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Threading.Tasks;

using Android.Content;

using AndroidUri = Android.Net.Uri;

namespace Aosta.Ava.Android;

public class AndroidLauncher(Context context) : ILauncher
{
    private const ActivityFlags flags = ActivityFlags.ClearTop | ActivityFlags.NewTask;

    public Task<bool> LaunchUriAsync(Uri uri)
    {
        ArgumentNullException.ThrowIfNull(uri);

        if (uri.IsAbsoluteUri && context.PackageManager is { } packageManager)
        {
            var intent = new Intent(Intent.ActionView, AndroidUri.Parse(uri.OriginalString));
            if (intent.ResolveActivity(packageManager) is not null)
            {
                intent.SetFlags(flags);
                context.StartActivity(intent);
            }
        }

        return Task.FromResult(false);
    }
}
