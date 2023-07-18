using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aosta.GUI.Features.AddJikanAnime;

public partial class AddJikanAnimePage : ContentPage
{
    public AddJikanAnimePage(AddJikanAnimeViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}