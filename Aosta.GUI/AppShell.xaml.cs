using Aosta.GUI.Features.AnimeManualAddPage;
using Aosta.GUI.Features.ProfileMainPage;
using Aosta.GUI.Features.OnboardingPage;

namespace Aosta.GUI;

public partial class AppShell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(AnimeManualAddPage), typeof(AnimeManualAddPage));
        Routing.RegisterRoute(nameof(OnboardingPage), typeof(OnboardingPage));
    }
}