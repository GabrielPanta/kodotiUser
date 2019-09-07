using System;
using System.Collections.Generic;
using System.Text;

namespace DtoLayer
{
    public abstract class SongBaseDto
    {
        public int SongId { get; set; }
        public int AlbumId { get; set; }
        public string Name { get; set; }
        public TimeSpan DurationTime { get; set; }
    }
    public class SongDto:SongBaseDto
    {
        public AlbumDto Album { get; set; }
    }
    public class SongCreateDto:SongBaseDto
    {
        public new int DurationTime { get; set; }
    }
}
