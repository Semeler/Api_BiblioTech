
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

        public async Task<ICollection<object>> GetAll()
    {
        var list = await _context.Funcionarios
            .Include(f => f.Emprestimos)
            .Select(f => new
            {
                f.Id,
                f.Nome,
                f.Cpf,
                f.Cargo,
                f.Telefone,
                f.Email,
                f.Cep,
                f.Rua,
                f.Bairro,
                f.Numero,
                f.Estado,
                f.Cidade,
                f.DataAdmissao,
                Emprestimos = f.Emprestimos.Select(e => new
                {
                    e.Id,
                    e.Status
                }).ToList()
            })
            .ToListAsync();

        return list.Cast<object>().ToList();
    }

    public async Task<object?> GetOneById(int id)
    {
        return await _context.Funcionarios
            .Include(f => f.Emprestimos)
            .Where(x => x.Id == id)
            .Select(f => new
            {
                f.Id,
                f.Nome,
                f.Cpf,
                f.Cargo,
                f.Telefone,
                f.Email,
                f.Cep,
                f.Rua,
                f.Bairro,
                f.Numero,
                f.Estado,
                f.Cidade,
                f.DataAdmissao,
                Emprestimos = f.Emprestimos.Select(e => new
                {
                    e.Id,
                    e.Status
                }).ToList()
            })
            .SingleOrDefaultAsync();
    }

    public async Task<object?> Create(FuncionarioDto funcionario)
    {
        try
        {
            var newFuncionario = _mapper.Map<Funcionario>(funcionario);

            await _context.Funcionarios.AddAsync(newFuncionario);
            await _context.SaveChangesAsync();

            return await GetOneById(newFuncionario.Id);
        }   
        catch (Exception)
        { 
            throw;
        }
    }
    
    public async Task<object?> Update(int id, FuncionarioDto funcionario)
    {
        try
        {
            var _funcionario = await _context.Funcionarios
                .Include(f => f.Emprestimos)
                .SingleOrDefaultAsync(x => x.Id == id);

            if (_funcionario is null)
            {
                return null;
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

            return await GetOneById(_funcionario.Id);
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
            var funcionario = await _context.Funcionarios
                .Include(f => f.Emprestimos)
                .SingleOrDefaultAsync(x => x.Id == id);

            if (funcionario is null)
            {
                return null;
            }

            _context.Funcionarios.Remove(funcionario);
            await _context.SaveChangesAsync();

            return new
            {
                funcionario.Id,
                funcionario.Nome,
                funcionario.Cpf,
                funcionario.Cargo,
                funcionario.Telefone,
                funcionario.Email,
                funcionario.Cep,
                funcionario.Rua,
                funcionario.Bairro,
                funcionario.Numero,
                funcionario.Estado,
                funcionario.Cidade,
                funcionario.DataAdmissao,
                Emprestimos = funcionario.Emprestimos.Select(e => new
                {
                    e.Id,
                    e.Status
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
