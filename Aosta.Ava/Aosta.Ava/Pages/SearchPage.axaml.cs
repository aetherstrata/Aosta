using Aosta.Ava.ViewModels;

using Avalonia.Input;
using Avalonia.ReactiveUI;

using Serilog;

namespace Aosta.Ava.Pages;

public partial class SearchPage : ReactiveUserControl<SearchPageViewModel>
{
    public SearchPage()
    {
        InitializeComponent();
    }

    private void OnFilterButtonTapped(object? sender, TappedEventArgs e)
    {
        FilterPane.Height = FilterPane.Height > 0 ? 0 : 400;
    }
}
