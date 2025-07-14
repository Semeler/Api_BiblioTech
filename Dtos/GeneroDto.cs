using System.ComponentModel.DataAnnotations;


namespace ApiLocadora.Dtos
{
    public class GeneroDto
    {
        [Required]
        [MinLength(1)]
        public required string Nome { get; set; }
        [MinLength(5)]
        public required string Descricao { get; set; }
        
    }
}