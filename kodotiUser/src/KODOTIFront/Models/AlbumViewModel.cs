using DtoLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KODOTIFront.Models
{
    public class AlbumViewModel
    {
        public int ArtistId { get; set; }
        public string ArtistName { get; set; }
        public AlbumCreateDto AlbumCreate { get; set; }
        public IEnumerable<AlbumDto> Albums { get; set; }
    }
}
