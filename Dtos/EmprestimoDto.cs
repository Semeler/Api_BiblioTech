using System.ComponentModel.DataAnnotations;
using ApiLocadora.Common.Validations;

namespace ApiLocadora.Dtos
{
    public class EmprestimoDto
    {
        
        [Required]
        public required bool Status { get; set; }

        [Required]
        public required DateTime DataInicio { get; set; }
        [Required]
        public required DateTime DataPrevista { get; set; }
        
        [Required]
        [CheckExist]
        public int ClienteId { get; set; }
        
        [Required]
        [CheckExist]
        public int FuncionarioId { get; set; }
    }
}
