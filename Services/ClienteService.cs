using ApiLocadora.DataContexts;
using ApiLocadora.Dtos;
using ApiLocadora.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiLocadora.Services
{
    public class ClienteService
    {
        private readonly AppDbContext _context;

        public ClienteService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Cliente>> GetAll()
        {
            var list = await _context.Clientes.ToListAsync();

            return list;
        }

        public async Task<Cliente?> GetOneById(int id)
        {
            try
            {
                return await _context.Clientes
                    .SingleOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Cliente?> Create(ClienteDto cliente)
        {
            try
            {
                var data = cliente.DataNascimento;

                var newCliente = new Cliente
                {
                    Nome = cliente.Nome,
                    CPF = cliente.CPF,
                    Endereco = cliente.Endereco,
                    Telefone = cliente.Telefone,
                    Email = cliente.Email,
                    DataNascimento = new DateOnly(data.Year, data.Month, data.Day)
                };

                await _context.Clientes.AddAsync(newCliente);
                await _context.SaveChangesAsync();

                return newCliente;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Cliente?> Update(int id, ClienteDto cliente)
        {
            try
            {
                var _cliente = await GetOneById(id);

                if (_cliente is null)
                {
                    return _cliente;
                }

                _cliente.Nome = cliente.Nome;
                _cliente.CPF = cliente.CPF;
                _cliente.Endereco = cliente.Endereco;
                _cliente.Telefone = cliente.Telefone;
                _cliente.Email = cliente.Endereco;

                _context.Clientes.Update(_cliente);
                await _context.SaveChangesAsync();

                return _cliente;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<Cliente?> Delete(int id)
        {
            return null;
        }

        private async Task<bool> Exist(int id)
        {
            return await _context.Clientes.AnyAsync(c => c.Id == id);
        }
    }
}
