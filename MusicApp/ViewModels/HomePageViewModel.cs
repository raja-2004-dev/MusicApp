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
        protected readonly SongServices _songServices;
        public HomePageViewModel(SongServices songServices)
        {
            
            PlaySongCommand = new AsyncRelayCommand<Song>(PlaySongCommandHandler);
            _songServices = songServices;

            var songs = _songServices.GetSongs();

            foreach (var song in songs)
            {
                RecentlyPlayed.Add(song);
                RecommendedSongs.Add(song);
                TrendingSongs.Add(song);
            }

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
            await Shell.Current.GoToAsync(nameof(PlayerPage), new Dictionary<string, object>
            {
                ["Song"] = song
            });
        }
    }
}
