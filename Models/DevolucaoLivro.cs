using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiLocadora.Models
{
    [Table("devolucaoslivros")]
    
    public class DevolucaoLivros
    {
        public int Id { get; set; }
        
        [JsonIgnore]
        public int? LivroId { get; set; }
        [JsonIgnore]
        public int? DevolucaoId { get; set; }

        public virtual Livro? Livro { get; set; }
        
        public virtual Devolucao? Devolucao { get; set; }
    }
}