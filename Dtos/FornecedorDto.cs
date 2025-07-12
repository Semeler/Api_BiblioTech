using System.ComponentModel.DataAnnotations;
using ApiLocadora.Common.Validations;

namespace ApiLocadora.Dtos
{
    public class FornecedorDto
    {
        [Required]
        [MinLength(5)]
        public required string Nome { get; set; }
        [Required]
        public required string Cnpj { get; set; }
        [Required]
        public required string Telefone { get; set; }
        [Required]
        public required string Email { get; set; }
        [Required]
        public required string Cep { get; set; }
        [Required]
        public required string Rua { get; set; }
        [Required]
        public required string Bairro { get; set; }
        [Required]
        public required string Numero { get; set; }
        [Required]
        public required string Estado { get; set; }
        [Required]
        public required string Cidade { get; set; }
        
        
        
    }
}
