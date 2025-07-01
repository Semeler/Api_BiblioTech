using System.ComponentModel.DataAnnotations;
using ApiLocadora.Common.Validations;

namespace ApiLocadora.Dtos
{
    public class LivroDto
    {
        [Required]
        [MinLength(1)]
        public required string Titulo { get; set; }
        [Required]
        public required string Editora { get; set; }
        [Required]
        public required string Sinopse { get; set; }
        [Required]
        public required string Isbn { get; set; }
        [Required]
        public required string Autor { get; set; }
        [Required]
        public required DateOnly AnoPublicacao { get; set; }
        
        [Required]
        [CheckExist]
        public int GeneroId { get; set; }
    }
}
