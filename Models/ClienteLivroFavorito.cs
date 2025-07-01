using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiLocadora.Models
{
    [Table("ClienteLivrosFavoritos")]
    
    public class ClienteLivroFavorito
    {
        
        [JsonIgnore]
        public int? ClienteId { get; set; }

        public virtual Cliente? Cliente { get; set; }
        
        [JsonIgnore]
        public int? LivroId { get; set; }

        public virtual Livro? Livro { get; set; }
    }
}