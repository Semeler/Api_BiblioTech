using ApiLocadora.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiLocadora.DataContexts
{
    public class AppDbContext : Microsoft.EntityFrameworkCore.DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Filme> Filmes { get; set; }

        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<Emprestimo> Emprestimos { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Devolucao> Devolucaos { get; set; }
        public DbSet<Estoque> Estoques { get; set; }
        public DbSet<Livro> Livros { get; set; }

        public DbSet<Genero> Generos { get; set; }

    }
}
