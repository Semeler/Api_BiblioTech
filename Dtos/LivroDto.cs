using System.ComponentModel.DataAnnotations;

namespace ApiLocadora.Dtos
{
    public class LivroDto
    {
        [Required]
        [MinLength(5)]
        public required string Titulo { get; set; }
        [Required]
        public required string Editora { get; set; }
        [Required]
        public required string Sinopse { get; set; }
        [Required]
        public required string ISBN { get; set; }
        [Required]
        public required string Autor { get; set; }
        [Required]
        public required DateTime AnoPublicacao { get; set; }
    }
}
