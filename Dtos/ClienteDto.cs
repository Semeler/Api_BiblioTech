using System.ComponentModel.DataAnnotations;



namespace ApiLocadora.Dtos
{
    public class ClienteDto
    {
        [Required]
        [MinLength(5)]
        public required string Nome { get; set; }
        [Required]
        public required string Cpf { get; set; }
        [Required]
        public required string Telefone { get; set; }
        [Required]
        public required string Email { get; set; }
        
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
        
        
        public required DateTime DataNascimento { get; set; }
    }
}
