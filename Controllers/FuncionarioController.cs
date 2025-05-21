using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiLocadora.Dtos;
using ApiLocadora.Models;
using ApiLocadora.DataContexts;
using Microsoft.EntityFrameworkCore;
using ApiLocadora.Services;

namespace ApiLocadora.Controllers
{
    [Route("funcionarios")]
    [ApiController]
    public class FuncionarioController : ControllerBase
    {
        private readonly FuncionarioService _service;

        public FuncionarioController(FuncionarioService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var listaFuncionarios = await _service.GetAll();

            return Ok(listaFuncionarios);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            try
            {
                var funcionario = await _service.GetOneById(id);

                if (funcionario is null)
                {
                    return NotFound("Informacao nao encontrada!");
                }

                return Ok(funcionario);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FuncionarioDto item)
        {
            try
            {
                var funcionario = await _service.Create(item);

                if (funcionario is null)
                {
                    return Problem("Ocorreram erros ao salvar!");
                }

                return Created("", funcionario);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] FuncionarioDto item)
        {
            try
            {
                var funcionario = await _service.Update(id, item);

                if (funcionario is null)
                    return NotFound();

                return Ok(funcionario);
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
