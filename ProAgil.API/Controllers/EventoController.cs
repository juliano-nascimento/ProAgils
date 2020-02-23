using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Domain;
using ProAgil.Repository;

namespace ProAgil.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        public readonly IProAgilRepository _Repo;
        public EventoController(IProAgilRepository repo)
        {
            _Repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var results = await _Repo.GetAllEventoAsync(true);
                return Ok(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }

        }

        [HttpGet("getById/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = await _Repo.GetEventoAsyncById(id, true);
                return Ok(result);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }

        }

         [HttpGet("getByTema/{Tema}")]
        public async Task<IActionResult> Get(string Tema)
        {
            try
            {
                var result = await _Repo.GetAllEventoAsyncByTema(Tema, true);
                return Ok(result);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }

        }

       [HttpPost]
        public async Task<IActionResult> Post(Evento model)
        {
            try
            {
                _Repo.Add(model);
                if(await _Repo.SaveChangesAsync()){
                    return Created($"/api/evento/{model.Id}", model);
                }
                
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }

            return BadRequest("Dados não foram inseridos");

        }

        [HttpPut]
        public async Task<IActionResult> Put(int EventoId, Evento model)
        {
            try
            {
                var evento = await _Repo.GetEventoAsyncById(EventoId, false);
                if(evento == null) return NotFound();

                _Repo.Update(model);

                if(await _Repo.SaveChangesAsync()){
                    return Created($"/api/evento/{model.Id}", model);
                }
                
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }

            return BadRequest("Dados não foram inseridos");

        }

         [HttpDelete]
        public async Task<IActionResult> Delete(int EventoId)
        {
            try
            {
                var evento = await _Repo.GetEventoAsyncById(EventoId, false);
                if(evento == null) return NotFound();

                _Repo.Delete(evento);
                
                if(await _Repo.SaveChangesAsync()){
                    return Ok();
                }
                
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }

            return BadRequest("Dados não foram inseridos");

        }

    }
}