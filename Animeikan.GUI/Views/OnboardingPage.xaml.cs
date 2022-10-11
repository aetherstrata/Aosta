using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Animeikan.GUI.ViewModels;

namespace Animeikan.GUI.Views;

public partial class OnboardingPage : ContentPage
{
    private OnboardingScreenViewModel obvm = new();

    public OnboardingPage()
    {
        InitializeComponent();
        BindingContext = obvm;
    }
}