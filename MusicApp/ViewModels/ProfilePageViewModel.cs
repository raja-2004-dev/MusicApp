using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MusicApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MusicApp.ViewModels
{
    public partial class ProfilePageViewModel : ObservableObject
    {
        [ObservableProperty]
        private string userName = "Siva Maharaj";

        [ObservableProperty]
        private string email = "sivamaharaj@gmail.com";

        public ObservableCollection<ProfileMenu> MenuItems { get; } = new();

        public ProfilePageViewModel()
        {
            MenuItems.Add(new ProfileMenu { Icon = "👤", Title = "Edit Profile" });
            MenuItems.Add(new ProfileMenu { Icon = "❤️", Title = "Favorite Songs" });
            MenuItems.Add(new ProfileMenu { Icon = "🎵", Title = "My Playlist" });
            MenuItems.Add(new ProfileMenu { Icon = "⬇", Title = "Downloads" });
            MenuItems.Add(new ProfileMenu { Icon = "⚙", Title = "Settings" });
            MenuItems.Add(new ProfileMenu { Icon = "❓", Title = "Help & Support" });
            MenuItems.Add(new ProfileMenu { Icon = "ℹ", Title = "About App" });
        }

        [RelayCommand]
        private async Task Back()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
