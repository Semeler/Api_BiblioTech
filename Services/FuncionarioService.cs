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
    public class FuncionarioService
    {
        private readonly AppDbContext _context;

        private readonly IMapper _mapper;

        public FuncionarioService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
                var newFuncionario = _mapper.Map<Funcionario>(funcionario);

                await _context.Funcionarios.AddAsync(newFuncionario);
                await _context.SaveChangesAsync();

                return newFuncionario;
            }   
            catch (Exception ex)
            { 
                throw ex;
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
                _funcionario.Cpf = funcionario.Cpf;
                _funcionario.Cargo = funcionario.Cargo;
                _funcionario.Telefone = funcionario.Telefone;
                _funcionario.Email = funcionario.Email;
                _funcionario.Cep = funcionario.Cep;
                _funcionario.Rua = funcionario.Rua;
                _funcionario.Bairro = funcionario.Bairro;
                _funcionario.Numero = funcionario.Numero;
                _funcionario.Estado = funcionario.Estado;
                _funcionario.Cidade = funcionario.Cidade;
                
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
