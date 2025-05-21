using System.ComponentModel.DataAnnotations;

namespace ApiLocadora.Dtos
{
    public class FornecedorDto
    {
        [Required]
        [MinLength(5)]
        public required string Nome { get; set; }
        [Required]
        public required string CNPJ { get; set; }
        [Required]
        public required string Endereco { get; set; }
        [Required]
        public required string Telefone { get; set; }
        [Required]
        public required string Email { get; set; }
        
    }
}
