using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MusicApp.Models;
using MusicApp.Services;
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
        protected readonly SongServices _songServices;

        public RelayCommand SearchCommand { get; set; }
        public ObservableCollection<string> RecentSearches { get; } = new();

        public ObservableCollection<Song> FilteredSongs { get; set;}

        public List<Song> Songs { get; } = new();

        public SearchPageViewModel(SongServices songServices)
        {
            _songServices = songServices;
            SearchCommand=new RelayCommand(SearchCommandHandler);

            foreach (var song in _songServices.GetSongs())
            {
                Songs.Add(song);
            }
            RecentSearches.Add("Believer");
            RecentSearches.Add("Perfect");
            RecentSearches.Add("Faded");

        }

       

        void SearchCommandHandler()
        {
            if(Songs==null || Songs.Count <= 0)
            {
                return;
            }
            if (string.IsNullOrEmpty(SearchText))
            {
                FilteredSongs = new ObservableCollection<Song>(Songs);
                return;
            }
            var filteredSongs = Songs.Where(s => s.Title.Contains(SearchText, StringComparison.OrdinalIgnoreCase) || s.Artist.Contains(SearchText, StringComparison.OrdinalIgnoreCase));
            FilteredSongs = new ObservableCollection<Song>(filteredSongs);
        }

        [RelayCommand]
        private async Task Back()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
