using Aosta.Ava.ViewModels;

using Avalonia.ReactiveUI;
using ReactiveUI;

namespace Aosta.Ava.Pages;

public partial class HomePage : ReactiveUserControl<HomePageViewModel>
{
    public HomePage()
    {
        this.WhenActivated(disposable => {  });
        InitializeComponent();
    }
}
