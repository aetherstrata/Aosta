using System.Windows.Input;
using Aosta.GUI.Globals;
using Aosta.GUI.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Aosta.GUI.Features.OnboardingPage;

public partial class OnboardingScreenViewModel : ObservableObject
{
    [ObservableProperty]
    private string _buttonGlyph = FontAwesomeIcons.ArrowRight;

    [ObservableProperty]
    private List<OnboardingScreenModel> _onboardingScreens = new();

    [ObservableProperty]
    private int _position;

    private readonly ISettingsService _settingsService;

    public OnboardingScreenViewModel(ISettingsService settingsService)
    {
        this._settingsService = settingsService;

        _onboardingScreens.AddRange(new[]
        {
            new OnboardingScreenModel
            {
                OnboardingTitle = "First page",
                OnboardingDescription = "Description number 1",
                OnboardingImage = "hehe.jpg"
            },
            new OnboardingScreenModel
            {
                OnboardingTitle = "Second page",
                OnboardingDescription = "Description number 2",
                OnboardingImage = "hehe.jpg"
            },
            new OnboardingScreenModel
            {
                OnboardingTitle = "Third page",
                OnboardingDescription = "Description number 3",
                OnboardingImage = "hehe.jpg"
            },
            new OnboardingScreenModel
            {
                OnboardingTitle = "Fourth page",
                OnboardingDescription = "Description number 4",
                OnboardingImage = "hehe.jpg"
            },
            new OnboardingScreenModel
            {
                OnboardingTitle = "Fifth page",
                OnboardingDescription = "Description number 5",
                OnboardingImage = "hehe.jpg"
            }
        });
    }

    [RelayCommand]
    public void PositionChanged()
    {
        if (Position == OnboardingScreens.Count - 1)
        {
            ButtonGlyph = FontAwesomeIcons.Check;
            return;
        }

        if (ButtonGlyph == FontAwesomeIcons.Check)
            ButtonGlyph = FontAwesomeIcons.ArrowRight;
    }

    [RelayCommand]
    private async Task NextPage()
    {
        if (Position == OnboardingScreens.Count - 1)
        {
            await _settingsService.Save("firstRun", false);
            await Shell.Current.GoToAsync($"//{nameof(MainPage.MainPage)}");
        }

        if (Position < OnboardingScreens.Count - 1)
            Position++;
    }
}
