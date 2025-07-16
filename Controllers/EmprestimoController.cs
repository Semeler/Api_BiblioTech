using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiLocadora.Dtos;
using ApiLocadora.Models;
using ApiLocadora.DataContexts;
using Microsoft.EntityFrameworkCore;
using ApiLocadora.Services;

namespace ApiLocadora.Controllers
{
    [Route("emprestimos")]
    [ApiController]
    public class EmprestimoController : ControllerBase
    {
        private readonly EmprestimoService _service;

        public EmprestimoController(EmprestimoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var listaEmprestimos = await _service.GetAll();

            return Ok(listaEmprestimos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            try
            {
                var emprestimo = await _service.GetOneById(id);

                if (emprestimo is null)
                {
                    return NotFound("Informacao nao encontrada!");
                }

                return Ok(emprestimo);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EmprestimoDto item)
        {
            try
            {
                var emprestimo = await _service.Create(item);

                if (emprestimo is null)
                {
                    return Problem("Ocorreram erros ao salvar!");
                }

                return Created("", emprestimo);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] EmprestimoDto item)
        {
            try
            {
                var emprestimo = await _service.Update(id, item);

                if (emprestimo is null)
                    return NotFound();

                return Ok(emprestimo);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var livro = await _service.Delete(id);

                if (livro is null)
                {
                    return NotFound("Livro não encontrado!");
                }
                    
                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        

    }
}
