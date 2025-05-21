using System.ComponentModel.DataAnnotations.Schema;

namespace ApiLocadora.Models
{
    [Table("fornecedores")]
    public class Fornecedor
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required string CNPJ { get; set; }
        public required string Endereco { get; set; }
        public required string Telefone { get; set; }
        public required string Email { get; set; }
       
    }
}
