using ApiLocadora.DataContexts;
using ApiLocadora.Dtos;
using ApiLocadora.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiLocadora.Services
{
    public class LivroService
    {
        private readonly AppDbContext _context;

        public LivroService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Livro>> GetAll()
        {
            var list = await _context.Livros.ToListAsync();

            return list;
        }

        public async Task<Livro?> GetOneById(int id)
        {
            try
            {
                return await _context.Livros
                    .SingleOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Livro?> Create(LivroDto livro)
        {
            try
            {
                var data = livro.AnoPublicacao;

                var newLivro = new Livro
                {
                    Titulo = livro.Titulo,
                    Autor = livro.Autor,
                    ISBN = livro.ISBN,
                    Sinopse = livro.Sinopse,
                    Editora = livro.Editora,
                    AnoPublicacao = new DateOnly(data.Year, data.Month, data.Day)
                };

                await _context.Livros.AddAsync(newLivro);
                await _context.SaveChangesAsync();

                return newLivro;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Livro?> Update(int id, LivroDto livro)
        {
            try
            {
                var _livro = await GetOneById(id);

                if (_livro is null)
                {
                    return _livro;
                }

                _livro.Titulo = livro.Titulo;
                _livro.Autor = livro.Autor;
                _livro.ISBN = livro.ISBN;
                _livro.Editora = livro.Editora;
                _livro.Sinopse = livro.Editora;

                _context.Livros.Update(_livro);
                await _context.SaveChangesAsync();

                return _livro;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<Livro?> Delete(int id)
        {
            return null;
        }

        private async Task<bool> Exist(int id)
        {
            return await _context.Livros.AnyAsync(c => c.Id == id);
        }
    }
}
