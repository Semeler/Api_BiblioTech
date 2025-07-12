using ApiLocadora.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiLocadora.DataContexts
{
    public class AppDbContext : Microsoft.EntityFrameworkCore.DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        

        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<Emprestimo> Emprestimos { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Devolucao> Devolucoes { get; set; }
        public DbSet<Estoque> Estoques { get; set; }
        public DbSet<Livro> Livros { get; set; }
        public DbSet<Genero> Generos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            // Configuração Livro e Gênero
            modelBuilder.Entity<Livro>()
                .HasOne(l => l.Genero)
                .WithMany(g => g.Livros)
                .HasForeignKey(l => l.GeneroId);

            // Configuração Livro e Estoque (one-to-one)
            modelBuilder.Entity<Estoque>()
                .HasOne(e => e.Livro)
                .WithMany(l => l.Estoques)
                .HasForeignKey(e => e.LivroId);


            // Configuração Empréstimo e seus relacionamentos
            modelBuilder.Entity<Emprestimo>()
                .HasOne(e => e.Cliente)
                .WithMany(c => c.Emprestimos)
                .HasForeignKey(e => e.ClienteId)
                .IsRequired(false);

            modelBuilder.Entity<Emprestimo>()
                .HasOne(e => e.Funcionario)
                .WithMany(f => f.Emprestimos)
                .HasForeignKey(e => e.FuncionarioId)
                .IsRequired(false);

            // Relacionamento many-to-many entre Livro e Empréstimo
            modelBuilder.Entity<Livro>()
                .HasMany(l => l.Emprestimos)
                .WithMany(e => e.Livros)
                .UsingEntity(
                    "EmprestimoLivro",
                    l => l.HasOne(typeof(Emprestimo)).WithMany().HasForeignKey("EmprestimoId"),
                    r => r.HasOne(typeof(Livro)).WithMany().HasForeignKey("LivroId")
                );

            // Relacionamento many-to-many entre Livro e Fornecedor
            modelBuilder.Entity<Livro>()
                .HasMany(l => l.Fornecedores)
                .WithMany(f => f.Livros)
                .UsingEntity(
                    "FornecedorLivro",
                    l => l.HasOne(typeof(Fornecedor)).WithMany().HasForeignKey("FornecedorId"),
                    r => r.HasOne(typeof(Livro)).WithMany().HasForeignKey("LivroId")
                );


            // Configuração Devolução
            modelBuilder.Entity<Devolucao>()
                .HasOne(d => d.Cliente)
                .WithMany(c => c.Devolucoes)
                .HasForeignKey(d => d.ClienteId)
                .IsRequired(false);

            modelBuilder.Entity<Devolucao>()
                .HasOne(d => d.Funcionario)
                .WithMany(f => f.Devolucoes)
                .HasForeignKey(d => d.FuncionarioId)
                .IsRequired(false);

            // Relacionamento many-to-many entre Livro e Devolução
            modelBuilder.Entity<Livro>()
                .HasMany(l => l.Devolucoes)
                .WithMany(d => d.Livros)
                .UsingEntity(
                    "DevolucaoLivros",
                    l => l.HasOne(typeof(Devolucao)).WithMany().HasForeignKey("DevolucaoId"),
                    r => r.HasOne(typeof(Livro)).WithMany().HasForeignKey("LivroId")
                );

            // Relacionamento many-to-many entre Cliente e Livro (Favoritos)
            modelBuilder.Entity<Livro>()
                .HasMany(l => l.Clientes)
                .WithMany(c => c.Livros)
                .UsingEntity(
                    "ClienteLivroFavorito",
                    l => l.HasOne(typeof(Cliente)).WithMany().HasForeignKey("ClienteId"),
                    r => r.HasOne(typeof(Livro)).WithMany().HasForeignKey("LivroId")
                );
        

                    
            
            
            //modelBuilder.Entity<Livro>()
            //    .HasMany(e => e.Fornecedores)
            //    .WithMany(e => e.Livros)
            //    .UsingEntity(
            //        "FornecedorLivro",
            //        r => r.HasOne(typeof(Fornecedor)).WithMany().HasForeignKey("GeneroId").HasPrincipalKey(nameof(Fornecedor.Id)),
            //        l => l.HasOne(typeof(Livro)).WithMany().HasForeignKey("LivroId").HasPrincipalKey(nameof(Livro.Id)),
            //        j => j.HasKey("LivroId", "GeneroId"));
            
            
            

        }
    }
}
