using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MusicApp.Models;
using MusicApp.Services;

namespace Music2._0.ViewModels;

public partial class PlayerPageViewModel : ObservableObject, IQueryAttributable
{
    private readonly SongServices _songServices;
    private readonly FavouritesServices _favouritesServices;
    private readonly AudioService _audioService;

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

    public string PlayPauseIcon => IsPlaying ? "pause.png" : "play.png";

    public PlayerPageViewModel(
        SongServices songServices,
        FavouritesServices favouritesServices,
        AudioService audioService)
    {
        _songServices = songServices;
        _favouritesServices = favouritesServices;
        _audioService = audioService;
        _audioService.PlaybackEnded += OnPlaybackEnded;
    }

    partial void OnIsPlayingChanged(bool value)
    {
        OnPropertyChanged(nameof(PlayPauseIcon));
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("Song", out var value) &&
            value is Song song)
        {
            _songServices.SetCurrentSong(song);

            LoadSong(song);
        }
    }
    private async void OnPlaybackEnded()
    {
        await NextSong();
    }

    private void LoadSong(Song song)
    {
        _currentSong = song;

        Title = song.Title;
        Artist = song.Artist;
        Image = song.Image;

        CurrentTime = "0:00";
        TotalTime = song.Duration;

        FavoriteIcon = _favouritesServices.IsFavorite(song)
            ? "heart_fill.png"
            : "heart.png";
    }

    [RelayCommand]
    private async Task PlayPause()
    {
        if (_currentSong == null)
            return;

        if (_audioService.IsPlaying)
        {
            _audioService.Pause();
            IsPlaying = false;
        }
        else
        {
            if (IsPlaying)
            {
                _audioService.Resume();
            }
            else
            {
                await _audioService.PlayAsync(_currentSong.AudioFile);
            }

            IsPlaying = true;
        }
    }

    [RelayCommand]
    private async Task NextSong()
    {
        _audioService.Stop();

        var song = _songServices.NextSong();

        LoadSong(song);

        await _audioService.PlayAsync(song.AudioFile);

        IsPlaying = true;
    }

    [RelayCommand]
    private async Task PreviousSong()
    {
        _audioService.Stop();

        var song = _songServices.PreviousSong();

        LoadSong(song);

        await _audioService.PlayAsync(song.AudioFile);

        IsPlaying = true;
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
    private async Task GoBack()
    {
        _audioService.Stop();

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