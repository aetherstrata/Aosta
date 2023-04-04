namespace Aosta.GUI.Features.SettingsPage;

public partial class SettingsPage
{
    private readonly SettingsViewModel _viewModel;

    public SettingsPage(SettingsViewModel vm)
    {
        InitializeComponent();

        BindingContext = _viewModel = vm;
    }

    protected override void OnAppearing()
    {
        _viewModel.UpdateRealmCount();
    }
}