using System.ComponentModel.DataAnnotations;

namespace ApiLocadora.Dtos
{
    public class EmprestimoDto
    {
        
        [Required]
        public required string Status { get; set; }

        [Required]
        public required DateTime DataInicio { get; set; }
        [Required]
        public required DateTime DataFim { get; set; }
    }
}
