using CommunityToolkit.Mvvm.ComponentModel;

namespace Aosta.GUI.Features.AnimeSummaryPage;

public partial class AnimeSummaryViewModel : ObservableObject
{
    [ObservableProperty]
    private string _animeTitle = string.Empty;
}