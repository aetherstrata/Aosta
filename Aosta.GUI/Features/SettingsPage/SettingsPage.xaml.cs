using Aosta.GUI.ViewModels;

namespace Aosta.GUI.Views;

public partial class SettingsPage : ContentPage
{
  public SettingsPage(SettingsViewModel vm)
  {
    InitializeComponent();

    BindingContext = vm;
  }
}