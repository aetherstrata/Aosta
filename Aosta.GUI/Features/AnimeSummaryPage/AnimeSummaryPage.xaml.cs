namespace Aosta.GUI.Features.AnimeSummaryPage;

public partial class AnimeSummaryPage : ContentPage
{
  public AnimeSummaryPage(AnimeSummaryViewModel vm)
  {
    InitializeComponent();
    BindingContext = vm;
  }
}