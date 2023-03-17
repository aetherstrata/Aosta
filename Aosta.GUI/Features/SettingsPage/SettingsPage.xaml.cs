namespace Aosta.GUI.Features.SettingsPage;

public partial class SettingsPage : ContentPage
{
    private SettingsViewModel viewModel;

    public SettingsPage(SettingsViewModel vm)
    {
        InitializeComponent();

        BindingContext = viewModel = vm;
    }

    protected override void OnAppearing()
    {
        viewModel.UpdateRealmCount();
    }
}