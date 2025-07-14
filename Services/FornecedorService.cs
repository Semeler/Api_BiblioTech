using ApiLocadora.DataContexts;
using ApiLocadora.Dtos;
using ApiLocadora.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApiLocadora.Services
{
    public class FornecedorService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public FornecedorService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ICollection<object>> GetAll()
    {
        var list = await _context.Fornecedores
            .Include(f => f.Livros)
            .Select(f => new
            {
                f.Id,
                f.Nome,
                f.Cnpj,
                f.Telefone,
                f.Email,
                f.Cep,
                f.Rua,
                f.Bairro,
                f.Numero,
                f.Estado,
                f.Cidade,
                Livros = f.Livros.Select(l => new
                {
                    l.Id,
                    l.Titulo
                }).ToList()
            })
            .ToListAsync();

        return list.Cast<object>().ToList();
    }

    public async Task<object?> GetOneById(int id)
    {
        return await _context.Fornecedores
            .Include(f => f.Livros)
            .Where(f => f.Id == id)
            .Select(f => new
            {
                f.Id,
                f.Nome,
                f.Cnpj,
                f.Telefone,
                f.Email,
                f.Cep,
                f.Rua,
                f.Bairro,
                f.Numero,
                f.Estado,
                f.Cidade,
                Livros = f.Livros.Select(l => new
                {
                    l.Id,
                    l.Titulo
                }).ToList()
            })
            .SingleOrDefaultAsync();
    }

    public async Task<object?> Create(FornecedorDto fornecedorDto)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var fornecedor = _mapper.Map<Fornecedor>(fornecedorDto);

            // Adiciona os livros
            if (fornecedorDto.LivrosIds.Count > 0)
            {
                var livros = await _context.Livros
                    .Where(l => fornecedorDto.LivrosIds.Contains(l.Id))
                    .ToListAsync();

                if (livros.Count != fornecedorDto.LivrosIds.Count)
                {
                    throw new Exception("Um ou mais livros não foram encontrados");
                }

                fornecedor.Livros = livros;
            }

            await _context.Fornecedores.AddAsync(fornecedor);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return await GetOneById(fornecedor.Id);
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<object?> Update(int id, FornecedorDto fornecedorDto)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var fornecedor = await _context.Fornecedores
                .Include(f => f.Livros)
                .SingleOrDefaultAsync(f => f.Id == id);

            if (fornecedor == null) return null;

            // Atualiza as propriedades básicas
            fornecedor.Nome = fornecedorDto.Nome;
            fornecedor.Cnpj = fornecedorDto.Cnpj;
            fornecedor.Telefone = fornecedorDto.Telefone;
            fornecedor.Email = fornecedorDto.Email;
            fornecedor.Cep = fornecedorDto.Cep;
            fornecedor.Rua = fornecedorDto.Rua;
            fornecedor.Bairro = fornecedorDto.Bairro;
            fornecedor.Numero = fornecedorDto.Numero;
            fornecedor.Estado = fornecedorDto.Estado;
            fornecedor.Cidade = fornecedorDto.Cidade;

            // Atualiza livros
            fornecedor.Livros.Clear();
            if (fornecedorDto.LivrosIds.Count > 0)
            {
                var livros = await _context.Livros
                    .Where(l => fornecedorDto.LivrosIds.Contains(l.Id))
                    .ToListAsync();

                if (livros.Count != fornecedorDto.LivrosIds.Count)
                {
                    throw new Exception("Um ou mais livros não foram encontrados");
                }

                fornecedor.Livros = livros;
            }

            _context.Fornecedores.Update(fornecedor);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return await GetOneById(fornecedor.Id);
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
            var fornecedor = await _context.Fornecedores
                .Include(f => f.Livros)
                .SingleOrDefaultAsync(f => f.Id == id);

            if (fornecedor == null)
            {
                return null;
            }

            // Limpa os relacionamentos
            fornecedor.Livros.Clear();
            await _context.SaveChangesAsync();

            _context.Fornecedores.Remove(fornecedor);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return fornecedor;
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}
}