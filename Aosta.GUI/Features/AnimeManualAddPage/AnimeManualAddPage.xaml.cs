using Aosta.GUI.ViewModels;

namespace Aosta.GUI.Views;

public partial class AddAnimePage : ContentPage
{
	public AddAnimePage(AnimeManualAddViewModel aavm)
	{
		InitializeComponent();

		BindingContext = aavm;
	}
}