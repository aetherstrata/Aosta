using Animeikan.GUI.ViewModels;

namespace Animeikan.GUI.Views;

public partial class AddAnimePage : ContentPage
{
	public AddAnimePage(AnimeManualAddViewModel aavm)
	{
		InitializeComponent();

		BindingContext = aavm;
	}
}