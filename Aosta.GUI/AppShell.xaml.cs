using Aosta.GUI.Features.AddJikanAnime;
using Aosta.GUI.Features.AddManualAnime;
using Aosta.GUI.Features.OnboardingPage;

namespace Aosta.GUI;

public partial class AppShell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(AddJikanAnimePage), typeof(AddJikanAnimePage));
        Routing.RegisterRoute(nameof(AddManualAnimePage), typeof(AddManualAnimePage));
        Routing.RegisterRoute(nameof(OnboardingPage), typeof(OnboardingPage));
    }
}