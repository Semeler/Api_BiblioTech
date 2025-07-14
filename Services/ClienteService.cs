
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

    public async Task<ICollection<object>> GetAll()
    {
        var list = await _context.Clientes
            .Include(c => c.Emprestimos)
            .Select(c => new
            {
                c.Id,
                c.Nome,
                c.Cpf,
                c.Telefone,
                c.Email,
                c.DataNascimento,
                c.Cep,
                c.Rua,
                c.Bairro,
                c.Numero,
                c.Estado,
                c.Cidade,
                Emprestimos = c.Emprestimos.Select(e => new
                {
                    e.Id,
                    e.DataInicio,
                    e.DataPrevista,
                    e.DataDevolucao,
                    e.Status
                }).ToList()
            })
            .ToListAsync();

        return list.Cast<object>().ToList();
    }

    public async Task<object?> GetOneById(int id)
    {
        return await _context.Clientes
            .Include(c => c.Emprestimos)
            .Where(c => c.Id == id)
            .Select(c => new
            {
                c.Id,
                c.Nome,
                c.Cpf,
                c.Telefone,
                c.Email,
                c.DataNascimento,
                c.Cep,
                c.Rua,
                c.Bairro,
                c.Numero,
                c.Estado,
                c.Cidade,
                Emprestimos = c.Emprestimos.Select(e => new
                {
                    e.Id,
                    e.DataInicio,
                    e.DataPrevista,
                    e.DataDevolucao,
                    e.Status
                }).ToList()
            })
            .SingleOrDefaultAsync();
    }

    public async Task<object?> Create(ClienteDto clienteDto)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var cliente = _mapper.Map<Cliente>(clienteDto);

            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return await GetOneById(cliente.Id);
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<object?> Update(int id, ClienteDto clienteDto)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var cliente = await _context.Clientes
                .Include(c => c.Emprestimos)
                .SingleOrDefaultAsync(c => c.Id == id);

            if (cliente == null) return null;

            // Atualiza as propriedades básicas
            cliente.Nome = clienteDto.Nome;
            cliente.Cpf = clienteDto.Cpf;
            cliente.Telefone = clienteDto.Telefone;
            cliente.Email = clienteDto.Email;
            cliente.DataNascimento = new DateOnly(
                clienteDto.DataNascimento.Year,
                clienteDto.DataNascimento.Month,
                clienteDto.DataNascimento.Day);
            cliente.Cep = clienteDto.Cep;
            cliente.Rua = clienteDto.Rua;
            cliente.Bairro = clienteDto.Bairro;
            cliente.Numero = clienteDto.Numero;
            cliente.Estado = clienteDto.Estado;
            cliente.Cidade = clienteDto.Cidade;

            _context.Clientes.Update(cliente);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return await GetOneById(cliente.Id);
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<object?> Delete(int id)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var cliente = await _context.Clientes
                .Include(c => c.Emprestimos)
                .SingleOrDefaultAsync(c => c.Id == id);

            if (cliente == null)
            {
                return null;
            }

            // Verifica se há empréstimos ativos
            if (cliente.Emprestimos.Any(e => e.Status))
            {
                throw new Exception("Não é possível excluir um cliente com empréstimos ativos");
            }

            // Remove os empréstimos relacionados
            foreach (var emprestimo in cliente.Emprestimos.ToList())
            {
                emprestimo.ClienteId = null;
                _context.Emprestimos.Update(emprestimo);
            }

            await _context.SaveChangesAsync();

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return new
            {
                cliente.Id,
                cliente.Nome,
                cliente.Cpf,
                cliente.Telefone,
                cliente.Email,
                cliente.DataNascimento,
                cliente.Cep,
                cliente.Rua,
                cliente.Bairro,
                cliente.Numero,
                cliente.Estado,
                cliente.Cidade
            };
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<bool> Exists(int id)
    {
        return await _context.Clientes.AnyAsync(c => c.Id == id);
    }
}
    
}
