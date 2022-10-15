using Animeikan.GUI.Models;
using Animeikan.GUI.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;

namespace Animeikan.GUI.ViewModels;

public partial class OnboardingScreenViewModel : ObservableObject
{
    [ObservableProperty]
    private string _buttonGlyph = FontAwesomeIcons.ArrowRight;

    [ObservableProperty]
    private int _position;

    [ObservableProperty]
    private List<OnboardingScreenModel> _onboardingScreens = new();

    public OnboardingScreenViewModel()
    {
        _onboardingScreens.AddRange(new[]
        {
            new OnboardingScreenModel()
            {
                OnboardingTitle = "First page",
                OnboardingDescription = "Description number 1",
                OnboardingImage = "hehe.jpg"
            },
            new OnboardingScreenModel()
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
            await AppShell.Current.GoToAsync($"//{nameof(MainPage)}");

        if (Position < OnboardingScreens.Count - 1)
            Position++;
    });
}