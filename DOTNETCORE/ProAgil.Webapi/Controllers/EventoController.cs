using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProAgil.Webapi.Data;
using System.Threading.Tasks;

namespace ProAgil.Webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventoController : ControllerBase
    {
        public readonly DataContext context;

        public EventoController(DataContext context) => this.context = context;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await context.Eventos.ToListAsync());
            }
            catch
            {
                return this.StatusCode(500, "Banco de dados falhou");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return Ok(await context.Eventos.FirstOrDefaultAsync(x => x.EventoId == id));
            }
            catch
            {
                return this.StatusCode(500, "Banco de dados falhou");
            }

        }
    }
}