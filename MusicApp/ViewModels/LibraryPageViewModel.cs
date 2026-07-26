using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MusicApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MusicApp.ViewModels
{
    public partial class LibraryPageViewModel : ObservableObject
    {
        public ObservableCollection<Song> Songs { get; } = new();

        public LibraryPageViewModel()
        {
            Songs.Add(new Song
            {
                Title = "Believer",
                Artist = "Imagine Dragons",
                Image = "album1.png"
            });

            Songs.Add(new Song
            {
                Title = "Perfect",
                Artist = "Ed Sheeran",
                Image = "album2.png"
            });

            Songs.Add(new Song
            {
                Title = "Heat Waves",
                Artist = "Glass Animals",
                Image = "album3.png"
            });
        }

        [RelayCommand]
        private async Task Back()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
