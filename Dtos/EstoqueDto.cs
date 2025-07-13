using System.ComponentModel.DataAnnotations;
using ApiLocadora.Common.Validations;

namespace ApiLocadora.Dtos
{
    public class EstoqueDto
    {
        [Required]
        public required int Quantidade { get; set; }
        
        [Required]
        public required string CodigoDeBarras { get; set; }
        
        [Required]
        
        public int LivroId { get; set; }
     
    }
}
