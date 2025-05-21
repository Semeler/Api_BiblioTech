using System.ComponentModel.DataAnnotations.Schema;

namespace ApiLocadora.Models
{
    [Table("devolucoes")]
    public class Devolucao
    {
        public int Id { get; set; }
        public required int Multa { get; set; }
        public DateOnly? DataDevolucao { get; set; }
    }
}
