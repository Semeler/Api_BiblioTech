using System.ComponentModel.DataAnnotations;

namespace ApiLocadora.Dtos
{
    public class ClienteDto
    {
        [Required]
        [MinLength(5)]
        public required string Nome { get; set; }
        [Required]
        public required string CPF { get; set; }
        [Required]
        public required string Endereco { get; set; }
        [Required]
        public required string Telefone { get; set; }
        [Required]
        public required string Email { get; set; }
        [Required]
        public required DateTime DataNascimento { get; set; }
    }
}
