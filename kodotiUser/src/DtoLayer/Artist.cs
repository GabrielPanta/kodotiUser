using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DtoLayer
{
    public abstract class ArtistBaseDto
    {
        public int ArtistId { get; set; }
        [Required(ErrorMessage ="Campo requerido"),MinLength(5,ErrorMessage ="Mínimo 5 caracteres")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Campo requerido"),MaxLength(500,ErrorMessage ="Máximo 500 caracteres")]
        public string Description { get; set; }
    }
    public class ArtistDto:ArtistBaseDto
    {    
        public string LogoUrl { get; set; }
    }
    public class ArtistCreateDto:ArtistBaseDto
    {
    }
    public class ArtistUpdateDto : ArtistBaseDto
    {

    }
}
