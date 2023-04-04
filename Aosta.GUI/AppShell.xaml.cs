using Aosta.GUI.Features.AnimeManualAddPage;
using Aosta.GUI.Features.ProfileMainPage;
using Aosta.GUI.Features.SettingsPage;

namespace Aosta.GUI;

public partial class AppShell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
        Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
        Routing.RegisterRoute(nameof(AnimeManualAddPage), typeof(AnimeManualAddPage));
    }
}