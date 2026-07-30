using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MusicApp.Models;
using MusicApp.Services;

namespace Music2._0.ViewModels;

public partial class PlayerPageViewModel : ObservableObject, IQueryAttributable
{
    private readonly SongServices _songServices;
    private readonly FavouritesServices _favouritesServices;

    private Song? _currentSong;

    [ObservableProperty]
    private string title = string.Empty;

    [ObservableProperty]
    private string artist = string.Empty;

    [ObservableProperty]
    private ImageSource image;

    [ObservableProperty]
    private string currentTime = "0:00";

    [ObservableProperty]
    private string totalTime = "0:00";

    [ObservableProperty]
    private double progress;

    [ObservableProperty]
    private bool isPlaying;

    [ObservableProperty]
    private string favoriteIcon = "heart.png";

    public PlayerPageViewModel(
        SongServices songServices,
        FavouritesServices favouritesServices)
    {
        _songServices = songServices;
        _favouritesServices = favouritesServices;
    }

    public string PlayPauseIcon => IsPlaying ? "pause.png" : "play.png";

    partial void OnIsPlayingChanged(bool value)
    {
        OnPropertyChanged(nameof(PlayPauseIcon));
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("Song", out var value) &&
            value is Song song)
        {
            _currentSong = song;

            Title = song.Title;
            Artist = song.Artist;
            Image = song.Image;
            CurrentTime = song.Duration;
            TotalTime = song.Duration;

            FavoriteIcon = _favouritesServices.IsFavorite(song)
                ? "heart_fill.png"
                : "heart.png";
        }
    }

    [RelayCommand]
    private void ToggleFavorite()
    {
        if (_currentSong == null)
            return;

        _favouritesServices.ToggleFavorite(_currentSong);

        FavoriteIcon = _favouritesServices.IsFavorite(_currentSong)
            ? "heart_fill.png"
            : "heart.png";
    }

    [RelayCommand]
    private void PlayPause()
    {
        IsPlaying = !IsPlaying;
    }

    [RelayCommand]
    private void NextSong()
    {
        // We'll implement this later.
    }

    [RelayCommand]
    private void PreviousSong()
    {
        // We'll implement this later.
    }

    [RelayCommand]
    private async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private async Task Lyrics()
    {
        await Shell.Current.DisplayAlert(
            "Lyrics",
            "Lyrics screen coming soon.",
            "OK");
    }
}