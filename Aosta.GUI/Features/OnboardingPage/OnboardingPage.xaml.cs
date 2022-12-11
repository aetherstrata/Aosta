using Aosta.GUI.ViewModels;

namespace Aosta.GUI.Views;

public partial class OnboardingPage : ContentPage
{
    public OnboardingPage(OnboardingScreenViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}