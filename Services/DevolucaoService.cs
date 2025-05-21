using ApiLocadora.DataContexts;
using ApiLocadora.Dtos;
using ApiLocadora.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiLocadora.Services
{
    public class DevolucaoService
    {
        private readonly AppDbContext _context;

        public DevolucaoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Devolucao>> GetAll()
        {
            var list = await _context.Devolucaos.ToListAsync();

            return list;
        }

        public async Task<Devolucao?> GetOneById(int id)
        {
            try
            {
                return await _context.Devolucaos
                    .SingleOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Devolucao?> Create(DevolucaoDto devolucao)
        {
            try
            {
                var data = devolucao.DataDevolucao;

                var newDevolucao = new Devolucao
                {
                    Multa = devolucao.Multa,
                    DataDevolucao = new DateOnly(data.Year, data.Month, data.Day)
                };

                await _context.Devolucaos.AddAsync(newDevolucao);
                await _context.SaveChangesAsync();

                return newDevolucao;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Devolucao?> Update(int id, DevolucaoDto devolucao)
        {
            try
            {
                var _devolucao = await GetOneById(id);

                if (_devolucao is null)
                {
                    return _devolucao;
                }

                _devolucao.Multa = devolucao.Multa;

                _context.Devolucaos.Update(_devolucao);
                await _context.SaveChangesAsync();

                return _devolucao;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<Devolucao?> Delete(int id)
        {
            return null;
        }

        private async Task<bool> Exist(int id)
        {
            return await _context.Devolucaos.AnyAsync(c => c.Id == id);
        }
    }
}
