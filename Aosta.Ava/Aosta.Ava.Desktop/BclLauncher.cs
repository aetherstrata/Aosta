// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Aosta.Ava.Desktop;

public class BclLauncher : ILauncher
{
    public Task<bool> LaunchUriAsync(Uri uri)
    {
        ArgumentNullException.ThrowIfNull(uri);

        return Task.FromResult(uri.IsAbsoluteUri && exec(uri.AbsoluteUri));
    }

    private static bool exec(string urlOrFile)
    {
        if (OperatingSystem.IsLinux())
        {
            // If no associated application/json MimeType is found xdg-open opens return error
            // but it tries to open it anyway using the console editor (nano, vim, other..)
            shellExec($"xdg-open {urlOrFile}", waitForExit: false);
            return true;
        }
        else if (OperatingSystem.IsWindows() || OperatingSystem.IsMacOS())
        {
            using var process = Process.Start(new ProcessStartInfo
            {
                FileName = OperatingSystem.IsWindows() ? urlOrFile : "open",
                Arguments = OperatingSystem.IsMacOS() ? $"{urlOrFile}" : "",
                CreateNoWindow = true,
                UseShellExecute = OperatingSystem.IsWindows()
            });
            return true;
        }
        else
        {
            return false;
        }
    }

    private static void shellExec(string cmd, bool waitForExit = true)
    {
        string escapedArgs = Regex.Replace(cmd, "(?=[`~!#&*()|;'<>])", "\\")
            .Replace("\"", "\\\\\\\"");

        using var process = Process.Start(
            new ProcessStartInfo
            {
                FileName = "/bin/sh",
                Arguments = $"-c \"{escapedArgs}\"",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden
            }
        );
        if (waitForExit)
        {
            process?.WaitForExit();
        }
    }
}
