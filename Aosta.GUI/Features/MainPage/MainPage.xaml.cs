namespace Aosta.GUI.Features.MainPage;

public partial class MainPage
{

    public MainPage(MainPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}