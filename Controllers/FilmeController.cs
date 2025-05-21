using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiLocadora.Dtos;
using ApiLocadora.Models;
using ApiLocadora.DataContexts;
using Microsoft.EntityFrameworkCore;
using ApiLocadora.Services;

namespace ApiLocadora.Controllers
{
    [Route("filmes")]
    [ApiController]
    public class FilmeController : ControllerBase
    {
        private readonly FilmeService _service;

        public FilmeController(FilmeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var listaFilmes = await _service.GetAll();

            return Ok(listaFilmes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            try
            {
                var filme = await _service.GetOneById(id);

                if (filme is null)
                {
                    return NotFound("Informacao nao encontrada!");
                }

                return Ok(filme);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FilmeDto item)
        {
            try
            {
                var filme = await _service.Create(item);

                if (filme is null)
                {
                    return Problem("Ocorreram erros ao salvar!");
                }

                return Created("", filme);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] FilmeDto item)
        {
            try
            {
                var filme = await _service.Update(id, item);

                if (filme is null)
                    return NotFound();

                return Ok(filme);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        //[HttpDelete("{id}")]
        //public IActionResult Remover(Guid id)
        //{
        //    return Ok();
        //}
    }
}
