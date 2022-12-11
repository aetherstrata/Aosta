using Animeikan.GUI.ViewModels;

namespace Animeikan.GUI.Views;

public partial class MainPage : ContentPage
{
  public MainPage(MainPageViewModel mpvm)
  {
    InitializeComponent();

    BindingContext = mpvm;
  }
}