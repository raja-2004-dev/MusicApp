using System;
using System.Collections.Generic;
using System.Text;

namespace MusicApp.Models
{
    public  class SongServices
    {
        public List<Song> GetSongs()
        {
            return new List<Song>
        {
            new Song
            {
                Title = "Believer",
                Artist = "Imagine Dragons",
                Image = "album1.png",
                Duration = "3:28"
            },

            new Song
            {
                Title = "Perfect",
                Artist = "Ed Sheeran",
                Image = "album2.png",
                Duration = "4:10"
            },

            new Song
            {
                Title = "Heat Waves",
                Artist = "Glass Animals",
                Image = "album3.png",
                Duration = "3:35"
            },

            new Song
            {
                Title = "Thunder",
                Artist = "Imagine Dragons",
                Image = "album4.png",
                Duration = "3:10"
            }
        };
        }
    }
}
