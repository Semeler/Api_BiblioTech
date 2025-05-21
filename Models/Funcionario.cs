using System.ComponentModel.DataAnnotations.Schema;

namespace ApiLocadora.Models
{
    [Table("funcionarios")]
    public class Funcionario
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required string CPF { get; set; }
        public required string Cargo { get; set; }
        public required string Telefone { get; set; }
        public required string Email { get; set; }
        public DateOnly? DataAdmissao { get; set; }
    }
}
