using Animeikan.GUI.ViewModels;

namespace Animeikan.GUI.Views;

public partial class OnboardingPage : ContentPage
{
    public OnboardingPage(OnboardingScreenViewModel obvm)
    {
        InitializeComponent();
        BindingContext = obvm;
    }
}