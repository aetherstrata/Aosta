namespace Aosta.GUI.Features.AnimeManualAddPage;

public partial class AnimeManualAddPage
{
    public AnimeManualAddPage(AnimeManualAddViewModel aavm)
    {
        InitializeComponent();

        BindingContext = aavm;
    }
}