using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiLocadora.Dtos;
using ApiLocadora.Models;
using ApiLocadora.DataContexts;
using Microsoft.EntityFrameworkCore;
using ApiLocadora.Services;

namespace ApiLocadora.Controllers
{
    [Route("clientes")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteService _service;

        public ClienteController(ClienteService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var listaClientes = await _service.GetAll();

            return Ok(listaClientes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            try
            {
                var cliente = await _service.GetOneById(id);

                if (cliente is null)
                {
                    return NotFound("Informacao nao encontrada!");
                }

                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ClienteDto item)
        {
            try
            {
                var cliente = await _service.Create(item);

                if (cliente is null)
                {
                    return Problem("Ocorreram erros ao salvar!");
                }

                return Created("", cliente);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ClienteDto item)
        {
            try
            {
                var cliente = await _service.Update(id, item);

                if (cliente is null)
                    return NotFound();

                return Ok(cliente);
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
