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
    public class EmprestimoService
    {
        private readonly AppDbContext _context;

        private readonly IMapper _mapper;

        public EmprestimoService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ICollection<Emprestimo>> GetAll()
        {
            var list = await _context.Emprestimos.Include(e => e.Cliente)
                .Include(e => e.Funcionario).ToListAsync();

            return list;
        }

        public async Task<Emprestimo?> GetOneById(int id)
        {
            try
            {
                return await _context.Emprestimos
                    .SingleOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Emprestimo?> Create(EmprestimoDto emprestimo)
        {
            try
            {
                var newEmprestimo = _mapper.Map<Emprestimo>(emprestimo);

                await _context.Emprestimos.AddAsync(newEmprestimo);
                await _context.SaveChangesAsync();

                return newEmprestimo;
            }   
            catch (Exception ex)
            { 
                throw ex;
            }
        }
        
        public async Task<Emprestimo?> Update(int id, EmprestimoDto emprestimo)
        {
            try
            {
                var _emprestimo = await GetOneById(id);

                if (_emprestimo is null)
                {
                    return _emprestimo;
                }

                _emprestimo.Status = emprestimo.Status;
                
                _context.Emprestimos.Update(_emprestimo);
                await _context.SaveChangesAsync();

                return _emprestimo;
            }
            catch (Exception ex)
            {
                    throw ex;
            }
            
        }

        //public async Task<Emprestimo?> Delete(int id)
        //{
        //    return null;
        //}

        public async Task<Emprestimo?> Delete(int id)
        {
            try
            {
                var emprestimo = await _context.Emprestimos
                    .SingleOrDefaultAsync(x => x.Id == id);

                if (emprestimo is null)
                {
                    return null;
                }
                

                _context.Emprestimos.Remove(emprestimo);
                await _context.SaveChangesAsync();

                return emprestimo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
        
        
        private async Task<bool> Exist(int id)
        {
            return await _context.Emprestimos.AnyAsync(c => c.Id == id);
        }
    }
}
