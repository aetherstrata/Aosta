namespace Aosta.GUI.Features.SettingsPage;

public partial class SettingsPage : ContentPage
{
  public SettingsPage(SettingsViewModel vm)
  {
    InitializeComponent();

    BindingContext = vm;
  }
}