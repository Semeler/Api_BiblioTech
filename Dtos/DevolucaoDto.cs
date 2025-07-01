using System.ComponentModel.DataAnnotations;
using ApiLocadora.Common.Validations;

namespace ApiLocadora.Dtos
{
    public class DevolucaoDto
    {
        [Required]
        public required float Multa { get; set; }
        [Required]
        public required DateOnly DataDevolucao { get; set; }
    }
}
