
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
    public class LivroService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public LivroService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ICollection<object>> GetAll()
        {
            var list = await _context.Livros
                .Include(l => l.Genero)
                .Include(l => l.Emprestimos)
                .Include(l => l.Fornecedores)
                .Include(l => l.Estoques)
                .Select(l => new
                {
                    l.Id,
                    l.Titulo,
                    l.Autor,
                    l.Isbn,
                    l.Editora,
                    l.Sinopse,
                    l.AnoPublicacao,
                    Genero = l.Genero == null
                        ? null
                        : new
                        {
                            l.Genero.Id,
                            l.Genero.Nome
                        },
                    Emprestimos = l.Emprestimos.Select(e => new
                    {
                        e.Id,
                        e.Status
                    }).ToList(),
                    Fornecedores = l.Fornecedores.Select(f => new
                    {
                        f.Id,
                        f.Nome,
                        f.Cnpj
                    }).ToList(),
                    Estoques = l.Estoques.Select(e => new
                    {
                        e.Id,
                        e.Quantidade,
                        e.CodigoDeBarras
                    }).ToList()
                })
                .ToListAsync();

            return list.Cast<object>().ToList();
        }

        public async Task<object?> GetOneById(int id)
        {
            return await _context.Livros
                .Include(l => l.Genero)
                .Include(l => l.Emprestimos)
                .Include(l => l.Fornecedores)
                .Include(l => l.Estoques)
                .Where(x => x.Id == id)
                .Select(l => new
                {
                    l.Id,
                    l.Titulo,
                    l.Autor,
                    l.Isbn,
                    l.Editora,
                    l.Sinopse,
                    l.AnoPublicacao,
                    Genero = l.Genero == null
                        ? null
                        : new
                        {
                            l.Genero.Id,
                            l.Genero.Nome
                        },
                    Emprestimos = l.Emprestimos.Select(e => new
                    {
                        e.Id,
                        e.Status
                    }).ToList(),
                    Fornecedores = l.Fornecedores.Select(f => new
                    {
                        f.Id,
                        f.Nome,
                        f.Cnpj
                    }).ToList(),
                    Estoques = l.Estoques.Select(e => new
                    {
                        e.Id,
                        e.Quantidade,
                        e.CodigoDeBarras
                    }).ToList()
                })
                .SingleOrDefaultAsync();
        }

        public async Task<object?> Create(LivroDto livroDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                if (livroDto.GeneroId.HasValue)
                {
                    var genero = await _context.Generos
                        .FirstOrDefaultAsync(g => g.Id == livroDto.GeneroId);

                    if (genero == null)
                    {
                        throw new Exception($"Gênero com ID {livroDto.GeneroId} não encontrado");
                    }
                }

                var livro = _mapper.Map<Livro>(livroDto);

                await _context.Livros.AddAsync(livro);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return await GetOneById(livro.Id);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<object?> Update(int id, LivroDto livroDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var livro = await _context.Livros
                    .SingleOrDefaultAsync(x => x.Id == id);

                if (livro == null) return null;

                if (livroDto.GeneroId.HasValue)
                {
                    var genero = await _context.Generos
                        .FirstOrDefaultAsync(g => g.Id == livroDto.GeneroId);

                    if (genero == null)
                    {
                        throw new Exception($"Gênero com ID {livroDto.GeneroId} não encontrado");
                    }
                }

                livro.Titulo = livroDto.Titulo;
                livro.Autor = livroDto.Autor;
                livro.Isbn = livroDto.Isbn;
                livro.Editora = livroDto.Editora;
                livro.Sinopse = livroDto.Sinopse;
                livro.AnoPublicacao = new DateOnly(
                    livroDto.AnoPublicacao.Year,
                    livroDto.AnoPublicacao.Month,
                    livroDto.AnoPublicacao.Day);
                livro.GeneroId = livroDto.GeneroId;

                _context.Livros.Update(livro);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return await GetOneById(livro.Id);
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
                var livro = await _context.Livros
                    .Include(l => l.Emprestimos)
                    .Include(l => l.Fornecedores)
                    .Include(l => l.Estoques)
                    .Include(l => l.Genero)
                    .SingleOrDefaultAsync(x => x.Id == id);

                if (livro is null)
                {
                    return null;
                }

                if (livro.Emprestimos.Any(e => e.Status == "Em Andamento" || e.Status == "Atrasado"))
                {
                    throw new Exception("Não é possível excluir um livro com empréstimos ativos");
                }

                var result = new
                {
                    livro.Id,
                    livro.Titulo,
                    livro.Autor,
                    livro.Isbn,
                    livro.Editora,
                    livro.Sinopse,
                    livro.AnoPublicacao,
                    Genero = livro.Genero == null
                        ? null
                        : new
                        {
                            livro.Genero.Id,
                            livro.Genero.Nome
                        },
                    Emprestimos = livro.Emprestimos.Select(e => new
                    {
                        e.Id,
                        e.Status
                    }).ToList(),
                    Fornecedores = livro.Fornecedores.Select(f => new
                    {
                        f.Id,
                        f.Nome,
                        f.Cnpj
                    }).ToList(),
                    Estoques = livro.Estoques.Select(e => new
                    {
                        e.Id,
                        e.Quantidade,
                        e.CodigoDeBarras
                    }).ToList()
                };

                livro.Emprestimos.Clear();
                livro.Fornecedores.Clear();
                _context.Estoques.RemoveRange(livro.Estoques);
                await _context.SaveChangesAsync();

                _context.Livros.Remove(livro);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return result;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
