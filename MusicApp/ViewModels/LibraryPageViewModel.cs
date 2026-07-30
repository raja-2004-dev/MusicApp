using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MusicApp.Models;
using MusicApp.Services;
using System.Collections.ObjectModel;

namespace MusicApp.ViewModels;

public partial class LibraryPageViewModel : ObservableObject
{
    private readonly FavouritesServices _favouritesServices;

    public ObservableCollection<Song> FavoriteSongs { get; }

    public int FavoriteCount => FavoriteSongs.Count;

    public LibraryPageViewModel(FavouritesServices favouritesServices)
    {
        _favouritesServices = favouritesServices;

        FavoriteSongs = _favouritesServices.FavoriteSongs;

        FavoriteSongs.CollectionChanged += (_, __) =>
        {
            OnPropertyChanged(nameof(FavoriteCount));
        };
    }

    [RelayCommand]
    private async Task Back()
    {
        await Shell.Current.GoToAsync("..");
    }
}