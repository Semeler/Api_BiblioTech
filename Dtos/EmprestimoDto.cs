using System.ComponentModel.DataAnnotations;


namespace ApiLocadora.Dtos
{
    public class EmprestimoDto
    {
        
        public DateTime DataInicio { get; set; }
        public DateTime DataPrevista { get; set; }
        public DateTime DataDevolucao { get; set; }
        public bool Status { get; set; }
        
        public int? ClienteId { get; set; }
        
        public int? FuncionarioId { get; set; }
        
        public List<int> LivrosIds { get; set; } = [];

        
    }
}
