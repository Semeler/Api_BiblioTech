using System.ComponentModel.DataAnnotations.Schema;

namespace ApiLocadora.Models
{
    [Table("filmes")]
    public class Filme
    {
        public int Id { get; set; }

        public required string Nome { get; set; }

        public required string Genero { get; set; }

        public DateOnly? AnoLancamento { get; set; }
    }
}
