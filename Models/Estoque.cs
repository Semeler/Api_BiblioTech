using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiLocadora.Models
{
    [Table("estoque")]
    public class Estoque
    {
        public int Id { get; set; }
        public int Quantidade { get; set; }

        public required string CodigoDeBarras { get; set; }
        
        public int? LivroId { get; set; }
        [JsonIgnore]
        public Livro? Livro { get; set; }
    }
}