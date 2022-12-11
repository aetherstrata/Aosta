using CommunityToolkit.Mvvm.ComponentModel;

namespace Animeikan.GUI.ViewModels;

[ObservableObject]
public partial class AnimeSummaryViewModel
{
    [ObservableProperty]
    private string _animeTitle;
}
