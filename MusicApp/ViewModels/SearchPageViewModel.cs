using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MusicApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MusicApp.ViewModels
{
    public partial class SearchPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private string searchText = string.Empty;

        public ObservableCollection<string> RecentSearches { get; } = new();

        public ObservableCollection<Song> Songs { get; } = new();

        public SearchPageViewModel()
        {
            RecentSearches.Add("Believer");
            RecentSearches.Add("Perfect");
            RecentSearches.Add("Faded");

            Songs.Add(new Song
            {
                Title = "Believer",
                Artist = "Imagine Dragons",
                Image = "album1.png",
                Duration = "3:28"
            });

            Songs.Add(new Song
            {
                Title = "Perfect",
                Artist = "Ed Sheeran",
                Image = "album2.png",
                Duration = "4:10"
            });

            Songs.Add(new Song
            {
                Title = "Heat Waves",
                Artist = "Glass Animals",
                Image = "album3.png",
                Duration = "3:35"
            });

            Songs.Add(new Song
            {
                Title = "Thunder",
                Artist = "Imagine Dragons",
                Image = "album4.png",
                Duration = "3:10"
            });
        }

        [RelayCommand]
        private async Task Back()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
