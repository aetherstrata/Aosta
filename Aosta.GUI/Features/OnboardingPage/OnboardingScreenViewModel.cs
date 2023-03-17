using System.Windows.Input;
using Aosta.GUI.Globals;
using Aosta.GUI.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Aosta.GUI.Features.OnboardingPage;

[ObservableObject]
public partial class OnboardingScreenViewModel
{
    [ObservableProperty] private string _buttonGlyph = FontAwesomeIcons.ArrowRight;

    [ObservableProperty] private List<OnboardingScreenModel> _onboardingScreens = new();

    [ObservableProperty] private int _position;

    private readonly ISettingsService settingsService;

    public OnboardingScreenViewModel(ISettingsService settingsService)
    {
        this.settingsService = settingsService;

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
            }
        });
    }

    public ICommand PositionChangedCommand => new Command(() =>
    {
        if (Position == OnboardingScreens.Count - 1)
        {
            ButtonGlyph = FontAwesomeIcons.Check;
            return;
        }

        if (ButtonGlyph == FontAwesomeIcons.Check)
            ButtonGlyph = FontAwesomeIcons.ArrowRight;
    });

    public ICommand NextPageCommand => new Command(async () =>
    {
        if (Position == OnboardingScreens.Count - 1)
        {
            await settingsService.Save("firstRun", false);
            await Shell.Current.GoToAsync($"//{nameof(MainPage.MainPage)}");
        }

        if (Position < OnboardingScreens.Count - 1)
            Position++;
    });
}