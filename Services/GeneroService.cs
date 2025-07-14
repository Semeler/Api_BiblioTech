
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
    public class GeneroService
    {
        private readonly AppDbContext _context;

        private readonly IMapper _mapper;

        public GeneroService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ICollection<Genero>> GetAll()
        {
            var list = await _context.Generos.Include(e => e.Livros).ToListAsync();

            return list;
        }

        public async Task<Genero?> GetOneById(int id)
        {
            try
            {
                return await _context.Generos.Include(e => e.Livros)
                    .SingleOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Genero?> Create(GeneroDto genero)
        {
            try
            {
                var newGenero = _mapper.Map<Genero>(genero);

                await _context.Generos.AddAsync(newGenero);
                await _context.SaveChangesAsync();

                return newGenero;
            }   
            catch (Exception ex)
            { 
                throw ex;
            }
        }
        
        public async Task<Genero?> Update(int id, GeneroDto genero)
        {
            try
            {
                var _genero = await GetOneById(id);

                if (_genero is null)
                {
                    return _genero;
                }

                _genero.Nome = genero.Nome;
                _genero.Descricao = genero.Descricao;;
                
                _context.Generos.Update(_genero);
                await _context.SaveChangesAsync();

                return _genero;
            }
            catch (Exception ex)
            {
                    throw ex;
            }
            
        }

        public async Task<Genero?> Delete(int id)
        {
            try
            {
                var genero = await _context.Generos
                    .SingleOrDefaultAsync(x => x.Id == id);

                if (genero is null)
                {
                    return null;
                }
                

                _context.Generos.Remove(genero);
                await _context.SaveChangesAsync();

                return genero;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        private async Task<bool> Exist(int id)
        {
            return await _context.Generos.AnyAsync(c => c.Id == id);
        }
    }
}
