using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MusicApp.Models;
using MusicApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MusicApp.ViewModels
{
    public partial class HomePageViewModel:ObservableObject
    {
        [ObservableProperty] private string _searchText;
        public ObservableCollection<Song> RecentlyPlayed { get; } = new();
        public ObservableCollection<Song> TrendingSongs { get; } = new();

        public ObservableCollection<Song> RecommendedSongs { get; } = new();

        public AsyncRelayCommand<Song>PlaySongCommand { get; set; }

        public HomePageViewModel()
        {
            PlaySongCommand = new AsyncRelayCommand<Song>(PlaySongCommandHandler);
            TrendingSongs.Add(new Song
            {
                Title = "Enemy",
                Artist = "Imagine Dragons",
                Image = "album7.png",
                Duration = "2:53"
            });

            TrendingSongs.Add(new Song
            {
                Title = "Closer",
                Artist = "The Chainsmokers",
                Image = "album8.png",
                Duration = "4:02"
            });

            TrendingSongs.Add(new Song
            {
                Title = "Thunder",
                Artist = "Imagine Dragons",
                Image = "album9.png",
                Duration = "3:09"
            });

            TrendingSongs.Add(new Song
            {
                Title = "Lovely",
                Artist = "Billie Eilish",
                Image = "album10.png",
                Duration = "3:18"
            });
            RecentlyPlayed.Add(new Song
            {
                Title = "Believer",
                Artist = "Imagine Dragons",
                Image = "home.png",
                Duration = "3:28"
            });

            RecentlyPlayed.Add(new Song
            {
                Title = "Faded",
                Artist = "Alan Walker",
                Image = "library.png",
                Duration = "3:15"
            });

            RecentlyPlayed.Add(new Song
            {
                Title = "Heat Waves",
                Artist = "Glass Animals",
                Image = "profile.png",
                Duration = "3:42"
            });

            RecommendedSongs.Add(new Song
            {
                Title = "Perfect",
                Artist = "Ed Sheeran",
                Image = "search.png",
                Duration = "4:21"
            });

            RecommendedSongs.Add(new Song
            {
                Title = "Blinding Lights",
                Artist = "The Weeknd",
                Image = "user.png",
                Duration = "3:20"
            });

            RecommendedSongs.Add(new Song
            {
                Title = "Shape of You",
                Artist = "Ed Sheeran",
                Image = "dontnet_bot.png",
                Duration = "3:55"
            });
        }

        [RelayCommand]
        private async Task OpenSearch()
        {
            await Shell.Current.GoToAsync(nameof(SearchPage));
        }

        [RelayCommand]
        private async Task OpenLibrary()
        {
            await Shell.Current.GoToAsync(nameof(LibraryPage));
        }

        [RelayCommand]
        private async Task OpenProfile()
        {
            await Shell.Current.GoToAsync(nameof(ProfilePage));
        }

        async Task PlaySongCommandHandler(Song song)
        {
            if (song == null)
            {
                return;
            }
            await Shell.Current.GoToAsync(nameof(PlayerPage));
        }
    }
}
