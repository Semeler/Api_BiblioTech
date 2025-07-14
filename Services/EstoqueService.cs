using ApiLocadora.DataContexts;
using ApiLocadora.Dtos;
using ApiLocadora.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApiLocadora.Services
{
    public class EstoqueService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public EstoqueService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ICollection<object>> GetAll()
        {
            var list = await _context.Estoques
                .Include(e => e.Livro)
                .Select(e => new
                {
                    e.Id,
                    e.Quantidade,
                    e.CodigoDeBarras,
                    Livro = e.Livro == null ? null : new
                    {
                        e.Livro.Id,
                        e.Livro.Titulo
                    }
                })
                .ToListAsync();

            return list.Cast<object>().ToList();
        }

        public async Task<object?> GetOneById(int id)
        {
            return await _context.Estoques
                .Include(e => e.Livro)
                .Where(e => e.Id == id)
                .Select(e => new
                {
                    e.Id,
                    e.Quantidade,
                    e.CodigoDeBarras,
                    Livro = e.Livro == null ? null : new
                    {
                        e.Livro.Id,
                        e.Livro.Titulo
                    }
                })
                .SingleOrDefaultAsync();
        }

        public async Task<object?> Create(EstoqueDto estoqueDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Verifica se o livro existe
                if (estoqueDto.LivroId.HasValue)
                {
                    var livro = await _context.Livros
                        .FirstOrDefaultAsync(l => l.Id == estoqueDto.LivroId);

                    if (livro == null)
                    {
                        throw new Exception($"Livro com ID {estoqueDto.LivroId} não encontrado");
                    }
                }

                var estoque = _mapper.Map<Estoque>(estoqueDto);

                await _context.Estoques.AddAsync(estoque);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return await GetOneById(estoque.Id);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<object?> Update(int id, EstoqueDto estoqueDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var estoque = await _context.Estoques.FindAsync(id);
                if (estoque == null) return null;

                // Verifica se o livro existe
                if (estoqueDto.LivroId.HasValue)
                {
                    var livro = await _context.Livros
                        .FirstOrDefaultAsync(l => l.Id == estoqueDto.LivroId);

                    if (livro == null)
                    {
                        throw new Exception($"Livro com ID {estoqueDto.LivroId} não encontrado");
                    }
                }

                estoque.Quantidade = estoqueDto.Quantidade;
                estoque.CodigoDeBarras = estoqueDto.CodigoDeBarras;
                estoque.LivroId = estoqueDto.LivroId;

                _context.Estoques.Update(estoque);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return await GetOneById(estoque.Id);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<object?> Delete(int id)
        {
            try
            {
                var estoque = await _context.Estoques
                    .Include(e => e.Livro)
                    .SingleOrDefaultAsync(e => e.Id == id);

                if (estoque == null)
                {
                    return null;
                }

                _context.Estoques.Remove(estoque);
                await _context.SaveChangesAsync();

                return new
                {
                    estoque.Id,
                    estoque.Quantidade,
                    estoque.CodigoDeBarras,
                    Livro = estoque.Livro == null ? null : new
                    {
                        estoque.Livro.Id,
                        estoque.Livro.Titulo
                    }
                };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}