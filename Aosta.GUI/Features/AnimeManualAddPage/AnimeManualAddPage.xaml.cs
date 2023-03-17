namespace Aosta.GUI.Features.AnimeManualAddPage;

public partial class AddAnimePage : ContentPage
{
    public AddAnimePage(AnimeManualAddViewModel aavm)
    {
        InitializeComponent();

        BindingContext = aavm;
    }
}