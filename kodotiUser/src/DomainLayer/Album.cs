using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer
{
    public class Album
    {
        public int AlbumId { get; set; }
        public Artist Artist { get; set; }
        public int ArtistId { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
