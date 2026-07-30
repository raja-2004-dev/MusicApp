using MusicApp.Models;
using MusicApp.ViewModels;

namespace MusicApp.Views;

public partial class SearchPage : ContentPage
{
    
        public SearchPage()
        {
            InitializeComponent();
            BindingContext = new SearchPageViewModel(new SongServices());
        }
    
}