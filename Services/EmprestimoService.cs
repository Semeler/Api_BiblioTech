using ApiLocadora.DataContexts;
using ApiLocadora.Dtos;
using ApiLocadora.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiLocadora.Services
{
    public class EmprestimoService
    {
        private readonly AppDbContext _context;

        public EmprestimoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Emprestimo>> GetAll()
        {
            var list = await _context.Emprestimos.ToListAsync();

            return list;
        }

        public async Task<Emprestimo?> GetOneById(int id)
        {
            try
            {
                return await _context.Emprestimos
                    .SingleOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Emprestimo?> Create(EmprestimoDto emprestimo)
        {
            try
            {
                var dataInicio = emprestimo.DataInicio;
                var dataFim = emprestimo.DataFim;

                var newEmprestimo = new Emprestimo
                {
                    Status = emprestimo.Status,
                    DataInicio = new DateOnly(dataInicio.Year, dataInicio.Month, dataInicio.Day),
                    DataFim = new DateOnly(dataFim.Year, dataFim.Month, dataFim.Day)
                };

                await _context.Emprestimos.AddAsync(newEmprestimo);
                await _context.SaveChangesAsync();

                return newEmprestimo;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Emprestimo?> Update(int id, EmprestimoDto emprestimo)
        {
            try
            {
                var _emprestimo = await GetOneById(id);

                if (_emprestimo is null)
                {
                    return _emprestimo;
                }

                _emprestimo.Status = emprestimo.Status;
                

                _context.Emprestimos.Update(_emprestimo);
                await _context.SaveChangesAsync();

                return _emprestimo;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<Emprestimo?> Delete(int id)
        {
            return null;
        }

        private async Task<bool> Exist(int id)
        {
            return await _context.Emprestimos.AnyAsync(c => c.Id == id);
        }
    }
}
