using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Domain;
using ProAgil.Repository;
using ProAgil.Webapi.Dtos;
using System.Threading.Tasks;

namespace ProAgil.Webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventoController : ControllerBase
    {
        public readonly IProAgilRepository _repo;
        private readonly IMapper mapper;

        public EventoController(IProAgilRepository _repo, IMapper mapper)
        {
            this._repo = _repo;
            this.mapper = mapper;
        }

        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(mapper.Map<EventoDto[]>(await _repo.GetAllEventoAsync(false)));
            }
            catch
            {
                return this.StatusCode(500, "Banco de dados falhou");
            }
        }

        [HttpGet("{EventoId}")]
        public async Task<IActionResult> Get(int EventoId)
        {
            try
            {
                return Ok(mapper.Map<EventoDto>(await _repo.GetAllEventoAsyncById(EventoId, true)));
            }
            catch
            {
                return this.StatusCode(500, "Banco de dados falhou");
            }
        }

        [HttpGet("getByTema/{Tema}")]
        public async Task<IActionResult> Get(string Tema)
        {
            try
            {
                return Ok(mapper.Map<EventoDto[]>(await _repo.GetAllEventoAsyncByTema(Tema, true)));
            }
            catch
            {
                return this.StatusCode(500, "Banco de dados falhou");
            }
        }

        [HttpPost()]
        public async Task<IActionResult> Post(EventoDto model)
        {
            try
            {
                Evento evento = mapper.Map<Evento>(model);

                _repo.Add(evento);

                return (await _repo.SaveChangesAsync()) ? Created($"/evento/{model.Id}", mapper.Map<EventoDto>(evento)) : BadRequest();
            }
            catch
            {
                return this.StatusCode(500, "Banco de dados falhou");
            }
        }

        [HttpPut("{EventoId}")]
        public async Task<IActionResult> Put(int EventoId, EventoDto model)
        {
            try
            {
                Evento evento = await _repo.GetAllEventoAsyncById(EventoId, false);
                if (evento == null) return NotFound();

                mapper.Map(model, evento);

                _repo.Update(evento);

                return (await _repo.SaveChangesAsync()) ? Created($"/evento/{model.Id}", mapper.Map<EventoDto>(evento)) : BadRequest();
            }
            catch
            {
                return this.StatusCode(500, "Banco de dados falhou");
            }
        }

        [HttpDelete("{EventoId}")]
        public async Task<IActionResult> Delete(int EventoId)
        {
            try
            {
                Evento evento = await _repo.GetAllEventoAsyncById(EventoId, false);
                if (evento == null) return NotFound();

                _repo.Delete(evento);
                await _repo.SaveChangesAsync();

                return Ok();
            }
            catch
            {
                return this.StatusCode(500, "Banco de dados falhou");
            }
        }
    }
}