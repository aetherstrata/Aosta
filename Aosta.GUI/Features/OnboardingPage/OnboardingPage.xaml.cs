namespace Aosta.GUI.Features.OnboardingPage;

public partial class OnboardingPage : ContentPage
{
    public OnboardingPage(OnboardingScreenViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}