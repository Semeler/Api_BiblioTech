using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiLocadora.Models
{
    [Table("genero")]
    public class Genero
    {
        public int Id { get; set; }

        public required string Nome { get; set; }

        public required string Descricao { get; set; }
        
        public List<Livro> Livros { get; set; } = [];
        
    }
}