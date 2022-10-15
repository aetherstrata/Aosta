using Animeikan.GUI.ViewModels;

namespace Animeikan.GUI.Views;

public partial class OnboardingPage : ContentPage
{
    private OnboardingScreenViewModel obvm = new();

    public OnboardingPage()
    {
        InitializeComponent();
        BindingContext = obvm;
    }
}