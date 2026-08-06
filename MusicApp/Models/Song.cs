using System;
using System.Collections.Generic;
using System.Text;

namespace MusicApp.Models
{
    public class Song
    {
        public string Title { get; set; }

        public string Artist { get; set; }

        public ImageSource Image { get; set; }

        public string Duration { get; set; }
        public bool IsFavourite { get; set; }
        public string AudioFile { get; set; }
    }

}
