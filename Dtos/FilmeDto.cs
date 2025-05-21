using System.ComponentModel.DataAnnotations;

namespace ApiLocadora.Dtos
{
    public class FilmeDto
    {
        [Required]
        [MinLength(5)]
        public required string Nome { get; set; }

        [Required]
        public required string Genero { get; set; }
        
        [Required]
        public required DateTime AnoLancamento { get; set; }
    }
}
