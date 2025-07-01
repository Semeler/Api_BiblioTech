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
    public class DevolucaoService
    {
        private readonly AppDbContext _context;

        private readonly IMapper _mapper;

        public DevolucaoService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ICollection<Devolucao>> GetAll()
        {
            var list = await _context.Devolucaos.Include(e => e.Cliente)
                .Include(e => e.Funcionario).ToListAsync();

            return list;
        }

        public async Task<Devolucao?> GetOneById(int id)
        {
            try
            {
                return await _context.Devolucaos
                    .SingleOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Devolucao?> Create(DevolucaoDto devolucao)
        {
            try
            {
                var newDevolucao = _mapper.Map<Devolucao>(devolucao);

                await _context.Devolucaos.AddAsync(newDevolucao);
                await _context.SaveChangesAsync();

                return newDevolucao;
            }   
            catch (Exception ex)
            { 
                throw ex;
            }
        }
        
        public async Task<Devolucao?> Update(int id, DevolucaoDto devolucao)
        {
            try
            {
                var _devolucao = await GetOneById(id);

                if (_devolucao is null)
                {
                    return _devolucao;
                }

                _devolucao.Multa = devolucao.Multa;
                
                _context.Devolucaos.Update(_devolucao);
                await _context.SaveChangesAsync();

                return _devolucao;
            }
            catch (Exception ex)
            {
                    throw ex;
            }
            
        }

        public async Task<Devolucao?> Delete(int id)
        {
            return null;
        }

        private async Task<bool> Exist(int id)
        {
            return await _context.Devolucaos.AnyAsync(c => c.Id == id);
        }
    }
}
