using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiLocadora.Models
{
    [Table("estudios")]
    public class Estudio
    {
        public int Id { get; set; }

        public required string Nome { get; set; }

        [JsonIgnore]
        public ICollection<Filme>? Filmes { get; set; }
    }
}
