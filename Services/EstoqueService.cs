using ApiLocadora.DataContexts;
using ApiLocadora.Dtos;
using ApiLocadora.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiLocadora.Services
{
    public class EstoqueService
    {
        private readonly AppDbContext _context;

        public EstoqueService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Estoque>> GetAll()
        {
            var list = await _context.Estoques.ToListAsync();

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
                

                var newEstoque = new Estoque
                {
                    Quantidade = estoque.Quantidade,
                    
                };

                await _context.Estoques.AddAsync(newEstoque);
                await _context.SaveChangesAsync();

                return newEstoque;
            }
            catch (Exception)
            {
                throw;
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
