using MusicApp.ViewModels;

namespace MusicApp.Views;

public partial class ProfilePage : ContentPage
{
    public ProfilePage()
    {
        InitializeComponent();
        BindingContext = new ProfilePageViewModel();
    }
}