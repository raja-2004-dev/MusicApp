using MusicApp.ViewModels;

namespace MusicApp.Views;

public partial class LibraryPage : ContentPage
{
    public LibraryPage()
    {
        InitializeComponent();
        BindingContext = new LibraryPageViewModel();
    }
}