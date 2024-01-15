using Aosta.Ava.ViewModels;

using Avalonia.Media;
using Avalonia.ReactiveUI;

namespace Aosta.Ava.Pages;

public partial class SearchPage : ReactiveUserControl<SearchPageViewModel>
{
    public SearchPage()
    {
        InitializeComponent();
    }

    /// <inheritdoc />
    public override void Render(DrawingContext context)
    {
        base.Render(context);

       // MainGrid.biu
    }
}
