using Animeikan.GUI.ViewModels;

namespace Animeikan.GUI.Views;

public partial class SettingsPage : ContentPage
{
  public SettingsPage(SettingsViewModel svm)
  {
    InitializeComponent();
    BindingContext = svm;
  }
}