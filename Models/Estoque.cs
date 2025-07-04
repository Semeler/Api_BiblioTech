using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiLocadora.Models
{
    [Table("estoques")]
    public class Estoque
    {
        public int Id { get; set; }
        public int Quantidade { get; set; }

        public required string CodigoDeBarras { get; set; }

        [JsonIgnore]
        public int? LivroId { get; set; }

        public virtual Livro? Livro { get; set; }
    }
}