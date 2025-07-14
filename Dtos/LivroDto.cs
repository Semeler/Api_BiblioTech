using System.ComponentModel.DataAnnotations;

using ApiLocadora.Models;

namespace ApiLocadora.Dtos
{
    public class LivroDto
    {
        public required string Titulo { get; set; }
        public required string Autor { get; set; }
        public required string Isbn { get; set; }
        public required string Editora { get; set; }
        public required string Sinopse { get; set; }
        public DateTime AnoPublicacao { get; set; }
        public int? GeneroId { get; set; }
        
        

        
        
        
        
        
    }
}
