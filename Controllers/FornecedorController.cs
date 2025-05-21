using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiLocadora.Dtos;
using ApiLocadora.Models;
using ApiLocadora.DataContexts;
using Microsoft.EntityFrameworkCore;
using ApiLocadora.Services;

namespace ApiLocadora.Controllers
{
    [Route("fornecedores")]
    [ApiController]
    public class FornecedorController : ControllerBase
    {
        private readonly FornecedorService _service;

        public FornecedorController(FornecedorService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var listaFornecedores = await _service.GetAll();

            return Ok(listaFornecedores);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            try
            {
                var fornecedor = await _service.GetOneById(id);

                if (fornecedor is null)
                {
                    return NotFound("Informacao não encontrada!");
                }

                return Ok(fornecedor);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FornecedorDto item)
        {
            try
            {
                var fornecedor = await _service.Create(item);

                if (fornecedor is null)
                {
                    return Problem("Ocorreram erros ao salvar!");
                }

                return Created("", fornecedor);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] FornecedorDto item)
        {
            try
            {
                var fornecedor = await _service.Update(id, item);

                if (fornecedor is null)
                    return NotFound();

                return Ok(fornecedor);
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
