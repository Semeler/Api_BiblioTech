using System.ComponentModel.DataAnnotations;

namespace ApiLocadora.Dtos
{
    public class FuncionarioDto
    {
        [Required]
        [MinLength(5)]
        public required string Nome { get; set; }
        [Required]
        public required string CPF { get; set; }
        [Required]
        public required string Cargo { get; set; }
        [Required]
        public required string Telefone { get; set; }
        [Required]
        public required string Email { get; set; }
        [Required]
        public required DateTime DataAdmissao { get; set; }
    }
}
