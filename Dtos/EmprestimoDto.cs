using System.ComponentModel.DataAnnotations;
using ApiLocadora.Common.Validations;

namespace ApiLocadora.Dtos
{
    public class EmprestimoDto
    {
        
        [Required]
        public required bool Status { get; set; }

        [Required]
        public required DateOnly DataInicio { get; set; }
        [Required]
        public required DateOnly DataPrevista { get; set; }
        
        [Required]
        [CheckExist]
        public int ClienteId { get; set; }
        
        [Required]
        [CheckExist]
        public int FuncionarioId { get; set; }
    }
}
