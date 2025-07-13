using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiLocadora.Dtos;
using ApiLocadora.Models;
using ApiLocadora.DataContexts;
using Microsoft.EntityFrameworkCore;
using ApiLocadora.Services;

namespace ApiLocadora.Controllers
{
    [Route("livros")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private readonly LivroService _service;

        public LivroController(LivroService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            
            var listaLivros = await _service.GetAll();

            return Ok(listaLivros);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            try
            {
                var livro = await _service.GetOneById(id);

                if (livro is null)
                {
                    return NotFound("Informação não encontrada!");
                }

                return Ok(livro);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LivroDto item)
        {
            try
            {
                var livro = await _service.Create(item);

                if (livro is null)
                {
                    return Problem("Ocorreram erros ao salvar!");
                }

                return Created("", livro);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] LivroDto item)
        {
            try
            {
                var livro = await _service.Update(id, item);

                if (livro is null)
                    return NotFound();

                return Ok(livro);
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
