using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MusicApp.Models;
using MusicApp.Services;
using MusicApp.Views;
using System.Collections.ObjectModel;

namespace MusicApp.ViewModels;

public partial class HomePageViewModel : ObservableObject
{
    private readonly SongServices _songServices;
    private readonly RecentSongsService _recentSongsService;

    public ObservableCollection<Song> TrendingSongs { get; } = new();

    public ObservableCollection<Song> RecommendedSongs { get; } = new();

    public ObservableCollection<Song> RecentlyPlayed =>
        _recentSongsService.RecentlyPlayed;


    public HomePageViewModel(
        SongServices songServices,
        RecentSongsService recentSongsService)
    {
        _songServices = songServices;
        _recentSongsService = recentSongsService;

        LoadSongs();
    }


    // ------------------------------------------------
    // LOAD SONGS
    // ------------------------------------------------

    private void LoadSongs()
    {
        var songs = _songServices.GetSongs();

        TrendingSongs.Clear();
        RecommendedSongs.Clear();

        foreach (var song in songs)
        {
            TrendingSongs.Add(song);
            RecommendedSongs.Add(song);
        }
    }


    // ------------------------------------------------
    // PLAY SONG
    // ------------------------------------------------

    [RelayCommand]
    private async Task PlaySong(Song song)
    {
        if (song == null)
            return;

        await Shell.Current.GoToAsync(
            nameof(PlayerPage),
            new Dictionary<string, object>
            {
                ["Song"] = song
            });
    }


    // ------------------------------------------------
    // SEARCH
    // ------------------------------------------------

    [RelayCommand]
    private async Task OpenSearch()
    {
        await Shell.Current.GoToAsync(
            nameof(SearchPage));
    }


    // ------------------------------------------------
    // LIBRARY
    // ------------------------------------------------

    [RelayCommand]
    private async Task OpenLibrary()
    {
        await Shell.Current.GoToAsync(
            nameof(LibraryPage));
    }


    // ------------------------------------------------
    // PROFILE
    // ------------------------------------------------

    [RelayCommand]
    private async Task OpenProfile()
    {
        await Shell.Current.GoToAsync(
            nameof(ProfilePage));
    }
}