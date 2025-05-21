using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiLocadora.Dtos;
using ApiLocadora.Models;
using ApiLocadora.DataContexts;
using Microsoft.EntityFrameworkCore;
using ApiLocadora.Services;

namespace ApiLocadora.Controllers
{
    [Route("devolucoes")]
    [ApiController]
    public class DevolucaoController : ControllerBase
    {
        private readonly DevolucaoService _service;

        public DevolucaoController(DevolucaoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var listaDevolucoes = await _service.GetAll();

            return Ok(listaDevolucoes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            try
            {
                var devolucao = await _service.GetOneById(id);

                if (devolucao is null)
                {
                    return NotFound("Informacao nao encontrada!");
                }

                return Ok(devolucao);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DevolucaoDto item)
        {
            try
            {
                var devolucao = await _service.Create(item);

                if (devolucao is null)
                {
                    return Problem("Ocorreram erros ao salvar!");
                }

                return Created("", devolucao);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] DevolucaoDto item)
        {
            try
            {
                var devolucao = await _service.Update(id, item);

                if (devolucao is null)
                    return NotFound();

                return Ok(devolucao);
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
