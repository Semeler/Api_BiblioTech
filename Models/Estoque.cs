using System.ComponentModel.DataAnnotations.Schema;

namespace ApiLocadora.Models
{
    [Table("estoques")]
    public class Estoque
    {
        public int Id { get; set; }
        public required int Quantidade { get; set; }
        
    }
}
