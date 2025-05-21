using ApiLocadora.DataContexts;
using ApiLocadora.Dtos;
using ApiLocadora.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiLocadora.Services
{
    public class FornecedorService
    {
        private readonly AppDbContext _context;

        public FornecedorService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Fornecedor>> GetAll()
        {
            var list = await _context.Fornecedores.ToListAsync();

            return list;
        }

        public async Task<Fornecedor?> GetOneById(int id)
        {
            try
            {
                return await _context.Fornecedores
                    .SingleOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Fornecedor?> Create(FornecedorDto fornecedor)
        {
            try
            {
                
                var newFornecedor = new Fornecedor
                {
                    Nome = fornecedor.Nome,
                    CNPJ = fornecedor.CNPJ,
                    Endereco = fornecedor.Endereco,
                    Telefone = fornecedor.Telefone,
                    Email = fornecedor.Email,
                    
                };

                await _context.Fornecedores.AddAsync(newFornecedor);
                await _context.SaveChangesAsync();

                return newFornecedor;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Fornecedor?> Update(int id, FornecedorDto fornecedor)
        {
            try
            {
                var _fornecedor = await GetOneById(id);

                if (_fornecedor is null)
                {
                    return _fornecedor;
                }

                _fornecedor.Nome = fornecedor.Nome;
                _fornecedor.CNPJ = fornecedor.CNPJ;
                _fornecedor.Endereco = fornecedor.Endereco;
                _fornecedor.Telefone = fornecedor.Telefone;
                _fornecedor.Email = fornecedor.Endereco;

                _context.Fornecedores.Update(_fornecedor);
                await _context.SaveChangesAsync();

                return _fornecedor;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<Fornecedor?> Delete(int id)
        {
            return null;
        }

        private async Task<bool> Exist(int id)
        {
            return await _context.Fornecedores.AnyAsync(c => c.Id == id);
        }
    }
}
