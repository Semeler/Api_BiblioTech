using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiLocadora.Models
{
    [Table("FornecedoresLivros")]
    
    public class FornecedorLivro
    {
        public int Id { get; set; }
        
        [JsonIgnore]
        public int? LivroId { get; set; }

        public virtual Livro? Livro { get; set; } 
        
        [JsonIgnore]
        public int? FornecedorId { get; set; }

        public virtual Fornecedor? Fornecedor { get; set; }
    }
}