using MusicApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MusicApp.Services
{
    public  class FavouritesServices
    {
        public ObservableCollection<Song> FavoriteSongs { get; } = new();

        public void AddFavorite(Song song)
        {
            if (song == null)
                return;

            if (!FavoriteSongs.Any(x => x.Title == song.Title))
            {
                FavoriteSongs.Add(song);
            }
        }

        public void RemoveFavorite(Song song)
        {
            if (song == null)
                return;

            var favorite = FavoriteSongs.FirstOrDefault(x => x.Title == song.Title);

            if (favorite != null)
            {
                FavoriteSongs.Remove(favorite);
            }
        }

        public bool IsFavorite(Song song)
        {
            if (song == null)
                return false;

            return FavoriteSongs.Any(x => x.Title == song.Title);
        }

        public void ToggleFavorite(Song song)
        {
            if (IsFavorite(song))
                RemoveFavorite(song);
            else
                AddFavorite(song);
        }
    }
}
