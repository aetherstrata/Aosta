using Aosta.GUI.Views;

namespace Aosta.GUI;

public partial class AppShell : Shell
{
  public AppShell()
  {
    InitializeComponent();

    Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
    Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
  }
}
