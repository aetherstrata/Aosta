using DevExpress.Maui.Controls;

namespace Aosta.GUI.Features.ProfileMainPage;

public partial class ProfilePage
{
    public ProfilePage()
    {
        InitializeComponent();
    }

    private void TapGestureRecognizer_OnTapped(object? sender, TappedEventArgs e)
    {
        bottomSheet.State = BottomSheetState.HalfExpanded;
    }

    private void ChangePicture_Clicked(object? sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private void DeletePicture_Clicked(object? sender, EventArgs e)
    {
        throw new NotImplementedException();
    }
}