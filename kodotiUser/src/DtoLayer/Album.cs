using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DtoLayer
{
    public abstract class AlbumBaseDto
    {
        public int AlbumId { get; set; }
        public int ArtistId { get; set; }
        [Required(ErrorMessage = "Campo requerido"), MinLength(3, ErrorMessage = "Mínimo 3 caracteres")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Campo requerido")]
        public DateTime ReleaseDate { get; set; }
    }

    public class AlbumDto : AlbumBaseDto
    {
        public ArtistDto Artist { get; set; }
        public IEnumerable<SongDto> Songs { get; set; }
    }

    public class AlbumCreateDto : AlbumBaseDto
    {

    }
}
