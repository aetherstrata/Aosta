using CommunityToolkit.Mvvm.ComponentModel;

namespace Aosta.GUI.ViewModels;

[ObservableObject]
public partial class AnimeSummaryViewModel
{
    [ObservableProperty]
    private string _animeTitle;
}
