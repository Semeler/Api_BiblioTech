
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

        public async Task<ICollection<object>> GetAll()
    {
        var list = await _context.Generos
            .Include(e => e.Livros)
            .Select(g => new
            {
                g.Id,
                g.Nome,
                g.Descricao,
                Livros = g.Livros.Select(l => new
                {
                    l.Id,
                    l.Titulo
                }).ToList()
            })
            .ToListAsync();

        return list.Cast<object>().ToList();
    }

    public async Task<object?> GetOneById(int id)
    {
        return await _context.Generos
            .Include(e => e.Livros)
            .Where(x => x.Id == id)
            .Select(g => new
            {
                g.Id,
                g.Nome,
                g.Descricao,
                Livros = g.Livros.Select(l => new
                {
                    l.Id,
                    l.Titulo
                }).ToList()
            })
            .SingleOrDefaultAsync();
    }

    public async Task<object?> Create(GeneroDto genero)
    {
        try
        {
            var newGenero = _mapper.Map<Genero>(genero);

            await _context.Generos.AddAsync(newGenero);
            await _context.SaveChangesAsync();

            return await GetOneById(newGenero.Id);
        }   
        catch (Exception)
        { 
            throw;
        }
    }
    
    public async Task<object?> Update(int id, GeneroDto genero)
    {
        try
        {
            var _genero = await _context.Generos
                .Include(g => g.Livros)
                .SingleOrDefaultAsync(x => x.Id == id);

            if (_genero is null)
            {
                return null;
            }

            _genero.Nome = genero.Nome;
            _genero.Descricao = genero.Descricao;
            
            _context.Generos.Update(_genero);
            await _context.SaveChangesAsync();

            return await GetOneById(_genero.Id);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<object?> Delete(int id)
    {
        try
        {
            var genero = await _context.Generos
                .Include(g => g.Livros)
                .SingleOrDefaultAsync(x => x.Id == id);

            if (genero is null)
            {
                return null;
            }

            _context.Generos.Remove(genero);
            await _context.SaveChangesAsync();

            return new
            {
                genero.Id,
                genero.Nome,
                genero.Descricao,
                Livros = genero.Livros.Select(l => new
                {
                    l.Id,
                    l.Titulo
                }).ToList()
            };
        }
        catch (Exception)
        {
            throw;
        }
    }

    



    }
}
