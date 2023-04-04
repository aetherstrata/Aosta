namespace Aosta.GUI.Features.OnboardingPage;

public partial class OnboardingPage
{
    public OnboardingPage(OnboardingScreenViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}