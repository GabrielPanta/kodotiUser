using System;
using System.Collections.Generic;
using System.Text;

namespace DtoLayer
{
    public class SongDto
    {
        public int SongId { get; set; }
        public AlbumDto Album { get; set; }
        public int AlbumId { get; set; }
        public string Name { get; set; }
        public TimeSpan DurationTime { get; set; }
    }
}
