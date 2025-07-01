using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiLocadora.Models
{
    [Table("EmprestimosLivros")]
    
    public class EmprestimoLivro
    {
        public int Id { get; set; }
        

        [JsonIgnore]
        public int? EstudioId { get; set; }

        public virtual Estudio? Estudio { get; set; }
    }
}