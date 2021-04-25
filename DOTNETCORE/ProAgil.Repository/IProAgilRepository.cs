using System.Threading.Tasks;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public interface IProAgilRepository
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();

        Task<Evento[]> GetAllEventoAsync(bool includePalestrantes);
        Task<Evento> GetAllEventoAsyncById(int EventoId, bool includePalestrantes);
        Task<Evento[]> GetAllEventoAsyncByTema(string Tema, bool includePalestrantes);

        Task<Palestrante> GetAllPalestranteAsync(int PalestranteId,bool includeEventos = false);
        Task<Palestrante[]> GetAllPalestranteAsyncByName(string Nome, bool includeEventos = false);
    }
}