using Animeikan.GUI.ViewModels;

namespace Animeikan.GUI.Views;

public partial class AnimeSummaryPage : ContentPage
{
  public AnimeSummaryPage(AnimeSummaryViewModel asvm)
  {
    InitializeComponent();
    BindingContext = asvm;
  }
}