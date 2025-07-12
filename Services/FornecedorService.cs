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
    public class FornecedorService
    {
        private readonly AppDbContext _context;

        private readonly IMapper _mapper;

        public FornecedorService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
                var newFornecedor = _mapper.Map<Fornecedor>(fornecedor);

                await _context.Fornecedores.AddAsync(newFornecedor);
                await _context.SaveChangesAsync();

                return newFornecedor;
            }   
            catch (Exception ex)
            { 
                throw ex;
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
                _fornecedor.Cnpj = fornecedor.Cnpj;
                _fornecedor.Telefone = fornecedor.Telefone;
                _fornecedor.Email = fornecedor.Email;
                _fornecedor.Cep = fornecedor.Cep;
                _fornecedor.Rua = fornecedor.Rua;
                _fornecedor.Bairro = fornecedor.Bairro;
                _fornecedor.Numero = fornecedor.Numero;
                _fornecedor.Estado = fornecedor.Estado;
                _fornecedor.Cidade = fornecedor.Cidade;
                
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
            try
            {
                var fornecedor = await _context.Fornecedores
                    .SingleOrDefaultAsync(x => x.Id == id);

                if (fornecedor is null)
                {
                    return null;
                }
                

                _context.Fornecedores.Remove(fornecedor);
                await _context.SaveChangesAsync();

                return fornecedor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        private async Task<bool> Exist(int id)
        {
            return await _context.Fornecedores.AnyAsync(c => c.Id == id);
        }
    }
}
