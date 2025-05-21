using System.ComponentModel.DataAnnotations;

namespace ApiLocadora.Dtos
{
    public class DevolucaoDto
    {
        [Required]
        public required int Multa { get; set; }
        [Required]
        public required DateTime DataDevolucao { get; set; }
    }
}
