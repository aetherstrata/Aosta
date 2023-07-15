using Aosta.GUI.Features.AddManualAnime;

namespace Aosta.GUI.Features.AddManualAnime;

public partial class AddManualAnimePage
{
    public AddManualAnimePage(AddManualAnimeViewModel vm)
    {
        InitializeComponent();

        BindingContext = vm;
    }
}