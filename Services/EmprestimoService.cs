using ApiLocadora.DataContexts;
using ApiLocadora.Dtos;
using ApiLocadora.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;

namespace ApiLocadora.Services
{
    public class EmprestimoService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public EmprestimoService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ICollection<object>> GetAll()
        {
            var list = await _context.Emprestimos
                .Include(e => e.Cliente)
                .Include(e => e.Funcionario)
                .Include(e => e.Livros)
                .Select(e => new
                {
                    e.Id,
                    e.DataInicio,
                    e.DataPrevista,
                    e.DataDevolucao,
                    e.Status,
                    Cliente = e.Cliente == null ? null : new
                    {
                        e.Cliente.Id,
                        e.Cliente.Nome,
                    },
                    Funcionario = e.Funcionario == null ? null : new
                    {
                        e.Funcionario.Id,
                        e.Funcionario.Nome,
                    },
                    Livros = e.Livros.Select(l => new
                    {
                        l.Id,
                        l.Titulo,
                    }).ToList()
                })
                .ToListAsync();

            return list.Cast<object>().ToList();
        }

        public async Task<object?> GetOneById(int id)
        {
            return await _context.Emprestimos
                .Include(e => e.Cliente)
                .Include(e => e.Funcionario)
                .Include(e => e.Livros)
                .Where(e => e.Id == id)
                .Select(e => new
                {
                    e.Id,
                    e.DataInicio,
                    e.DataPrevista,
                    e.DataDevolucao,
                    e.Status,
                    Cliente = e.Cliente == null ? null : new
                    {
                        e.Cliente.Id,
                        e.Cliente.Nome,
                    },
                    Funcionario = e.Funcionario == null ? null : new
                    {
                        e.Funcionario.Id,
                        e.Funcionario.Nome,
                    },
                    Livros = e.Livros.Select(l => new
                    {
                        l.Id,
                        l.Titulo,
                    }).ToList()
                })
                .SingleOrDefaultAsync();
        }

        public async Task<object?> Create(EmprestimoDto emprestimoDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var emprestimo = _mapper.Map<Emprestimo>(emprestimoDto);

                if (emprestimoDto.LivrosIds != null && emprestimoDto.LivrosIds.Any())
                {
                    var livros = await _context.Livros
                        .Where(l => emprestimoDto.LivrosIds.Contains(l.Id))
                        .ToListAsync();

                    if (livros.Count != emprestimoDto.LivrosIds.Count)
                    {
                        throw new Exception("Um ou mais livros não foram encontrados");
                    }

                    emprestimo.Livros = livros;
                }

                await _context.Emprestimos.AddAsync(emprestimo);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return await _context.Emprestimos
                    .Include(e => e.Cliente)
                    .Include(e => e.Funcionario)
                    .Include(e => e.Livros)
                    .Where(e => e.Id == emprestimo.Id)
                    .Select(e => new
                    {
                        e.Id,
                        e.DataInicio,
                        e.DataPrevista,
                        e.DataDevolucao,
                        e.Status,
                        Cliente = new
                        {
                            e.Cliente.Id,
                            e.Cliente.Nome,
                        },
                        Funcionario = new
                        {
                            e.Funcionario.Id,
                            e.Funcionario.Nome,
                        },
                        Livros = e.Livros.Select(l => new
                        {
                            l.Id,
                            l.Titulo,
                        }).ToList()
                    })
                    .SingleOrDefaultAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<object?> Update(int id, EmprestimoDto emprestimoDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var emprestimo = await _context.Emprestimos
                    .Include(e => e.Livros)
                    .SingleOrDefaultAsync(e => e.Id == id);
                    
                if (emprestimo == null) return null;

                emprestimo.DataInicio = emprestimoDto.DataInicio.HasValue
                                        ? DateOnly.FromDateTime(emprestimoDto.DataInicio.Value)
                                        : (DateOnly?)null;
                emprestimo.DataPrevista = emprestimoDto.DataPrevista.HasValue
                                        ? DateOnly.FromDateTime(emprestimoDto.DataPrevista.Value)
                                        : (DateOnly?)null;
                emprestimo.DataDevolucao = emprestimoDto.DataDevolucao.HasValue
                                            ? DateOnly.FromDateTime(emprestimoDto.DataDevolucao.Value)
                                            : (DateOnly?)null;

                emprestimo.Status = emprestimoDto.Status;
                emprestimo.ClienteId = emprestimoDto.ClienteId;
                emprestimo.FuncionarioId = emprestimoDto.FuncionarioId;

                emprestimo.Livros.Clear();
                if (emprestimoDto.LivrosIds != null && emprestimoDto.LivrosIds.Any())
                {
                    var livros = await _context.Livros
                        .Where(l => emprestimoDto.LivrosIds.Contains(l.Id))
                        .ToListAsync();

                    if (livros.Count != emprestimoDto.LivrosIds.Count)
                    {
                        throw new Exception("Um ou mais livros não foram encontrados");
                    }

                    emprestimo.Livros = livros;
                }

                _context.Emprestimos.Update(emprestimo);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return await _context.Emprestimos
                    .Include(e => e.Cliente)
                    .Include(e => e.Funcionario)
                    .Include(e => e.Livros)
                    .Where(e => e.Id == emprestimo.Id)
                    .Select(e => new
                    {
                        e.Id,
                        e.DataInicio,
                        e.DataPrevista,
                        e.DataDevolucao,
                        e.Status,
                        Cliente = e.Cliente == null ? null : new { e.Cliente.Id, e.Cliente.Nome },
                        Funcionario = e.Funcionario == null ? null : new { e.Funcionario.Id, e.Funcionario.Nome },
                        Livros = e.Livros.Select(l => new { l.Id, l.Titulo }).ToList()
                    })
                    .SingleOrDefaultAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<Emprestimo?> Delete(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var emprestimo = await _context.Emprestimos
                    .Include(e => e.Livros)
                    .Include(e => e.Cliente)
                    .Include(e => e.Funcionario)
                    .SingleOrDefaultAsync(x => x.Id == id);

                if (emprestimo is null)
                {
                    return null;
                }

                emprestimo.Livros.Clear();
                emprestimo.Cliente = null;
                emprestimo.ClienteId = null;
                emprestimo.Funcionario = null;
                emprestimo.FuncionarioId = null;
                
                await _context.SaveChangesAsync();

                _context.Emprestimos.Remove(emprestimo);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return emprestimo;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}