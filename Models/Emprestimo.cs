using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiLocadora.Models
{
    [Table("emprestimo")]
    public class Emprestimo
    {
        public int Id { get; set; }
        
        public DateOnly? DataInicio { get; set; }
        
        public DateOnly? DataPrevista { get; set; }
        
        public bool Status { get; set; }    

        [JsonIgnore]
        public int? ClienteId { get; set; }
        [JsonIgnore]
        public int? FuncionarioId { get; set; }

        public virtual Cliente? Cliente { get; set; }
        
        
        public virtual Funcionario? Funcionario { get; set; }
        
        public List<Livro> Livros { get; set; } = [];
    }
}