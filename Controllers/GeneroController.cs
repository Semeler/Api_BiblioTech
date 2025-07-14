using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiLocadora.Dtos;
using ApiLocadora.Models;
using ApiLocadora.DataContexts;
using Microsoft.EntityFrameworkCore;
using ApiLocadora.Services;

namespace ApiLocadora.Controllers
{
        [Route("generos")]
        [ApiController]
        public class GeneroController : ControllerBase
        {
        private readonly GeneroService _service;

        public GeneroController(GeneroService service)
        {
            _service = service;
        }

    [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var listaGeneros = await _service.GetAll();
                return Ok(listaGeneros);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            try
            {
                var genero = await _service.GetOneById(id);

                if (genero is null)
                {
                    return NotFound("Informação não encontrada!");
                }

                return Ok(genero);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GeneroDto item)
        {
            try
            {
                var genero = await _service.Create(item);

                if (genero is null)
                {
                    return Problem("Ocorreram erros ao salvar!");
                }

                return Created("", genero);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] GeneroDto item)
        {
            try
            {
                var genero = await _service.Update(id, item);

                if (genero is null)
                {
                    return NotFound("Gênero não encontrado!");
                }

                return Ok(genero);
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
                var genero = await _service.Delete(id);

                if (genero is null)
                {
                    return NotFound("Gênero não encontrado!");
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
