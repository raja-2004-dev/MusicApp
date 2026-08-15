using MusicApp.Models;
using System.Collections.ObjectModel;

namespace MusicApp.Services;

public class RecentSongsService
{
    public ObservableCollection<Song> RecentlyPlayed { get; } = new();

    public void AddRecentlyPlayed(Song song)
    {
        if (song == null)
            return;

        var existingSong = RecentlyPlayed
            .FirstOrDefault(x => x.Title == song.Title);

        if (existingSong != null)
        {
            RecentlyPlayed.Remove(existingSong);
        }

        RecentlyPlayed.Insert(0, song);

        while (RecentlyPlayed.Count > 10)
        {
            RecentlyPlayed.RemoveAt(RecentlyPlayed.Count - 1);
        }
    }
}