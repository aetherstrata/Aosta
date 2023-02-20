namespace Aosta.GUI.Features.MainPage;

public partial class MainPage : ContentPage
{
  public MainPage(MainPageViewModel mpvm)
  {
    InitializeComponent();

    BindingContext = mpvm;
  }
}