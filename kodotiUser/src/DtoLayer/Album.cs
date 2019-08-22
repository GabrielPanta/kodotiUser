using System;
using System.Collections.Generic;
using System.Text;

namespace DtoLayer
{
    public class AlbumDto
    {
        public int AlbumId { get; set; }
        public ArtistDto Artist { get; set; }
        public int ArtistId { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
