﻿using System.Collections.Generic;

namespace ProAgil.Webapi.Dtos
{
    public class EventoDto
    {
        public int Id { get; set; }
        public string Local { get; set; }
        public string DataEvento { get; set; }
        public string Tema { get; set; }
        public int QtdPessoas { get; set; }
        public string ImagemURL { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Lote { get; set; }
        public List<LoteDto> Lotes { get; }
        public List<RedeSocialDto> RedesSociais { get; }
        public List<PalestranteDto> Palestrantes { get; }
    }
}