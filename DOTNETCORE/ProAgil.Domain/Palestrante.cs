using System.Collections.Generic;

namespace ProAgil.Domain
{
    public class Palestrante
    {
        public int Id { get; private set; }
        public string Nome { get; set; }
        public string MiniCurriculo { get; set; }
        public string ImageUrl { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public List<RedeSocial> RedesSociais { get;  }
        public List<PalestranteEvento> PalestrantesEventos { get;  }
    }
}