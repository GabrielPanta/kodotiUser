using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer
{
    public class Song
    {
        public int SongId { get; set; }
        public Album Album { get; set; }
        public int AlbumId { get; set; }
        public string Name { get; set; }
        public TimeSpan DurationTime { get; set; }
    }
}
