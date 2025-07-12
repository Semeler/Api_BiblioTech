using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiLocadora.Models
{
    [Table("Devolucao")]
    public class Devolucao
    {
        public int Id { get; set; }

        public required float Multa { get; set; }
        public DateOnly? DataDevolucao { get; set; }

        [JsonIgnore]
        public int? FuncionarioId { get; set; }
        [JsonIgnore]
        public int? ClienteId { get; set; }
        
        

        public virtual Funcionario? Funcionario { get; set; }
        
        public virtual Cliente? Cliente { get; set; }
        
        public List<Livro> Livros { get; set; } = [];
        
        
    }
}