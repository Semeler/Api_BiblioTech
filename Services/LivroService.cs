using ApiLocadora.Common.Exceptions;
using ApiLocadora.DataContexts;
using ApiLocadora.Dtos;
using ApiLocadora.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Windows.Markup;

namespace ApiLocadora.Services
{
    public class LivroService
    {
        private readonly AppDbContext _context;

        private readonly IMapper _mapper;

        public LivroService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
                var newLivro = _mapper.Map<Livro>(livro);

                await _context.Livros.AddAsync(newLivro);
                await _context.SaveChangesAsync();

                return newLivro;
            }   
            catch (Exception ex)
            { 
                throw ex;
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
                _livro.Isbn = livro.Isbn;
                _livro.Editora = livro.Editora;
                _livro.Sinopse = livro.Sinopse;
                
                _context.Livros.Update(_livro);
                await _context.SaveChangesAsync();

                return _livro;
            }
            catch (Exception ex)
            {
                    throw ex;
            }
            
        }

        //public async Task<Livro?> Delete(int id)
        //{
        //    return null;
        //}

        public async Task<Livro?> Delete(int id)
        {
            try
            {
                var livro = await _context.Livros
                    .SingleOrDefaultAsync(x => x.Id == id);

                if (livro is null)
                {
                    return null;
                }
                

                _context.Livros.Remove(livro);
                await _context.SaveChangesAsync();

                return livro;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
        private async Task<bool> Exist(int id)
        {
            return await _context.Livros.AnyAsync(c => c.Id == id);
        }
    }
}
