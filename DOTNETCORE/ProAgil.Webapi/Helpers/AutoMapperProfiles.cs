using AutoMapper;
using ProAgil.Domain;
using ProAgil.Webapi.Dtos;
using System.Linq;

namespace ProAgil.Webapi.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Evento, EventoDto>()
                .ForMember(dest => dest.Palestrantes, opt =>
                    {
                        opt.MapFrom(src => src.PalestrantesEventos.Select(x => x.Palestrante).ToList());
                    })
                .ReverseMap();

            CreateMap<Palestrante, PalestranteDto>()
                .ForMember(dest => dest.Eventos, opt =>
                    {
                        opt.MapFrom(src => src.PalestrantesEventos.Select(x => x.Evento).ToList());
                    })
                .ReverseMap();

            CreateMap<Lote, LoteDto>().ReverseMap();
            CreateMap<RedeSocial, RedeSocialDto>().ReverseMap();
        }
    }
}