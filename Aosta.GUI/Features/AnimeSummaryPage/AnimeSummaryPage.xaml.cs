namespace Aosta.GUI.Features.AnimeSummaryPage;

public partial class AnimeSummaryPage
{
    public AnimeSummaryPage(AnimeSummaryViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}