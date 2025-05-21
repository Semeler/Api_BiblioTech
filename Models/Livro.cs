using System.ComponentModel.DataAnnotations.Schema;

namespace ApiLocadora.Models
{
    [Table("livros")]
    public class Livro
    {
        public int Id { get; set; }
        public required string Titulo { get; set; }
        public required string Autor { get; set; }
        public required string ISBN { get; set; }
        public required string Editora { get; set; }
        public required string Sinopse { get; set; }
        public DateOnly? AnoPublicacao { get; set; }
    }
}
