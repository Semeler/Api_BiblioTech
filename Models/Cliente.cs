using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiLocadora.Models
{
    [Table("clientes")]
    public class Cliente
    {
        public int Id { get; set; }

        public required string Nome { get; set; }
        public required string Cpf { get; set; }
        public required string Telefone { get; set; }
        public required string Email { get; set; }
        
        public required string Cep { get; set; }
        
        public required string Rua { get; set; }
        
        public required string Bairro { get; set; }
        
        public required string Numero { get; set; }
        
        public required string Estado { get; set; }
        
        public required string Cidade { get; set; }
        
        public DateOnly? DataNascimento { get; set; }
        
    }
}