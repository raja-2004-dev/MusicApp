using MusicApp.Models;

namespace MusicApp.Services;

public class SongServices
{
    public List<Song> Songs { get; }

    public int CurrentIndex { get; private set; }

    public Song CurrentSong => Songs[CurrentIndex];

    public SongServices()
    {
        Songs = new List<Song>
        {
            new Song
            {
                Title = "Believer",
                Artist = "Imagine Dragons",
                Image = "album1.png",
                Duration = "3:28",
                AudioFile = "believer.mp3"
            },

            new Song
            {
                Title = "Perfect",
                Artist = "Ed Sheeran",
                Image = "album2.png",
                Duration = "4:10",
                AudioFile = "perfect.mp3"
            },

            new Song
            {
                Title = "Heat Waves",
                Artist = "Glass Animals",
                Image = "album3.png",
                Duration = "3:35",
                AudioFile = "heatwave.mp3"
            },

            new Song
            {
                Title = "Thunder",
                Artist = "Imagine Dragons",
                Image = "album4.png",
                Duration = "3:10",
                AudioFile = "thunder.mp3"
            }
        };

        CurrentIndex = 0;
    }

    public List<Song> GetSongs()
    {
        return Songs;
    }

    public void SetCurrentSong(Song song)
    {
        int index = Songs.FindIndex(x => x.Title == song.Title);

        if (index >= 0)
        {
            CurrentIndex = index;
        }
    }

    public Song NextSong()
    {
        CurrentIndex++;

        if (CurrentIndex >= Songs.Count)
        {
            CurrentIndex = 0;
        }

        return Songs[CurrentIndex];
    }

    public Song PreviousSong()
    {
        CurrentIndex--;

        if (CurrentIndex < 0)
        {
            CurrentIndex = Songs.Count - 1;
        }

        return Songs[CurrentIndex];
    }
}