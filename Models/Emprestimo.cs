using System.ComponentModel.DataAnnotations.Schema;

namespace ApiLocadora.Models
{
    [Table("emprestimos")]
    public class Emprestimo
    {
        public int Id { get; set; }
        public DateOnly? DataInicio { get; set; }
        public DateOnly? DataFim { get; set; }
        public required string Status { get; set; }
        
    }
}
