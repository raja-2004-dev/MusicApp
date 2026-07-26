using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Music2._0.ViewModels;

public partial class PlayerPageViewModel : ObservableObject
{
    [ObservableProperty]
    private string title = "Believer";

    [ObservableProperty]
    private string artist = "Imagine Dragons";

    [ObservableProperty]
    private string image = "album1.png";

    [ObservableProperty]
    private string currentTime = "1:20";

    [ObservableProperty]
    private string totalTime = "3:28";

    [ObservableProperty]
    private double progress = 30;

    [ObservableProperty]
    private bool isPlaying;
    [ObservableProperty]
    private bool isFavorite;

    public string FavoriteIcon => IsFavorite ? "heart_fill.png" : "heart.png";

    partial void OnIsFavoriteChanged(bool value)
    {
        OnPropertyChanged(nameof(FavoriteIcon));
    }

    [RelayCommand]
    private void ToggleFavorite()
    {
        IsFavorite = !IsFavorite;
    }

    // This property changes automatically
    public string PlayPauseIcon => IsPlaying ? "pause.png" : "play.png";

    partial void OnIsPlayingChanged(bool value)
    {
        OnPropertyChanged(nameof(PlayPauseIcon));
    }

    [RelayCommand]
    private void PlayPause()
    {
        IsPlaying = !IsPlaying;
    }

    [RelayCommand]
    private void NextSong()
    {
        // TODO: Next song
    }

    [RelayCommand]
    private void PreviousSong()
    {
        // TODO: Previous song
    }

    [RelayCommand]
    private async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }
}