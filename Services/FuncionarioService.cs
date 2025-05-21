using ApiLocadora.DataContexts;
using ApiLocadora.Dtos;
using ApiLocadora.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiLocadora.Services
{
    public class FuncionarioService
    {
        private readonly AppDbContext _context;

        public FuncionarioService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Funcionario>> GetAll()
        {
            var list = await _context.Funcionarios.ToListAsync();

            return list;
        }

        public async Task<Funcionario?> GetOneById(int id)
        {
            try
            {
                return await _context.Funcionarios
                    .SingleOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Funcionario?> Create(FuncionarioDto funcionario)
        {
            try
            {
                var data = funcionario.DataAdmissao;

                var newFuncionario = new Funcionario
                {
                    Nome = funcionario.Nome,
                    CPF = funcionario.CPF,
                    Cargo = funcionario.Cargo,
                    Telefone = funcionario.Telefone,
                    Email = funcionario.Email,
                    DataAdmissao = new DateOnly(data.Year, data.Month, data.Day)
                };

                await _context.Funcionarios.AddAsync(newFuncionario);
                await _context.SaveChangesAsync();

                return newFuncionario;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Funcionario?> Update(int id, FuncionarioDto funcionario)
        {
            try
            {
                var _funcionario = await GetOneById(id);

                if (_funcionario is null)
                {
                    return _funcionario;
                }

                _funcionario.Nome = funcionario.Nome;
                _funcionario.CPF = funcionario.CPF;
                _funcionario.Cargo = funcionario.Cargo;
                _funcionario.Telefone = funcionario.Telefone;
                _funcionario.Email = funcionario.Email;

                _context.Funcionarios.Update(_funcionario);
                await _context.SaveChangesAsync();

                return _funcionario;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<Funcionario?> Delete(int id)
        {
            return null;
        }

        private async Task<bool> Exist(int id)
        {
            return await _context.Funcionarios.AnyAsync(c => c.Id == id);
        }
    }
}
