using Aosta.GUI.ViewModels;

namespace Aosta.GUI.Views;

public partial class AnimeSummaryPage : ContentPage
{
  public AnimeSummaryPage(AnimeSummaryViewModel vm)
  {
    InitializeComponent();
    BindingContext = vm;
  }
}