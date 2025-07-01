using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiLocadora.Dtos;
using ApiLocadora.Models;
using ApiLocadora.DataContexts;
using Microsoft.EntityFrameworkCore;
using ApiLocadora.Services;
using MySqlConnector;
using ApiLocadora.Common.Exceptions;

namespace ApiLocadora.Controllers
{
    [Route("filmes")]
    [ApiController]
    public class FilmeController : ControllerBase
    {
        private readonly FilmeService _service;

        //private readonly AppDbContext _context;

        public FilmeController(FilmeService service)
        {
            _service = service;
            //_context = context; 
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var listaFilmes = await _service.GetAll();

            //var listaFilmes = await _context.Filmes
            //    .Include(e => e.Estudio)
            //    //.Select(e => new
            //    //{
            //    //    e.Id,
            //    //    e.Nome,
            //    //    e.AnoLancamento,
            //    //    e.Genero,
            //    //    Estudio = new { e.Estudio.Id, e.Estudio.Nome },
            //    //})
            //    .ToListAsync();

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

            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
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
