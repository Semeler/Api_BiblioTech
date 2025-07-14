using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiLocadora.Models
{
    [Table("Livro")]
    public class Livro
    {
        public int Id { get; set; }
        public required string Titulo { get; set; }

        public required string Autor { get; set; }
        public required string Isbn { get; set; }
        public required string Editora { get; set; }
        public required string Sinopse { get; set; }
        public DateOnly? AnoPublicacao { get; set; }
        
        public int? GeneroId { get; set; }
        
        [JsonIgnore]
        public Genero? Genero { get; set; }
        
        [JsonIgnore]
        public List<Fornecedor> Fornecedores { get; set; } = [];
        
        [JsonIgnore]
        public List<Emprestimo> Emprestimos { get; set; } = [];
        [JsonIgnore]
        public List<Cliente> Clientes { get; set; } = []; 
        [JsonIgnore]
        public List<Estoque> Estoques { get; set; } = [];
        
        

        
       
        
        
        
    }
}