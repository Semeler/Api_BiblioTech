using System.ComponentModel.DataAnnotations.Schema;

namespace ApiLocadora.Models
{
    [Table("clientes")]
    public class Cliente
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required string CPF { get; set; }
        public required string Endereco { get; set; }
        public required string Telefone { get; set; }
        public required string Email { get; set; }
        public DateOnly? DataNascimento { get; set; }
    }
}
