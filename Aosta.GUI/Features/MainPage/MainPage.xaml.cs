using Aosta.GUI.ViewModels;

namespace Aosta.GUI.Views;

public partial class MainPage : ContentPage
{
  public MainPage(MainPageViewModel mpvm)
  {
    InitializeComponent();

    BindingContext = mpvm;
  }
}