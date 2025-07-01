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
    public class ClienteService
    {
        private readonly AppDbContext _context;

        private readonly IMapper _mapper;

        public ClienteService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
                var newCliente = _mapper.Map<Cliente>(cliente);

                await _context.Clientes.AddAsync(newCliente);
                await _context.SaveChangesAsync();

                return newCliente;
            }   
            catch (Exception ex)
            { 
                throw ex;
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
                _cliente.Cpf = cliente.Cpf;
                _cliente.Telefone = cliente.Telefone;
                _cliente.Email = cliente.Email;
                _cliente.DataNascimento = cliente.DataNascimento;
                _cliente.Cep = cliente.Cep;
                _cliente.Rua = cliente.Rua;
                _cliente.Bairro = cliente.Bairro;
                _cliente.Numero = cliente.Numero;
                _cliente.Estado = cliente.Estado;
                _cliente.Cidade = cliente.Cidade;

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

        public async Task<bool> Exist(int id)
        {
            return await _context.Clientes.AnyAsync(c => c.Id == id);
        }
    }
}
