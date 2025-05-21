using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiLocadora.Dtos;
using ApiLocadora.Models;
using ApiLocadora.DataContexts;
using Microsoft.EntityFrameworkCore;
using ApiLocadora.Services;

namespace ApiLocadora.Controllers
{
    [Route("estoques")]
    [ApiController]
    public class EstoqueController : ControllerBase
    {
        private readonly EstoqueService _service;

        public EstoqueController(EstoqueService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var listaEstoques = await _service.GetAll();

            return Ok(listaEstoques);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            try
            {
                var estoque = await _service.GetOneById(id);

                if (estoque is null)
                {
                    return NotFound("Informacao nao encontrada!");
                }

                return Ok(estoque);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EstoqueDto item)
        {
            try
            {
                var estoque = await _service.Create(item);

                if (estoque is null)
                {
                    return Problem("Ocorreram erros ao salvar!");
                }

                return Created("", estoque);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] EstoqueDto item)
        {
            try
            {
                var estoque = await _service.Update(id, item);

                if (estoque is null)
                    return NotFound();

                return Ok(estoque);
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
