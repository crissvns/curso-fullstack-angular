using Microsoft.AspNetCore.Mvc;
using ProAgil.Webapi.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProAgil.Webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Evento>> Get()
        {
            return Ok(Eventos());
        }

        [HttpGet("{id}")]
        public ActionResult<Evento> Get(int id)
        {
            return Eventos().FirstOrDefault(x=>x.EventoId == id);
        }

        private IEnumerable<Evento> Eventos()
        {
            return new Evento[] {
                            new Evento () {
                                EventoId = 1,
                                Tema = "Angular e .NET Core",
                                Local = "Belo Horizonte",
                                Lote = "1º Lote",
                                QtdPessoas = 250,
                                DataEvento = DateTime.Now.AddDays(2).ToString("dd/MM/yyyy")
                            },
                            new Evento () {
                                EventoId = 2,
                                Tema = "Angular e .NET Core",
                                Local = "São Paulo",
                                Lote = "2º Lote",
                                QtdPessoas = 350,
                                DataEvento = DateTime.Now.AddDays(3).ToString("dd/MM/yyyy")
                            }
                        };
        }
    }
}