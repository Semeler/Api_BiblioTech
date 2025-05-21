using System.ComponentModel.DataAnnotations;

namespace ApiLocadora.Dtos
{
    public class EstoqueDto
    {
        [Required]
        public required int Quantidade { get; set; }
     
    }
}
