using CommunityToolkit.Mvvm.ComponentModel;

namespace Aosta.GUI.Features.AnimeSummaryPage;

[ObservableObject]
public partial class AnimeSummaryViewModel
{
    [ObservableProperty] private string _animeTitle = string.Empty;
}