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
    public class EstoqueService
    {
        private readonly AppDbContext _context;

        private readonly IMapper _mapper;

        public EstoqueService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ICollection<Estoque>> GetAll()
        {
            var list = await _context.Estoques.Include(e => e.Livro).ToListAsync();

            return list;
        }

        public async Task<Estoque?> GetOneById(int id)
        {
            try
            {
                return await _context.Estoques 
                    .SingleOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Estoque?> Create(EstoqueDto estoque)
        {
            try
            {
                var newEstoque = _mapper.Map<Estoque>(estoque);

                await _context.Estoques.AddAsync(newEstoque);
                await _context.SaveChangesAsync();

                return newEstoque;
            }   
            catch (Exception ex)
            { 
                throw ex;
            }
        }
        
        public async Task<Estoque?> Update(int id, EstoqueDto estoque)
        {
            try
            {
                var _estoque = await GetOneById(id);

                if (_estoque is null)
                {
                    return _estoque;
                }

                _estoque.Quantidade = estoque.Quantidade;
                _estoque.CodigoDeBarras = estoque.CodigoDeBarras;
                
                _context.Estoques.Update(_estoque);
                await _context.SaveChangesAsync();

                return _estoque;
            }
            catch (Exception ex)
            {
                    throw ex;
            }
            
        }

        public async Task<Estoque?> Delete(int id)
        {
            return null;
        }

        private async Task<bool> Exist(int id)
        {
            return await _context.Estoques.AnyAsync(c => c.Id == id);
        }
    }
}
