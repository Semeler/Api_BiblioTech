using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiLocadora.Models
{
    [Table("Livros")]
    public class Livro
    {
        public int Id { get; set; }
        public required string Titulo { get; set; }

        public required string Autor { get; set; }
        public required string Isbn { get; set; }
        public required string Editora { get; set; }
        public required string Sinopse { get; set; }
        public DateOnly? AnoPublicacao { get; set; }

        [JsonIgnore]
        public int? GeneroId { get; set; }

        public virtual Genero? Genero { get; set; }
    }
}