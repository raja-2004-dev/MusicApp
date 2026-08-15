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
    private readonly RecentSongsService _recentSongsService;

    private Song? _currentSong;

    private CancellationTokenSource? _progressCancellation;

    [ObservableProperty]
    private string title = string.Empty;

    [ObservableProperty]
    private string artist = string.Empty;
    [ObservableProperty]
    private bool isShuffleEnabled;

    [ObservableProperty]
    private int repeatMode;

    [ObservableProperty]
    private ImageSource? image;

    [ObservableProperty]
    private string currentTime = "0:00";

    [ObservableProperty]
    private string totalTime = "0:00";

    [ObservableProperty]
    private double progress;

    [ObservableProperty]
    private double duration;

    [ObservableProperty]
    private bool isPlaying;

    [ObservableProperty]
    private string favoriteIcon = "heart.png";


    public string PlayPauseIcon =>
        IsPlaying ? "pause.png" : "play.png";
    public string ShuffleIcon =>
    IsShuffleEnabled ? "shuffle_active.png" : "shuffle.png";

    public string RepeatIcon =>
        RepeatMode == 1 ? "repeat_active.png" :
        RepeatMode == 2 ? "repeat_one_active.png" :
        "repeat.png";

    public PlayerPageViewModel(
    SongServices songServices,
    FavouritesServices favouritesServices,
    AudioService audioService,
    RecentSongsService recentSongsService)
    {
        _songServices = songServices;
        _favouritesServices = favouritesServices;
        _audioService = audioService;
        _recentSongsService = recentSongsService;

        _audioService.PlaybackEnded += OnPlaybackEnded;
    }
    partial void OnIsShuffleEnabledChanged(bool value)
    {
        OnPropertyChanged(nameof(ShuffleIcon));
    }

    partial void OnRepeatModeChanged(int value)
    {
        OnPropertyChanged(nameof(RepeatIcon));
    }

    [RelayCommand]
    private void ToggleShuffle()
    {
        IsShuffleEnabled = !IsShuffleEnabled;
    }

    [RelayCommand]
    private void ToggleRepeat()
    {
        RepeatMode++;

        if (RepeatMode > 2)
            RepeatMode = 0;
    }


    // ------------------------------------------------
    // RECEIVE SELECTED SONG
    // ------------------------------------------------

    public void ApplyQueryAttributes(
        IDictionary<string, object> query)
    {
        if (query.TryGetValue("Song", out var value)
            && value is Song song)
        {
            _songServices.SetCurrentSong(song);

            LoadSong(song);
        }
    }


    // ------------------------------------------------
    // LOAD SONG DETAILS
    // ------------------------------------------------

    private void LoadSong(Song song)
    {
        _currentSong = song;

        Title = song.Title;
        Artist = song.Artist;
        Image = song.Image;

        CurrentTime = "0:00";

        Progress = 0;
        Duration = 0;

        TotalTime = song.Duration;

        FavoriteIcon =
            _favouritesServices.IsFavorite(song)
            ? "heart_fill.png"
            : "heart.png";
    }


    // ------------------------------------------------
    // PLAY / PAUSE
    // ------------------------------------------------

    [RelayCommand]
    private async Task PlayPause()
    {
        if (_currentSong == null)
            return;


        // Song currently playing
        if (_audioService.IsPlaying)
        {
            _audioService.Pause();

            IsPlaying = false;

            return;
        }


        // Player already exists but paused
        if (Duration > 0)
        {
            _audioService.Resume();

            IsPlaying = true;

            StartProgressTimer();

            return;
        }


        // First time playing song
        await PlayCurrentSong();
    }


    // ------------------------------------------------
    // PLAY CURRENT SONG
    // ------------------------------------------------

    private async Task PlayCurrentSong()
    {
        if (_currentSong == null ||
            string.IsNullOrWhiteSpace(_currentSong.AudioFile))
        {
            return;
        }

        await _audioService.PlayAsync(
            _currentSong.AudioFile);

        // Add to Recently Played
        _recentSongsService.AddRecentlyPlayed(_currentSong);

        IsPlaying = true;

        Duration = _audioService.Duration;

        TotalTime = FormatTime(Duration);

        StartProgressTimer();
    }


    // ------------------------------------------------
    // NEXT SONG
    // ------------------------------------------------

   
    [RelayCommand]
    private async Task NextSong()
    {
        StopProgressTimer();

        _audioService.Stop();

        Song song;

        if (IsShuffleEnabled)
        {
            song = GetRandomSong();
        }
        else
        {
            song = _songServices.NextSong();
        }

        LoadSong(song);

        await PlayCurrentSong();
    }

    private readonly Random _random = new();

    private Song GetRandomSong()
    {
        var songs = _songServices.GetSongs();

        if (songs.Count == 0)
            throw new InvalidOperationException("No songs available.");

        if (songs.Count == 1)
            return songs[0];

        Song song;

        do
        {
            song = songs[_random.Next(songs.Count)];
        }
        while (_currentSong != null &&
               song.Title == _currentSong.Title);

        _songServices.SetCurrentSong(song);

        return song;
    }


    // ------------------------------------------------
    // PREVIOUS SONG
    // ------------------------------------------------

    [RelayCommand]
    private async Task PreviousSong()
    {
        StopProgressTimer();

        _audioService.Stop();

        var song = _songServices.PreviousSong();

        LoadSong(song);

        await PlayCurrentSong();
    }


    // ------------------------------------------------
    // AUTO NEXT
    // ------------------------------------------------

    private async void OnPlaybackEnded()
    {
        await MainThread.InvokeOnMainThreadAsync(async () =>
        {
            if (RepeatMode == 2)
            {
                if (_currentSong == null)
                    return;

                await PlayCurrentSong();
                return;
            }

            await NextSong();
        });
    }


    // ------------------------------------------------
    // PROGRESS TIMER
    // ------------------------------------------------

    private void StartProgressTimer()
    {
        StopProgressTimer();

        _progressCancellation =
            new CancellationTokenSource();

        var token = _progressCancellation.Token;

        _ = UpdateProgressAsync(token);
    }


    private async Task UpdateProgressAsync(
        CancellationToken token)
    {
        try
        {
            while (!token.IsCancellationRequested)
            {
                if (_audioService.IsPlaying)
                {
                    var position =
                        _audioService.CurrentPosition;

                    var totalDuration =
                        _audioService.Duration;

                    await MainThread.InvokeOnMainThreadAsync(
                        () =>
                        {
                            Progress = position;

                            Duration = totalDuration;

                            CurrentTime =
                                FormatTime(position);

                            TotalTime =
                                FormatTime(totalDuration);
                        });
                }

                await Task.Delay(500, token);
            }
        }
        catch (TaskCanceledException)
        {
            // Timer intentionally stopped.
        }
    }


    private void StopProgressTimer()
    {
        if (_progressCancellation == null)
            return;

        _progressCancellation.Cancel();
        _progressCancellation.Dispose();

        _progressCancellation = null;
    }


    // ------------------------------------------------
    // SEEK
    // ------------------------------------------------

    [RelayCommand]
    private void Seek(double position)
    {
        if (Duration <= 0)
            return;

        _audioService.Seek(position);

        Progress = position;

        CurrentTime = FormatTime(position);
    }


    // ------------------------------------------------
    // FORMAT TIME
    // ------------------------------------------------

    private static string FormatTime(double seconds)
    {
        if (double.IsNaN(seconds) ||
            double.IsInfinity(seconds) ||
            seconds < 0)
        {
            return "0:00";
        }

        var time = TimeSpan.FromSeconds(seconds);

        return $"{(int)time.TotalMinutes}:{time.Seconds:00}";
    }


    // ------------------------------------------------
    // FAVORITES
    // ------------------------------------------------

    [RelayCommand]
    private void ToggleFavorite()
    {
        if (_currentSong == null)
            return;

        _favouritesServices.ToggleFavorite(
            _currentSong);

        FavoriteIcon =
            _favouritesServices.IsFavorite(
                _currentSong)
            ? "heart_fill.png"
            : "heart.png";
    }


    // ------------------------------------------------
    // BACK
    // ------------------------------------------------

    [RelayCommand]
    private async Task GoBack()
    {
        StopProgressTimer();

        _audioService.Stop();

        IsPlaying = false;

        await Shell.Current.GoToAsync("..");
    }


    // ------------------------------------------------
    // LYRICS
    // ------------------------------------------------

    [RelayCommand]
    private async Task Lyrics()
    {
        await Shell.Current.DisplayAlert(
            "Lyrics",
            "Lyrics screen coming soon.",
            "OK");
    }


    partial void OnIsPlayingChanged(bool value)
    {
        OnPropertyChanged(
            nameof(PlayPauseIcon));
    }
}